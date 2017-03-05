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

            guid12[2] = packet.ReadBit();
            guidA[7] = packet.ReadBit();
            guidA[6] = packet.ReadBit();
            guidA[1] = packet.ReadBit();
            guidA[5] = packet.ReadBit();
            var bit5C = packet.ReadBit();
            guidA[0] = packet.ReadBit();
            guid12[0] = packet.ReadBit();
            guid12[7] = packet.ReadBit();
            guidA[3] = packet.ReadBit();
            guid12[6] = packet.ReadBit();
            var bit14 = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            guid12[1] = packet.ReadBit();
            var bit44 = packet.ReadBit();
            if (bit44)
            {
                bit3C = !packet.ReadBit();
                bit28 = !packet.ReadBit();
                bit24 = !packet.ReadBit();
                bit30 = !packet.ReadBit();
                bit2C = !packet.ReadBit();
                bit20 = !packet.ReadBit();
                bit1C = !packet.ReadBit();
                bit34 = !packet.ReadBit();
                bit38 = !packet.ReadBit();
                bit40 = !packet.ReadBit();
            }

            guid12[5] = packet.ReadBit();
            guidA[2] = packet.ReadBit();
            guidA[4] = packet.ReadBit();
            guid12[3] = packet.ReadBit();

            var bits70 = 0u;
            if (hasPowerData)
                bits70 = packet.ReadBits(21);

            guid12[4] = packet.ReadBit();
            packet.ReadInt32("Int8C");
            if (bit44)
            {
                if (bit30)
                    packet.ReadSingle("Float30");
                if (bit20)
                    packet.ReadSingle("Float20");
                if (bit3C)
                    packet.ReadSingle("Float3C");
                if (bit2C)
                    packet.ReadSingle("Float2C");
                if (bit34)
                    packet.ReadSingle("Float34");
                if (bit24)
                    packet.ReadSingle("Float24");
                if (bit1C)
                    packet.ReadSingle("Float1C");
                if (bit40)
                    packet.ReadSingle("Float40");
                if (bit28)
                    packet.ReadSingle("Float28");
                if (bit38)
                    packet.ReadSingle("Float38");
            }

            packet.ReadXORByte(guidA, 1);
            packet.ReadInt32("Overkill");
            packet.ReadXORByte(guid12, 3);
            packet.ReadXORByte(guidA, 0);
            packet.ReadXORByte(guid12, 6);
            packet.ReadXORByte(guid12, 4);
            packet.ReadXORByte(guidA, 7);
            packet.ReadInt32("Resist");
            packet.ReadInt32("Absorb");
            if (hasPowerData)
            {
                packet.ReadInt32("Int64");
                for (var i = 0; i < bits70; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int6C");
                packet.ReadInt32("Int68");
            }

            packet.ReadXORByte(guidA, 5);
            packet.ReadXORByte(guid12, 5);
            packet.ReadXORByte(guidA, 3);
            packet.ReadXORByte(guidA, 2);
            packet.ReadXORByte(guid12, 2);
            packet.ReadXORByte(guidA, 6);
            packet.ReadXORByte(guid12, 0);
            packet.ReadXORByte(guidA, 4);
            packet.ReadInt32("Damage");
            packet.ReadByte("SchoolMask");
            packet.ReadXORByte(guid12, 7);
            packet.ReadInt32E<AttackerStateFlags>("Attacker State Flags");
            packet.ReadXORByte(guid12, 1);
            packet.ReadUInt32<SpellId>("Spell ID");

            packet.WriteGuid("GuidA", guidA);
            packet.WriteGuid("Guid12", guid12);
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var casterGUID = new byte[8];

            targetGUID[7] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            var bits3C = (int)packet.ReadBits(21);
            targetGUID[0] = packet.ReadBit();

            var hasOverDamage = new bool[bits3C];
            var bit10 = new bool[bits3C];
            var bit14 = new bool[bits3C];
            var hasSpellProto = new bool[bits3C];

            for (var i = 0; i < bits3C; ++i)
            {
                hasOverDamage[i] = !packet.ReadBit(); // +8
                bit10[i] = !packet.ReadBit(); // +16
                packet.ReadBit("Unk bit", i); // +24
                bit14[i] = !packet.ReadBit(); // +20
                hasSpellProto[i] = !packet.ReadBit(); // +12
            }

            targetGUID[5] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            casterGUID[3] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();

            var bits24 = 0u;
            if (hasPowerData)
                bits24 = packet.ReadBits(21);

            targetGUID[4] = packet.ReadBit();
            for (var i = 0; i < bits3C; ++i)
            {
                if (hasOverDamage[i])
                    packet.ReadInt32("Over damage", i); // +8

                packet.ReadInt32("Damage", i);
                packet.ReadInt32E<AuraType>("Aura Type", i);

                if (bit14[i])
                    packet.ReadInt32("Int14", i); // +20

                if (bit10[i])
                    packet.ReadInt32("Int10"); // +16

                if (hasSpellProto[i])
                    packet.ReadUInt32("Spell Proto", i); // +12
            }

            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 1);
            if (hasPowerData)
            {
                for (var i = 0; i < bits24; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int34");
                packet.ReadInt32("Int3C");
                packet.ReadInt32("Int38");
            }

            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(casterGUID, 6);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();

            var bits1C = 0u;
            if (hasPowerData)
                bits1C = packet.ReadBits(21);

            guid2[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 6);
            if (hasPowerData)
            {
                packet.ReadInt32("Int14");

                for (var i = 0; i < bits1C; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int18");
                packet.ReadInt32("Int10");
            }

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadInt32("Amount");
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 3);
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadUInt32E<PowerType>("Power Type");

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            var bits24 = 0;

            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadInt32("Absorb");
            packet.ReadInt32("Damage");
            packet.ReadInt32("Overheal");
            targetGUID[0] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            var bit18 = packet.ReadBit();
            casterGUID[3] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();
            if (hasPowerData)
                bits24 = (int)packet.ReadBits(21);
            var bit48 = packet.ReadBit();
            var bit58 = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            if (hasPowerData)
            {
                for (var i = 0; i < bits24; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int34");
                packet.ReadInt32("Int3C");
                packet.ReadInt32("Int38");
            }

            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(targetGUID, 7);
            if (bit58)
                packet.ReadSingle("Float54");
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(casterGUID, 0);
            if (bit48)
                packet.ReadSingle("Float44");
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 4);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_DISPELL_LOG)]
        public static void SpellDispelLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            targetGUID[2] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            var bit38 = packet.ReadBit();
            var bit2C = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            var bits1C = (int)packet.ReadBits(22);
            casterGUID[0] = packet.ReadBit();

            var bit4 = new int[bits1C];
            var bit14 = new bool[bits1C];
            var bitC = new bool[bits1C];

            for (var i = 0; i < bits1C; ++i)
            {
                bit14[i] = packet.ReadBit();
                bitC[i] = packet.ReadBit();
                bit4[i] = packet.ReadBit();
            }

            casterGUID[3] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();

            for (var i = 0; i < bits1C; ++i)
            {
                packet.AddValue("bit4", bit4[i], i);

                if (bit14[i])
                    packet.ReadInt32("Int20", i);

                packet.ReadInt32<SpellId>("Spell", i);

                if (bitC[i])
                    packet.ReadInt32("Int20", i);
            }

            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadInt32<SpellId>("Spell");
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(targetGUID, 4);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);

        }
    }
}
