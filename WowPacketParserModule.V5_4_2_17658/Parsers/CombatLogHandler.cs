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

            casterGUID[4] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            var hasPawerData = packet.ReadBit();
            var bit20 = packet.ReadBit();
            var bit90 = packet.ReadBit();

            var hasDebugOutput = packet.ReadBit("Has Debug Output");
            if (hasDebugOutput)
            {
                bit30 = !packet.ReadBit();
                bit38 = !packet.ReadBit();
                bit44 = !packet.ReadBit();
                bit34 = !packet.ReadBit();
                bit48 = !packet.ReadBit();
                bit3C = !packet.ReadBit();
                bit54 = !packet.ReadBit();
                bit40 = !packet.ReadBit();
                bit4C = !packet.ReadBit();
                bit50 = !packet.ReadBit();
            }

            casterGUID[3] = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            if (hasPawerData)
            {
                var bits74 = packet.ReadBits(21);
                if (hasPawerData)
                {
                    packet.ReadInt32("Int70");

                    for (var i = 0; i < bits74; ++i)
                    {
                        packet.ReadInt32("IntED", i);
                        packet.ReadInt32("IntED", i);
                    }

                    packet.ReadInt32("Int6C");
                    packet.ReadInt32("Int68");
                }

            }

            packet.ReadXORByte(targetGUID, 6);
            packet.ReadUInt32("Blocked"); // correct?
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadByte("SchoolMask");
            if (hasDebugOutput)
            {
                if (bit54)
                    packet.ReadSingle("Float54");
                if (bit38)
                    packet.ReadSingle("Float38");
                if (bit40)
                    packet.ReadSingle("Float40");
                if (bit3C)
                    packet.ReadSingle("Float3C");
                if (bit4C)
                    packet.ReadSingle("Float4C");
                if (bit34)
                    packet.ReadSingle("Float34");
                if (bit30)
                    packet.ReadSingle("Float30");
                if (bit48)
                    packet.ReadSingle("Float48");
                if (bit50)
                    packet.ReadSingle("Float50");
                if (bit44)
                    packet.ReadSingle("Float44");
            }

            packet.ReadXORByte(casterGUID, 6);
            packet.ReadInt32E<AttackerStateFlags>("Attacker State Flags");
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadUInt32("Absorb"); // correct?
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadInt32("Overkill");
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadUInt32("Damage");
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadUInt32("Resist"); // correct?
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(casterGUID, 7);

            packet.WriteGuid("CasterGUID", casterGUID);
            packet.WriteGuid("TargetGUID", targetGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var AttackerGUID = new byte[8];
            var VictimGUID = new byte[8];

            VictimGUID[7] = packet.ReadBit();
            AttackerGUID[3] = packet.ReadBit();
            VictimGUID[6] = packet.ReadBit();
            VictimGUID[2] = packet.ReadBit();
            VictimGUID[0] = packet.ReadBit();
            VictimGUID[5] = packet.ReadBit();
            AttackerGUID[5] = packet.ReadBit();
            AttackerGUID[2] = packet.ReadBit();
            VictimGUID[3] = packet.ReadBit();
            AttackerGUID[0] = packet.ReadBit();
            AttackerGUID[4] = packet.ReadBit();
            AttackerGUID[7] = packet.ReadBit();
            AttackerGUID[1] = packet.ReadBit();
            VictimGUID[1] = packet.ReadBit();
            VictimGUID[4] = packet.ReadBit();
            AttackerGUID[6] = packet.ReadBit();

            packet.ReadXORByte(AttackerGUID, 2);
            packet.ReadXORByte(VictimGUID, 5);
            packet.ReadXORByte(AttackerGUID, 3);
            packet.ReadXORByte(AttackerGUID, 7);
            packet.ReadXORByte(VictimGUID, 1);
            packet.ReadXORByte(AttackerGUID, 0);
            packet.ReadXORByte(AttackerGUID, 1);
            packet.ReadXORByte(VictimGUID, 6);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(AttackerGUID, 6);
            packet.ReadXORByte(VictimGUID, 2);
            packet.ReadXORByte(AttackerGUID, 4);
            packet.ReadXORByte(VictimGUID, 7);
            packet.ReadXORByte(VictimGUID, 0);
            packet.ReadXORByte(AttackerGUID, 5);
            packet.ReadXORByte(VictimGUID, 4);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var VictimGUID = new byte[8];
            var AttackerGUID = new byte[8];

            AttackerGUID[2] = packet.ReadBit();
            VictimGUID[3] = packet.ReadBit();
            VictimGUID[7] = packet.ReadBit();
            VictimGUID[2] = packet.ReadBit();
            VictimGUID[1] = packet.ReadBit();
            AttackerGUID[1] = packet.ReadBit();
            VictimGUID[6] = packet.ReadBit();

            packet.ReadBit("Unk bit");

            VictimGUID[0] = packet.ReadBit();
            AttackerGUID[5] = packet.ReadBit();
            AttackerGUID[6] = packet.ReadBit();
            AttackerGUID[0] = packet.ReadBit();
            VictimGUID[4] = packet.ReadBit();
            VictimGUID[5] = packet.ReadBit();
            AttackerGUID[3] = packet.ReadBit();
            AttackerGUID[4] = packet.ReadBit();
            AttackerGUID[7] = packet.ReadBit();

            packet.ReadXORByte(AttackerGUID, 3);
            packet.ReadXORByte(AttackerGUID, 7);
            packet.ReadXORByte(AttackerGUID, 2);
            packet.ReadXORByte(VictimGUID, 4);
            packet.ReadXORByte(AttackerGUID, 0);
            packet.ReadXORByte(VictimGUID, 1);
            packet.ReadXORByte(AttackerGUID, 4);
            packet.ReadXORByte(VictimGUID, 5);
            packet.ReadXORByte(VictimGUID, 6);
            packet.ReadXORByte(VictimGUID, 7);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(AttackerGUID, 1);
            packet.ReadXORByte(VictimGUID, 2);
            packet.ReadXORByte(AttackerGUID, 6);
            packet.ReadXORByte(AttackerGUID, 5);
            packet.ReadXORByte(VictimGUID, 0);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 4, 3, 5, 7, 0, 1, 6, 2);
            packet.ParseBitStream(guid, 6, 7, 5, 4, 2, 0, 1, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            targetGUID[1] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            casterGUID[3] = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();

            var bits3C = 0u;
            if (hasPowerData)
                bits3C = packet.ReadBits(21);

            casterGUID[7] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();

            var bits20 = (int)packet.ReadBits(21);

            var bit14 = new bool[bits20];
            var hasSpellProto = new bool[bits20];
            var bit18 = new bool[bits20];
            var hasOverDamage = new bool[bits20];
            var hasAbsorb = new bool[bits20];

            for (var i = 0; i < bits20; ++i)
            {
                bit14[i] = !packet.ReadBit();
                hasSpellProto[i] = !packet.ReadBit();
                packet.ReadBit("Unk bit");
                hasOverDamage[i] = !packet.ReadBit();
                hasAbsorb[i] = !packet.ReadBit();
            }

            for (var i = 0; i < bits20; ++i)
            {
                var aura = packet.ReadUInt32E<AuraType>("Aura Type", i);
                packet.ReadInt32("Damage", i);

                if (hasOverDamage[i])
                    packet.ReadInt32("Over damage", i);
                if (hasSpellProto[i])
                    packet.ReadUInt32("Spell Proto", i);
                if (hasAbsorb[i])
                    packet.ReadInt32("Absorb", i);
                if (bit14[i])
                    packet.ReadUInt32("Int14", i);
            }

            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadInt32<SpellId>("Spell ID");
            if (hasPowerData)
            {
                packet.ReadInt32("Int38");
                for (var i = 0; i < bits3C; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int30");
                packet.ReadInt32("Int34");
            }

            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(casterGUID, 7);

            packet.WriteGuid("Target GUID", targetGUID);
            packet.WriteGuid("Caster GUID", casterGUID);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            var guid2 = new byte[8];
            var guid4 = new byte[8];

            var bits44 = 0;

            guid4[7] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            var bit5C = packet.ReadBit();
            guid4[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            var bit64 = packet.ReadBit();
            guid4[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid4[1] = packet.ReadBit();
            guid4[3] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid4[2] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            guid4[4] = packet.ReadBit();
            packet.ReadBit("Critical");
            if (hasPowerData)
                bits44 = (int)packet.ReadBits(21);
            guid4[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            packet.ReadInt32("Int34");
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid4, 7);
            packet.ReadXORByte(guid4, 0);
            packet.ReadXORByte(guid4, 4);
            packet.ReadInt32("Overheal");
            packet.ReadInt32<SpellId>("Spell ID");
            if (bit5C)
                packet.ReadSingle("Float58");
            if (hasPowerData)
            {
                packet.ReadInt32("Int3C");
                packet.ReadInt32("Int40");
                for (var i = 0; i < bits44; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int38");
            }

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 3);
            if (bit64)
                packet.ReadSingle("Float60");
            packet.ReadXORByte(guid2, 7);
            packet.ReadInt32("Damage");
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid4, 1);
            packet.ReadXORByte(guid4, 5);
            packet.ReadXORByte(guid4, 3);
            packet.ReadXORByte(guid4, 2);
            packet.ReadXORByte(guid4, 6);
            packet.ReadXORByte(guid2, 2);

            packet.WriteGuid("Guid2", guid2);
            packet.WriteGuid("Guid4", guid4);
        }
    }
}
