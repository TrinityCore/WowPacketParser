using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17659.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            var bit30 = false;
            var bit34 = false;
            var bit38 = false;
            var bit3C = false;
            var bit40 = false;
            var bit44 = false;
            var bit48 = false;
            var bit4C = false;
            var bit50 = false;
            var bit54 = false;

            casterGUID[4] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();
            targetGUID[4] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            targetGUID[7] = packet.Translator.ReadBit();
            var hasPawerData = packet.Translator.ReadBit();
            var bit20 = packet.Translator.ReadBit();
            var bit90 = packet.Translator.ReadBit();

            var hasDebugOutput = packet.Translator.ReadBit("Has Debug Output");
            if (hasDebugOutput)
            {
                bit30 = !packet.Translator.ReadBit();
                bit38 = !packet.Translator.ReadBit();
                bit44 = !packet.Translator.ReadBit();
                bit34 = !packet.Translator.ReadBit();
                bit48 = !packet.Translator.ReadBit();
                bit3C = !packet.Translator.ReadBit();
                bit54 = !packet.Translator.ReadBit();
                bit40 = !packet.Translator.ReadBit();
                bit4C = !packet.Translator.ReadBit();
                bit50 = !packet.Translator.ReadBit();
            }

            casterGUID[3] = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            if (hasPawerData)
            {
                var bits74 = packet.Translator.ReadBits(21);
                if (hasPawerData)
                {
                    packet.Translator.ReadInt32("Int70");

                    for (var i = 0; i < bits74; ++i)
                    {
                        packet.Translator.ReadInt32("IntED", i);
                        packet.Translator.ReadInt32("IntED", i);
                    }

                    packet.Translator.ReadInt32("Int6C");
                    packet.Translator.ReadInt32("Int68");
                }

            }

            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadUInt32("Blocked"); // correct?
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadByte("SchoolMask");
            if (hasDebugOutput)
            {
                if (bit54)
                    packet.Translator.ReadSingle("Float54");
                if (bit38)
                    packet.Translator.ReadSingle("Float38");
                if (bit40)
                    packet.Translator.ReadSingle("Float40");
                if (bit3C)
                    packet.Translator.ReadSingle("Float3C");
                if (bit4C)
                    packet.Translator.ReadSingle("Float4C");
                if (bit34)
                    packet.Translator.ReadSingle("Float34");
                if (bit30)
                    packet.Translator.ReadSingle("Float30");
                if (bit48)
                    packet.Translator.ReadSingle("Float48");
                if (bit50)
                    packet.Translator.ReadSingle("Float50");
                if (bit44)
                    packet.Translator.ReadSingle("Float44");
            }

            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadInt32E<AttackerStateFlags>("Attacker State Flags");
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadUInt32("Absorb"); // correct?
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadInt32("Overkill");
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadUInt32("Damage");
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadUInt32("Resist"); // correct?
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 7);

            packet.Translator.WriteGuid("CasterGUID", casterGUID);
            packet.Translator.WriteGuid("TargetGUID", targetGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var AttackerGUID = new byte[8];
            var VictimGUID = new byte[8];

            VictimGUID[7] = packet.Translator.ReadBit();
            AttackerGUID[3] = packet.Translator.ReadBit();
            VictimGUID[6] = packet.Translator.ReadBit();
            VictimGUID[2] = packet.Translator.ReadBit();
            VictimGUID[0] = packet.Translator.ReadBit();
            VictimGUID[5] = packet.Translator.ReadBit();
            AttackerGUID[5] = packet.Translator.ReadBit();
            AttackerGUID[2] = packet.Translator.ReadBit();
            VictimGUID[3] = packet.Translator.ReadBit();
            AttackerGUID[0] = packet.Translator.ReadBit();
            AttackerGUID[4] = packet.Translator.ReadBit();
            AttackerGUID[7] = packet.Translator.ReadBit();
            AttackerGUID[1] = packet.Translator.ReadBit();
            VictimGUID[1] = packet.Translator.ReadBit();
            VictimGUID[4] = packet.Translator.ReadBit();
            AttackerGUID[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(AttackerGUID, 2);
            packet.Translator.ReadXORByte(VictimGUID, 5);
            packet.Translator.ReadXORByte(AttackerGUID, 3);
            packet.Translator.ReadXORByte(AttackerGUID, 7);
            packet.Translator.ReadXORByte(VictimGUID, 1);
            packet.Translator.ReadXORByte(AttackerGUID, 0);
            packet.Translator.ReadXORByte(AttackerGUID, 1);
            packet.Translator.ReadXORByte(VictimGUID, 6);
            packet.Translator.ReadXORByte(VictimGUID, 3);
            packet.Translator.ReadXORByte(AttackerGUID, 6);
            packet.Translator.ReadXORByte(VictimGUID, 2);
            packet.Translator.ReadXORByte(AttackerGUID, 4);
            packet.Translator.ReadXORByte(VictimGUID, 7);
            packet.Translator.ReadXORByte(VictimGUID, 0);
            packet.Translator.ReadXORByte(AttackerGUID, 5);
            packet.Translator.ReadXORByte(VictimGUID, 4);

            packet.Translator.WriteGuid("Attacker GUID", AttackerGUID);
            packet.Translator.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var VictimGUID = new byte[8];
            var AttackerGUID = new byte[8];

            AttackerGUID[2] = packet.Translator.ReadBit();
            VictimGUID[3] = packet.Translator.ReadBit();
            VictimGUID[7] = packet.Translator.ReadBit();
            VictimGUID[2] = packet.Translator.ReadBit();
            VictimGUID[1] = packet.Translator.ReadBit();
            AttackerGUID[1] = packet.Translator.ReadBit();
            VictimGUID[6] = packet.Translator.ReadBit();

            packet.Translator.ReadBit("Unk bit");

            VictimGUID[0] = packet.Translator.ReadBit();
            AttackerGUID[5] = packet.Translator.ReadBit();
            AttackerGUID[6] = packet.Translator.ReadBit();
            AttackerGUID[0] = packet.Translator.ReadBit();
            VictimGUID[4] = packet.Translator.ReadBit();
            VictimGUID[5] = packet.Translator.ReadBit();
            AttackerGUID[3] = packet.Translator.ReadBit();
            AttackerGUID[4] = packet.Translator.ReadBit();
            AttackerGUID[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(AttackerGUID, 3);
            packet.Translator.ReadXORByte(AttackerGUID, 7);
            packet.Translator.ReadXORByte(AttackerGUID, 2);
            packet.Translator.ReadXORByte(VictimGUID, 4);
            packet.Translator.ReadXORByte(AttackerGUID, 0);
            packet.Translator.ReadXORByte(VictimGUID, 1);
            packet.Translator.ReadXORByte(AttackerGUID, 4);
            packet.Translator.ReadXORByte(VictimGUID, 5);
            packet.Translator.ReadXORByte(VictimGUID, 6);
            packet.Translator.ReadXORByte(VictimGUID, 7);
            packet.Translator.ReadXORByte(VictimGUID, 3);
            packet.Translator.ReadXORByte(AttackerGUID, 1);
            packet.Translator.ReadXORByte(VictimGUID, 2);
            packet.Translator.ReadXORByte(AttackerGUID, 6);
            packet.Translator.ReadXORByte(AttackerGUID, 5);
            packet.Translator.ReadXORByte(VictimGUID, 0);

            packet.Translator.WriteGuid("Attacker GUID", AttackerGUID);
            packet.Translator.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 4, 3, 5, 7, 0, 1, 6, 2);
            packet.Translator.ParseBitStream(guid, 6, 7, 5, 4, 2, 0, 1, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            targetGUID[1] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();
            casterGUID[3] = packet.Translator.ReadBit();
            targetGUID[7] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            targetGUID[4] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();

            var bits3C = 0u;
            if (hasPowerData)
                bits3C = packet.Translator.ReadBits(21);

            casterGUID[7] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();

            var bits20 = (int)packet.Translator.ReadBits(21);

            var bit14 = new bool[bits20];
            var hasSpellProto = new bool[bits20];
            var bit18 = new bool[bits20];
            var hasOverDamage = new bool[bits20];
            var hasAbsorb = new bool[bits20];

            for (var i = 0; i < bits20; ++i)
            {
                bit14[i] = !packet.Translator.ReadBit();
                hasSpellProto[i] = !packet.Translator.ReadBit();
                packet.Translator.ReadBit("Unk bit");
                hasOverDamage[i] = !packet.Translator.ReadBit();
                hasAbsorb[i] = !packet.Translator.ReadBit();
            }

            for (var i = 0; i < bits20; ++i)
            {
                var aura = packet.Translator.ReadUInt32E<AuraType>("Aura Type", i);
                packet.Translator.ReadInt32("Damage", i);

                if (hasOverDamage[i])
                    packet.Translator.ReadInt32("Over damage", i);
                if (hasSpellProto[i])
                    packet.Translator.ReadUInt32("Spell Proto", i);
                if (hasAbsorb[i])
                    packet.Translator.ReadInt32("Absorb", i);
                if (bit14[i])
                    packet.Translator.ReadUInt32("Int14", i);
            }

            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Int38");
                for (var i = 0; i < bits3C; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int30");
                packet.Translator.ReadInt32("Int34");
            }

            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 7);

            packet.Translator.WriteGuid("Target GUID", targetGUID);
            packet.Translator.WriteGuid("Caster GUID", casterGUID);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            var guid2 = new byte[8];
            var guid4 = new byte[8];

            var bits44 = 0;

            guid4[7] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            var bit5C = packet.Translator.ReadBit();
            guid4[5] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            var bit64 = packet.Translator.ReadBit();
            guid4[0] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid4[1] = packet.Translator.ReadBit();
            guid4[3] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid4[2] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            guid4[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Critical");
            if (hasPowerData)
                bits44 = (int)packet.Translator.ReadBits(21);
            guid4[6] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            packet.Translator.ReadInt32("Int34");
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid4, 7);
            packet.Translator.ReadXORByte(guid4, 0);
            packet.Translator.ReadXORByte(guid4, 4);
            packet.Translator.ReadInt32("Overheal");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            if (bit5C)
                packet.Translator.ReadSingle("Float58");
            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Int3C");
                packet.Translator.ReadInt32("Int40");
                for (var i = 0; i < bits44; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int38");
            }

            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            if (bit64)
                packet.Translator.ReadSingle("Float60");
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid4, 1);
            packet.Translator.ReadXORByte(guid4, 5);
            packet.Translator.ReadXORByte(guid4, 3);
            packet.Translator.ReadXORByte(guid4, 2);
            packet.Translator.ReadXORByte(guid4, 6);
            packet.Translator.ReadXORByte(guid2, 2);

            packet.Translator.WriteGuid("Guid2", guid2);
            packet.Translator.WriteGuid("Guid4", guid4);
        }
    }
}
