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

        [Parser(Opcode.SMSG_PERIODICAURALOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            targetGUID[6] = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            casterGUID[3] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();

            var bits24 = packet.ReadBits(21);
            
            var hasAbsorb = new bool[bits24];
            var hasSpellProto = new bool[bits24];
            var hasOverDamage = new bool[bits24];
            var bit14 = new bool[bits24];

            for (var i = 0; i < bits24; ++i)
            {
                packet.ReadBit("Unk bit", i);
                hasAbsorb[i] = !packet.ReadBit();
                hasSpellProto[i] = !packet.ReadBit();
                hasOverDamage[i] = !packet.ReadBit();
                bit14[i] = !packet.ReadBit();
            }

            targetGUID[7] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();

            var bits40 = 0u;
            if (hasPowerData)
                bits40 = packet.ReadBits(21);

            casterGUID[0] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            packet.ReadXORByte(targetGUID, 3);
            for (var i = 0; i < bits24; ++i)
            {
                packet.ReadEnum<AuraType>("Aura Type", TypeCode.UInt32, i);
                if (hasSpellProto[i])
                    packet.ReadUInt32("Spell Proto", i);
                packet.ReadInt32("Damage", i);
                if (hasOverDamage[i])
                    packet.ReadInt32("Over damage", i);
                if (hasAbsorb[i])
                    packet.ReadInt32("Absorb", i);
                if (bit14[i])
                    packet.ReadInt32("xxxx 2");
            }

            packet.ReadXORByte(casterGUID, 4);
            if (hasPowerData)
            {
                packet.ReadInt32("Int34");

                for (var i = 0; i < bits24; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int3C");
                packet.ReadInt32("Int38");
            }

            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(targetGUID, 6);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }
    }
}
