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

            guid[3] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Despawn Animation");
            guid[7] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(guid, 0, 4, 7, 2, 6, 3, 1, 5);

            packet.Translator.WriteGuid("GUID", guid);
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

            var bit676 = packet.Translator.ReadBit("bit676", index);
            var hasAnimKits = packet.Translator.ReadBit("Has Anim Kits", index); // 498
            var isLiving = packet.Translator.ReadBit("Is Living", index); // 368
            var bit810 = packet.Translator.ReadBit("bit810", index);
            packet.Translator.ReadBit(); //fake bit
            var transportFrames = packet.Translator.ReadBits("Transport Frames Count", 22, index); // 1068
            var hasVehicleData = packet.Translator.ReadBit("Has Vehicle Data", index); // 488
            var bit1044 = packet.Translator.ReadBit("bit1044", index);
            packet.Translator.ReadBit(); //fake bit
            var bit476 = packet.Translator.ReadBit("bit476", index);
            var hasGameObjectRotation = packet.Translator.ReadBit("Has GameObject Rotation", index); // 512
            packet.Translator.ReadBit(); //fake bit
            var bit680 = packet.Translator.ReadBit("bit680", index);
            var hasAttackingTarget = packet.Translator.ReadBit("Has Attacking Target", index); // 464
            var hasSceneObjectData = packet.Translator.ReadBit("Has Scene Object Data", index); // 1032
            var bit1064 = packet.Translator.ReadBit("bit1064", index);
            packet.Translator.ReadBit(); //fake bit
            var bit668 = packet.Translator.ReadBit("bit668", index);
            var hasTransportPosition = packet.Translator.ReadBit("Has Transport Position", index); // 424
            var bit681 = packet.Translator.ReadBit("bit681", index);
            var hasStationaryPosition = packet.Translator.ReadBit("Has Stationary Position", index); // 448

            if (isLiving)
            {
                guid1[2] = packet.Translator.ReadBit();
                var bit140 = packet.Translator.ReadBit();
                hasPitch = !packet.Translator.ReadBit("Has Pitch", index); //104
                hasTransportData = packet.Translator.ReadBit("Has Transport Data", index); //96
                packet.Translator.ReadBit(); // 164 fake bit

                if (hasTransportData)
                {
                    transportGuid[4] = packet.Translator.ReadBit();
                    transportGuid[2] = packet.Translator.ReadBit();
                    hasTransportTime3 = packet.Translator.ReadBit();
                    transportGuid[0] = packet.Translator.ReadBit();
                    transportGuid[1] = packet.Translator.ReadBit();
                    transportGuid[3] = packet.Translator.ReadBit();
                    transportGuid[6] = packet.Translator.ReadBit();
                    transportGuid[7] = packet.Translator.ReadBit();
                    hasTransportTime2 = packet.Translator.ReadBit();
                    transportGuid[5] = packet.Translator.ReadBit();
                }
                hasTimestamp = !packet.Translator.ReadBit();
                guid1[6] = packet.Translator.ReadBit();
                guid1[4] = packet.Translator.ReadBit();
                guid1[3] = packet.Translator.ReadBit();
                hasOrientation = !packet.Translator.ReadBit(); //40
                bit160 = !packet.Translator.ReadBit();
                guid1[5] = packet.Translator.ReadBit();
                bits98 = packet.Translator.ReadBits("bits98", 22, index); //144
                hasMovementFlags = !packet.Translator.ReadBit();
                bits168 = packet.Translator.ReadBits(19); //352

                /*var bits356 = new uint[bits168];
                for (var i = 0; i < bits168; ++i)
                {
                    bits356[i] = packet.Translator.ReadBits(2);
                }*/

                hasFallData = packet.Translator.ReadBit(); //132

                if (hasMovementFlags)
                    moveInfo.Flags = packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                hasSplineElevation = !packet.Translator.ReadBit("Has SplineElevation", index);//136
                moveInfo.HasSplineData = packet.Translator.ReadBit("Has SplineData", index);
                var bit141 = packet.Translator.ReadBit();
                guid1[0] = packet.Translator.ReadBit();
                guid1[7] = packet.Translator.ReadBit();
                guid1[1] = packet.Translator.ReadBit();

                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.Translator.ReadBit();
                    if (hasFullSpline)
                    {
                        hasSplineVerticalAcceleration = packet.Translator.ReadBit(); //260
                        hasSplineStartTime = packet.Translator.ReadBit(); //252
                        var bit304 = packet.Translator.ReadBit();

                        splineCount = packet.Translator.ReadBits(20);
                        packet.Translator.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        packet.Translator.ReadBitsE<SplineFlag434>("Spline flags", 25, index);

                        if (bit304)
                        {
                            bits138 = packet.Translator.ReadBits(21);
                            packet.Translator.ReadBits(2);
                        }
                    }
                }

                hasMoveFlagsExtra = !packet.Translator.ReadBit(); //20

                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit(); //128

                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13, index);

            }

            /*if (hasSceneObjectData)
            {

            }*/

            if (hasTransportPosition) //424
            {
                packet.Translator.StartBitStream(goTransportGuid, 4, 1, 0);
                hasGOTransportTime2 = packet.Translator.ReadBit(); // 420
                packet.Translator.StartBitStream(goTransportGuid, 6, 5, 3, 2, 7);
                hasGOTransportTime3 = packet.Translator.ReadBit(); // 412

            }

            if (bit668)
            {
                bit528 = packet.Translator.ReadBit();
                bit600 = packet.Translator.ReadBit();
                bit544 = packet.Translator.ReadBit();
                bit526 = packet.Translator.ReadBit();
                bit552 = packet.Translator.ReadBit();
                bit524 = packet.Translator.ReadBit();
                bit572 = packet.Translator.ReadBit();
                bit525 = packet.Translator.ReadBit();
                bit664 = packet.Translator.ReadBit();
                bit527 = packet.Translator.ReadBit();

                if (bit664)
                    bits288 = packet.Translator.ReadBits(20);

                bit536 = packet.Translator.ReadBit();
                bit644 = packet.Translator.ReadBit();
                bit560 = packet.Translator.ReadBit();

                if (bit664)
                {
                    bits25C = packet.Translator.ReadBits(21); //604
                    bits26C = packet.Translator.ReadBits(21); //624
                }
            }

            if (hasAnimKits)
            {
                hasAnimKit2 = !packet.Translator.ReadBit();
                hasAnimKit3 = !packet.Translator.ReadBit();
                hasAnimKit1 = !packet.Translator.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.Translator.StartBitStream(4, 6, 5, 2, 0, 1, 3, 7);

            if (bit1064)
                bits418 = packet.Translator.ReadBits(22);

            if (bit810)
                bits2AA = packet.Translator.ReadBits(7);

            packet.Translator.ResetBitReader();

            for (var i = 0; i < transportFrames; ++i)
                packet.Translator.ReadInt32("Transport frame", index, i);

            /*if (hasSceneObjectData)
            {
                until if ( *(_BYTE *)(v3 + 668) )
            }*/

            if (isLiving)
            {
                if (hasTransportData) //96
                {
                    packet.Translator.ReadXORByte(transportGuid, 7);
                    moveInfo.TransportOffset.X = packet.Translator.ReadSingle();

                    if (hasTransportTime3)
                        packet.Translator.ReadUInt32("Transport Time 3", index);

                    moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 4);
                    packet.Translator.ReadXORByte(transportGuid, 1);
                    packet.Translator.ReadXORByte(transportGuid, 3);
                    moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 5);

                    if (hasTransportTime2)
                        packet.Translator.ReadUInt32("Transport Time 2", index);

                    packet.Translator.ReadXORByte(transportGuid, 0);
                    var seat = packet.Translator.ReadSByte("Transport Seat", index); //72
                    packet.Translator.ReadXORByte(transportGuid, 6);
                    packet.Translator.ReadXORByte(transportGuid, 2);
                    packet.Translator.ReadUInt32("Transport Time", index); //76

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

                packet.Translator.ReadXORByte(guid1, 4);

                /*for (var i = 0; i < 10; ++i)
                    packet.Translator.ReadSingle("unk float");
                if (bits98 > 0)
                    packet.Translator.ReadBits((int)bits98);*/
                //for (var i = 0; i < bits98; ++i)
                //    packet.Translator.ReadInt32("Int9C", index, i);

                if (moveInfo.HasSplineData) //344
                {
                    if (hasFullSpline) //336
                    {
                        packet.Translator.ReadUInt32("Spline Time", index); //232
                        packet.Translator.ReadSingle("Duration Mod Next", index); //244

                        //    NYI block here
                        packet.Translator.ReadSingle("Duration Mod", index); //240

                        for (uint i = 0; i < splineCount; i++)
                        {
                            Vector3 v = new Vector3();
                            v.X = packet.Translator.ReadSingle(); //0
                            v.Z = packet.Translator.ReadSingle(); //8
                            v.Y = packet.Translator.ReadSingle(); //4

                            packet.AddValue("Spline", v, index);
                        }

                        if (hasSplineStartTime)
                            packet.Translator.ReadUInt32("Spline Start time", index); //256

                        var type = packet.Translator.ReadByte(); // 228

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
                            packet.Translator.ReadSingle("Facing Angle", index);

                        if (splineType == SplineType.FacingSpot) // == 2
                            packet.Translator.ReadVector3("Facing spot", index);

                        if (hasSplineVerticalAcceleration) //252
                            packet.Translator.ReadSingle("Spline Vertical Acceleration", index);

                        packet.Translator.ReadUInt32("Spline FULL Time", index); //236

                    }

                    moveInfo.Position.X = packet.Translator.ReadSingle(); //212
                    moveInfo.Position.Z = packet.Translator.ReadSingle(); //220
                    packet.Translator.ReadUInt32("Spline ID", index); //208
                    moveInfo.Position.Y = packet.Translator.ReadSingle(); //216
                }
                packet.Translator.ReadSingle("Fly Speed", index); //188

                if (bit160)
                    packet.Translator.ReadUInt32("unk160");

                packet.Translator.ReadXORByte(guid1, 2);

                if (hasFallData) //132
                {
                    if (hasFallDirection)//128
                    {
                        packet.Translator.ReadSingle("Jump Sin Angle", index); //124
                        packet.Translator.ReadSingle("Jump XY Speed", index); //116
                        packet.Translator.ReadSingle("Jump Cos Angle", index); //120

                    }
                    packet.Translator.ReadUInt32("Jump Fall Time", index); //108
                    packet.Translator.ReadSingle("Jump Z Speed", index); //112
                }
                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadSingle("Turn Speed", index);

                if (hasTimestamp)
                    packet.Translator.ReadUInt32("Time?", index);

                packet.Translator.ReadSingle("Swim Speed", index); //176

                if (hasSplineElevation) //136
                    packet.Translator.ReadSingle("Spline Elevation", index);//196

                packet.Translator.ReadXORByte(guid1, 7);
                packet.Translator.ReadSingle("Pitch Speed", index); //200

                for (var i = 0; i < bits98; ++i)
                    packet.Translator.ReadInt32("UNK counter", index, i);

                moveInfo.Position.X = packet.Translator.ReadSingle(); //28

                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch", index); //104

                if (hasOrientation)
                    moveInfo.Orientation = packet.Translator.ReadSingle("Orientation", index); //40

                moveInfo.WalkSpeed = packet.Translator.ReadSingle("Walk Speed", index) / 2.5f; // 168
                moveInfo.Position.Y = packet.Translator.ReadSingle(); //32
                packet.Translator.ReadSingle("Fly Back Speed", index); //192
                packet.Translator.ReadXORByte(guid1, 3);
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.ReadSingle("Run Back Speed", index);//184
                moveInfo.RunSpeed = packet.Translator.ReadSingle("Run Speed", index) / 7.0f; //172
                packet.Translator.ReadSingle("Swim Back Speed", index);//180
                moveInfo.Position.Z = packet.Translator.ReadSingle(); //36
            }

            packet.AddValue("Position", moveInfo.Position, index);

            if (bit668)
            {
                if (bit664)
                {
                    for (var i = 0; i < bits288; ++i)
                    {
                        packet.Translator.ReadSingle("Float652+4", index, i);
                        packet.Translator.ReadSingle("Float652", index, i);
                        packet.Translator.ReadSingle("Float652+8", index, i);
                    }
                }

                if (bit600)
                {
                    packet.Translator.ReadSingle("Float584", index);
                    packet.Translator.ReadSingle("Float580", index);
                    packet.Translator.ReadSingle("Float596", index);
                    packet.Translator.ReadSingle("Float592", index);
                    packet.Translator.ReadSingle("Float576", index);
                    packet.Translator.ReadSingle("Float588", index);
                }

                if (bit644)
                {
                    for (var i = 0; i < bits25C; ++i)
                    {
                        packet.Translator.ReadSingle("Float608", index, i);
                        packet.Translator.ReadSingle("Float608+4", index, i);
                    }

                    for (var i = 0; i < bits26C; ++i)
                    {
                        packet.Translator.ReadSingle("Float260+0", index, i);
                        packet.Translator.ReadSingle("Float260+1", index, i);
                    }

                    packet.Translator.ReadSingle("Float624", index);
                    packet.Translator.ReadSingle("Float624+4", index);
                }

                packet.Translator.ReadUInt32("unk520", index);

                if (bit544)
                    packet.Translator.ReadUInt32("unk544", index);

                if (bit552)
                    packet.Translator.ReadUInt32("unk548", index);

                if (bit536)
                    packet.Translator.ReadUInt32("unk532", index);

                if (bit560)
                    packet.Translator.ReadUInt32("unk556", index);

                if (bit572)
                {
                    packet.Translator.ReadSingle("Float564", index);
                    packet.Translator.ReadSingle("Float568", index);
                }
            }

            if (hasTransportPosition)
            {
                if (hasGOTransportTime2)
                    packet.Translator.ReadUInt32("Transport Time 2", index);

                moveInfo.TransportOffset.Y = packet.Translator.ReadSingle(); //388
                packet.Translator.ReadSByte("Transport Seat", index); //400
                moveInfo.TransportOffset.X = packet.Translator.ReadSingle();//384

                packet.Translator.ReadXORBytes(goTransportGuid, 2, 4, 1);

                if (hasGOTransportTime3)
                    packet.Translator.ReadUInt32("Transport Time 3", index); //412

                packet.Translator.ReadUInt32("Transport Time", index); //404
                moveInfo.TransportOffset.O = packet.Translator.ReadSingle(); //396
                moveInfo.TransportOffset.Z = packet.Translator.ReadSingle(); //392

                packet.Translator.ReadXORBytes(goTransportGuid, 6, 0, 5, 3, 7);

                moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("Transport GUID", moveInfo.TransportGuid, index);
                packet.AddValue("Transport Position", moveInfo.TransportOffset, index);
            }

            if (hasAttackingTarget)
            {
                packet.Translator.ParseBitStream(attackingTargetGuid, 7, 1, 5, 2, 6, 3, 0, 4);
                packet.Translator.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.Translator.ReadUInt32("Vehicle Id", index);
                packet.Translator.ReadSingle("Vehicle Orientation", index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                moveInfo.Position.Z = packet.Translator.ReadSingle();
                moveInfo.Orientation = packet.Translator.ReadSingle("Stationary Orientation", index);
                moveInfo.Position.X = packet.Translator.ReadSingle();

                packet.AddValue("Stationary Position", moveInfo.Position, index);
            }

            if (bit676)
                packet.Translator.ReadUInt32("unk672");

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.Translator.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit3)
                    packet.Translator.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit2)
                    packet.Translator.ReadUInt16("Anim Kit 2", index);
            }

            if (bit810)
                packet.Translator.ReadBytes("Bytes", (int)bits2AA);

            if (bit476)
                packet.Translator.ReadUInt32("unk472");

            if (bit1064)
            {
                for (var i = 0; i < bits418; ++i)
                    packet.Translator.ReadInt32("unk1052+4", index, i);
            }

            if (hasGameObjectRotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GameObject Rotation", index);

            if (bit1044)
                packet.Translator.ReadInt32("unk1040", index);

            if (isLiving && moveInfo.HasSplineData && hasFullSpline && splineType == SplineType.FacingTarget)
            {

                var facingTargetGuid = new byte[8];
                packet.Translator.StartBitStream(facingTargetGuid, 4, 7, 0, 5, 1, 2, 3, 6);
                packet.Translator.ParseBitStream(facingTargetGuid, 4, 2, 0, 5, 6, 3, 1, 7);
                packet.Translator.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }
            return moveInfo;
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)]
        public static void HandleObjectUpdateFailed(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 5, 6, 0, 1, 2, 7, 4);
            packet.Translator.ParseBitStream(guid, 0, 6, 5, 7, 2, 1, 3, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
