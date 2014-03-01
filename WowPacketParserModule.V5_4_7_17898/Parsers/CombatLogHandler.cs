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

        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            var guid10 = new byte[8];
            var guid11 = new byte[8];

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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadInt32("Int90");
            packet.ReadInt32("Overkill");
            packet.ReadUInt32("Damage");
            packet.ReadEnum<AttackerStateFlags>("Attacker State Flags", TypeCode.Int32);
            guid10[4] = packet.ReadBit();
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

            guid11[7] = packet.ReadBit();
            guid11[3] = packet.ReadBit();
            guid10[3] = packet.ReadBit();
            guid11[1] = packet.ReadBit();
            guid10[6] = packet.ReadBit();
            guid10[2] = packet.ReadBit();
            var hasPawerData = packet.ReadBit();
            guid10[0] = packet.ReadBit();
            guid11[6] = packet.ReadBit();
            guid10[7] = packet.ReadBit();
            var bit40 = packet.ReadBit();
            guid10[5] = packet.ReadBit();
            guid10[1] = packet.ReadBit();
            guid11[0] = packet.ReadBit();
            guid11[4] = packet.ReadBit();
            guid11[2] = packet.ReadBit();
            var bit50 = packet.ReadBit();

            if (hasPawerData)
                bits68 = (int)packet.ReadBits(21);

            guid11[5] = packet.ReadBit();

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

            packet.ReadXORByte(guid10, 7);
            packet.ReadXORByte(guid10, 6);
            packet.ReadXORByte(guid11, 5);
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

            packet.ReadXORByte(guid10, 2);
            packet.ReadXORByte(guid11, 6);
            packet.ReadXORByte(guid10, 1);
            packet.ReadXORByte(guid10, 4);
            packet.ReadXORByte(guid11, 2);
            packet.ReadXORByte(guid11, 1);
            packet.ReadXORByte(guid11, 7);
            packet.ReadXORByte(guid10, 5);
            packet.ReadXORByte(guid11, 3);
            packet.ReadXORByte(guid10, 0);
            packet.ReadXORByte(guid11, 0);
            packet.ReadXORByte(guid10, 3);
            packet.ReadXORByte(guid11, 4);

            packet.WriteGuid("Guid10", guid10);
            packet.WriteGuid("Guid11", guid11);

        }
    }
}
