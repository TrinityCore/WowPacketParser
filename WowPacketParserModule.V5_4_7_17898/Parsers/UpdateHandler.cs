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

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit("Despawn Animation");
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ParseBitStream(guid, 2, 1, 5, 4, 3, 6, 7, 0);

            var destroyed = packet.WriteGuid("GUID", guid);

            var update = packet.Holder.UpdateObject = new();
            update.Destroyed.Add(new DestroyedObject()
            {
                Guid = destroyed,
                Text = packet.Writer.ToString()
            });
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)]
        public static void HandleObjectUpdateFailed(Packet packet)
        {
            var guid = packet.StartBitStream(4, 6, 3, 0, 7, 5, 1, 2);
            packet.ParseBitStream(guid, 4, 7, 0, 6, 5, 2, 1, 3);

            packet.WriteGuid("Guid", guid);
        }

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
                        var createObject = new CreateObject() { Guid = guid, Values = new(){Legacy = new()}, CreateType = createType };
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
            var guid5 = new byte[8];
            var guid6 = new byte[8];
            var guid7 = new byte[8];

            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;
            var hasFullSpline = false;
            var hasSplineStartTime = false;
            var hasSplineVerticalAcceleration = false;
            var hasMovementFlags = false;
            var hasMoveFlagsExtra = false;
            var hasTimestamp = false;
            var hasOrientation = false;
            var hasTransportData = false;
            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasGOTransportTime2 = false;
            var hasGOTransportTime3 = false;
            var hasPitch = false;
            var hasFallData = false;
            var hasFallDirection = false;
            var hasSplineElevation = false;

            var bit134 = false;
            var bitA8 = false;
            var bit23C = false;
            var bit298 = false;
            var bit230 = false;
            var bit284 = false;
            var bit228 = false;
            var bit258 = false;
            var bit218 = false;
            var bit220 = false;

            var bits168 = 0u;
            var splineCount = 0u;
            var bits138 = 0u;
            var bits98 = 0u;
            var bits25C = 0u;
            var bits26C = 0u;
            var bits288 = 0u;
            var bits418 = 0u;
            var bits2AA = 0u;
            var splineType = SplineType.Stop;

            var bit2A4 = packet.ReadBit(); // 676
            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index); // 488
            var bit414 = packet.ReadBit(); // 1044
            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index); // 512
            packet.ReadBit(); // fake 0
            var isLiving = packet.ReadBit("Is Living", index); // 368
            var hasSceneObjectData = packet.ReadBit("Has Scene Object Data", index); // 1032
            packet.ReadBit(); // fake 2
            var bit29C = packet.ReadBit(); // 668
            var bit2A8 = packet.ReadBit(); // 680
            var bit2A9 = packet.ReadBit(); // 681
            packet.ReadBit(); // fake 1
            var hasTransportPosition = packet.ReadBit("Has Transport Position", index); // 424
            var bit1DC = packet.ReadBit(); // 476
            var hasAnimKits = packet.ReadBit("Has Anim Kits", index); // 498
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index); // 448
            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index); // 464
            packet.ReadBit(); // fake 3
            var transportFrames = packet.ReadBits("Transport Frames Count", 22, index); // 1068
            var bit32A = packet.ReadBit(); // 810
            var bit428 = packet.ReadBit(); // 1064

            if (bit428)
                bits418 = packet.ReadBits(22);

            if (hasTransportPosition)
            {
                packet.StartBitStream(goTransportGuid, 3, 5, 2, 1, 4);
                hasGOTransportTime3 = packet.ReadBit();
                hasGOTransportTime2 = packet.ReadBit();
                packet.StartBitStream(goTransportGuid, 0, 6, 7);
            }

            if (isLiving)
            {
                hasTransportData = packet.ReadBit("Has Transport Data", index);
                if (hasTransportData)
                {
                    transportGuid[4] = packet.ReadBit();
                    transportGuid[0] = packet.ReadBit();
                    transportGuid[5] = packet.ReadBit();
                    transportGuid[2] = packet.ReadBit();
                    transportGuid[3] = packet.ReadBit();
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit();
                    transportGuid[6] = packet.ReadBit();
                    transportGuid[1] = packet.ReadBit();
                    hasTransportTime3 = packet.ReadBit();
                }

                hasPitch = !packet.ReadBit("Has Pitch", index);
                packet.ReadBit();
                bits168 = packet.ReadBits(19);
                guid1[1] = packet.ReadBit();
                hasMoveFlagsExtra = !packet.ReadBit();
                packet.ReadBit();
                hasSplineElevation = !packet.ReadBit("Has SplineElevation", index);

                if (hasMoveFlagsExtra)
                    moveInfo.Flags2 = (uint)packet.ReadBitsE<MovementFlag2>("Extra Movement Flags", 13, index);

                hasOrientation = !packet.ReadBit();
                hasTimestamp = !packet.ReadBit();
                hasMovementFlags = !packet.ReadBit();
                bitA8 = !packet.ReadBit("bitA8", index);
                guid1[2] = packet.ReadBit();
                guid1[6] = packet.ReadBit();
                hasFallData = packet.ReadBit();
                guid1[5] = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                guid1[0] = packet.ReadBit();

                if (hasMovementFlags)
                    moveInfo.Flags = (uint)packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);
                packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit();
                bits98 = packet.ReadBits("bits98", 22, index);

                guid1[7] = packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit();
                guid1[3] = packet.ReadBit();


                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.ReadBit();
                    if (hasFullSpline)
                    {
                        hasSplineVerticalAcceleration = packet.ReadBit();
                        packet.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        splineCount = packet.ReadBits(20);
                        packet.ReadBitsE<SplineFlag434>("Spline flags", 25, index);

                        hasSplineStartTime = packet.ReadBit();
                        bit134 = packet.ReadBit();
                        if (bit134)
                        {
                            bits138 = packet.ReadBits(21);
                            packet.ReadBits(2);
                        }
                    }
                }
            }

            if (bit29C)
            {
                bit230 = packet.ReadBit();
                bit258 = packet.ReadBit();
                packet.ReadBit("bit20E", index);
                packet.ReadBit("bit20F", index);
                bit228 = packet.ReadBit();
                packet.ReadBit("bit20C", index);
                bit218 = packet.ReadBit();
                packet.ReadBit("bit20D", index);
                bit284 = packet.ReadBit();

                if (bit284)
                {
                    bits25C = packet.ReadBits(21);
                    bits26C = packet.ReadBits(21);
                }

                bit298 = packet.ReadBit();

                if (bit298)
                    bits288 = packet.ReadBits(20);

                bit23C = packet.ReadBit();
                packet.ReadBit("bit210", index);
                bit220 = packet.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.StartBitStream(4, 6, 3, 5, 0, 2, 7, 1);

            if (hasAnimKits)
            {
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
            }

            if (bit32A)
                bits2AA = packet.ReadBits(7);

            packet.ResetBitReader();

            for (var i = 0; i < transportFrames; ++i)
                packet.ReadInt32("Transport frame", index, i);

            if (bit29C)
            {
                if (bit23C)
                {
                    packet.ReadSingle("Float234", index);
                    packet.ReadSingle("Float238", index);
                }

                if (bit284)
                {
                    for (var i = 0; i < bits26C; ++i)
                    {
                        packet.ReadSingle("Float270+0", index, i);
                        packet.ReadSingle("Float270+1", index, i);
                    }

                    packet.ReadSingle("Float27C", index);

                    for (var i = 0; i < bits25C; ++i)
                    {
                        packet.ReadSingle("Float260+0", index, i);
                        packet.ReadSingle("Float260+1", index, i);
                    }

                    packet.ReadSingle("Float280", index);
                }

                if (bit258)
                {
                    packet.ReadSingle("Float244", index);
                    packet.ReadSingle("Float250", index);
                    packet.ReadSingle("Float254", index);
                    packet.ReadSingle("Float248", index);
                    packet.ReadSingle("Float240", index);
                    packet.ReadSingle("Float24C", index);
                }

                packet.ReadInt32("Int208", index);

                if (bit298)
                {
                    for (var i = 0; i < bits288; ++i)
                    {
                        packet.ReadSingle("Float28C+0", index, i);
                        packet.ReadSingle("Float28C+1", index, i);
                        packet.ReadSingle("Float28C+2", index, i);
                    }
                }

                if (bit220)
                    packet.ReadInt32("int21C", index);

                if (bit218)
                    packet.ReadInt32("int214", index);

                if (bit230)
                    packet.ReadInt32("int22C", index);

                if (bit228)
                    packet.ReadInt32("int224", index);
            }

            if (isLiving)
            {
                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump Sin Angle", index);
                        packet.ReadSingle("Jump Cos Angle", index);
                        packet.ReadSingle("Jump XY Speed", index);
                    }

                    packet.ReadSingle("Jump Z Speed", index);
                    packet.ReadUInt32("Jump Fall Time", index);
                }

                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
                        packet.ReadSingle("Duration Mod Next", index);
                        for (uint i = 0; i < splineCount; i++)
                        {
                            Vector3 v = new Vector3();
                            v.Z = packet.ReadSingle();
                            v.Y = packet.ReadSingle();
                            v.X = packet.ReadSingle();

                            packet.AddValue("Spline", v, index);
                        }

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

                        packet.ReadSingle("Duration Mod", index);

                        //    NYI block here

                        if (splineType == SplineType.FacingSpot)
                            packet.ReadVector3("Facing spot", index);

                        if (hasSplineVerticalAcceleration)
                            packet.ReadSingle("Spline Vertical Acceleration", index);

                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);

                        packet.ReadUInt32("Spline FULL Time", index);
                        if (hasSplineStartTime)
                            packet.ReadUInt32("Spline Start time", index);

                        packet.ReadUInt32("Spline Time", index);
                    }

                    packet.ReadUInt32("Spline ID", index);
                    packet.ReadVector3("Spline Endpoint", index);
                }

                /*for (var i = 0; i < 10; ++i)
                    packet.ReadSingle("unk float");
                if (bits98 > 0)
                    packet.ReadBits((int)bits98);*/
                //for (var i = 0; i < bits98; ++i)
                //    packet.ReadInt32("Int9C", index, i);


                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Position.Y = packet.ReadSingle();
                packet.ReadSingle("Fly Speed", index);
                packet.ReadXORByte(guid1, 6);
                packet.ReadSingle("Fly Back Speed", index);

                if (hasTransportData)
                {
                    moveInfo.Transport = new MovementInfo.TransportInfo();

                    packet.ReadXORByte(transportGuid, 7);
                    packet.ReadXORByte(transportGuid, 4);

                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);

                    packet.ReadUInt32("Transport Time", index);

                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);

                    moveInfo.Transport.Offset.O = packet.ReadSingle();
                    moveInfo.Transport.Offset.X = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 3);
                    packet.ReadXORByte(transportGuid, 2);
                    moveInfo.Transport.Offset.Z = packet.ReadSingle();
                    moveInfo.Transport.Offset.Y = packet.ReadSingle();
                    var seat = packet.ReadSByte("Transport Seat", index);
                    packet.ReadXORByte(transportGuid, 1);
                    packet.ReadXORByte(transportGuid, 0);
                    packet.ReadXORByte(transportGuid, 5);

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

                moveInfo.Position.X = packet.ReadSingle();
                packet.AddValue("Position", moveInfo.Position, index);
                packet.ReadXORByte(guid1, 2);

                if (hasPitch)
                    packet.ReadSingle("Pitch", index);

                packet.ReadSingle("Swim Speed", index);
                packet.ReadXORByte(guid1, 1);
                packet.ReadSingle("Run Back Speed", index);
                packet.ReadSingle("Swim Back Speed", index);
                packet.ReadXORByte(guid1, 5);

                if (hasSplineElevation)
                    packet.ReadSingle("Spline Elevation", index);

                if (bitA8)
                    packet.ReadInt32("IntA8", index);

                packet.ReadSingle("Turn Speed", index);
                packet.ReadXORByte(guid1, 3);
                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                packet.ReadXORByte(guid1, 7);
                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                packet.ReadSingle("Pitch Speed", index);

                if (hasTimestamp)
                    packet.ReadUInt32("Time?", index);

                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid1, 0);
                packet.WriteGuid("GUID2", guid1);

                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle("Orientation", index);
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 5, 1, 2, 0, 3, 4, 6, 7);
                packet.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                moveInfo.Position = packet.ReadVector3("Stationary Position", index);
            }

            if (hasTransportPosition)
            {
                moveInfo.Transport = new MovementInfo.TransportInfo();

                packet.ReadSByte("Transport Seat", index);
                moveInfo.Transport.Offset.X = packet.ReadSingle();
                packet.ReadXORBytes(goTransportGuid, 1, 0, 2, 6, 5, 4);

                if (hasGOTransportTime2)
                    packet.ReadUInt32("Transport Time 3", index);

                packet.ReadXORByte(goTransportGuid, 7);
                moveInfo.Transport.Offset.O = packet.ReadSingle();
                moveInfo.Transport.Offset.Z = packet.ReadSingle();
                moveInfo.Transport.Offset.Y = packet.ReadSingle();

                if (hasGOTransportTime3)
                    packet.ReadUInt32("Transport Time 2", index);

                packet.ReadXORByte(goTransportGuid, 3);
                packet.ReadUInt32("Transport Time", index);

                moveInfo.Transport.Guid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("Transport GUID", moveInfo.Transport.Guid, index);
                packet.AddValue("Transport Position", moveInfo.Transport.Offset, index);
            }

            if (bit1DC)
                packet.ReadInt32("int1D8", index);

            if (bit2A4)
                packet.ReadInt32("int2A0", index);

            if (bit32A)
                packet.ReadBytes("Bytes", (int)bits2AA);

            if (hasGameObjectRotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
                packet.ReadSingle("Vehicle Orientation", index);
            }

            if (bit414)
                packet.ReadInt32("int410", index);

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
            }

            if (bit428)
                for (var i = 0; i < bits418; ++i)
                    packet.ReadInt32("Int3F8", index, i);

            if (isLiving && moveInfo.HasSplineData && hasFullSpline && splineType == SplineType.FacingTarget)
            {

                var facingTargetGuid = new byte[8];
                packet.StartBitStream(facingTargetGuid, 6, 7, 3, 0, 5, 1, 4, 2);
                packet.ParseBitStream(facingTargetGuid, 4, 2, 5, 6, 0, 7, 1, 3);
                packet.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }

            return moveInfo;
        }
    }
}
