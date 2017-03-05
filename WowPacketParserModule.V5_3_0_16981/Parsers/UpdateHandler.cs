using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            uint map = packet.Translator.ReadUInt16("Map");
            var count = packet.Translator.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var type = packet.Translator.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

                packet.AddValue("UpdateType", typeString, i);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.Translator.ReadPackedGuid("GUID", i);

                        WoWObject obj;
                        var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, guid.GetObjectType(), i, false);

                        if (Storage.Objects.TryGetValue(guid, out obj))
                        {
                            if (obj.ChangedUpdateFieldsList == null)
                                obj.ChangedUpdateFieldsList = new List<Dictionary<int, UpdateField>>();
                            obj.ChangedUpdateFieldsList.Add(updates);
                        }

                        break;
                    }
                    case "CreateObject1":
                    case "CreateObject2": // Might != CreateObject1 on Cata
                    {
                        var guid = packet.Translator.ReadPackedGuid("GUID", i);
                        ReadCreateObjectBlock(packet, guid, map, i);
                        break;
                    }
                    case "DestroyObjects":
                    {
                        CoreParsers.UpdateHandler.ReadObjectsBlock(packet, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, WowGuid guid, uint map, object index)
        {
            var objType = packet.Translator.ReadByteE<ObjectType>("Object Type", index);
            var moves = ReadMovementUpdateBlock(packet, guid, index);
            var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, objType, index, true);

            WoWObject obj;
            switch (objType)
            {
                case ObjectType.Unit:
                    obj = new Unit();
                    break;
                case ObjectType.GameObject:
                    obj = new GameObject();
                    break;
                case ObjectType.Item:
                    obj = new Item();
                    break;
                case ObjectType.Player:
                    obj = new Player();
                    break;
                default:
                    obj = new WoWObject();
                    break;
            }

            obj.Type = objType;
            obj.Movement = moves;
            obj.UpdateFields = updates;
            obj.Map = map;
            obj.Area = CoreParsers.WorldStateHandler.CurrentAreaId;
            obj.PhaseMask = (uint)CoreParsers.MovementHandler.CurrentPhaseMask;
            obj.Phases = new HashSet<ushort>(CoreParsers.MovementHandler.ActivePhases);

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
            var guid5 = new byte[8];
            var guid6 = new byte[8];
            var guid7 = new byte[8];

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

            var bit95 = false;
            var bit94 = false;
            var bit134 = false;
            var bitA8 = false;
            var bit228 = false;
            var bit270 = false;
            var bit21C = false;
            var bit244 = false;

            var bits168 = 0u;
            var splineCount = 0u;
            var bits138 = 0u;
            var bits98 = 0u;
            var bits248 = 0u;
            var bits258 = 0u;
            var bits274 = 0u;
            var bits3F4 = 0u;
            var bits28F = 0u;
            var splineType = SplineType.Stop;

            packet.Translator.ReadBit();
            var transportFrames = packet.Translator.ReadBits(22);
            var hasVehicleData = packet.Translator.ReadBit("Has Vehicle Data", index);
            var bit3F0 = packet.Translator.ReadBit();
            var hasGameObjectPosition = packet.Translator.ReadBit("Has GameObject Position", index);
            packet.Translator.ReadBit(); // fake bit
            var isSceneObject = packet.Translator.ReadBit("Scene Object", index);
            var transport = packet.Translator.ReadBit("On transport", index);
            var bit284 = packet.Translator.ReadBit();
            var bit208 = packet.Translator.ReadBit();
            var hasGameObjectRotation = packet.Translator.ReadBit("Has GameObject Rotation", index);
            var hasAttackingTarget = packet.Translator.ReadBit("Has Attacking Target", index);
            packet.Translator.ReadBit(); // fake bit
            packet.Translator.ReadBit(); // fake bit
            packet.Translator.ReadBit("Self", index);
            packet.Translator.ReadBit(); // fake bit
            var living = packet.Translator.ReadBit("Living", index);
            var bit3E8 = packet.Translator.ReadBit(); // something with scene object
            var bit28E = packet.Translator.ReadBit();
            var hasAnimKits = packet.Translator.ReadBit("Has AnimKits", index);
            var hasStationaryPosition = packet.Translator.ReadBit("Has Stationary Position", index);

            if (living)
            {
                guid1[0] = packet.Translator.ReadBit();
                hasSplineElevation = !packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid1, 4, 7);
                hasMoveFlagsExtra = !packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid1, 5, 2);
                moveInfo.HasSplineData = packet.Translator.ReadBit("Has spline data", index);
                hasMovementFlags = !packet.Translator.ReadBit();
                hasTimestamp = !packet.Translator.ReadBit("Lacks timestamp", index);
                bit95 = packet.Translator.ReadBit();
                bit94 = packet.Translator.ReadBit();
                hasOrientation = !packet.Translator.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                hasTransportData = packet.Translator.ReadBit("Has Transport Data", index);
                if (hasTransportData)
                {
                    packet.Translator.StartBitStream(transportGuid, 1, 0, 6);
                    hasTransportTime3 = packet.Translator.ReadBit();
                    packet.Translator.StartBitStream(transportGuid, 2, 7, 4, 3);
                    hasTransportTime2 = packet.Translator.ReadBit();
                    transportGuid[5] = packet.Translator.ReadBit();
                }

                hasPitch = !packet.Translator.ReadBit("Lacks pitch", index);
                guid1[6] = packet.Translator.ReadBit();
                bits168 = packet.Translator.ReadBits(19);
                for (var i = 0; i < bits168; ++i)
                    packet.Translator.ReadBits("bits168", 2, index);

                guid1[1] = packet.Translator.ReadBit();
                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.Translator.ReadBit();
                    if (hasFullSpline)
                    {
                        hasSplineStartTime = packet.Translator.ReadBit();
                        bit134 = packet.Translator.ReadBit();
                        packet.Translator.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        splineCount = packet.Translator.ReadBits("SplineWaypointsCount", 20, index);
                        hasSplineVerticalAcceleration = packet.Translator.ReadBit("Has Spline Vertical Acceleration", index);
                        if (bit134)
                        {
                            bits138 = packet.Translator.ReadBits(21);
                            packet.Translator.ReadBits("bits148", 2, index);
                        }
                        packet.Translator.ReadBitsE<SplineFlag434>("Spline flags", 25, index);
                    }
                }

                bitA8 = !packet.Translator.ReadBit();
                guid1[3] = packet.Translator.ReadBit();
                bits98 = packet.Translator.ReadBits(22);
                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13, index);

                hasFallData = packet.Translator.ReadBit("Has Fall Data", index);
                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit("Has Fall Direction", index);

                packet.Translator.ReadBit("bitAC", index);
            }

            if (hasGameObjectPosition)
            {
                packet.Translator.StartBitStream(goTransportGuid, 7, 3);
                hasGOTransportTime2 = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(goTransportGuid, 1, 4, 0, 2, 6);
                hasGOTransportTime3 = packet.Translator.ReadBit();
                goTransportGuid[5] = packet.Translator.ReadBit();
            }

            if (hasAnimKits)
            {
                hasAnimKit2 = !packet.Translator.ReadBit();
                hasAnimKit3 = !packet.Translator.ReadBit();
                hasAnimKit1 = !packet.Translator.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.Translator.StartBitStream(7, 3, 6, 1, 5, 4, 0, 2);

            if (bit208)
            {
                packet.Translator.ReadBit("bit21A", index);
                bit228 = packet.Translator.ReadBit();
                bit270 = packet.Translator.ReadBit();
                packet.Translator.ReadBit("bit219", index);
                packet.Translator.ReadBit("bit218", index);
                bit21C = packet.Translator.ReadBit();
                bit244 = packet.Translator.ReadBit();

                if (bit244)
                {
                    bits248 = packet.Translator.ReadBits(21);
                    bits258 = packet.Translator.ReadBits(21);
                }

                if (bit270)
                    bits274 = packet.Translator.ReadBits(20);

                packet.Translator.ReadBit("bit21B", index);
            }

            // Sceneobject data
            var CreatorGUID = new byte[8];
            byte[][] guid310 = null;
            byte[][][] guid34C_4 = null;
            uint[] bits34C_0 = null;
            uint[][] bits34C_4_84 = null;
            uint[][] bits34C_4_94 = null;
            uint[][] bits34C_4_31 = null;
            uint[][] bits34C_4_74 = null;
            byte[][][] bit34C_4_78_9 = null;
            byte[][][] bit34C_4_88_10 = null;
            bool[][][] bit34C_4_88_C = null;
            bool[][][] bit34C_4_88_8 = null;
            bool[][] bit34C_4_2E = null;
            bool[][] bit34C_4_30 = null;
            bool[] bit34C_4 = null;
            bool[] bit34C_8 = null;

            var bit318 = false;
            var bit31A = false;
            var bit329 = false;
            var bit320 = false;
            var bit328 = false;
            var bit324 = false;

            uint[] bits388 = null;
            uint[] bits388_10 = null;
            byte[][] bit388_1_10 = null;
            bool[][] bit388_1_C = null;
            bool[][] bit388_1_8 = null;

            if (isSceneObject)
            {
                guid310 = new byte[2][];
                guid34C_4 = new byte[2][][];
                bits34C_0 = new uint[2];
                bits34C_4_84 = new uint[2][];
                bits34C_4_94 = new uint[2][];
                bits34C_4_31 = new uint[2][];
                bits34C_4_74 = new uint[2][];
                bit34C_4_78_9 = new byte[2][][];
                bit34C_4_88_10 = new byte[2][][];
                bit34C_4_88_C = new bool[2][][];
                bit34C_4_88_8 = new bool[2][][];
                bit34C_4_2E = new bool[2][];
                bit34C_4_30 = new bool[2][];
                bit34C_4 = new bool[2];
                bit34C_8 = new bool[2];

                for (var i = 0; i < 2; ++i)
                {
                    guid310[i] = new byte[8];
                    packet.Translator.StartBitStream(guid310[i], 7, 6);
                    bits34C_0[i] = packet.Translator.ReadBits(2);

                    guid34C_4[i] = new byte[bits34C_0[i]][];
                    bits34C_4_84[i] = new uint[bits34C_0[i]];
                    bits34C_4_94[i] = new uint[bits34C_0[i]];
                    bits34C_4_31[i] = new uint[bits34C_0[i]];
                    bits34C_4_74[i] = new uint[bits34C_0[i]];
                    bit34C_4_78_9[i] = new byte[bits34C_0[i]][];
                    bit34C_4_88_10[i] = new byte[bits34C_0[i]][];
                    bit34C_4_88_C[i] = new bool[bits34C_0[i]][];
                    bit34C_4_88_8[i] = new bool[bits34C_0[i]][];
                    bit34C_4_2E[i] = new bool[bits34C_0[i]];
                    bit34C_4_30[i] = new bool[bits34C_0[i]];

                    for (var j = 0; j < bits34C_0[i]; ++j)
                    {
                        guid34C_4[i][j] = new byte[8];
                        bit34C_4_2E[i][j] = !packet.Translator.ReadBit();
                        packet.Translator.StartBitStream(guid34C_4[i][j], 7, 0, 6, 3);

                        bits34C_4_84[i][j] = packet.Translator.ReadBits(21);
                        bit34C_4_88_8[i][j] = new bool[bits34C_4_84[i][j]];
                        bit34C_4_88_C[i][j] = new bool[bits34C_4_84[i][j]];
                        bit34C_4_88_10[i][j] = new byte[bits34C_4_84[i][j]];

                        for (var k = 0; k < bits34C_4_84[i][j]; ++k)
                        {
                            bit34C_4_88_10[i][j][k] = (byte)(10 - packet.Translator.ReadBit());
                            bit34C_4_88_C[i][j][k] = !packet.Translator.ReadBit();
                            bit34C_4_88_8[i][j][k] = !packet.Translator.ReadBit();
                        }

                        bits34C_4_94[i][j] = packet.Translator.ReadBits(21);
                        packet.Translator.StartBitStream(guid34C_4[i][j], 2);
                        bit34C_4_30[i][j] = packet.Translator.ReadBit();
                        packet.Translator.StartBitStream(guid34C_4[i][j], 5, 4, 1);

                        bits34C_4_74[i][j] = packet.Translator.ReadBits(20);
                        bit34C_4_78_9[i][j] = new byte[bits34C_4_74[i][j]];

                        for (var k = 0; k < bits34C_4_74[i][j]; ++k)
                            bit34C_4_78_9[i][j][k] = (byte)(10 - packet.Translator.ReadBit());

                        bits34C_4_31[i][j] = packet.Translator.ReadBits(7);
                    }

                    bit34C_8[i] = !packet.Translator.ReadBit();
                    packet.Translator.StartBitStream(guid310[i], 3, 5, 2);
                    bit34C_4[i] = packet.Translator.ReadBit();
                    packet.Translator.StartBitStream(guid310[i], 1, 0, 4);
                }

                bits388 = new uint[3];
                bits388_10 = new uint[3];
                bit388_1_10 = new byte[3][];
                bit388_1_C = new bool[3][];
                bit388_1_8 = new bool[3][];

                for (var i = 0; i < 3; ++i)
                {
                    bits388[i] = packet.Translator.ReadBits(21);
                    bit388_1_10[i] = new byte[bits388[i]];
                    bit388_1_C[i] = new bool[bits388[i]];
                    bit388_1_8[i] = new bool[bits388[i]];

                    for (var j = 0; j < bits388[i]; ++j)
                    {
                        bit388_1_10[i][j] = (byte)(10 - packet.Translator.ReadBit());
                        bit388_1_C[i][j] = packet.Translator.ReadBit();
                        bit388_1_8[i][j] = packet.Translator.ReadBit();
                    }

                    bits388_10[i] = packet.Translator.ReadBits(21);
                }

                bit31A = !packet.Translator.ReadBit();
                bit329 = !packet.Translator.ReadBit();
                bit320 = !packet.Translator.ReadBit();
                bit328 = !packet.Translator.ReadBit();
                packet.Translator.ReadBit(); // fake bit
                packet.Translator.StartBitStream(CreatorGUID, 7, 1, 0, 2, 3, 6, 4, 5);
                bit324 = !packet.Translator.ReadBit();
                bit318 = !packet.Translator.ReadBit();
            }

            if (bit3F0)
                bits3F4 = packet.Translator.ReadBits(22);

            if (bit28E)
                bits28F = packet.Translator.ReadBits(7);

            packet.Translator.ResetBitReader();

            for (var i = 0; i < transportFrames; ++i)
                packet.Translator.ReadInt32("Transport frame", i, index);

            if (living)
            {
                if (hasTimestamp)
                    packet.Translator.ReadUInt32("Time", index);

                for (var i = 0; i < bits168; ++i)
                {
                    packet.Translator.ReadSingle("Float16C+5", index);
                    packet.Translator.ReadInt32("Int16C+4", index);
                    packet.Translator.ReadSingle("Float16C+1", index);
                    packet.Translator.ReadInt32("Int16C+0", index);
                    packet.Translator.ReadSingle("Float16C+2", index);
                    packet.Translator.ReadSingle("Float16C+3", index);
                }

                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
                        if (hasSplineStartTime)
                            packet.Translator.ReadInt32("Spline Start Time", index);

                        packet.Translator.ReadSingle("Spline Duration Multiplier", index);
                        if (hasSplineVerticalAcceleration)
                            packet.Translator.ReadSingle("Spline Vertical Acceleration", index);

                        packet.Translator.ReadInt32("Spline Time", index);
                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Z = packet.Translator.ReadSingle(),
                                Y = packet.Translator.ReadSingle(),
                                X = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        if (bit134)
                        {
                            for (var i = 0; i < bits138; ++i)
                            {
                                packet.Translator.ReadSingle("Float13C+1", index, i);
                                packet.Translator.ReadSingle("Float13C+0", index, i);
                            }
                        }

                        packet.Translator.ReadSingle("Spline Duration Multiplier Next", index);
                        var type = packet.Translator.ReadByte();
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

                        if (splineType == SplineType.FacingAngle)
                            packet.Translator.ReadSingle("Facing Angle", index);

                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                X = packet.Translator.ReadSingle(),
                                Z = packet.Translator.ReadSingle(),
                                Y = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Facing Spot", point, index);
                        }

                        packet.Translator.ReadInt32("Spline Full Time", index);
                    }

                    moveInfo.Position.Y = packet.Translator.ReadSingle();
                    moveInfo.Position.Z = packet.Translator.ReadSingle();
                    packet.Translator.ReadInt32("Spline Id", index);
                    moveInfo.Position.X = packet.Translator.ReadSingle();
                }

                if (hasTransportData)
                {
                    packet.Translator.ReadXORByte(transportGuid, 4);
                    moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    packet.Translator.ReadUInt32("Transport Time", index);
                    var seat = packet.Translator.ReadByte("Transport Seat", index);
                    packet.Translator.ReadXORBytes(transportGuid, 3, 1, 6);
                    if (hasTransportTime2)
                        packet.Translator.ReadUInt32("Transport Time 2", index);

                    packet.Translator.ReadXORByte(transportGuid, 5);
                    moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORBytes(transportGuid, 2, 0, 7);
                    if (hasTransportTime3)
                        packet.Translator.ReadUInt32("Transport Time 3", index);

                    moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(transportGuid, 0));
                    packet.AddValue("Transport GUID", moveInfo.TransportGuid, index);
                    packet.AddValue("Transport Position", moveInfo.TransportOffset, index);

                    if (moveInfo.TransportGuid.HasEntry() && moveInfo.TransportGuid.GetHighType() == HighGuidType.Vehicle &&
                        guid.HasEntry() && guid.GetHighType() == HighGuidType.Creature)
                    {
                        VehicleTemplateAccessory vehicleAccessory = new VehicleTemplateAccessory
                        {
                            Entry = moveInfo.TransportGuid.GetEntry(),
                            AccessoryEntry = guid.GetEntry(),
                            SeatId = seat
                        };
                        Storage.VehicleTemplateAccessories.Add(vehicleAccessory, packet.TimeSpan);
                    }
                }

                packet.Translator.ReadXORBytes(guid1, 2, 1);
                packet.Translator.ReadSingle("RunBack Speed", index);
                packet.Translator.ReadSingle("Fly Speed", index);
                packet.Translator.ReadXORBytes(guid1, 0, 3);
                packet.Translator.ReadSingle("SwimBack Speed", index);
                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Jump Sin", index);
                        packet.Translator.ReadSingle("Jump Velocity", index);
                        packet.Translator.ReadSingle("Jump Cos", index);
                    }
                    packet.Translator.ReadSingle("Fall Start Velocity", index);
                    packet.Translator.ReadUInt32("Time Fallen", index);
                }

                packet.Translator.ReadSingle("Turn Speed", index);
                packet.Translator.ReadXORByte(guid1, 5);
                moveInfo.Position.Z = packet.Translator.ReadSingle();
                if (hasOrientation)
                    moveInfo.Orientation = packet.Translator.ReadSingle();

                packet.Translator.ReadXORByte(guid1, 6);
                if (hasSplineElevation)
                    packet.Translator.ReadSingle("Spline Elevation", index);

                packet.Translator.ReadSingle("Pitch Speed", index);
                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch", index);

                for (var i = 0; i < bits98; ++i)
                    packet.Translator.ReadInt32("Int9C", index, i);

                moveInfo.WalkSpeed = packet.Translator.ReadSingle("Walk Speed", index) / 2.5f;
                if (bitA8)
                    packet.Translator.ReadInt32("IntA8", index);

                moveInfo.Position.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadSingle("Swim Speed", index);
                packet.Translator.ReadSingle("FlyBack Speed", index);
                packet.Translator.ReadXORByte(guid1, 7);
                moveInfo.RunSpeed = packet.Translator.ReadSingle("Run Speed", index) / 7.0f;
                moveInfo.Position.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid1, 4);

                packet.Translator.WriteGuid("GUID1", guid1, index);
                packet.AddValue("Position", moveInfo.Position, index);
                packet.AddValue("Orientation", moveInfo.Orientation, index);
            }

            if (isSceneObject)
            {
                if (bit318)
                    packet.Translator.ReadInt16("Short318", index);

                for (var i = 0; i < 2; ++i)
                {
                    for (var j = 0; j < bits34C_0[i]; ++j)
                    {
                        packet.Translator.ReadXORBytes(guid34C_4[i][j], 0, 2);
                        packet.Translator.ReadInt32("Int34C+4+8", index, i, j);
                        packet.Translator.ReadXORBytes(guid34C_4[i][j], 5, 7);
                        packet.Translator.ReadInt32("Int34C+4+18", index, i, j);

                        for (var k = 0; k < bits34C_4_84[i][j]; ++k)
                        {
                            if (bit34C_4_88_C[i][j][k])
                                packet.Translator.ReadInt32("int34C+4+88+C", index, i, j, k);

                            if (bit34C_4_88_8[i][j][k])
                                packet.Translator.ReadInt32("int34C+4+88+8", index, i, j, k);

                            if (bit34C_4_88_10[i][j][k] != 9)
                                packet.Translator.ReadByte("byte34C+4+88+10", index, i, j, k);

                            packet.Translator.ReadInt32("int34C+4+88+0", index, i, j, k);
                            packet.Translator.ReadInt32("int34C+4+88+4", index, i, j, k);
                        }

                        packet.Translator.ReadInt32("int34C+4+28", index, i, j);

                        for (var k = 0; k < bits34C_4_94[i][j]; ++k)
                        {
                            packet.Translator.ReadInt32("int34C+4+98+0", index, i, j, k);
                            packet.Translator.ReadInt32("int34C+4+98+4", index, i, j, k);
                        }

                        packet.Translator.ReadBytes("Bytes34C+4+31", (int)bits34C_4_31[i][j], index, i, j);
                        packet.Translator.ReadXORByte(guid34C_4[i][j], 6);

                        for (var k = 0; k < bits34C_4_74[i][j]; ++k)
                        {
                            packet.Translator.ReadInt32("int34C+4+78+0", index, i, j, k);
                            packet.Translator.ReadByte("byte34C+4+78+8", index, i, j, k);
                            packet.Translator.ReadInt16("short34C+4+78+3", index, i, j, k);
                            packet.Translator.ReadInt16("short34C+4+78+2", index, i, j, k);
                            if (bit34C_4_78_9[i][j][k] != 9)
                                packet.Translator.ReadByte("byte34C+4+78+9", index, i, j, k);
                        }

                        if (bit34C_4_2E[i][j])
                            packet.Translator.ReadInt16("short34C+4+2E", index, i, j);

                        packet.Translator.ReadXORByte(guid34C_4[i][j], 4);
                        packet.Translator.ReadInt32("int34C+4+24", index, i, j);
                        packet.Translator.ReadXORBytes(guid34C_4[i][j], 1, 3);
                        packet.Translator.ReadInt16("short34C+4+16", index, i, j);
                        packet.Translator.ReadInt32("int34C+4+C", index, i, j);
                        packet.Translator.ReadInt32("int34C+4+10", index, i, j);

                        if (!bit34C_4_30[i][j])
                            packet.Translator.ReadByte("byte34C+4+30", index, i, j);

                        packet.Translator.ReadInt32("int34C+4+20", index, i, j);
                        packet.Translator.ReadInt32("int34C+4+1C", index, i, j);
                        packet.Translator.ReadInt16("short34C+4+14", index, i, j);
                        packet.Translator.ReadInt16("short34C+4+2C", index, i, j);
                        packet.Translator.WriteGuid("Guid 34C_4", guid34C_4[i][j]);
                    }

                    if (!bit34C_4[i])
                        packet.Translator.ReadByte("byte34C-4", index, i);

                    packet.Translator.ReadXORBytes(guid310[i], 6, 3, 7);
                    packet.Translator.ReadInt32("int34C-12", index, i);
                    packet.Translator.ReadXORBytes(guid310[i], 5, 1, 4, 0);
                    packet.Translator.ReadByte("byte34C+16", index, i);

                    if (bit34C_8[i])
                        packet.Translator.ReadInt32("int34C-8", index, i);

                    packet.Translator.ReadXORByte(guid310[i], 2);
                    packet.Translator.WriteGuid("Guid34C-20", guid310[i], index, i);
                }

                packet.Translator.ParseBitStream(CreatorGUID, 2, 5, 4, 7, 3, 1, 0, 6);
                packet.Translator.WriteGuid("Creator GUID", CreatorGUID);

                if (bit329)
                    packet.Translator.ReadByte("byte329", index);

                for (var i = 0; i < 3; ++i)
                {
                    for (var j = 0; j < bits388_10[i]; ++j)
                    {
                        packet.Translator.ReadByte("byte388+4+4", index, i, j);
                        packet.Translator.ReadByte("byte388+4+0", index, i, j);
                    }

                    for (var j = 0; j < bits388[i]; ++j)
                    {
                        if (bit388_1_10[i][j] != 9)
                            packet.Translator.ReadByte("byte388+1+10", index, i, j);

                        if (bit388_1_C[i][j])
                            packet.Translator.ReadInt32("int388+1+C", index, i, j);

                        packet.Translator.ReadInt32("int388+1+4", index, i, j);
                        packet.Translator.ReadInt32("int388+1+0", index, i, j);

                        if (bit388_1_8[i][j])
                            packet.Translator.ReadInt32("int388+1+8", index, i, j);
                    }
                }

                if (bit320)
                    packet.Translator.ReadInt32("int320", index);
                if (bit31A)
                    packet.Translator.ReadInt16("short31A", index);

                packet.Translator.ReadInt32("int31C", index);
                if (bit324)
                    packet.Translator.ReadInt32("int324", index);
                if (bit328)
                    packet.Translator.ReadByte("byte328", index);
            }

            if (hasGameObjectPosition)
            {
                packet.Translator.ReadUInt32("GO Transport Time", index);
                packet.Translator.ReadXORByte(goTransportGuid, 7);
                moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 0);
                if (hasGOTransportTime3)
                    packet.Translator.ReadUInt32("GO Transport Time 3", index);

                packet.Translator.ReadXORByte(goTransportGuid, 3);
                packet.Translator.ReadSByte("GO Transport Seat", index);
                packet.Translator.ReadXORByte(goTransportGuid, 1);
                moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                if (hasGOTransportTime2)
                    packet.Translator.ReadUInt32("GO Transport Time 2", index);

                moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(goTransportGuid, 2, 4, 5, 6);

                moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("GO Transport GUID", moveInfo.TransportGuid, index);
                packet.AddValue("GO Transport Position", moveInfo.TransportOffset, index);
            }

            if (bit208)
            {
                if (bit21C)
                {
                    packet.Translator.ReadSingle("Float224", index);
                    packet.Translator.ReadSingle("Float220", index);
                }

                packet.Translator.ReadSingle("Float210", index);
                if (bit244)
                {
                    for (var i = 0; i < bits258; ++i)
                    {
                        packet.Translator.ReadSingle("Float25C+1", index);
                        packet.Translator.ReadSingle("Float25C+0", index);
                    }

                    for (var i = 0; i < bits248; ++i)
                    {
                        packet.Translator.ReadSingle("Float24C+0", index);
                        packet.Translator.ReadSingle("Float24C+1", index);
                    }

                    packet.Translator.ReadSingle("Float26C", index);
                    packet.Translator.ReadSingle("Float268", index);
                }

                if (bit228)
                {
                    packet.Translator.ReadSingle("Float22C", index);
                    packet.Translator.ReadSingle("Float230", index);
                    packet.Translator.ReadSingle("Float23C", index);
                    packet.Translator.ReadSingle("Float234", index);
                    packet.Translator.ReadSingle("Float238", index);
                    packet.Translator.ReadSingle("Float240", index);
                }

                if (bit270)
                {
                    for (var i = 0; i < bits274; ++i)
                    {
                        packet.Translator.ReadSingle("Float277+1", index);
                        packet.Translator.ReadSingle("Float277+0", index);
                        packet.Translator.ReadSingle("Float277+2", index);
                    }
                }

                packet.Translator.ReadSingle("Float214", index);
                packet.Translator.ReadInt32("Int20C", index);
            }

            if (hasVehicleData)
            {
                packet.Translator.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.Translator.ReadUInt32("Vehicle Id", index);
            }

            if (hasAttackingTarget)
            {
                packet.Translator.ParseBitStream(attackingTargetGuid, 7, 1, 4, 6, 0, 2, 5, 3);
                packet.Translator.WriteGuid("Attacking GUID", attackingTargetGuid, index);
            }

            if (hasGameObjectRotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasAnimKits)
            {
                if (hasAnimKit2)
                    packet.Translator.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit1)
                    packet.Translator.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit3)
                    packet.Translator.ReadUInt16("Anim Kit 3", index);
            }

            if (bit28E)
            {
                packet.Translator.ReadBytes("Bytes", (int)bits28F, index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position.X = packet.Translator.ReadSingle();
                moveInfo.Position.Z = packet.Translator.ReadSingle();
                moveInfo.Orientation = packet.Translator.ReadSingle("Stationary Orientation", index);
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                packet.AddValue("Stationary Position", moveInfo.Position, index);
            }

            if (transport)
                packet.Translator.ReadInt32("Transport path timer", index);

            if (bit3F0)
                for (var i = 0; i < bits3F4; ++i)
                    packet.Translator.ReadInt32("Int3F8", index, i);

            if (bit284)
                packet.Translator.ReadInt32("Int288", index);

            if (bit3E8)
                packet.Translator.ReadInt32("Int3EC", index);

            if (living && moveInfo.HasSplineData && hasFullSpline && splineType == SplineType.FacingTarget)
            {
                var facingTargetGuid = new byte[8];
                facingTargetGuid = packet.Translator.StartBitStream(2, 4, 6, 3, 1, 5, 7, 0);
                packet.Translator.ParseBitStream(facingTargetGuid, 1, 3, 6, 7, 2, 4, 5, 0);
                packet.Translator.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = new byte[8];
            guid[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Despawn Animation");
            packet.Translator.StartBitStream(guid, 0, 1, 6, 2, 5, 7, 3);
            packet.Translator.ParseBitStream(guid, 7, 1, 2, 5, 0, 3, 6, 4);
            packet.Translator.WriteGuid("GUID", guid);
        }
    }
}
