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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadInt32("Int90");
            packet.ReadInt32("Overkill");
            packet.ReadUInt32("Damage");
            packet.ReadEnum<AttackerStateFlags>("Attacker State Flags", TypeCode.Int32);
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

        [Parser(Opcode.SMSG_SPELLHEALLOG)]
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
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadInt32("Int68");
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

        [Parser(Opcode.SMSG_ATTACKERSTATEUPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            packet.ReadInt32("Length");
            var hitInfo = packet.ReadEnum<SpellHitInfo>("HitInfo", TypeCode.Int32);

            packet.ReadPackedGuid("AttackerGUID");
            packet.ReadPackedGuid("TargetGUID");

            packet.ReadInt32("Damage");
            packet.ReadInt32("OverDamage");

            var subDmgCount = packet.ReadByte();

            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.ReadInt32("SchoolMask", i);
                packet.ReadSingle("Float Damage", i);
                packet.ReadInt32("Int Damage", i);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.ReadInt32("Damage Absorbed", i);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.ReadInt32("Damage Resisted", i);

            var state = packet.ReadEnum<VictimStates>("VictimState", TypeCode.Byte);
            packet.ReadInt32("Unk Attacker State 0");

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Melee Spell ID ");

            var block = 0;
            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                block = packet.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("Rage Gained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.ReadInt32("Unk Attacker State 3 1");
                packet.ReadSingle("Unk Attacker State 3 2");
                packet.ReadSingle("Unk Attacker State 3 3");
                packet.ReadSingle("Unk Attacker State 3 4");
                packet.ReadSingle("Unk Attacker State 3 5");
                packet.ReadSingle("Unk Attacker State 3 6");
                packet.ReadSingle("Unk Attacker State 3 7");
                packet.ReadSingle("Unk Attacker State 3 8");
                packet.ReadSingle("Unk Attacker State 3 9");
                packet.ReadSingle("Unk Attacker State 3 10");
                packet.ReadSingle("Unk Attacker State 3 11");
                packet.ReadInt32("Unk Attacker State 3 12");
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.ReadSingle("Unk Float");

            var bit2C = packet.ReadBit();

            if (bit2C)
            {
                var bits1C = (int)packet.ReadBits(21);
                packet.ReadInt32("Int18");
                for (var i = 0; i < bits1C; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int10");
                packet.ReadInt32("Int14");
            }
        }
    }
}
