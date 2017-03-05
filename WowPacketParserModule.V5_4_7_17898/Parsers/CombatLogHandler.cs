using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            targetGUID[6] = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();
            casterGUID[3] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            targetGUID[4] = packet.Translator.ReadBit();

            var bits24 = packet.Translator.ReadBits(21);

            var hasAbsorb = new bool[bits24];
            var hasSpellProto = new bool[bits24];
            var hasOverDamage = new bool[bits24];
            var hasResist = new bool[bits24];

            for (var i = 0; i < bits24; ++i)
            {
                packet.Translator.ReadBit("Unk bit", i);
                hasAbsorb[i] = !packet.Translator.ReadBit();
                hasSpellProto[i] = !packet.Translator.ReadBit();
                hasOverDamage[i] = !packet.Translator.ReadBit();
                hasResist[i] = !packet.Translator.ReadBit();
            }

            targetGUID[7] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();

            var bits40 = 0u;
            if (hasPowerData)
                bits40 = packet.Translator.ReadBits(21);

            casterGUID[0] = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(targetGUID, 3);
            for (var i = 0; i < bits24; ++i)
            {
                packet.Translator.ReadUInt32E<AuraType>("Aura Type", i);
                if (hasSpellProto[i])
                    packet.Translator.ReadUInt32("Spell Proto", i);
                packet.Translator.ReadInt32("Damage", i);
                if (hasOverDamage[i])
                    packet.Translator.ReadInt32("Over damage", i);
                if (hasAbsorb[i])
                    packet.Translator.ReadInt32("Absorb", i);
                if (hasResist[i])
                    packet.Translator.ReadInt32("Resist");
            }

            packet.Translator.ReadXORByte(casterGUID, 4);
            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Int34");

                for (var i = 0; i < bits24; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int3C");
                packet.Translator.ReadInt32("Int38");
            }

            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 6);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var casterGUID = new byte[8];

            var bit10 = false;
            var bit14 = false;
            var bit18 = false;
            var bit1C = false;
            var bit20 = false;
            var bit24 = false;
            var bit28 = false;
            var bit2C = false;
            var bit30 = false;
            var bit34 = false;

            var bits68 = 0;

            packet.Translator.ReadByte("SchoolMask");
            packet.Translator.ReadInt32("Int44");
            packet.Translator.ReadUInt32("Absorb"); // correct?
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32("Int90");
            packet.Translator.ReadInt32("Overkill");
            packet.Translator.ReadUInt32("Damage");
            packet.Translator.ReadInt32E<AttackerStateFlags>("Attacker State Flags");
            targetGUID[4] = packet.Translator.ReadBit();
            var hasDebugOutput = packet.Translator.ReadBit("Has Debug Output");
            if (hasDebugOutput)
            {
                bit34 = !packet.Translator.ReadBit();
                bit30 = !packet.Translator.ReadBit();
                bit28 = !packet.Translator.ReadBit();
                bit2C = !packet.Translator.ReadBit();
                bit1C = !packet.Translator.ReadBit();
                bit10 = !packet.Translator.ReadBit();
                bit20 = !packet.Translator.ReadBit();
                bit18 = !packet.Translator.ReadBit();
                bit14 = !packet.Translator.ReadBit();
                bit24 = !packet.Translator.ReadBit();
            }

            casterGUID[7] = packet.Translator.ReadBit();
            casterGUID[3] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            var hasPawerData = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            targetGUID[7] = packet.Translator.ReadBit();
            var bit40 = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            var bit50 = packet.Translator.ReadBit();

            if (hasPawerData)
                bits68 = (int)packet.Translator.ReadBits(21);

            casterGUID[5] = packet.Translator.ReadBit();

            if (hasDebugOutput)
            {
                if (bit1C)
                    packet.Translator.ReadSingle("Float1C");
                if (bit34)
                    packet.Translator.ReadSingle("Float34");
                if (bit14)
                    packet.Translator.ReadSingle("Float14");
                if (bit10)
                    packet.Translator.ReadSingle("Float10");
                if (bit18)
                    packet.Translator.ReadSingle("Float18");
                if (bit2C)
                    packet.Translator.ReadSingle("Float2C");
                if (bit30)
                    packet.Translator.ReadSingle("Float30");
                if (bit24)
                    packet.Translator.ReadSingle("Float24");
                if (bit20)
                    packet.Translator.ReadSingle("Float20");
                if (bit28)
                    packet.Translator.ReadSingle("Float28");
            }

            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 5);
            if (hasPawerData)
            {
                for (var i = 0; i < bits68; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int60");
                packet.Translator.ReadInt32("Int5C");
                packet.Translator.ReadInt32("Int64");
            }

            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 4);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var casterGUID = new byte[8];

            var bits38 = 0;

            casterGUID[0] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            var bit14 = packet.Translator.ReadBit();
            if (hasPowerData)
                bits38 = (int)packet.Translator.ReadBits(21);
            targetGUID[4] = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();
            casterGUID[3] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();
            var bit5C = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            var bit64 = packet.Translator.ReadBit();
            targetGUID[7] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(targetGUID, 7);

            if (bit14)
                packet.Translator.ReadSingle("Float10");

            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Int34");

                for (var i = 0; i < bits38; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int2C");
                packet.Translator.ReadInt32("Int30");
            }

            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadInt32("Absorb");
            packet.Translator.ReadInt32("Overheal");
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 0);
            if (bit64)
                packet.Translator.ReadSingle("Float60");
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadXORByte(casterGUID, 4);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var casterGUID = new byte[8];

            var powerCount = 0;

            var hasPowerData = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();
            targetGUID[4] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            targetGUID[7] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            if (hasPowerData)
                powerCount = (int)packet.Translator.ReadBits(21);
            casterGUID[3] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadUInt32E<PowerType>("Power Type");
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 6);
            if (hasPowerData)
            {
                for (var i = 0; i < powerCount; i++)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int20");
                packet.Translator.ReadInt32("Int1C");
                packet.Translator.ReadInt32("Int24");
            }

            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadInt32("Amount");
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 1);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_INSTAKILL_LOG)]
        public static void HandleSpellInstakillLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var casterGUID = new byte[8];

            casterGUID[1] = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            casterGUID[3] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            targetGUID[7] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            targetGUID[4] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadUInt32<SpellId>("Spell Id");
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 6);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_DISPELL_LOG)]
        public static void SpellDispelLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            targetGUID[1] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            targetGUID[5] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            targetGUID[4] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            targetGUID[2] = packet.Translator.ReadBit();
            var bit39 = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            var bits1C = (int)packet.Translator.ReadBits(22);
            casterGUID[3] = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();
            targetGUID[3] = packet.Translator.ReadBit();

            var bit4 = new int[bits1C];
            var bit14 = new bool[bits1C];
            var bitC = new bool[bits1C];

            for (var i = 0; i < bits1C; ++i)
            {
                bit4[i] = packet.Translator.ReadBit();
                bit14[i] = packet.Translator.ReadBit();
                bitC[i] = packet.Translator.ReadBit();
            }

            targetGUID[7] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            var bit38 = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadInt32<SpellId>("Spell");
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 2);

            for (var i = 0; i < bits1C; ++i)
            {
                packet.AddValue("bit4", bit4[i], i);

                if (bit14[i])
                    packet.Translator.ReadInt32("Int20", i);

                packet.Translator.ReadInt32<SpellId>("Spell", i);

                if (bitC[i])
                    packet.Translator.ReadInt32("Int20", i);
            }

            packet.Translator.ReadXORByte(targetGUID, 1);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);
        }
    }
}
