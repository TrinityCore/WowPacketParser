using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;
using UpdateFields = WowPacketParser.Enums.Version.UpdateFields;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class UpdateHandler
    {
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

                packet.WriteLine("[" + i + "] UpdateType: " + typeString);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);

                        WoWObject obj;
                        var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(ref packet, guid.GetObjectType(), i, false);

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
                        ReadCreateObjectBlock(ref packet, guid, map, i);
                        break;
                    }
                    case "DestroyObjects":
                    {
                        CoreParsers.UpdateHandler.ReadObjectsBlock(ref packet, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(ref Packet packet, Guid guid, uint map, int index)
        {
            var objType = packet.ReadEnum<ObjectType>("Object Type", TypeCode.Byte, index);
            var moves = ReadMovementUpdateBlock(ref packet, guid, index);
            var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(ref packet, objType, index, true);

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

        private static MovementInfo ReadMovementUpdateBlock(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();

            var guid0 = new byte[8];
            var guid1 = new byte[8];
            var transportGuid = new byte[8];
            var goTransportGuid = new byte[8];
            var attackingTargetGuid = new byte[8];
            var guid5 = new byte[8];
            var guid6 = new byte[8];
            var guid7 = new byte[8];

            var hasGOTransportTime2 = false;
            var hasGOTransportTime3 = false;
            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;

            var bit28D = packet.ReadBit();
            var bits404 = packet.ReadBits(22);
            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            var bit3F0 = packet.ReadBit();
            var hasGameObjectPosition = packet.ReadBit("Has GameObject Rotation", index);
            guid0[3] = packet.ReadBit();
            var bit310 = packet.ReadBit();
            var bit1D8 = packet.ReadBit();
            var bit284 = packet.ReadBit();
            var bit208 = packet.ReadBit();
            var bit1F8 = packet.ReadBit();
            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            guid0[2] = packet.ReadBit();
            guid0[0] = packet.ReadBit();
            var isSelf = packet.ReadBit("Self", index);
            guid0[1] = packet.ReadBit();
            var living = packet.ReadBit("Living", index);
            var bit3E8 = packet.ReadBit();
            var bit28E = packet.ReadBit();
            var hasAnimKits = packet.ReadBit("Has AnimKits", index);
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);

            var bit90 = false;
            var hasMoveFlagsExtra = false;
            var bitD8 = false;
            var bit18 = false;
            var hasTimestamp = false;
            var bit95 = false;
            var bit94 = false;
            var hasOrientation = false;
            var hasTransportData = false;
            var hasTransportTime3 = false;
            var hasTransportTime2 = false;
            var hasPitch = false;
            var bitF0 = false;
            var bit118 = false;
            var bit134 = false;
            var bit110 = false;
            var bitA8 = false;
            var hasFallData = false;
            var hasFallDirection = false;
            var bit228 = false;
            var bit270 = false;
            var bit21C = false;
            var bit244 = false;

            var bits168 = 0u;
            var bits120 = 0u;
            var bits138 = 0u;
            var bits98 = 0u;
            var bits248 = 0u;
            var bits258 = 0u;
            var bits274 = 0u;
            var bits3F4 = 0u;
            var bits28F = 0u;
            var splineType = -1;

            uint[] bits168A;

            if (living)
            {
                guid1[0] = packet.ReadBit();
                bit90 = !packet.ReadBit();
                packet.StartBitStream(guid1, 4, 7);
                hasMoveFlagsExtra = !packet.ReadBit();
                packet.StartBitStream(guid1, 5, 2);
                bitD8 = packet.ReadBit();
                bit18 = !packet.ReadBit();
                hasTimestamp = !packet.ReadBit("Lacks timestamp", index);
                bit95 = packet.ReadBit();
                bit94 = packet.ReadBit();
                hasOrientation = !packet.ReadBit();
                if (bit18)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                hasTransportData = packet.ReadBit("Has Transport Data", index);
                if (hasTransportData)
                {
                    packet.StartBitStream(transportGuid, 1, 0, 6);
                    hasTransportTime3 = packet.ReadBit();
                    packet.StartBitStream(transportGuid, 2, 7, 4, 3);
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[5] = packet.ReadBit();
                }

                hasPitch = !packet.ReadBit("Lacks pitch", index);
                guid1[6] = packet.ReadBit();
                bits168 = packet.ReadBits(19);
                bits168A = new uint[bits168];
                for (var i = 0; i < bits168; ++i)
                    bits168A[i] = packet.ReadBits("bits168", 2, index);

                guid1[1] = packet.ReadBit();
                if (bitD8)
                {
                    bitF0 = packet.ReadBit();
                    if (bitF0)
                    {
                        bit118 = packet.ReadBit();
                        bit134 = packet.ReadBit();
                        packet.ReadBits("bits130", 2, index);
                        bits120 = packet.ReadBits(20);
                        bit110 = packet.ReadBit();
                        if (bit134)
                        {
                            bits138 = packet.ReadBits(21);
                            packet.ReadBits("bits148", 2, index);
                        }
                        packet.ReadEnum<SplineFlag434>("Spline flags", 25, index);
                    }
                }

                bitA8 = !packet.ReadBit();
                guid1[3] = packet.ReadBit();
                bits98 = packet.ReadBits(22);
                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13, index);

                hasFallData = packet.ReadBit("Has Fall Data", index);
                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                packet.ReadBit("bitAC", index);
            }

            if (hasGameObjectPosition)
            {
                packet.StartBitStream(goTransportGuid, 7, 3);
                hasGOTransportTime2 = packet.ReadBit();
                packet.StartBitStream(goTransportGuid, 1, 4, 0, 2, 6);
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[5] = packet.ReadBit();
            }

            if (hasAnimKits)
            {
                hasAnimKit2 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.StartBitStream(7, 3, 6, 1, 5, 4, 0, 2);

            if (bit208)
            {
                packet.ReadBit("bit21A", index);
                bit228 = packet.ReadBit();
                bit270 = packet.ReadBit();
                packet.ReadBit("bit219", index);
                packet.ReadBit("bit218", index);
                bit21C = packet.ReadBit();
                bit244 = packet.ReadBit();

                if (bit244)
                {
                    bits248 = packet.ReadBits(21);
                    bits258 = packet.ReadBits(21);
                }

                if (bit270)
                    bits274 = packet.ReadBits(20);

                packet.ReadBit("bit21B", index);
            }

            if (bit310)
                packet.WriteLine("Missing data for bit310");

            if (bit3F0)
                bits3F4 = packet.ReadBits(22);

            if (bit28E)
                bits28F = packet.ReadBits(7);

            packet.ResetBitReader();

            for (var i = 0; i < bits404; ++i)
                packet.ReadInt32("Int408", index);

            if (living)
            {
                if (hasTimestamp)
                    packet.ReadUInt32("Time", index);

                for (var i = 0; i < bits168; ++i)
                {
                    packet.ReadSingle("Float16C+5", index);
                    packet.ReadInt32("Int16C+4", index);
                    packet.ReadSingle("Float16C+1", index);
                    packet.ReadInt32("Int16C+0", index);
                    packet.ReadSingle("Float16C+2", index);
                    packet.ReadSingle("Float16C+3", index);
                }

                if (bitD8)
                {
                    if (bitF0)
                    {
                        if (bit118)
                            packet.ReadInt32("Int11C", index);

                        packet.ReadSingle("Float108", index);
                        if (bit110)
                            packet.ReadSingle("Float114", index);

                        packet.ReadInt32("Int100", index);
                        for (var i = 0u; i < bits120; ++i)
                        {
                            var wp = new Vector3
                            {
                                Z = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                            };

                            packet.WriteLine("[{0}][{1}] Spline Waypoint: {2}", index, i, wp);
                        }

                        if (bit134)
                        {
                            for (var i = 0; i < bits138; ++i)
                            {
                                packet.ReadSingle("Float13C+1", index, i);
                                packet.ReadSingle("Float13C+0", index, i);
                            }
                        }

                        packet.ReadSingle("Float10C", index);
                        splineType = packet.ReadByte("ByteFC", index);
                        if (splineType == 4)
                            packet.ReadSingle("Facing Angle", index);

                        if (splineType == 2)
                        {
                            var point = new Vector3
                            {
                                X = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                            };

                            packet.WriteLine("[{0}] Facing Spot: {1}", index, point);
                        }

                        packet.ReadInt32("Int104", index);
                    }

                    moveInfo.Position.Y = packet.ReadSingle();
                    moveInfo.Position.Z = packet.ReadSingle();
                    packet.ReadInt32("IntE0", index);
                    moveInfo.Position.X = packet.ReadSingle();
                }

                if (hasTransportData)
                {
                    packet.ReadXORByte(transportGuid, 4);
                    moveInfo.TransportOffset.Z = packet.ReadSingle();
                    moveInfo.TransportOffset.Y = packet.ReadSingle();
                    packet.ReadUInt32("Transport Time", index);
                    packet.ReadByte("Transport Seat", index);
                    packet.ReadXORBytes(transportGuid, 3, 1, 6);
                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);

                    packet.ReadXORByte(transportGuid, 5);
                    moveInfo.TransportOffset.O = packet.ReadSingle();
                    moveInfo.TransportOffset.X = packet.ReadSingle();
                    packet.ReadXORBytes(transportGuid, 2, 0, 7);
                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);

                    moveInfo.TransportGuid = new Guid(BitConverter.ToUInt64(transportGuid, 0));
                    packet.WriteLine("[{0}] Transport GUID {1}", index, moveInfo.TransportGuid);
                    packet.WriteLine("[{0}] Transport Position: {1}", index, moveInfo.TransportOffset);
                }

                packet.ReadXORBytes(guid1, 2, 1);
                packet.ReadSingle("SwimBack Speed", index);
                packet.ReadSingle("Turn Speed", index);
                packet.ReadXORBytes(guid1, 0, 3);
                packet.ReadSingle("RunBack Speed", index);
                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Float88", index);
                        packet.ReadSingle("Float8C", index);
                        packet.ReadSingle("Float84", index);
                    }
                    packet.ReadSingle("Fall Start Velocity", index);
                    packet.ReadUInt32("Time Fallen", index);
                }

                packet.ReadSingle("FlyBack Speed", index);
                packet.ReadXORByte(guid1, 5);
                moveInfo.Position.Z = packet.ReadSingle();
                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                packet.ReadXORByte(guid1, 6);
                if (bit90)
                    packet.ReadSingle("Float90", index);

                packet.ReadSingle("Pitch Speed", index);
                if (hasPitch)
                    packet.ReadSingle("Pitch", index);

                for (var i = 0; i < bits98; ++i)
                    packet.ReadInt32("Int9C", index, i);

                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                if (bitA8)
                    packet.ReadInt32("IntA8", index);

                moveInfo.Position.Y = packet.ReadSingle();
                packet.ReadSingle("Swim Speed", index);
                packet.ReadSingle("Fly Speed", index);
                packet.ReadXORByte(guid1, 7);
                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                moveInfo.Position.X = packet.ReadSingle();
                packet.ReadXORByte(guid1, 4);

                packet.WriteGuid("GUID1", guid1, index);
                packet.WriteLine("[{0}] Position: {1}", index, moveInfo.Position);
                packet.WriteLine("[{0}] Orientation: {1}", index, moveInfo.Orientation);
            }

            if (bit310)
            {
                packet.WriteLine("Missing data for bit310", index);
            }

            if (hasGameObjectPosition)
            {
                packet.ReadUInt32("GO Transport Time", index);
                packet.ReadXORByte(goTransportGuid, 7);
                moveInfo.TransportOffset.Y = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 0);
                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                packet.ReadXORByte(goTransportGuid, 3);
                packet.ReadSByte("GO Transport Seat", index);
                packet.ReadXORByte(goTransportGuid, 1);
                moveInfo.TransportOffset.Z = packet.ReadSingle();
                moveInfo.TransportOffset.O = packet.ReadSingle();
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);

                moveInfo.TransportOffset.X = packet.ReadSingle();
                packet.ReadXORBytes(goTransportGuid, 2, 4, 5, 6);

                moveInfo.TransportGuid = new Guid(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.WriteLine("[{0}] GO Transport GUID {1}", index, moveInfo.TransportGuid);
                packet.WriteLine("[{0}] GO Transport Position: {1}", index, moveInfo.TransportOffset);
            }

            if (bit208)
            {
                if (bit21C)
                {
                    packet.ReadSingle("Float224", index);
                    packet.ReadSingle("Float220", index);
                }

                packet.ReadSingle("Float210", index);
                if (bit244)
                {
                    for (var i = 0; i < bits258; ++i)
                    {
                        packet.ReadSingle("Float25C+1", index);
                        packet.ReadSingle("Float25C+0", index);
                    }

                    for (var i = 0; i < bits248; ++i)
                    {
                        packet.ReadSingle("Float24C+0", index);
                        packet.ReadSingle("Float24C+1", index);
                    }

                    packet.ReadSingle("Float26C", index);
                    packet.ReadSingle("Float268", index);
                }

                if (bit228)
                {
                    packet.ReadSingle("Float22C", index);
                    packet.ReadSingle("Float230", index);
                    packet.ReadSingle("Float23C", index);
                    packet.ReadSingle("Float234", index);
                    packet.ReadSingle("Float238", index);
                    packet.ReadSingle("Float240", index);
                }

                if (bit270)
                {
                    for (var i = 0; i < bits274; ++i)
                    {
                        packet.ReadSingle("Float277+1", index);
                        packet.ReadSingle("Float277+0", index);
                        packet.ReadSingle("Float277+2", index);
                    }
                }

                packet.ReadSingle("Float214", index);
                packet.ReadInt32("Int20C", index);
            }

            if (hasVehicleData)
            {
                packet.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 7, 1, 4, 6, 0, 2, 5, 3);
                packet.WriteGuid("Attacking GUID", attackingTargetGuid, index);
            }

            if (bit1F8)
                packet.ReadGuid("GUIDX", index);

            if (hasAnimKits)
            {
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
            }

            if (bit28E)
            {
                var bytes = packet.ReadBytes((int)bits28F);
                packet.WriteLine("Bytes", bytes.ToString(), index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position.X = packet.ReadSingle();
                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                moveInfo.Position.Y = packet.ReadSingle();
                packet.WriteLine("[{0}] Stationary Position: {1}", index, moveInfo.Position);
            }

            if (bit1D8)
                packet.ReadInt32("Int1DC", index);

            if (bit3F0)
                for (var i = 0; i < bits3F4; ++i)
                    packet.ReadInt32("Int1DC", index, i);

            if (bit284)
                packet.ReadInt32("Int288", index);

            if (bit3E8)
                packet.ReadInt32("Int3EC", index);

            if (living && bitD8 && bitF0 && splineType == 3)
            {
                var guid8 = new byte[8];
                guid8 = packet.StartBitStream(2, 4, 6, 3, 1, 5, 6, 0);
                packet.ParseBitStream(guid8, 1, 3, 6, 7, 2, 4, 5, 0);
                packet.WriteGuid("Guid8 GUID", guid8, index);
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = new byte[8];
            guid[4] = packet.ReadBit();
            packet.ReadBit("Despawn Animation");
            packet.StartBitStream(guid, 0, 1, 6, 2, 5, 7, 3);
            packet.ParseBitStream(guid, 7, 1, 2, 5, 0, 3, 6, 4);
            packet.WriteGuid("GUID", guid);
        }
    }
}
