using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var victimGUID = new byte[8];
            var attackerGUID = new byte[8];

            packet.StartBitStream(victimGUID, 6, 2, 1);
            attackerGUID[6] = packet.ReadBit();
            victimGUID[5] = packet.ReadBit();
            packet.StartBitStream(attackerGUID, 1, 3, 0);
            packet.StartBitStream(victimGUID, 0, 7);
            packet.StartBitStream(attackerGUID, 7, 5, 4);
            packet.StartBitStream(victimGUID, 4, 3);
            attackerGUID[2] = packet.ReadBit();

            packet.ReadXORByte(attackerGUID, 4);
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORBytes(attackerGUID, 2, 6);
            packet.ReadXORByte(victimGUID, 7);
            packet.ReadXORByte(attackerGUID, 0);
            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(attackerGUID, 1);
            packet.ReadXORBytes(victimGUID, 2, 6);
            packet.ReadXORByte(attackerGUID, 7);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(attackerGUID, 5);
            packet.ReadXORByte(victimGUID, 0);
            packet.ReadXORByte(attackerGUID, 3);
            packet.ReadXORByte(victimGUID, 1);

            packet.WriteGuid("Attacker GUID", attackerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var victimGUID = new byte[8];
            var attackerGUID = new byte[8];

            victimGUID[4] = packet.ReadBit();
            packet.StartBitStream(attackerGUID, 1, 3, 0, 6, 5);
            victimGUID[1] = packet.ReadBit();
            attackerGUID[7] = packet.ReadBit();
            packet.StartBitStream(victimGUID, 5, 6, 0);
            packet.StartBitStream(attackerGUID, 2, 4);
            packet.StartBitStream(victimGUID, 7, 2);
            packet.ReadBit("Unk bit");
            victimGUID[3] = packet.ReadBit();

            packet.ReadXORBytes(victimGUID, 2, 0);
            packet.ReadXORBytes(attackerGUID, 5, 0, 4);
            packet.ReadXORBytes(victimGUID, 4, 6, 7);
            packet.ReadXORBytes(attackerGUID, 2, 3);
            packet.ReadXORBytes(victimGUID, 5, 1);
            packet.ReadXORBytes(attackerGUID, 1, 7);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(attackerGUID, 6);

            packet.WriteGuid("Attacker GUID", attackerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }
    }
}
