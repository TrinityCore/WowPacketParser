using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using MovementFlag = WowPacketParser.Enums.v4.MovementFlag;
using MovementFlag2 = WowPacketParser.Enums.v4.MovementFlag2;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var updateObject = packet.Holder.UpdateObject = new();
            uint map = updateObject.MapId = packet.ReadUInt16("Map");
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var type = (UpdateTypeCataclysm)packet.ReadByte();

                var partWriter = new StringBuilderProtoPart(packet.Writer);
                packet.AddValue("UpdateType", type.ToString(), i);
                switch (type)
                {
                    case UpdateTypeCataclysm.Values:
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);
                        var updateValues = new UpdateValues(){Legacy = new()};
                        CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, updateValues.Legacy, guid, i);
                        updateObject.Updated.Add(new UpdateObject{Guid = guid, Values = updateValues, TextStartOffset = partWriter.StartOffset, TextLength = partWriter.Length, Text = partWriter.Text });
                        break;
                    }
                    case UpdateTypeCataclysm.CreateObject1:
                    case UpdateTypeCataclysm.CreateObject2: // Might != CreateObject1 on Cata
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);
                        var createType = type.ToCreateObjectType();
                        var createObject = new CreateObject() { Guid = guid, Values = new(){Legacy = new()}, CreateType = createType};
                        ReadCreateObjectBlock(packet, createObject, guid, map, createType, i);
                        createObject.Text = partWriter.Text;
                        createObject.TextStartOffset = partWriter.StartOffset;
                        createObject.TextLength = partWriter.Length;
                        updateObject.Created.Add(createObject);
                        break;
                    }
                    case UpdateTypeCataclysm.DestroyObjects:
                    {
                        CoreParsers.UpdateHandler.ReadDestroyObjectsBlock(packet, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, CreateObject createObject, WowGuid guid, uint map, CreateObjectType createType, object index)
        {
            ObjectType objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectTypeLegacy>("Object Type", index));
            WoWObject obj = CoreParsers.UpdateHandler.CreateObject(objType, guid, map);

            obj.CreateType = createType;
            obj.Movement = ReadMovementUpdateBlock(packet, guid, index);
            obj.UpdateFields = CoreParsers.UpdateHandler.ReadValuesUpdateBlockOnCreate(packet, createObject.Values.Legacy, objType, index);
            obj.DynamicUpdateFields = CoreParsers.UpdateHandler.ReadDynamicValuesUpdateBlockOnCreate(packet, objType, index);

            // If this is the second time we see the same object (same guid,
            // same position) update its phasemask
            if (Storage.Objects.ContainsKey(guid))
            {
                var existObj = Storage.Objects[guid].Item1;
                CoreParsers.UpdateHandler.ProcessExistingObject(ref existObj, obj, guid); // can't do "ref Storage.Objects[guid].Item1 directly
            }
            else
                Storage.Objects.Add(guid, obj, packet.TimeSpan);

            if (guid.HasEntry() && (objType == ObjectType.Unit || objType == ObjectType.GameObject))
                packet.AddSniffData(Utilities.ObjectTypeToStore(objType), (int)guid.GetEntry(), "SPAWN");
        }

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();

            var guid1 = new byte[8];
            var transportGuid = new byte[8];
            var goTransportGuid = new byte[8];
            var attackingTargetGuid = new byte[8];

            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;
            var hasFullSpline = false;
            var hasSplineStartTime = false;
            var hasSplineVerticalAcceleration = false;
            var hasGOTransportTime2 = false;
            var hasGOTransportTime3 = false;
            var hasSplineElevation = false;
            var hasMovementFlags = false;
            var hasMoveFlagsExtra = false;
            var hasTimestamp = false;
            var hasOrientation = false;
            var hasTransportData = false;
            var hasTransportTime3 = false;
            var hasTransportTime2 = false;
            var hasPitch = false;
            var hasFallData = false;
            var hasFallDirection = false;

            var bit130 = false;
            var bit98 = false;
            var bit23C = false;
            var bit298 = false;
            var bit230 = false;
            var bit284 = false;
            var bit228 = false;
            var bit258 = false;
            var bit218 = false;
            var bit220 = false;

            var bits160 = 0u;
            var splineCount = 0u;
            var bits11C = 0u;
            var bits88 = 0u;
            var bits25C = 0u;
            var bits26C = 0u;
            var bits288 = 0u;
            var bits418 = 0u;
            var bits2AA = 0u;
            SplineType splineType = SplineType.Stop;

            var bit29C = packet.ReadBit();
            var hasSceneObjectData = packet.ReadBit("Has Scene Object Data", index);
            var bit2A4 = packet.ReadBit();
            var bit1DC = packet.ReadBit();
            var bit32A = packet.ReadBit();
            var hasAnimKits = packet.ReadBit("Has Anim Kits", index);
            var bit2A8 = packet.ReadBit();
            var bit414 = packet.ReadBit();
            var transportFrames = packet.ReadBits("Transport Frames Count", 22, index);
            packet.ReadBit(); // fake 2
            packet.ReadBit(); // fake 1
            var isLiving = packet.ReadBit("Is Living", index);
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);
            var hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);
            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index);
            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            var bit428 = packet.ReadBit();
            packet.ReadBit(); // fake 3
            packet.ReadBit(); // fake 0
            var bit2A9 = packet.ReadBit();
            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index);

            if (hasGameObjectPosition)
            {
                packet.StartBitStream(goTransportGuid, 5, 0, 6);
                hasGOTransportTime2 = packet.ReadBit();
                packet.StartBitStream(goTransportGuid, 7, 3, 2);
                hasGOTransportTime3 = packet.ReadBit();
                packet.StartBitStream(goTransportGuid, 4, 1);
            }

            // Sceneobject data
            var CreatorGUID = new byte[8];
            byte[][] guid358 = null;
            byte[][][] guid358_6 = null;
            uint[] bits358_5 = null;
            uint[][] bits358_6_84 = null;
            uint[][] bits358_6_94 = null;
            uint[][] bits358_6_31 = null;
            uint[][] bits358_6_74 = null;
            byte[][][] bit358_6_78_9 = null;
            byte[][][] bit358_6_88_10 = null;
            bool[][][] bit358_6_88_C = null;
            bool[][][] bit358_6_88_8 = null;
            bool[][] bit358_6_2E = null;
            bool[][] bit358_6_30 = null;
            bool[] bit358_10 = null;
            bool[] bit358_C = null;
            bool[] bit358_24 = null;

            var bit338 = false;
            var bit330 = false;
            var bit341 = false;
            var bit340 = false;
            var bit332 = false;
            var bit33C = false;

            uint[] bits388 = null;
            uint[] bits388_10 = null;
            byte[][] bit388_1_10 = null;
            bool[][] bit388_1_C = null;
            bool[][] bit388_1_8 = null;

            if (hasSceneObjectData)
            {
                guid358 = new byte[2][];
                guid358_6 = new byte[2][][];
                bits358_5 = new uint[2];
                bits358_6_84 = new uint[2][];
                bits358_6_94 = new uint[2][];
                bits358_6_31 = new uint[2][];
                bits358_6_74 = new uint[2][];
                bit358_6_78_9 = new byte[2][][];
                bit358_6_88_10 = new byte[2][][];
                bit358_6_88_C = new bool[2][][];
                bit358_6_88_8 = new bool[2][][];
                bit358_6_2E = new bool[2][];
                bit358_6_30 = new bool[2][];
                bit358_10 = new bool[2];
                bit358_C = new bool[2];
                bit358_24 = new bool[2];

                for (var i = 0; i < 2; ++i)
                {
                    guid358[i] = new byte[8];
                    packet.StartBitStream(guid358[i], 2, 0, 4);
                    bit358_10[i] = !packet.ReadBit();
                    guid358[i][7] = packet.ReadBit();
                    bits358_5[i] = packet.ReadBits(2);

                    guid358_6[i] = new byte[bits358_5[i]][];
                    bits358_6_84[i] = new uint[bits358_5[i]];
                    bits358_6_94[i] = new uint[bits358_5[i]];
                    bits358_6_31[i] = new uint[bits358_5[i]];
                    bits358_6_74[i] = new uint[bits358_5[i]];
                    bit358_6_78_9[i] = new byte[bits358_5[i]][];
                    bit358_6_88_10[i] = new byte[bits358_5[i]][];
                    bit358_6_88_C[i] = new bool[bits358_5[i]][];
                    bit358_6_88_8[i] = new bool[bits358_5[i]][];
                    bit358_6_2E[i] = new bool[bits358_5[i]];
                    bit358_6_30[i] = new bool[bits358_5[i]];

                    for (var j = 0; j < bits358_5[i]; ++j)
                    {
                        guid358_6[i][j] = new byte[8];
                        bits358_6_31[i][j] = packet.ReadBits(7);
                        guid358_6[i][j][3] = packet.ReadBit();
                        bit358_6_2E[i][j] = !packet.ReadBit();
                        packet.StartBitStream(guid358_6[i][j], 2, 6, 4);
                        bits358_6_84[i][j] = packet.ReadBits(21);
                        guid358_6[i][j][1] = packet.ReadBit();

                        bit358_6_88_8[i][j] = new bool[bits358_6_84[i][j]];
                        bit358_6_88_C[i][j] = new bool[bits358_6_84[i][j]];
                        bit358_6_88_10[i][j] = new byte[bits358_6_84[i][j]];
                        for (var k = 0; k < bits358_6_84[i][j]; ++k)
                        {
                            bit358_6_88_8[i][j][k] = !packet.ReadBit();
                            bit358_6_88_C[i][j][k] = !packet.ReadBit();
                            bit358_6_88_10[i][j][k] = (byte)(10 - packet.ReadBit());
                        }

                        guid358_6[i][j][5] = packet.ReadBit();
                        bits358_6_74[i][j] = packet.ReadBits(20);
                        packet.StartBitStream(guid358_6[i][j], 7, 0);

                        bit358_6_78_9[i][j] = new byte[bits358_6_74[i][j]];
                        for (var k = 0; k < bits358_6_74[i][j]; ++k)
                            bit358_6_78_9[i][j][k] = (byte)(10 - packet.ReadBit());

                        bit358_6_30[i][j] = packet.ReadBit();
                        bits358_6_94[i][j] = packet.ReadBits(21);
                    }

                    packet.StartBitStream(guid358[i], 6, 1, 5, 3);
                    bit358_C[i] = !packet.ReadBit();
                    bit358_24[i] = packet.ReadBit();
                }

                bit338 = !packet.ReadBit();
                bit33C = !packet.ReadBit();

                bits388 = new uint[3];
                bits388_10 = new uint[3];
                bit388_1_10 = new byte[3][];
                bit388_1_C = new bool[3][];
                bit388_1_8 = new bool[3][];

                for (var i = 0; i < 3; ++i)
                {
                    bits388_10[i] = packet.ReadBits(21);
                    bits388[i] = packet.ReadBits(21);
                    bit388_1_10[i] = new byte[bits388[i]];
                    bit388_1_C[i] = new bool[bits388[i]];
                    bit388_1_8[i] = new bool[bits388[i]];

                    for (var j = 0; j < bits388[i]; ++j)
                    {
                        bit388_1_C[i][j] = !packet.ReadBit();
                        bit388_1_10[i][j] = (byte)(10 - packet.ReadBit());
                        bit388_1_8[i][j] = !packet.ReadBit();
                    }
                }

                packet.ReadBit(); // fake bit
                packet.StartBitStream(CreatorGUID, 5, 1, 6, 4, 3, 7, 0, 2);
                bit330 = !packet.ReadBit();
                packet.ReadBit("bit351", index);
                packet.ReadBit("bit350", index);
                bit341 = !packet.ReadBit();
                bit340 = !packet.ReadBit();
                bit332 = !packet.ReadBit();
            }

            if (bit29C)
            {
                bit23C = packet.ReadBit();
                bit230 = packet.ReadBit();
                bit298 = packet.ReadBit();
                packet.ReadBit("bit20C", index);
                if (bit298)
                    bits288 = packet.ReadBits(20);

                bit284 = packet.ReadBit();
                bit228 = packet.ReadBit();
                bit258 = packet.ReadBit();
                packet.ReadBit("bit20F", index);
                if (bit284)
                {
                    bits26C = packet.ReadBits(21);
                    bits25C = packet.ReadBits(21);
                }

                packet.ReadBit("bit20D", index);
                packet.ReadBit("bit20E", index);
                packet.ReadBit("bit210", index);
                bit218 = packet.ReadBit();
                bit220 = packet.ReadBit();
            }

            if (isLiving)
            {
                guid1[5] = packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit();
                packet.ReadBit("bit9C", index);
                packet.StartBitStream(guid1, 1, 3);
                hasOrientation = !packet.ReadBit();
                packet.ReadBit("bit85", index);
                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.ReadBit();
                    if (hasFullSpline)
                    {
                        hasSplineStartTime = packet.ReadBit("Has Spline Start Time", index);
                        packet.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        packet.ReadBitsE<SplineFlag434>("Spline flags", 25, index);
                        bit130 = packet.ReadBit();
                        if (bit130)
                        {
                            bits11C = packet.ReadBits(21);
                            packet.ReadBits("bits12C", 2, index);
                        }
                        splineCount = packet.ReadBits("SplineWaypointsCount", 20, index);
                        hasSplineVerticalAcceleration = packet.ReadBit("has Spline Vertical Acceleration", index);
                    }
                }
                bits88 = packet.ReadBits(22);
                packet.StartBitStream(guid1, 6, 7);
                hasTransportData = packet.ReadBit();
                hasSplineElevation = !packet.ReadBit();
                if (hasTransportData)
                {
                    hasTransportTime3 = packet.ReadBit();
                    packet.StartBitStream(transportGuid, 6, 2, 1, 0, 5);
                    hasTransportTime2 = packet.ReadBit();
                    packet.StartBitStream(transportGuid, 3, 7, 4);
                }

                bits160 = packet.ReadBits(19);
                for (var i = 0; i < bits160; ++i)
                    packet.ReadBits("bits164", 2, index, i);
                hasMoveFlagsExtra = !packet.ReadBit();
                guid1[2] = packet.ReadBit();
                hasMovementFlags = !packet.ReadBit();
                hasTimestamp = !packet.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = (uint)packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                hasFallData = packet.ReadBit();
                if (hasMoveFlagsExtra)
                    moveInfo.Flags2 = (uint)packet.ReadBitsE<MovementFlag2>("Extra Movement Flags", 13, index);
                guid1[4] = packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit();
                guid1[0] = packet.ReadBit();
                packet.ReadBit("bit84", index);
                hasPitch = !packet.ReadBit();
                bit98 = !packet.ReadBit();
            }

            if (hasAttackingTarget)
                packet.StartBitStream(attackingTargetGuid, 6, 4, 5, 2, 3, 7, 0, 1);

            if (bit32A)
                bits2AA = packet.ReadBits(7);

            if (hasAnimKits)
            {
                hasAnimKit2 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
            }

            if (bit428)
                bits418 = packet.ReadBits(22);

            packet.ResetBitReader();

            for (var i = 0; i < transportFrames; ++i)
                packet.ReadInt32("Transport frame", index, i);

            if (bit29C)
            {
                if (bit258)
                {
                    packet.ReadSingle("Float24C", index);
                    packet.ReadSingle("Float248", index);
                    packet.ReadSingle("Float250", index);
                    packet.ReadSingle("Float244", index);
                    packet.ReadSingle("Float254", index);
                    packet.ReadSingle("Float240", index);
                }

                if (bit284)
                {
                    for (var i = 0; i < bits25C; ++i)
                    {
                        packet.ReadSingle("Float260+1", index, i);
                        packet.ReadSingle("Float260+0", index, i);
                    }

                    packet.ReadSingle("Float280", index);
                    for (var i = 0; i < bits26C; ++i)
                    {
                        packet.ReadSingle("Float270+1", index, i);
                        packet.ReadSingle("Float270+0", index, i);
                    }

                    packet.ReadSingle("Float27C", index);
                }

                if (bit228)
                    packet.ReadInt32("int224", index);
                if (bit218)
                    packet.ReadInt32("int214", index);

                packet.ReadInt32("Int208", index);

                if (bit298)
                {
                    for (var i = 0; i < bits288; ++i)
                    {
                        packet.ReadSingle("Float28C+0", index, i);
                        packet.ReadSingle("Float28C+2", index, i);
                        packet.ReadSingle("Float28C+1", index, i);
                    }
                }

                if (bit230)
                    packet.ReadInt32("int22C", index);
                if (bit220)
                    packet.ReadInt32("int21C", index);

                if (bit23C)
                {
                    packet.ReadSingle("Float234", index);
                    packet.ReadSingle("Float238", index);
                }
            }

            if (isLiving)
            {
                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
                        var type = packet.ReadByte();
                        switch (type)
                        {
                            case 1:
                                splineType = SplineType.Normal;
                                break;
                            case 2:
                                splineType = SplineType.FacingSpot;
                                break;
                            case 3:
                                splineType = SplineType.FacingTarget;
                                break;
                            case 4:
                                splineType = SplineType.FacingAngle;
                                break;
                        }

                        packet.ReadSingle("Spline Duration Multiplier", index); // if need swap with "Spline Duration Multiplier Next"
                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                                Y = packet.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        if (bit130)
                        {
                            for (var i = 0; i < bits11C; ++i)
                            {
                                packet.ReadSingle("Float13C+0", index, i);
                                packet.ReadSingle("Float13C+1", index, i);
                            }
                        }

                        packet.ReadSingle("Spline Duration Multiplier Next", index);
                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Z = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                                X = packet.ReadSingle()
                            };

                            packet.AddValue("Facing Spot", point, index);
                        }

                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);

                        if (hasSplineVerticalAcceleration)
                            packet.ReadSingle("Spline Vertical Acceleration", index);

                        if (hasSplineStartTime)
                            packet.ReadInt32("Spline Start Time", index);

                        packet.ReadInt32("Spline Time", index); // if need swap with "Spline Full Time"
                        packet.ReadInt32("Spline Full Time", index);
                    }

                    packet.ReadInt32("Spline Id", index);
                    moveInfo.Position.Z = packet.ReadSingle();
                    moveInfo.Position.X = packet.ReadSingle();
                    moveInfo.Position.Y = packet.ReadSingle();
                }

                if (hasTransportData)
                {
                    moveInfo.Transport = new MovementInfo.TransportInfo();

                    moveInfo.Transport.Offset.X = packet.ReadSingle();
                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);

                    packet.ReadUInt32("Transport Time", index);
                    moveInfo.Transport.Offset.Z = packet.ReadSingle();
                    packet.ReadXORBytes(transportGuid, 4, 3);
                    var seat = packet.ReadByte("Transport Seat", index);
                    moveInfo.Transport.Offset.Y = packet.ReadSingle();
                    moveInfo.Transport.Offset.O = packet.ReadSingle();
                    packet.ReadXORBytes(transportGuid, 0, 7, 6, 5, 1, 2);
                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);

                    moveInfo.Transport.Guid = new WowGuid64(BitConverter.ToUInt64(transportGuid, 0));
                    packet.AddValue("Transport GUID", moveInfo.Transport.Guid, index);
                    packet.AddValue("Transport Position", moveInfo.Transport.Offset, index);

                    if (moveInfo.Transport.Guid.HasEntry() && moveInfo.Transport.Guid.GetHighType() == HighGuidType.Vehicle &&
                        guid.HasEntry() && guid.GetHighType() == HighGuidType.Creature)
                    {
                        VehicleTemplateAccessory vehicleAccessory = new VehicleTemplateAccessory
                        {
                            Entry = moveInfo.Transport.Guid.GetEntry(),
                            AccessoryEntry = guid.GetEntry(),
                            SeatId = seat
                        };
                        Storage.VehicleTemplateAccessories.Add(vehicleAccessory, packet.TimeSpan);
                    }
                }

                for (var i = 0; i < bits160; ++i)
                {
                    packet.ReadSingle("Float16C+1", index, i);
                    packet.ReadInt32("Int16C+4", index, i);
                    packet.ReadInt32("Int16C+0", index, i);
                    packet.ReadSingle("Float16C+3", index, i);
                    packet.ReadSingle("Float16C+5", index, i);
                    packet.ReadSingle("Float16C+2", index, i);
                }

                packet.ReadXORBytes(guid1, 2, 6, 0);

                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump Sin", index);
                        packet.ReadSingle("Jump Cos", index);
                        packet.ReadSingle("Jump Velocity", index);
                    }
                    packet.ReadSingle("Fall Start Velocity", index);
                    packet.ReadUInt32("Time Fallen", index);
                }

                if (hasPitch)
                    packet.ReadSingle("Pitch", index);

                moveInfo.Position.Y = packet.ReadSingle();

                if (hasTimestamp)
                    packet.ReadUInt32("Time", index);

                packet.ReadSingle("FloatC8", index);
                packet.ReadXORByte(guid1, 7);
                for (var i = 0; i < bits88; ++i)
                    packet.ReadInt32("Int8C", index, i);

                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                packet.ReadSingle("FloatC0", index);
                packet.ReadSingle("FloatB4", index);
                packet.ReadXORByte(guid1, 1);
                moveInfo.Position.Z = packet.ReadSingle();
                if (hasSplineElevation)
                    packet.ReadSingle("Spline Elevation", index);
                packet.ReadXORByte(guid1, 4);
                packet.ReadSingle("FloatBC", index);
                packet.ReadSingle("FloatAC", index);
                packet.ReadSingle("FloatB8", index);
                packet.ReadSingle("FloatC4", index);
                packet.ReadSingle("FloatB0", index);
                moveInfo.Position.X = packet.ReadSingle();
                packet.ReadXORBytes(guid1, 5, 3);
                if (bit98)
                    packet.ReadInt32("Int98", index);

                packet.ReadSingle("FloatA8", index);

                packet.WriteGuid("GUID1", guid1, index);
                packet.AddValue("Position", moveInfo.Position, index);
                packet.AddValue("Orientation", moveInfo.Orientation, index);
            }

            if (hasSceneObjectData)
            {
                packet.ReadInt32("int334", index);
                if (bit340)
                    packet.ReadByte("byte340", index);

                for (var i = 0; i < 2; ++i)
                {
                    packet.ReadXORByte(guid358[i], 3);
                    for (var j = 0; j < bits358_5[i]; ++j)
                    {
                        if (bit358_6_2E[i][j])
                            packet.ReadInt16("short358+6+2E", index, i, j);

                        packet.ReadXORByte(guid358_6[i][j], 3);
                        for (var k = 0; k < bits358_6_74[i][j]; ++k)
                        {
                            packet.ReadByte("byte358+6+78+8", index, i, j, k);
                            if (bit358_6_78_9[i][j][k] != 9)
                                packet.ReadByte("byte358+6+78+9", index, i, j, k);

                            packet.ReadInt16("short358+6+78+2", index, i, j, k);
                            packet.ReadInt32("int358+6+78+0", index, i, j, k);
                            packet.ReadInt16("short358+6+78+3", index, i, j, k);
                        }

                        packet.ReadXORByte(guid358_6[i][j], 6);
                        for (var k = 0; k < bits358_6_84[i][j]; ++k)
                        {
                            packet.ReadInt32("int358+6+88+0", index, i, j, k);
                            if (bit358_6_88_C[i][j][k])
                                packet.ReadInt32("int358+6+88+C", index, i, j, k);

                            if (bit358_6_88_8[i][j][k])
                                packet.ReadInt32("int358+6+88+8", index, i, j, k);

                            if (bit358_6_88_10[i][j][k] != 9)
                                packet.ReadByte("byte358+6+88+10", index, i, j, k);

                            packet.ReadInt32("int358+6+88+4", index, i, j, k);
                        }

                        packet.ReadInt32("int358+6+28", index, i, j);
                        packet.ReadWoWString("String358+6+31", (int)bits358_6_31[i][j], index, i, j);

                        if (!bit358_6_30[i][j])
                            packet.ReadByte("byte358+6+30", index, i, j);

                        packet.ReadInt32("int358+6+24", index, i, j);
                        packet.ReadXORByte(guid358_6[i][j], 5);
                        packet.ReadInt32("int358+6+C", index, i, j);
                        for (var k = 0; k < bits358_6_94[i][j]; ++k)
                        {
                            packet.ReadInt32("int358+6+98+0", index, i, j, k);
                            packet.ReadInt32("int358+6+98+4", index, i, j, k);
                        }

                        packet.ReadXORByte(guid358_6[i][j], 7);
                        packet.ReadInt32("int358+6+18", index, i, j);
                        packet.ReadInt16("short358+6+2C", index, i, j);
                        packet.ReadInt32("int358+6+20", index, i, j);
                        packet.ReadInt32("int358+6+1C", index, i, j);
                        packet.ReadInt32("int358+6+10", index, i, j);
                        packet.ReadXORByte(guid358_6[i][j], 4);
                        packet.ReadInt16("short358+6+16", index, i, j);
                        packet.ReadXORByte(guid358_6[i][j], 2);
                        packet.ReadInt32("Int358+6+8", index, i, j);
                        packet.ReadXORByte(guid358_6[i][j], 1);
                        packet.ReadInt16("short358+6+14", index, i, j);
                        packet.ReadXORByte(guid358_6[i][j], 0);
                        packet.WriteGuid("Guid 358_6", guid358_6[i][j], index, i, j);
                    }

                    packet.ReadXORBytes(guid358[i], 5, 4, 0);
                    packet.ReadInt32("int358+8", index, i);
                    packet.ReadXORBytes(guid358[i], 7);
                    if (!bit358_24[i])
                        packet.ReadByte("byte358+24", index, i);

                    if (bit358_10[i])
                        packet.ReadInt16("Short358+10", index, i);

                    packet.ReadXORBytes(guid358[i], 6);
                    if (bit358_C[i])
                        packet.ReadInt32("int358+C", index, i);

                    packet.ReadByte("byte358+37", index, i);
                    packet.ReadXORBytes(guid358[i], 1, 2);
                    packet.WriteGuid("Guid358", guid358[i], index, i);
                }

                for (var i = 0; i < 3; ++i)
                {
                    for (var j = 0; j < bits388_10[i]; ++j)
                    {
                        packet.ReadInt32("int388+6+4", index, i, j);
                        packet.ReadInt32("int388+6+0", index, i, j);
                    }

                    for (var j = 0; j < bits388[i]; ++j)
                    {
                        if (bit388_1_C[i][j])
                            packet.ReadInt32("int388+1+C", index, i, j);

                        packet.ReadInt32("int388+1+0", index, i, j);

                        if (bit388_1_10[i][j] != 9)
                            packet.ReadByte("byte388+1+10", index, i, j);

                        packet.ReadInt32("int388+1+4", index, i, j);

                        if (bit388_1_8[i][j])
                            packet.ReadInt32("int388+1+8", index, i, j);
                    }
                }

                packet.ParseBitStream(CreatorGUID, 7, 0, 6, 2, 5, 1, 3, 4);
                if (bit332)
                    packet.ReadInt16("short332", index);
                if (bit338)
                    packet.ReadInt32("int338", index);
                if (bit341)
                    packet.ReadByte("byte341", index);
                if (bit33C)
                    packet.ReadInt32("int33C", index);
                if (bit330)
                    packet.ReadInt16("Short318", index);

                packet.WriteGuid("Creator GUID", CreatorGUID, index);
            }

            if (hasGameObjectPosition)
            {
                moveInfo.Transport = new MovementInfo.TransportInfo();

                packet.ReadUInt32("GO Transport Time", index);
                moveInfo.Transport.Offset.Y = packet.ReadSingle();
                packet.ReadXORBytes(goTransportGuid, 0, 5);
                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                packet.ReadXORBytes(goTransportGuid, 7, 4);
                moveInfo.Transport.Offset.O = packet.ReadSingle();
                moveInfo.Transport.Offset.Z = packet.ReadSingle();
                packet.ReadXORBytes(goTransportGuid, 1, 3, 6);
                packet.ReadSByte("GO Transport Seat", index);
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);

                packet.ReadXORByte(goTransportGuid, 2);
                moveInfo.Transport.Offset.X = packet.ReadSingle();
                moveInfo.Transport.Guid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("GO Transport GUID", moveInfo.Transport.Guid, index);
                packet.AddValue("GO Transport Position", moveInfo.Transport.Offset, index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Position.X = packet.ReadSingle();
                moveInfo.Position.Y = packet.ReadSingle();

                packet.AddValue("Stationary Position", moveInfo.Position, index);
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 1, 5, 4, 7, 6, 2, 0, 3);
                packet.WriteGuid("Attacking GUID", attackingTargetGuid, index);
            }

            if (bit1DC)
                packet.ReadInt32("int1D8", index);
            if (bit2A4)
                packet.ReadInt32("int2A0", index);
            if (hasGameObjectRotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasVehicleData)
            {
                packet.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
            }

            if (bit428)
                for (var i = 0; i < bits418; ++i)
                    packet.ReadInt32("Int3F8", index, i);

            if (bit414)
                packet.ReadInt32("int410", index);

            if (bit32A)
                packet.ReadBytes("Bytes", (int)bits2AA);

            if (isLiving && moveInfo.HasSplineData && hasFullSpline && splineType == SplineType.FacingTarget)
            {
                var facingTargetGuid = new byte[8];
                facingTargetGuid = packet.StartBitStream(3, 1, 0, 7, 6, 4, 5, 2);
                packet.ParseBitStream(facingTargetGuid, 3, 0, 4, 6, 1, 5, 2, 7);
                packet.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 5, 4, 6, 7, 2, 0);
            packet.ReadBit("Despawn Animation");
            guid[1] = packet.ReadBit();
            packet.ParseBitStream(guid, 4, 2, 0, 3, 7, 1, 5, 6);

            var destroyed = packet.WriteGuid("GUID", guid);

            var update = packet.Holder.UpdateObject = new();
            update.Destroyed.Add(new DestroyedObject()
            {
                Guid = destroyed,
                Text = packet.Writer?.ToString() ?? ""
            });
        }
    }
}
