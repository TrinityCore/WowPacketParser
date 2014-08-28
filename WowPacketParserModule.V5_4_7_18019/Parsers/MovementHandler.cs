using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.WowGuid;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.CMSG_MOUNTSPECIAL_ANIM)]
        public static void HandleMountSpecialAnim(Packet packet)
        {
        }

        [Parser(Opcode.MSG_MOVE_SET_FACING)]
        public static void HandleMoveSetFacing434(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_CLIENT_CONTROL_UPDATE)]
        public static void HandleClientControlUpdate(Packet packet)
        {
            packet.ReadBit("AllowMove");
            var guid = packet.StartBitStream(7, 1, 6, 3, 2, 4, 5, 0);
            packet.ParseBitStream(guid, 0, 5, 3, 2, 4, 7, 6, 1);

            packet.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            packet.ReadSingle("Orientation");
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntry<Int32>(StoreNameType.Map, "Map");
            packet.ReadSingle("Z");
            packet.ReadSingle("X");
            packet.ReadSingle("Y");

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_MOVE_SET_CAN_FLY)]
        public static void HandleSetCanFly(Packet packet)
        {
            var guid = packet.StartBitStream(4, 2, 3, 0, 5, 1, 7, 6);
            packet.ParseBitStream(guid, 0, 5);
            packet.ReadInt32("Counter");
            packet.ParseBitStream(guid, 2, 1, 6, 3, 4, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_FLIGHT_SPEED)]
        public static void HandleMoveSetFlightSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(2, 1, 6, 5, 7, 3, 0, 4);
            packet.ParseBitStream(guid, 7, 4, 3);
            packet.ReadInt32("Counter");
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 6, 2, 0, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_RUN_SPEED)]
        public static void HandleMoveSetRunSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 2, 0, 4, 3, 7, 6);
            packet.ParseBitStream(guid, 3, 2, 6, 0);
            packet.ReadInt32("Counter");
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 7, 4, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_SWIM_SPEED)]
        public static void HandleMoveSetSwimSpeed(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadSingle("Speed");
                packet.ReadInt32("Counter");
                var guid = packet.StartBitStream(4, 7, 6, 3, 5, 2, 0, 1);
                packet.ParseBitStream(guid, 1, 6, 5, 2, 0, 3, 4, 7);
                packet.WriteGuid("Guid", guid);
            }
            else
            {
                packet.WriteLine("              : CMSG_INSPECT");
                packet.Opcode = (int)Opcode.CMSG_INSPECT;
                var guid = packet.StartBitStream(5, 0, 7, 4, 6, 2, 1, 3);
                packet.ParseBitStream(guid, 5, 6, 3, 4, 0, 1, 7, 2);

                packet.WriteGuid("Guid", guid);
            }
        }

        [Parser(Opcode.SMSG_MOVE_SET_WALK_SPEED)]
        public static void HandleMovementSetWalkSpeed(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 6, 3, 5, 4, 7, 2);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 6, 1, 5);
            packet.ReadInt32("Counter");
            packet.ParseBitStream(guid, 2, 4, 3, 0, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_FLY)]
        public static void HandleUnSetCanFly(Packet packet)
        {
            var guid = packet.StartBitStream(5, 3, 2, 4, 7, 1, 0, 6);
            packet.ParseBitStream(guid, 4, 5);
            packet.ReadInt32("Counter");
            packet.ParseBitStream(guid, 1, 3, 7, 2, 6, 0);
            packet.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntry<Int32>(StoreNameType.Map, "Map");
            packet.ReadSingle("Y");
            packet.ReadSingle("Z");
            packet.ReadSingle("Orientation");
            packet.ReadSingle("X");

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_PLAYER_MOVE)]
        public static void HandlePlayerMove(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPLINE_MOVE_SET_PITCH_RATE)] //??? maybe other name
        public static void HandleSplineMovementSetPitchRate(Packet packet)
        {
            var guid = packet.StartBitStream(0, 7, 4, 1, 5, 2, 3, 6); // only 3 - OK
            packet.ReadXORByte(guid, 3);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 2, 5, 7, 0, 6);
            packet.ReadInt32("Counter");
            packet.ParseBitStream(guid, 1, 4);  // all parse OK
            packet.WriteGuid("Guid", guid);
        }
    }
}
