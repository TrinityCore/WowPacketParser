using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var attackerGUID = new byte[8];
            var victimGUID = new byte[8];

            attackerGUID[4] = packet.ReadBit();
            victimGUID[4] = packet.ReadBit();
            attackerGUID[6] = packet.ReadBit();
            attackerGUID[3] = packet.ReadBit();
            attackerGUID[5] = packet.ReadBit();
            victimGUID[2] = packet.ReadBit();
            victimGUID[6] = packet.ReadBit();
            attackerGUID[0] = packet.ReadBit();
            victimGUID[5] = packet.ReadBit();
            victimGUID[0] = packet.ReadBit();
            victimGUID[3] = packet.ReadBit();
            attackerGUID[2] = packet.ReadBit();
            victimGUID[1] = packet.ReadBit();
            victimGUID[7] = packet.ReadBit();
            attackerGUID[7] = packet.ReadBit();
            attackerGUID[1] = packet.ReadBit();

            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(victimGUID, 7);
            packet.ReadXORByte(attackerGUID, 4);
            packet.ReadXORByte(attackerGUID, 5);
            packet.ReadXORByte(attackerGUID, 3);
            packet.ReadXORByte(victimGUID, 1);
            packet.ReadXORByte(attackerGUID, 1);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(attackerGUID, 7);
            packet.ReadXORByte(victimGUID, 0);
            packet.ReadXORByte(victimGUID, 2);
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORByte(attackerGUID, 6);
            packet.ReadXORByte(attackerGUID, 2);
            packet.ReadXORByte(victimGUID, 6);
            packet.ReadXORByte(attackerGUID, 0);

            packet.WriteGuid("Attacker GUID", attackerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var victimGUID = new byte[8];
            var attackerGUID = new byte[8];

            victimGUID[5] = packet.ReadBit();
            attackerGUID[1] = packet.ReadBit();
            attackerGUID[2] = packet.ReadBit();
            victimGUID[7] = packet.ReadBit();
            attackerGUID[3] = packet.ReadBit();
            attackerGUID[7] = packet.ReadBit();
            attackerGUID[6] = packet.ReadBit();

            packet.ReadBit("Unk bit");

            victimGUID[0] = packet.ReadBit();
            victimGUID[2] = packet.ReadBit();
            victimGUID[3] = packet.ReadBit();
            victimGUID[6] = packet.ReadBit();
            victimGUID[4] = packet.ReadBit();
            attackerGUID[0] = packet.ReadBit();
            victimGUID[1] = packet.ReadBit();
            attackerGUID[4] = packet.ReadBit();
            attackerGUID[5] = packet.ReadBit();

            packet.ReadXORByte(attackerGUID, 2);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(victimGUID, 2);
            packet.ReadXORByte(attackerGUID, 4);
            packet.ReadXORByte(attackerGUID, 1);
            packet.ReadXORByte(attackerGUID, 0);
            packet.ReadXORByte(victimGUID, 1);
            packet.ReadXORByte(attackerGUID, 3);
            packet.ReadXORByte(attackerGUID, 7);
            packet.ReadXORByte(attackerGUID, 6);
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORByte(victimGUID, 6);
            packet.ReadXORByte(attackerGUID, 5);
            packet.ReadXORByte(victimGUID, 7);
            packet.ReadXORByte(victimGUID, 0);

            packet.WriteGuid("Attacker GUID", attackerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 5, 7, 0, 4, 6, 3, 2);
            packet.ParseBitStream(guid, 1, 2, 5, 7, 0, 3, 6, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 3, 0, 1, 4, 2, 7, 5);
            packet.ParseBitStream(guid, 4, 6, 3, 1, 2, 0, 7, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PARTY_KILL_LOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            var playerGUID = new byte[8];
            var victimGUID = new byte[8];

            playerGUID[4] = packet.ReadBit();
            victimGUID[2] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            victimGUID[1] = packet.ReadBit();
            victimGUID[6] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            victimGUID[7] = packet.ReadBit();
            victimGUID[3] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            victimGUID[0] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            victimGUID[5] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            victimGUID[4] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(victimGUID, 0);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadXORByte(victimGUID, 2);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(victimGUID, 6);
            packet.ReadXORByte(victimGUID, 7);
            packet.ReadXORByte(victimGUID, 1);
            packet.ReadXORByte(playerGUID, 2);

            packet.WriteGuid("Player GUID", playerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        public static void HandleCanelCombat(Packet packet)
        {
            packet.ReadBits("bits10", 2);
        }
    }
}
