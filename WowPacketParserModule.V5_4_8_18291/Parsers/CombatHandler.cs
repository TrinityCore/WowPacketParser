using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var attackerGUID = new byte[8];
            var victimGUID = new byte[8];

            victimGUID[7] = packet.ReadBit();
            attackerGUID[7] = packet.ReadBit();
            attackerGUID[3] = packet.ReadBit();
            victimGUID[3] = packet.ReadBit();
            victimGUID[5] = packet.ReadBit();
            attackerGUID[4] = packet.ReadBit();
            attackerGUID[1] = packet.ReadBit();
            victimGUID[4] = packet.ReadBit();
            attackerGUID[0] = packet.ReadBit();
            victimGUID[6] = packet.ReadBit();
            attackerGUID[5] = packet.ReadBit();
            victimGUID[2] = packet.ReadBit();
            attackerGUID[6] = packet.ReadBit();
            victimGUID[1] = packet.ReadBit();
            attackerGUID[2] = packet.ReadBit();
            victimGUID[0] = packet.ReadBit();
            packet.ReadXORByte(attackerGUID, 5);
            packet.ReadXORByte(attackerGUID, 0);
            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(attackerGUID, 4);
            packet.ReadXORByte(attackerGUID, 6);
            packet.ReadXORByte(victimGUID, 6);
            packet.ReadXORByte(victimGUID, 1);
            packet.ReadXORByte(victimGUID, 0);
            packet.ReadXORByte(attackerGUID, 7);
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORByte(attackerGUID, 2);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(victimGUID, 7);
            packet.ReadXORByte(victimGUID, 2);
            packet.ReadXORByte(attackerGUID, 3);
            packet.ReadXORByte(attackerGUID, 1);

            packet.WriteGuid("Attacker GUID", attackerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var victimGUID = new byte[8];
            var attackerGUID = new byte[8];

            victimGUID[5] = packet.ReadBit();
            victimGUID[6] = packet.ReadBit();
            attackerGUID[3] = packet.ReadBit();
            attackerGUID[6] = packet.ReadBit();
            attackerGUID[7] = packet.ReadBit();
            attackerGUID[2] = packet.ReadBit();
            attackerGUID[5] = packet.ReadBit();
            victimGUID[4] = packet.ReadBit();
            packet.ReadBit("Unk Bit");
            victimGUID[3] = packet.ReadBit();
            victimGUID[0] = packet.ReadBit();
            victimGUID[2] = packet.ReadBit();
            victimGUID[7] = packet.ReadBit();
            attackerGUID[4] = packet.ReadBit();
            attackerGUID[1] = packet.ReadBit();
            attackerGUID[0] = packet.ReadBit();
            victimGUID[1] = packet.ReadBit();
            packet.ReadXORByte(victimGUID, 0);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(victimGUID, 2);
            packet.ReadXORByte(attackerGUID, 0);
            packet.ReadXORByte(attackerGUID, 6);
            packet.ReadXORByte(attackerGUID, 3);
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORByte(attackerGUID, 1);
            packet.ReadXORByte(attackerGUID, 4);
            packet.ReadXORByte(victimGUID, 6);
            packet.ReadXORByte(attackerGUID, 5);
            packet.ReadXORByte(attackerGUID, 7);
            packet.ReadXORByte(attackerGUID, 2);
            packet.ReadXORByte(victimGUID, 1);
            packet.ReadXORByte(victimGUID, 7);

            packet.WriteGuid("Attacker GUID", attackerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 5, 7, 0, 3, 1, 4, 2);
            packet.ParseBitStream(guid, 6, 7, 1, 3, 2, 0, 4, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            var guid = new byte[8];

            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var bit30 = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            if (bit30)
            {
                var bits20 = packet.ReadBits(21);

                packet.ReadInt32("Int14");
                for (var i = 0; i < bits20; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int1C");
                packet.ReadInt32("Int18");
            }

            packet.ReadInt32("Int3C");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadByteE<EnvironmentDamage>("Type");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Int38");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Damage");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 3, 0, 4, 6, 7, 5, 2);
            packet.ParseBitStream(guid, 7, 6, 2, 5, 0, 4, 1, 3);

            packet.WriteGuid("Guid", guid);
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

        [Parser(Opcode.CMSG_DUEL_PROPOSED)]
        public static void HandleDuelProposed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 5, 4, 6, 3, 2, 7, 0);
            packet.ParseBitStream(guid, 4, 2, 5, 7, 1, 3, 6, 0);

            packet.WriteGuid("Opponent GUID", guid);
        }
    }
}
