using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellHitInfo = WowPacketParser.Enums.SpellHitInfo;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.CMSG_ATTACKSWING, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_ATTACKSWING)]
        public static void HandleAttackSwing547(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 7, 0, 4, 6, 3, 2);
            packet.ParseBitStream(guid, 1, 2, 5, 7, 0, 3, 6, 4);

            packet.WriteGuid("Target Guid", guid);
        }

        [Parser(Opcode.SMSG_ATTACKSTART, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_ATTACKSTART)]
        public static void HandleAttackStart547(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[4] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[1] = packet.ReadBit();

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 0);

            packet.WriteGuid("Attacker Guid", guid1);
            packet.WriteGuid("Victim Guid", guid2);
        }

        [Parser(Opcode.SMSG_ATTACKSTOP, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_ATTACKSTOP)]
        public static void HandleAttackStop547(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[6] = packet.ReadBit();

            packet.ReadBit("Unk 1");

            guid2[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[5] = packet.ReadBit();

            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 0);

            packet.WriteGuid("Attacker Guid", guid1);
            packet.WriteGuid("Victim Guid", guid2);
        }

        [Parser(Opcode.SMSG_CANCEL_COMBAT, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        public static void HandleCancelCombat547(Packet packet)
        {
            var guid = packet.StartBitStream(3, 1, 0, 7, 6, 4, 2, 5);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);

            packet.ReadByte("Unk 1");

            packet.ReadXORByte(guid, 7);

            packet.ReadByte("Unk 2");

            packet.ReadXORByte(guid, 1);

            packet.ReadByte("Unk 3");

            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Target Guid", guid);
        }
    }
}
