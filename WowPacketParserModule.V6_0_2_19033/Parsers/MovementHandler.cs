using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            var pos = new Vector4();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntry<Int32>(StoreNameType.Map, "Map");
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.O = packet.ReadSingle();
            packet.ReadUInt32("Reason");

            packet.AddValue("Position", pos);
            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadInt32("Time");
        }

        [Parser(Opcode.SMSG_PLAYER_MOVE)]
        public static void HandlePlayerMove(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            packet.ReadUInt32("Id");
            packet.ReadVector4("Position");

            packet.ReadSingle("Pitch");
            packet.ReadSingle("Spline Elevation");

            var int152 = packet.ReadInt32("Int152");
            packet.ReadInt32("Int168");

            for (var i = 0; i < int152; i++)
                packet.ReadPackedGuid128("Guid156");

            packet.ReadEnum<MovementFlag>("Movement Flags", 30);
            packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 15);

            var bit104 = packet.ReadBit("Has Transport Data");
            var bit140 = packet.ReadBit("Has Fall Data");
            packet.ReadBit("bit148");
            packet.ReadBit("bit149");

            if (bit104)
            {
                packet.ReadPackedGuid128("Transport Guid");
                packet.ReadVector4("Transport Position");
                packet.ReadSByte("Transport Seat");
                packet.ReadInt32("Transport Time");

                packet.ResetBitReader();
                var bit44 = packet.ReadBit("Has Transport Time 2");
                var bit52 = packet.ReadBit("Has Transport Time 3");
                if (bit44)
                    packet.ReadUInt32("Transport Time 2");

                if (bit52)
                    packet.ReadUInt32("Transport Time 3");
            }

            if (bit140)
            {
                packet.ReadUInt32("Fall Time");
                packet.ReadSingle("Vertical Speed");

                packet.ResetBitReader();
                var bit20 = packet.ReadBit("Has Fall Direction");
                if (bit20)
                {
                    packet.ReadVector2("Fall");
                    packet.ReadSingle("Horizontal Speed");
                }
            }
        }
    }
}
