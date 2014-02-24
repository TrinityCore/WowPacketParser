using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_ATTACKSTART)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var AttackerGUID = new byte[8];
            var VictimGUID = new byte[8];

            AttackerGUID[4] = packet.ReadBit();
            VictimGUID[4] = packet.ReadBit();
            AttackerGUID[6] = packet.ReadBit();
            AttackerGUID[3] = packet.ReadBit();
            AttackerGUID[5] = packet.ReadBit();
            VictimGUID[2] = packet.ReadBit();
            VictimGUID[6] = packet.ReadBit();
            AttackerGUID[0] = packet.ReadBit();
            VictimGUID[5] = packet.ReadBit();
            VictimGUID[0] = packet.ReadBit();
            VictimGUID[3] = packet.ReadBit();
            AttackerGUID[2] = packet.ReadBit();
            VictimGUID[1] = packet.ReadBit();
            VictimGUID[7] = packet.ReadBit();
            AttackerGUID[7] = packet.ReadBit();
            AttackerGUID[1] = packet.ReadBit();

            packet.ReadXORByte(VictimGUID, 5);
            packet.ReadXORByte(VictimGUID, 7);
            packet.ReadXORByte(AttackerGUID, 4);
            packet.ReadXORByte(AttackerGUID, 5);
            packet.ReadXORByte(AttackerGUID, 3);
            packet.ReadXORByte(VictimGUID, 1);
            packet.ReadXORByte(AttackerGUID, 1);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(AttackerGUID, 7);
            packet.ReadXORByte(VictimGUID, 0);
            packet.ReadXORByte(VictimGUID, 2);
            packet.ReadXORByte(VictimGUID, 4);
            packet.ReadXORByte(AttackerGUID, 6);
            packet.ReadXORByte(AttackerGUID, 2);
            packet.ReadXORByte(VictimGUID, 6);
            packet.ReadXORByte(AttackerGUID, 0);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACKSTOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var VictimGUID = new byte[8];
            var AttackerGUID = new byte[8];

            VictimGUID[5] = packet.ReadBit();
            AttackerGUID[1] = packet.ReadBit();
            AttackerGUID[2] = packet.ReadBit();
            VictimGUID[7] = packet.ReadBit();
            AttackerGUID[3] = packet.ReadBit();
            AttackerGUID[7] = packet.ReadBit();
            AttackerGUID[6] = packet.ReadBit();

            packet.ReadBit("Unk bit");

            VictimGUID[0] = packet.ReadBit();
            VictimGUID[2] = packet.ReadBit();
            VictimGUID[3] = packet.ReadBit();
            VictimGUID[6] = packet.ReadBit();
            VictimGUID[4] = packet.ReadBit();
            AttackerGUID[0] = packet.ReadBit();
            VictimGUID[1] = packet.ReadBit();
            AttackerGUID[4] = packet.ReadBit();
            AttackerGUID[5] = packet.ReadBit();

            packet.ReadXORByte(AttackerGUID, 2);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(VictimGUID, 5);
            packet.ReadXORByte(VictimGUID, 2);
            packet.ReadXORByte(AttackerGUID, 4);
            packet.ReadXORByte(AttackerGUID, 1);
            packet.ReadXORByte(AttackerGUID, 0);
            packet.ReadXORByte(VictimGUID, 1);
            packet.ReadXORByte(AttackerGUID, 3);
            packet.ReadXORByte(AttackerGUID, 7);
            packet.ReadXORByte(AttackerGUID, 6);
            packet.ReadXORByte(VictimGUID, 4);
            packet.ReadXORByte(VictimGUID, 6);
            packet.ReadXORByte(AttackerGUID, 5);
            packet.ReadXORByte(VictimGUID, 7);
            packet.ReadXORByte(VictimGUID, 0);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }
    }
}
