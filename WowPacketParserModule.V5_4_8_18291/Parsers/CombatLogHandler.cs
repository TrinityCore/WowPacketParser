using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            var guidA = new byte[8];
            var guid12 = new byte[8];

            var bit1C = false;
            var bit20 = false;
            var bit24 = false;
            var bit28 = false;
            var bit2C = false;
            var bit30 = false;
            var bit34 = false;
            var bit38 = false;
            var bit3C = false;
            var bit40 = false;

            guid12[2] = packet.Translator.ReadBit();
            guidA[7] = packet.Translator.ReadBit();
            guidA[6] = packet.Translator.ReadBit();
            guidA[1] = packet.Translator.ReadBit();
            guidA[5] = packet.Translator.ReadBit();
            var bit5C = packet.Translator.ReadBit();
            guidA[0] = packet.Translator.ReadBit();
            guid12[0] = packet.Translator.ReadBit();
            guid12[7] = packet.Translator.ReadBit();
            guidA[3] = packet.Translator.ReadBit();
            guid12[6] = packet.Translator.ReadBit();
            var bit14 = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            guid12[1] = packet.Translator.ReadBit();
            var bit44 = packet.Translator.ReadBit();
            if (bit44)
            {
                bit3C = !packet.Translator.ReadBit();
                bit28 = !packet.Translator.ReadBit();
                bit24 = !packet.Translator.ReadBit();
                bit30 = !packet.Translator.ReadBit();
                bit2C = !packet.Translator.ReadBit();
                bit20 = !packet.Translator.ReadBit();
                bit1C = !packet.Translator.ReadBit();
                bit34 = !packet.Translator.ReadBit();
                bit38 = !packet.Translator.ReadBit();
                bit40 = !packet.Translator.ReadBit();
            }

            guid12[5] = packet.Translator.ReadBit();
            guidA[2] = packet.Translator.ReadBit();
            guidA[4] = packet.Translator.ReadBit();
            guid12[3] = packet.Translator.ReadBit();

            var bits70 = 0u;
            if (hasPowerData)
                bits70 = packet.Translator.ReadBits(21);

            guid12[4] = packet.Translator.ReadBit();
            packet.Translator.ReadInt32("Int8C");
            if (bit44)
            {
                if (bit30)
                    packet.Translator.ReadSingle("Float30");
                if (bit20)
                    packet.Translator.ReadSingle("Float20");
                if (bit3C)
                    packet.Translator.ReadSingle("Float3C");
                if (bit2C)
                    packet.Translator.ReadSingle("Float2C");
                if (bit34)
                    packet.Translator.ReadSingle("Float34");
                if (bit24)
                    packet.Translator.ReadSingle("Float24");
                if (bit1C)
                    packet.Translator.ReadSingle("Float1C");
                if (bit40)
                    packet.Translator.ReadSingle("Float40");
                if (bit28)
                    packet.Translator.ReadSingle("Float28");
                if (bit38)
                    packet.Translator.ReadSingle("Float38");
            }

            packet.Translator.ReadXORByte(guidA, 1);
            packet.Translator.ReadInt32("Overkill");
            packet.Translator.ReadXORByte(guid12, 3);
            packet.Translator.ReadXORByte(guidA, 0);
            packet.Translator.ReadXORByte(guid12, 6);
            packet.Translator.ReadXORByte(guid12, 4);
            packet.Translator.ReadXORByte(guidA, 7);
            packet.Translator.ReadInt32("Resist");
            packet.Translator.ReadInt32("Absorb");
            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Int64");
                for (var i = 0; i < bits70; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int6C");
                packet.Translator.ReadInt32("Int68");
            }

            packet.Translator.ReadXORByte(guidA, 5);
            packet.Translator.ReadXORByte(guid12, 5);
            packet.Translator.ReadXORByte(guidA, 3);
            packet.Translator.ReadXORByte(guidA, 2);
            packet.Translator.ReadXORByte(guid12, 2);
            packet.Translator.ReadXORByte(guidA, 6);
            packet.Translator.ReadXORByte(guid12, 0);
            packet.Translator.ReadXORByte(guidA, 4);
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadByte("SchoolMask");
            packet.Translator.ReadXORByte(guid12, 7);
            packet.Translator.ReadInt32E<AttackerStateFlags>("Attacker State Flags");
            packet.Translator.ReadXORByte(guid12, 1);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");

            packet.Translator.WriteGuid("GuidA", guidA);
            packet.Translator.WriteGuid("Guid12", guid12);
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var casterGUID = new byte[8];

            targetGUID[7] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            var bits3C = (int)packet.Translator.ReadBits(21);
            targetGUID[0] = packet.Translator.ReadBit();

            var hasOverDamage = new bool[bits3C];
            var bit10 = new bool[bits3C];
            var bit14 = new bool[bits3C];
            var hasSpellProto = new bool[bits3C];

            for (var i = 0; i < bits3C; ++i)
            {
                hasOverDamage[i] = !packet.Translator.ReadBit(); // +8
                bit10[i] = !packet.Translator.ReadBit(); // +16
                packet.Translator.ReadBit("Unk bit", i); // +24
                bit14[i] = !packet.Translator.ReadBit(); // +20
                hasSpellProto[i] = !packet.Translator.ReadBit(); // +12
            }

            targetGUID[5] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            casterGUID[3] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();

            var bits24 = 0u;
            if (hasPowerData)
                bits24 = packet.Translator.ReadBits(21);

            targetGUID[4] = packet.Translator.ReadBit();
            for (var i = 0; i < bits3C; ++i)
            {
                if (hasOverDamage[i])
                    packet.Translator.ReadInt32("Over damage", i); // +8

                packet.Translator.ReadInt32("Damage", i);
                packet.Translator.ReadInt32E<AuraType>("Aura Type", i);

                if (bit14[i])
                    packet.Translator.ReadInt32("Int14", i); // +20

                if (bit10[i])
                    packet.Translator.ReadInt32("Int10"); // +16

                if (hasSpellProto[i])
                    packet.Translator.ReadUInt32("Spell Proto", i); // +12
            }

            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 1);
            if (hasPowerData)
            {
                for (var i = 0; i < bits24; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int34");
                packet.Translator.ReadInt32("Int3C");
                packet.Translator.ReadInt32("Int38");
            }

            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 6);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();

            var bits1C = 0u;
            if (hasPowerData)
                bits1C = packet.Translator.ReadBits(21);

            guid2[4] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 6);
            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Int14");

                for (var i = 0; i < bits1C; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int18");
                packet.Translator.ReadInt32("Int10");
            }

            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadInt32("Amount");
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadUInt32E<PowerType>("Power Type");

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            var bits24 = 0;

            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32("Absorb");
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32("Overheal");
            targetGUID[0] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            var bit18 = packet.Translator.ReadBit();
            casterGUID[3] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            targetGUID[7] = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            targetGUID[4] = packet.Translator.ReadBit();
            if (hasPowerData)
                bits24 = (int)packet.Translator.ReadBits(21);
            var bit48 = packet.Translator.ReadBit();
            var bit58 = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            if (hasPowerData)
            {
                for (var i = 0; i < bits24; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int34");
                packet.Translator.ReadInt32("Int3C");
                packet.Translator.ReadInt32("Int38");
            }

            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(targetGUID, 7);
            if (bit58)
                packet.Translator.ReadSingle("Float54");
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 0);
            if (bit48)
                packet.Translator.ReadSingle("Float44");
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 4);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_DISPELL_LOG)]
        public static void SpellDispelLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            targetGUID[2] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            var bit38 = packet.Translator.ReadBit();
            var bit2C = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            targetGUID[7] = packet.Translator.ReadBit();
            targetGUID[4] = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            var bits1C = (int)packet.Translator.ReadBits(22);
            casterGUID[0] = packet.Translator.ReadBit();

            var bit4 = new int[bits1C];
            var bit14 = new bool[bits1C];
            var bitC = new bool[bits1C];

            for (var i = 0; i < bits1C; ++i)
            {
                bit14[i] = packet.Translator.ReadBit();
                bitC[i] = packet.Translator.ReadBit();
                bit4[i] = packet.Translator.ReadBit();
            }

            casterGUID[3] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();

            for (var i = 0; i < bits1C; ++i)
            {
                packet.AddValue("bit4", bit4[i], i);

                if (bit14[i])
                    packet.Translator.ReadInt32("Int20", i);

                packet.Translator.ReadInt32<SpellId>("Spell", i);

                if (bitC[i])
                    packet.Translator.ReadInt32("Int20", i);
            }

            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadInt32<SpellId>("Spell");
            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 4);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);

        }
    }
}
