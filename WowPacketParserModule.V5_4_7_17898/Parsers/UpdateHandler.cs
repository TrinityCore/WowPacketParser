using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Despawn Animation");
            guid[6] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(guid, 2, 1, 5, 4, 3, 6, 7, 0);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)]
        public static void HandleObjectUpdateFailed(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(4, 6, 3, 0, 7, 5, 1, 2);
            packet.Translator.ParseBitStream(guid, 4, 7, 0, 6, 5, 2, 1, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

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

            var bit2A4 = packet.Translator.ReadBit(); // 676
            var hasVehicleData = packet.Translator.ReadBit("Has Vehicle Data", index); // 488
            var bit414 = packet.Translator.ReadBit(); // 1044
            var hasGameObjectRotation = packet.Translator.ReadBit("Has GameObject Rotation", index); // 512
            packet.Translator.ReadBit(); // fake 0
            var isLiving = packet.Translator.ReadBit("Is Living", index); // 368
            var hasSceneObjectData = packet.Translator.ReadBit("Has Scene Object Data", index); // 1032
            packet.Translator.ReadBit(); // fake 2
            var bit29C = packet.Translator.ReadBit(); // 668
            var bit2A8 = packet.Translator.ReadBit(); // 680
            var bit2A9 = packet.Translator.ReadBit(); // 681
            packet.Translator.ReadBit(); // fake 1
            var hasTransportPosition = packet.Translator.ReadBit("Has Transport Position", index); // 424
            var bit1DC = packet.Translator.ReadBit(); // 476
            var hasAnimKits = packet.Translator.ReadBit("Has Anim Kits", index); // 498
            var hasStationaryPosition = packet.Translator.ReadBit("Has Stationary Position", index); // 448
            var hasAttackingTarget = packet.Translator.ReadBit("Has Attacking Target", index); // 464
            packet.Translator.ReadBit(); // fake 3
            var transportFrames = packet.Translator.ReadBits("Transport Frames Count", 22, index); // 1068
            var bit32A = packet.Translator.ReadBit(); // 810
            var bit428 = packet.Translator.ReadBit(); // 1064

            if (bit428)
                bits418 = packet.Translator.ReadBits(22);

            if (hasTransportPosition)
            {
                packet.Translator.StartBitStream(goTransportGuid, 3, 5, 2, 1, 4);
                hasGOTransportTime3 = packet.Translator.ReadBit();
                hasGOTransportTime2 = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(goTransportGuid, 0, 6, 7);
            }

            if (isLiving)
            {
                hasTransportData = packet.Translator.ReadBit("Has Transport Data", index);
                if (hasTransportData)
                {
                    transportGuid[4] = packet.Translator.ReadBit();
                    transportGuid[0] = packet.Translator.ReadBit();
                    transportGuid[5] = packet.Translator.ReadBit();
                    transportGuid[2] = packet.Translator.ReadBit();
                    transportGuid[3] = packet.Translator.ReadBit();
                    hasTransportTime2 = packet.Translator.ReadBit();
                    transportGuid[7] = packet.Translator.ReadBit();
                    transportGuid[6] = packet.Translator.ReadBit();
                    transportGuid[1] = packet.Translator.ReadBit();
                    hasTransportTime3 = packet.Translator.ReadBit();
                }

                hasPitch = !packet.Translator.ReadBit("Has Pitch", index);
                packet.Translator.ReadBit();
                bits168 = packet.Translator.ReadBits(19);
                guid1[1] = packet.Translator.ReadBit();
                hasMoveFlagsExtra = !packet.Translator.ReadBit();
                packet.Translator.ReadBit();
                hasSplineElevation = !packet.Translator.ReadBit("Has SplineElevation", index);

                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13, index);

                hasOrientation = !packet.Translator.ReadBit();
                hasTimestamp = !packet.Translator.ReadBit();
                hasMovementFlags = !packet.Translator.ReadBit();
                bitA8 = !packet.Translator.ReadBit("bitA8", index);
                guid1[2] = packet.Translator.ReadBit();
                guid1[6] = packet.Translator.ReadBit();
                hasFallData = packet.Translator.ReadBit();
                guid1[5] = packet.Translator.ReadBit();
                guid1[4] = packet.Translator.ReadBit();
                guid1[0] = packet.Translator.ReadBit();

                if (hasMovementFlags)
                    moveInfo.Flags = packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30, index);
                packet.Translator.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit();
                bits98 = packet.Translator.ReadBits("bits98", 22, index);

                guid1[7] = packet.Translator.ReadBit();
                moveInfo.HasSplineData = packet.Translator.ReadBit();
                guid1[3] = packet.Translator.ReadBit();


                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.Translator.ReadBit();
                    if (hasFullSpline)
                    {
                        hasSplineVerticalAcceleration = packet.Translator.ReadBit();
                        packet.Translator.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        splineCount = packet.Translator.ReadBits(20);
                        packet.Translator.ReadBitsE<SplineFlag434>("Spline flags", 25, index);

                        hasSplineStartTime = packet.Translator.ReadBit();
                        bit134 = packet.Translator.ReadBit();
                        if (bit134)
                        {
                            bits138 = packet.Translator.ReadBits(21);
                            packet.Translator.ReadBits(2);
                        }
                    }
                }
            }

            if (bit29C)
            {
                bit230 = packet.Translator.ReadBit();
                bit258 = packet.Translator.ReadBit();
                packet.Translator.ReadBit("bit20E", index);
                packet.Translator.ReadBit("bit20F", index);
                bit228 = packet.Translator.ReadBit();
                packet.Translator.ReadBit("bit20C", index);
                bit218 = packet.Translator.ReadBit();
                packet.Translator.ReadBit("bit20D", index);
                bit284 = packet.Translator.ReadBit();

                if (bit284)
                {
                    bits25C = packet.Translator.ReadBits(21);
                    bits26C = packet.Translator.ReadBits(21);
                }

                bit298 = packet.Translator.ReadBit();

                if (bit298)
                    bits288 = packet.Translator.ReadBits(20);

                bit23C = packet.Translator.ReadBit();
                packet.Translator.ReadBit("bit210", index);
                bit220 = packet.Translator.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.Translator.StartBitStream(4, 6, 3, 5, 0, 2, 7, 1);

            if (hasAnimKits)
            {
                hasAnimKit1 = !packet.Translator.ReadBit();
                hasAnimKit2 = !packet.Translator.ReadBit();
                hasAnimKit3 = !packet.Translator.ReadBit();
            }

            if (bit32A)
                bits2AA = packet.Translator.ReadBits(7);

            packet.Translator.ResetBitReader();

            for (var i = 0; i < transportFrames; ++i)
                packet.Translator.ReadInt32("Transport frame", index, i);

            if (bit29C)
            {
                if (bit23C)
                {
                    packet.Translator.ReadSingle("Float234", index);
                    packet.Translator.ReadSingle("Float238", index);
                }

                if (bit284)
                {
                    for (var i = 0; i < bits26C; ++i)
                    {
                        packet.Translator.ReadSingle("Float270+0", index, i);
                        packet.Translator.ReadSingle("Float270+1", index, i);
                    }

                    packet.Translator.ReadSingle("Float27C", index);

                    for (var i = 0; i < bits25C; ++i)
                    {
                        packet.Translator.ReadSingle("Float260+0", index, i);
                        packet.Translator.ReadSingle("Float260+1", index, i);
                    }

                    packet.Translator.ReadSingle("Float280", index);
                }

                if (bit258)
                {
                    packet.Translator.ReadSingle("Float244", index);
                    packet.Translator.ReadSingle("Float250", index);
                    packet.Translator.ReadSingle("Float254", index);
                    packet.Translator.ReadSingle("Float248", index);
                    packet.Translator.ReadSingle("Float240", index);
                    packet.Translator.ReadSingle("Float24C", index);
                }

                packet.Translator.ReadInt32("Int208", index);

                if (bit298)
                {
                    for (var i = 0; i < bits288; ++i)
                    {
                        packet.Translator.ReadSingle("Float28C+0", index, i);
                        packet.Translator.ReadSingle("Float28C+1", index, i);
                        packet.Translator.ReadSingle("Float28C+2", index, i);
                    }
                }

                if (bit220)
                    packet.Translator.ReadInt32("int21C", index);

                if (bit218)
                    packet.Translator.ReadInt32("int214", index);

                if (bit230)
                    packet.Translator.ReadInt32("int22C", index);

                if (bit228)
                    packet.Translator.ReadInt32("int224", index);
            }

            if (isLiving)
            {
                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Jump Sin Angle", index);
                        packet.Translator.ReadSingle("Jump Cos Angle", index);
                        packet.Translator.ReadSingle("Jump XY Speed", index);
                    }

                    packet.Translator.ReadSingle("Jump Z Speed", index);
                    packet.Translator.ReadUInt32("Jump Fall Time", index);
                }

                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
                        packet.Translator.ReadSingle("Duration Mod Next", index);
                        for (uint i = 0; i < splineCount; i++)
                        {
                            Vector3 v = new Vector3();
                            v.Z = packet.Translator.ReadSingle();
                            v.Y = packet.Translator.ReadSingle();
                            v.X = packet.Translator.ReadSingle();

                            packet.AddValue("Spline", v, index);
                        }

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

                        packet.Translator.ReadSingle("Duration Mod", index);

                        //    NYI block here

                        if (splineType == SplineType.FacingSpot)
                            packet.Translator.ReadVector3("Facing spot", index);

                        if (hasSplineVerticalAcceleration)
                            packet.Translator.ReadSingle("Spline Vertical Acceleration", index);

                        if (splineType == SplineType.FacingAngle)
                            packet.Translator.ReadSingle("Facing Angle", index);

                        packet.Translator.ReadUInt32("Spline FULL Time", index);
                        if (hasSplineStartTime)
                            packet.Translator.ReadUInt32("Spline Start time", index);

                        packet.Translator.ReadUInt32("Spline Time", index);
                    }

                    packet.Translator.ReadUInt32("Spline ID", index);
                    packet.Translator.ReadVector3("Spline Endpoint", index);
                }

                /*for (var i = 0; i < 10; ++i)
                    packet.Translator.ReadSingle("unk float");
                if (bits98 > 0)
                    packet.Translator.ReadBits((int)bits98);*/
                //for (var i = 0; i < bits98; ++i)
                //    packet.Translator.ReadInt32("Int9C", index, i);


                moveInfo.Position.Z = packet.Translator.ReadSingle();
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadSingle("Fly Speed", index);
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadSingle("Fly Back Speed", index);

                if (hasTransportData)
                {
                    packet.Translator.ReadXORByte(transportGuid, 7);
                    packet.Translator.ReadXORByte(transportGuid, 4);

                    if (hasTransportTime3)
                        packet.Translator.ReadUInt32("Transport Time 3", index);

                    packet.Translator.ReadUInt32("Transport Time", index);

                    if (hasTransportTime2)
                        packet.Translator.ReadUInt32("Transport Time 2", index);

                    moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 6);
                    packet.Translator.ReadXORByte(transportGuid, 3);
                    packet.Translator.ReadXORByte(transportGuid, 2);
                    moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    var seat = packet.Translator.ReadSByte("Transport Seat", index);
                    packet.Translator.ReadXORByte(transportGuid, 1);
                    packet.Translator.ReadXORByte(transportGuid, 0);
                    packet.Translator.ReadXORByte(transportGuid, 5);

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

                moveInfo.Position.X = packet.Translator.ReadSingle();
                packet.AddValue("Position", moveInfo.Position, index);
                packet.Translator.ReadXORByte(guid1, 2);

                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch", index);

                packet.Translator.ReadSingle("Swim Speed", index);
                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadSingle("Run Back Speed", index);
                packet.Translator.ReadSingle("Swim Back Speed", index);
                packet.Translator.ReadXORByte(guid1, 5);

                if (hasSplineElevation)
                    packet.Translator.ReadSingle("Spline Elevation", index);

                if (bitA8)
                    packet.Translator.ReadInt32("IntA8", index);

                packet.Translator.ReadSingle("Turn Speed", index);
                packet.Translator.ReadXORByte(guid1, 3);
                moveInfo.RunSpeed = packet.Translator.ReadSingle("Run Speed", index) / 7.0f;
                packet.Translator.ReadXORByte(guid1, 7);
                moveInfo.WalkSpeed = packet.Translator.ReadSingle("Walk Speed", index) / 2.5f;
                packet.Translator.ReadSingle("Pitch Speed", index);

                if (hasTimestamp)
                    packet.Translator.ReadUInt32("Time?", index);

                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.WriteGuid("GUID2", guid1);

                if (hasOrientation)
                    moveInfo.Orientation = packet.Translator.ReadSingle("Orientation", index);
            }

            if (hasAttackingTarget)
            {
                packet.Translator.ParseBitStream(attackingTargetGuid, 5, 1, 2, 0, 3, 4, 6, 7);
                packet.Translator.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Orientation = packet.Translator.ReadSingle("Stationary Orientation", index);
                moveInfo.Position = packet.Translator.ReadVector3("Stationary Position", index);
            }

            if (hasTransportPosition)
            {
                packet.Translator.ReadSByte("Transport Seat", index);
                moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(goTransportGuid, 1, 0, 2, 6, 5, 4);

                if (hasGOTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 3", index);

                packet.Translator.ReadXORByte(goTransportGuid, 7);
                moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();

                if (hasGOTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 2", index);

                packet.Translator.ReadXORByte(goTransportGuid, 3);
                packet.Translator.ReadUInt32("Transport Time", index);

                moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("Transport GUID", moveInfo.TransportGuid, index);
                packet.AddValue("Transport Position", moveInfo.TransportOffset, index);
            }

            if (bit1DC)
                packet.Translator.ReadInt32("int1D8", index);

            if (bit2A4)
                packet.Translator.ReadInt32("int2A0", index);

            if (bit32A)
                packet.Translator.ReadBytes("Bytes", (int)bits2AA);

            if (hasGameObjectRotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.Translator.ReadUInt32("Vehicle Id", index);
                packet.Translator.ReadSingle("Vehicle Orientation", index);
            }

            if (bit414)
                packet.Translator.ReadInt32("int410", index);

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.Translator.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.Translator.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit3)
                    packet.Translator.ReadUInt16("Anim Kit 3", index);
            }

            if (bit428)
                for (var i = 0; i < bits418; ++i)
                    packet.Translator.ReadInt32("Int3F8", index, i);

            if (isLiving && moveInfo.HasSplineData && hasFullSpline && splineType == SplineType.FacingTarget)
            {

                var facingTargetGuid = new byte[8];
                packet.Translator.StartBitStream(facingTargetGuid, 6, 7, 3, 0, 5, 1, 4, 2);
                packet.Translator.ParseBitStream(facingTargetGuid, 4, 2, 5, 6, 0, 7, 1, 3);
                packet.Translator.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }

            return moveInfo;
        }
    }
}
