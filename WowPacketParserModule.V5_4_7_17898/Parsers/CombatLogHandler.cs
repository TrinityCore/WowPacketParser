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
            var hasResist = new bool[bits24];

            for (var i = 0; i < bits24; ++i)
            {
                packet.ReadBit("Unk bit", i);
                hasAbsorb[i] = !packet.ReadBit();
                hasSpellProto[i] = !packet.ReadBit();
                hasOverDamage[i] = !packet.ReadBit();
                hasResist[i] = !packet.ReadBit();
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
                packet.ReadUInt32E<AuraType>("Aura Type", i);
                if (hasSpellProto[i])
                    packet.ReadUInt32("Spell Proto", i);
                packet.ReadInt32("Damage", i);
                if (hasOverDamage[i])
                    packet.ReadInt32("Over damage", i);
                if (hasAbsorb[i])
                    packet.ReadInt32("Absorb", i);
                if (hasResist[i])
                    packet.ReadInt32("Resist");
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
            packet.ReadInt32<SpellId>("Spell ID");
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

            packet.ReadByte("SchoolMask");
            packet.ReadInt32("Int44");
            packet.ReadUInt32("Absorb"); // correct?
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadInt32("Int90");
            packet.ReadInt32("Overkill");
            packet.ReadUInt32("Damage");
            packet.ReadInt32E<AttackerStateFlags>("Attacker State Flags");
            targetGUID[4] = packet.ReadBit();
            var hasDebugOutput = packet.ReadBit("Has Debug Output");
            if (hasDebugOutput)
            {
                bit34 = !packet.ReadBit();
                bit30 = !packet.ReadBit();
                bit28 = !packet.ReadBit();
                bit2C = !packet.ReadBit();
                bit1C = !packet.ReadBit();
                bit10 = !packet.ReadBit();
                bit20 = !packet.ReadBit();
                bit18 = !packet.ReadBit();
                bit14 = !packet.ReadBit();
                bit24 = !packet.ReadBit();
            }

            casterGUID[7] = packet.ReadBit();
            casterGUID[3] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            var hasPawerData = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            var bit40 = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            var bit50 = packet.ReadBit();

            if (hasPawerData)
                bits68 = (int)packet.ReadBits(21);

            casterGUID[5] = packet.ReadBit();

            if (hasDebugOutput)
            {
                if (bit1C)
                    packet.ReadSingle("Float1C");
                if (bit34)
                    packet.ReadSingle("Float34");
                if (bit14)
                    packet.ReadSingle("Float14");
                if (bit10)
                    packet.ReadSingle("Float10");
                if (bit18)
                    packet.ReadSingle("Float18");
                if (bit2C)
                    packet.ReadSingle("Float2C");
                if (bit30)
                    packet.ReadSingle("Float30");
                if (bit24)
                    packet.ReadSingle("Float24");
                if (bit20)
                    packet.ReadSingle("Float20");
                if (bit28)
                    packet.ReadSingle("Float28");
            }

            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(casterGUID, 5);
            if (hasPawerData)
            {
                for (var i = 0; i < bits68; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int60");
                packet.ReadInt32("Int5C");
                packet.ReadInt32("Int64");
            }

            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 4);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var casterGUID = new byte[8];

            var bits38 = 0;

            casterGUID[0] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            var bit14 = packet.ReadBit();
            if (hasPowerData)
                bits38 = (int)packet.ReadBits(21);
            targetGUID[4] = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            casterGUID[3] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            var bit5C = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            var bit64 = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();

            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(targetGUID, 7);

            if (bit14)
                packet.ReadSingle("Float10");

            if (hasPowerData)
            {
                packet.ReadInt32("Int34");

                for (var i = 0; i < bits38; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int2C");
                packet.ReadInt32("Int30");
            }

            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadInt32("Absorb");
            packet.ReadInt32("Overheal");
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 0);
            if (bit64)
                packet.ReadSingle("Float60");
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadInt32("Damage");
            packet.ReadXORByte(casterGUID, 4);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var casterGUID = new byte[8];

            var powerCount = 0;

            var hasPowerData = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            if (hasPowerData)
                powerCount = (int)packet.ReadBits(21);
            casterGUID[3] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadUInt32E<PowerType>("Power Type");
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 6);
            if (hasPowerData)
            {
                for (var i = 0; i < powerCount; i++)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int20");
                packet.ReadInt32("Int1C");
                packet.ReadInt32("Int24");
            }

            packet.ReadXORByte(targetGUID, 7);
            packet.ReadInt32("Amount");
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(casterGUID, 4);
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(targetGUID, 1);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_INSTAKILL_LOG)]
        public static void HandleSpellInstakillLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var casterGUID = new byte[8];

            casterGUID[1] = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            casterGUID[3] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadUInt32<SpellId>("Spell Id");
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 6);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_DISPELL_LOG)]
        public static void SpellDispelLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            targetGUID[1] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            var bit39 = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            var bits1C = (int)packet.ReadBits(22);
            casterGUID[3] = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();

            var bit4 = new int[bits1C];
            var bit14 = new bool[bits1C];
            var bitC = new bool[bits1C];

            for (var i = 0; i < bits1C; ++i)
            {
                bit4[i] = packet.ReadBit();
                bit14[i] = packet.ReadBit();
                bitC[i] = packet.ReadBit();
            }

            targetGUID[7] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            var bit38 = packet.ReadBit();
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(casterGUID, 4);
            packet.ReadInt32<SpellId>("Spell");
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(casterGUID, 2);

            for (var i = 0; i < bits1C; ++i)
            {
                packet.AddValue("bit4", bit4[i], i);

                if (bit14[i])
                    packet.ReadInt32("Int20", i);

                packet.ReadInt32<SpellId>("Spell", i);

                if (bitC[i])
                    packet.ReadInt32("Int20", i);
            }

            packet.ReadXORByte(targetGUID, 1);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }
    }
}
