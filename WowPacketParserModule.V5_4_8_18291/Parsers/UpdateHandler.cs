using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            packet.ReadBit("Despawn Animation");
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ParseBitStream(guid, 0, 4, 7, 2, 6, 3, 1, 5);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            uint map = packet.ReadUInt16("Map");
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

                packet.AddValue("UpdateType", typeString, i);
                switch (typeString)
                {
                    case "Values":
                        {
                            var guid = packet.ReadPackedGuid("GUID", i);

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
                            var guid = packet.ReadPackedGuid("GUID", i);
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
            var objType = packet.ReadByteE<ObjectType>("Object Type", index);
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

            var bit160 = false;
            var bit528 = false;
            var bit600 = false;
            var bit544 = false;
            var bit526 = false;
            var bit552 = false;
            var bit524 = false;
            var bit572 = false;
            var bit525 = false;
            var bit664 = false;
            var bit527 = false;
            var bit536 = false;
            var bit644 = false;
            var bit560 = false;

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

            var bit676 = packet.ReadBit("bit676", index);
            var hasAnimKits = packet.ReadBit("Has Anim Kits", index); // 498
            var isLiving = packet.ReadBit("Is Living", index); // 368
            var bit810 = packet.ReadBit("bit810", index);
            packet.ReadBit(); //fake bit
            var transportFrames = packet.ReadBits("Transport Frames Count", 22, index); // 1068
            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index); // 488
            var bit1044 = packet.ReadBit("bit1044", index);
            packet.ReadBit(); //fake bit
            var bit476 = packet.ReadBit("bit476", index);
            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index); // 512
            packet.ReadBit(); //fake bit
            var bit680 = packet.ReadBit("bit680", index);
            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index); // 464
            var hasSceneObjectData = packet.ReadBit("Has Scene Object Data", index); // 1032
            var bit1064 = packet.ReadBit("bit1064", index);
            packet.ReadBit(); //fake bit
            var bit668 = packet.ReadBit("bit668", index);
            var hasTransportPosition = packet.ReadBit("Has Transport Position", index); // 424
            var bit681 = packet.ReadBit("bit681", index);
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index); // 448

            if (isLiving)
            {
                guid1[2] = packet.ReadBit();
                var bit140 = packet.ReadBit();
                hasPitch = !packet.ReadBit("Has Pitch", index); //104
                hasTransportData = packet.ReadBit("Has Transport Data", index); //96
                packet.ReadBit(); // 164 fake bit

                if (hasTransportData)
                {
                    transportGuid[4] = packet.ReadBit();
                    transportGuid[2] = packet.ReadBit();
                    hasTransportTime3 = packet.ReadBit();
                    transportGuid[0] = packet.ReadBit();
                    transportGuid[1] = packet.ReadBit();
                    transportGuid[3] = packet.ReadBit();
                    transportGuid[6] = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit();
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[5] = packet.ReadBit();
                }
                hasTimestamp = !packet.ReadBit();
                guid1[6] = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                guid1[3] = packet.ReadBit();
                hasOrientation = !packet.ReadBit(); //40
                bit160 = !packet.ReadBit();
                guid1[5] = packet.ReadBit();
                bits98 = packet.ReadBits("bits98", 22, index); //144
                hasMovementFlags = !packet.ReadBit();
                bits168 = packet.ReadBits(19); //352

                /*var bits356 = new uint[bits168];
                for (var i = 0; i < bits168; ++i)
                {
                    bits356[i] = packet.ReadBits(2);
                }*/

                hasFallData = packet.ReadBit(); //132

                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                hasSplineElevation = !packet.ReadBit("Has SplineElevation", index);//136
                moveInfo.HasSplineData = packet.ReadBit("Has SplineData", index);
                var bit141 = packet.ReadBit();
                guid1[0] = packet.ReadBit();
                guid1[7] = packet.ReadBit();
                guid1[1] = packet.ReadBit();

                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.ReadBit();
                    if (hasFullSpline)
                    {
                        hasSplineVerticalAcceleration = packet.ReadBit(); //260
                        hasSplineStartTime = packet.ReadBit(); //252
                        var bit304 = packet.ReadBit();

                        splineCount = packet.ReadBits(20);
                        packet.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        packet.ReadBitsE<SplineFlag434>("Spline flags", 25, index);

                        if (bit304)
                        {
                            bits138 = packet.ReadBits(21);
                            packet.ReadBits(2);
                        }
                    }
                }

                hasMoveFlagsExtra = !packet.ReadBit(); //20

                if (hasFallData)
                    hasFallDirection = packet.ReadBit(); //128

                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13, index);

            }

            /*if (hasSceneObjectData)
            {

            }*/

            if (hasTransportPosition) //424
            {
                packet.StartBitStream(goTransportGuid, 4, 1, 0);
                hasGOTransportTime2 = packet.ReadBit(); // 420
                packet.StartBitStream(goTransportGuid, 6, 5, 3, 2, 7);
                hasGOTransportTime3 = packet.ReadBit(); // 412

            }

            if (bit668)
            {
                bit528 = packet.ReadBit();
                bit600 = packet.ReadBit();
                bit544 = packet.ReadBit();
                bit526 = packet.ReadBit();
                bit552 = packet.ReadBit();
                bit524 = packet.ReadBit();
                bit572 = packet.ReadBit();
                bit525 = packet.ReadBit();
                bit664 = packet.ReadBit();
                bit527 = packet.ReadBit();

                if (bit664)
                    bits288 = packet.ReadBits(20);

                bit536 = packet.ReadBit();
                bit644 = packet.ReadBit();
                bit560 = packet.ReadBit();

                if (bit664)
                {
                    bits25C = packet.ReadBits(21); //604
                    bits26C = packet.ReadBits(21); //624
                }
            }

            if (hasAnimKits)
            {
                hasAnimKit2 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.StartBitStream(4, 6, 5, 2, 0, 1, 3, 7);

            if (bit1064)
                bits418 = packet.ReadBits(22);

            if (bit810)
                bits2AA = packet.ReadBits(7);

            packet.ResetBitReader();

            for (var i = 0; i < transportFrames; ++i)
                packet.ReadInt32("Transport frame", index, i);

            /*if (hasSceneObjectData)
            {
                until if ( *(_BYTE *)(v3 + 668) )
            }*/

            if (isLiving)
            {
                if (hasTransportData) //96
                {
                    packet.ReadXORByte(transportGuid, 7);
                    moveInfo.TransportOffset.X = packet.ReadSingle();

                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);

                    moveInfo.TransportOffset.O = packet.ReadSingle();
                    moveInfo.TransportOffset.Y = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 4);
                    packet.ReadXORByte(transportGuid, 1);
                    packet.ReadXORByte(transportGuid, 3);
                    moveInfo.TransportOffset.Z = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 5);

                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);

                    packet.ReadXORByte(transportGuid, 0);
                    var seat = packet.ReadSByte("Transport Seat", index); //72
                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 2);
                    packet.ReadUInt32("Transport Time", index); //76

                    moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(transportGuid, 0));
                    packet.AddValue("Transport GUID", moveInfo.TransportGuid, index);
                    packet.AddValue("Transport Position", moveInfo.TransportOffset, index);

                    if (moveInfo.TransportGuid.HasEntry() && moveInfo.TransportGuid.GetHighType() == HighGuidType.Vehicle &&
                        guid.HasEntry() && guid.GetHighType() == HighGuidType.Creature)
                    {
                        var vehicleAccessory = new VehicleTemplateAccessory
                        {
                            AccessoryEntry = guid.GetEntry(),
                            SeatId = seat
                        };

                        Storage.VehicleTemplateAccessorys.Add(moveInfo.TransportGuid.GetEntry(), vehicleAccessory, packet.TimeSpan);
                    }
                }

                packet.ReadXORByte(guid1, 4);

                /*for (var i = 0; i < 10; ++i)
                    packet.ReadSingle("unk float");
                if (bits98 > 0)
                    packet.ReadBits((int)bits98);*/
                //for (var i = 0; i < bits98; ++i)
                //    packet.ReadInt32("Int9C", index, i);

                if (moveInfo.HasSplineData) //344
                {
                    if (hasFullSpline) //336
                    {
                        packet.ReadUInt32("Spline Time", index); //232
                        packet.ReadSingle("Duration Mod Next", index); //244

                        //    NYI block here
                        packet.ReadSingle("Duration Mod", index); //240

                        for (uint i = 0; i < splineCount; i++)
                        {
                            Vector3 v = new Vector3();
                            v.X = packet.ReadSingle(); //0
                            v.Z = packet.ReadSingle(); //8
                            v.Y = packet.ReadSingle(); //4

                            packet.AddValue("Spline", v, index);
                        }

                        if (hasSplineStartTime)
                            packet.ReadUInt32("Spline Start time", index); //256

                        var type = packet.ReadByte(); // 228

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

                        if (splineType == SplineType.FacingAngle) // == 4
                            packet.ReadSingle("Facing Angle", index);

                        if (splineType == SplineType.FacingSpot) // == 2
                            packet.ReadVector3("Facing spot", index);

                        if (hasSplineVerticalAcceleration) //252
                            packet.ReadSingle("Spline Vertical Acceleration", index);

                        packet.ReadUInt32("Spline FULL Time", index); //236

                    }

                    moveInfo.Position.X = packet.ReadSingle(); //212
                    moveInfo.Position.Z = packet.ReadSingle(); //220
                    packet.ReadUInt32("Spline ID", index); //208
                    moveInfo.Position.Y = packet.ReadSingle(); //216
                }
                packet.ReadSingle("Fly Speed", index); //188

                if (bit160)
                    packet.ReadUInt32("unk160");

                packet.ReadXORByte(guid1, 2);

                if (hasFallData) //132
                {
                    if (hasFallDirection)//128
                    {
                        packet.ReadSingle("Jump Sin Angle", index); //124
                        packet.ReadSingle("Jump XY Speed", index); //116
                        packet.ReadSingle("Jump Cos Angle", index); //120

                    }
                    packet.ReadUInt32("Jump Fall Time", index); //108
                    packet.ReadSingle("Jump Z Speed", index); //112
                }
                packet.ReadXORByte(guid1, 1);
                packet.ReadSingle("Turn Speed", index);

                if (hasTimestamp)
                    packet.ReadUInt32("Time?", index);

                packet.ReadSingle("Swim Speed", index); //176

                if (hasSplineElevation) //136
                    packet.ReadSingle("Spline Elevation", index);//196

                packet.ReadXORByte(guid1, 7);
                packet.ReadSingle("Pitch Speed", index); //200

                for (var i = 0; i < bits98; ++i)
                    packet.ReadInt32("UNK counter", index, i);

                moveInfo.Position.X = packet.ReadSingle(); //28

                if (hasPitch)
                    packet.ReadSingle("Pitch", index); //104

                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle("Orientation", index); //40

                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f; // 168
                moveInfo.Position.Y = packet.ReadSingle(); //32
                packet.ReadSingle("Fly Back Speed", index); //192
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid1, 5);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid1, 0);
                packet.ReadSingle("Run Back Speed", index);//184
                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f; //172
                packet.ReadSingle("Swim Back Speed", index);//180
                moveInfo.Position.Z = packet.ReadSingle(); //36
            }

            packet.AddValue("Position", moveInfo.Position, index);

            if (bit668)
            {
                if (bit664)
                {
                    for (var i = 0; i < bits288; ++i)
                    {
                        packet.ReadSingle("Float652+4", index, i);
                        packet.ReadSingle("Float652", index, i);
                        packet.ReadSingle("Float652+8", index, i);
                    }
                }

                if (bit600)
                {
                    packet.ReadSingle("Float584", index);
                    packet.ReadSingle("Float580", index);
                    packet.ReadSingle("Float596", index);
                    packet.ReadSingle("Float592", index);
                    packet.ReadSingle("Float576", index);
                    packet.ReadSingle("Float588", index);
                }

                if (bit644)
                {
                    for (var i = 0; i < bits25C; ++i)
                    {
                        packet.ReadSingle("Float608", index, i);
                        packet.ReadSingle("Float608+4", index, i);
                    }

                    for (var i = 0; i < bits26C; ++i)
                    {
                        packet.ReadSingle("Float260+0", index, i);
                        packet.ReadSingle("Float260+1", index, i);
                    }

                    packet.ReadSingle("Float624", index);
                    packet.ReadSingle("Float624+4", index);
                }

                packet.ReadUInt32("unk520", index);

                if (bit544)
                    packet.ReadUInt32("unk544", index);

                if (bit552)
                    packet.ReadUInt32("unk548", index);

                if (bit536)
                    packet.ReadUInt32("unk532", index);

                if (bit560)
                    packet.ReadUInt32("unk556", index);

                if (bit572)
                {
                    packet.ReadSingle("Float564", index);
                    packet.ReadSingle("Float568", index);
                }
            }

            if (hasTransportPosition)
            {
                if (hasGOTransportTime2)
                    packet.ReadUInt32("Transport Time 2", index);

                moveInfo.TransportOffset.Y = packet.ReadSingle(); //388
                packet.ReadSByte("Transport Seat", index); //400
                moveInfo.TransportOffset.X = packet.ReadSingle();//384

                packet.ReadXORBytes(goTransportGuid, 2, 4, 1);

                if (hasGOTransportTime3)
                    packet.ReadUInt32("Transport Time 3", index); //412

                packet.ReadUInt32("Transport Time", index); //404
                moveInfo.TransportOffset.O = packet.ReadSingle(); //396
                moveInfo.TransportOffset.Z = packet.ReadSingle(); //392

                packet.ReadXORBytes(goTransportGuid, 6, 0, 5, 3, 7);

                moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("Transport GUID", moveInfo.TransportGuid, index);
                packet.AddValue("Transport Position", moveInfo.TransportOffset, index);
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 7, 1, 5, 2, 6, 3, 0, 4);
                packet.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
                packet.ReadSingle("Vehicle Orientation", index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position.Y = packet.ReadSingle();
                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                moveInfo.Position.X = packet.ReadSingle();

                packet.AddValue("Stationary Position", moveInfo.Position, index);
            }

            if (bit676)
                packet.ReadUInt32("unk672");

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
            }

            if (bit810)
                packet.ReadBytes("Bytes", (int)bits2AA);

            if (bit476)
                packet.ReadUInt32("unk472");

            if (bit1064)
            {
                for (var i = 0; i < bits418; ++i)
                    packet.ReadInt32("unk1052+4", index, i);
            }

            if (hasGameObjectRotation)
                packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (bit1044)
                packet.ReadInt32("unk1040", index);

            if (isLiving && moveInfo.HasSplineData && hasFullSpline && splineType == SplineType.FacingTarget)
            {

                var facingTargetGuid = new byte[8];
                packet.StartBitStream(facingTargetGuid, 4, 7, 0, 5, 1, 2, 3, 6);
                packet.ParseBitStream(facingTargetGuid, 4, 2, 0, 5, 6, 3, 1, 7);
                packet.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }
            return moveInfo;
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)]
        public static void HandleObjectUpdateFailed(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 6, 0, 1, 2, 7, 4);
            packet.ParseBitStream(guid, 0, 6, 5, 7, 2, 1, 3, 4);

            packet.WriteGuid("Guid", guid);
        }
    }
}
