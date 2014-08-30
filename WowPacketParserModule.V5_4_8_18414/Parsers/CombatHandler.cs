using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellHitInfo = WowPacketParser.Enums.SpellHitInfo;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.CMSG_ATTACKSWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_TOGGLE_PVP)]
        public static void HandleCTogglePvp(Packet packet)
        {
            packet.ReadBit("Value");
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleSAIReaction(Packet packet)
        {
            var guid = packet.StartBitStream(5, 7, 0, 4, 6, 2, 3, 1);
            packet.ParseBitStream(guid, 4, 6, 5);
            packet.ReadEnum<AIReaction>("Reaction", TypeCode.Int32);
            packet.ParseBitStream(guid, 7, 1, 2, 0, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = packet.StartBitStream(1, 3, 0, 4, 6, 7, 5, 2);
            packet.ParseBitStream(guid, 7, 6, 2, 5, 0, 4, 1, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_DUEL_INBOUNDS)]
        public static void HandleDuelInbounds(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_DUEL_REQUESTED)]
        public static void HandleDuelRequested(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[5] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 5);

            packet.WriteGuid("Flag GUID", guid1);
            packet.WriteGuid("Opponent GUID", guid2);
        }

        [Parser(Opcode.SMSG_DUEL_WINNER)]
        public static void HandleDuelWinner(Packet packet)
        {
            packet.ReadBit("Abnormal finish");
            var unk24 = packet.ReadBits("unk24", 6);
            var unk73 = packet.ReadBits("unk73", 6);

            packet.ReadInt32("unk20");
            packet.ReadWoWString("str1", unk24);
            packet.ReadInt32("unk16");
            packet.ReadWoWString("str2", unk73);
        }

        [Parser(Opcode.SMSG_UPDATE_COMBO_POINTS)]
        public static void HandleUpdateComboPoints(Packet packet)
        {
            var guid = packet.StartBitStream(0, 5, 6, 3, 7, 4, 1, 2);
            packet.ParseBitStream(guid, 5, 6, 4, 7, 3, 0);
            packet.ReadByte("Combo Points");
            packet.ParseBitStream(guid, 2, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ATTACKERSTATEUPDATE)]
        [Parser(Opcode.SMSG_ATTACKSTART)]
        [Parser(Opcode.SMSG_ATTACKSTOP)]
        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
