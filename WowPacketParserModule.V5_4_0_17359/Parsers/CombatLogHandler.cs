using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            var target = new byte[8];
            var caster = new byte[8];
            var powerTargetGUID = new byte[8];

            packet.Translator.ReadUInt32("Damage");
            packet.Translator.ReadUInt32("Resist");
            packet.Translator.ReadInt32E<AttackerStateFlags>("Attacker State Flags");
            packet.Translator.ReadUInt32("Blocked");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32("Overkill");
            packet.Translator.ReadUInt32("Absorb");
            packet.Translator.ReadByte("SchoolMask");

            var hasDebugOutput = packet.Translator.ReadBit("Has Debug Output");
            packet.Translator.StartBitStream(caster, 1, 7);
            target[1] = packet.Translator.ReadBit();

            var bit2C = false;
            var bit10 = false;
            var bit1C = false;
            var bit24 = false;
            var bit20 = false;
            var bit34 = false;
            var bit18 = false;
            var bit14 = false;
            var bit28 = false;
            var bit30 = false;
            if (hasDebugOutput)
            {
                bit2C = !packet.Translator.ReadBit();
                bit10 = !packet.Translator.ReadBit();
                bit1C = !packet.Translator.ReadBit();
                bit24 = !packet.Translator.ReadBit();
                bit20 = !packet.Translator.ReadBit();
                bit34 = !packet.Translator.ReadBit();
                bit18 = !packet.Translator.ReadBit();
                bit14 = !packet.Translator.ReadBit();
                bit28 = !packet.Translator.ReadBit();
                bit30 = !packet.Translator.ReadBit();
            }

            packet.Translator.ReadBit("bit44");
            packet.Translator.StartBitStream(target, 5, 0, 3, 6, 2);
            packet.Translator.StartBitStream(caster, 6, 5, 4);
            var hasPawerData = packet.Translator.ReadBit();

            var bits8C = 0u;
            if (hasPawerData)
            {
                packet.Translator.StartBitStream(powerTargetGUID, 1, 5, 7, 4, 6, 3);
                bits8C = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerTargetGUID, 2, 0);
            }

            target[7] = packet.Translator.ReadBit();
            caster[3] = packet.Translator.ReadBit();
            target[4] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(caster, 0, 2);
            packet.Translator.ReadBit("bit50");
            packet.Translator.ResetBitReader();

            if (hasDebugOutput)
            {
                if (bit28)
                    packet.Translator.ReadSingle("float28");
                if (bit20)
                    packet.Translator.ReadSingle("float20");
                if (bit24)
                    packet.Translator.ReadSingle("float24");
                if (bit1C)
                    packet.Translator.ReadSingle("float1C");
                if (bit30)
                    packet.Translator.ReadSingle("float30");
                if (bit18)
                    packet.Translator.ReadSingle("float18");
                if (bit14)
                    packet.Translator.ReadSingle("float14");
                if (bit10)
                    packet.Translator.ReadSingle("float10");
                if (bit2C)
                    packet.Translator.ReadSingle("float2C");
                if (bit34)
                    packet.Translator.ReadSingle("float34");
            }

            packet.Translator.ReadXORByte(caster, 7);
            if (hasPawerData)
            {
                packet.Translator.ReadXORByte(powerTargetGUID, 3);
                packet.Translator.ReadInt32("Attack Power");
                packet.Translator.ReadXORBytes(powerTargetGUID, 5, 1, 7);
                for (var i = 0; i < bits8C; ++i)
                {
                    packet.Translator.ReadInt32("Value", i);
                    packet.Translator.ReadUInt32E<PowerType>("Power type", i);
                }

                packet.Translator.ReadXORBytes(powerTargetGUID, 4, 0);
                packet.Translator.ReadInt32("Current Health");
                packet.Translator.ReadXORByte(powerTargetGUID, 6);
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORByte(powerTargetGUID, 2);
                packet.Translator.WriteGuid("Power Target GUID", powerTargetGUID);
            }

            packet.Translator.ReadXORBytes(target, 2, 1);
            packet.Translator.ReadXORByte(caster, 1);
            packet.Translator.ReadXORBytes(target, 4, 7);
            packet.Translator.ReadXORByte(caster, 6);
            packet.Translator.ReadXORByte(target, 5);
            packet.Translator.ReadXORBytes(caster, 2, 3);
            packet.Translator.ReadXORByte(target, 6);
            packet.Translator.ReadXORBytes(caster, 0, 4, 5);
            packet.Translator.ReadXORBytes(target, 3, 0);

            packet.Translator.WriteGuid("CasterGUID", caster);
            packet.Translator.WriteGuid("TargetGUID", target);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleRandom(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var powerGUID = new byte[8];

            packet.Translator.ReadBit("Critical");
            guid1[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 4, 2, 7, 0);
            var bit14 = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 6, 5);
            guid1[7] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 0, 2, 3);
            var hasPowerData = packet.Translator.ReadBit();
            var bit24 = packet.Translator.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 0, 4, 6, 2, 5);
                powerCount = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 1, 7, 3);
            }
            packet.Translator.StartBitStream(guid1, 6, 4);
            guid2[1] = packet.Translator.ReadBit();
            packet.Translator.ResetBitReader();

            packet.Translator.ReadXORByte(guid2, 4);
            if (bit24)
                packet.Translator.ReadSingle("float24");

            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadInt32("int2C");
            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadInt32("Current health");
                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadXORByte(powerGUID, 3);
                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                    packet.Translator.ReadInt32("Value", i);
                }

                packet.Translator.ReadXORBytes(powerGUID, 0, 1, 7, 2, 4, 6, 5);
                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadXORBytes(guid2, 6, 0);
            packet.Translator.ReadXORBytes(guid1, 1, 3, 7);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 2);

            if (bit14)
                packet.Translator.ReadSingle("float10");

            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadInt32("Overheal");
            packet.Translator.WriteGuid("GUID1", guid1);
            packet.Translator.WriteGuid("GUID2", guid2);
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var powerGUID = new byte[8];
            var casterGUID = new byte[8];

            casterGUID[7] = packet.Translator.ReadBit();

            var hasPowerData = packet.Translator.ReadBit();

            packet.Translator.StartBitStream(casterGUID, 3, 5);

            var bits34 = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 4, 6, 7, 1, 0, 2);
                bits34 = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 5, 3);
            }

            casterGUID[6] = packet.Translator.ReadBit();
            targetGUID[1] = packet.Translator.ReadBit();
            var bits58 = packet.Translator.ReadBits(21);
            packet.Translator.StartBitStream(casterGUID, 2, 1);
            packet.Translator.StartBitStream(targetGUID, 2, 4);
            casterGUID[4] = packet.Translator.ReadBit();

            var bit8 = new bool[bits58];
            var hasSpellProto = new bool[bits58];
            var hasAbsorb = new bool[bits58];
            var hasOverDamage = new bool[bits58];

            for (var i = 0; i < bits58; ++i)
            {
                packet.Translator.ReadBit(); // fake bit

                hasOverDamage[i] = !packet.Translator.ReadBit();
                hasAbsorb[i] = !packet.Translator.ReadBit();
                hasSpellProto[i] = !packet.Translator.ReadBit();
                bit8[i] = !packet.Translator.ReadBit();
            }

            casterGUID[0] = packet.Translator.ReadBit();

            packet.Translator.StartBitStream(targetGUID, 0, 7, 3, 6, 5);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 7);

            for (var i = 0; i < bits58; ++i)
            {
                if (bit8[i])
                    packet.Translator.ReadInt32("bit8", i);

                var aura = packet.Translator.ReadUInt32E<AuraType>("Aura Type", i);

                if (hasOverDamage[i])
                    packet.Translator.ReadInt32("Over damage", i);

                if (hasAbsorb[i])
                    packet.Translator.ReadInt32("Absorb", i);

                packet.Translator.ReadInt32("Damage", i);

                if (hasSpellProto[i])
                    packet.Translator.ReadUInt32("Spell Proto", i);
            }

            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 2);

            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(powerGUID, 0);
                packet.Translator.ReadXORByte(powerGUID, 3);
                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadXORByte(powerGUID, 5);
                packet.Translator.ReadXORByte(powerGUID, 2);
                packet.Translator.ReadInt32("Current health");

                for (var i = 0; i < bits34; ++i)
                {
                    packet.Translator.ReadInt32("Value", i);
                    packet.Translator.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                }

                packet.Translator.ReadXORByte(powerGUID, 6);
                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 5);

            packet.Translator.ReadInt32<SpellId>("Spell ID");

            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 6);

            packet.Translator.WriteGuid("Target GUID", targetGUID);
            packet.Translator.WriteGuid("Caster GUID", casterGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var AttackerGUID = new byte[8];
            var VictimGUID = new byte[8];

            packet.Translator.StartBitStream(VictimGUID, 6, 1);
            packet.Translator.StartBitStream(AttackerGUID, 7, 5);
            VictimGUID[2] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(AttackerGUID, 2, 1);
            VictimGUID[0] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(AttackerGUID, 4, 0);
            packet.Translator.StartBitStream(VictimGUID, 4, 7, 5);
            AttackerGUID[3] = packet.Translator.ReadBit();
            VictimGUID[3] = packet.Translator.ReadBit();
            AttackerGUID[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(AttackerGUID, 6);
            packet.Translator.ReadXORByte(AttackerGUID, 2);
            packet.Translator.ReadXORByte(AttackerGUID, 0);
            packet.Translator.ReadXORByte(VictimGUID, 5);
            packet.Translator.ReadXORByte(VictimGUID, 6);
            packet.Translator.ReadXORByte(VictimGUID, 0);
            packet.Translator.ReadXORByte(VictimGUID, 1);
            packet.Translator.ReadXORByte(AttackerGUID, 7);
            packet.Translator.ReadXORByte(AttackerGUID, 4);
            packet.Translator.ReadXORByte(VictimGUID, 7);
            packet.Translator.ReadXORByte(AttackerGUID, 3);
            packet.Translator.ReadXORByte(VictimGUID, 3);
            packet.Translator.ReadXORByte(VictimGUID, 2);
            packet.Translator.ReadXORByte(VictimGUID, 4);
            packet.Translator.ReadXORByte(AttackerGUID, 1);
            packet.Translator.ReadXORByte(AttackerGUID, 5);

            packet.Translator.WriteGuid("Attacker GUID", AttackerGUID);
            packet.Translator.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var AttackerGUID = new byte[8];
            var VictimGUID = new byte[8];

            AttackerGUID[0] = packet.Translator.ReadBit();
            VictimGUID[4] = packet.Translator.ReadBit();
            AttackerGUID[1] = packet.Translator.ReadBit();
            VictimGUID[7] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(AttackerGUID, 6, 3);

            packet.Translator.ReadBit("Unk bit");

            AttackerGUID[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(VictimGUID, 1, 0);
            AttackerGUID[7] = packet.Translator.ReadBit();

            VictimGUID[6] = packet.Translator.ReadBit();
            AttackerGUID[4] = packet.Translator.ReadBit();
            AttackerGUID[2] = packet.Translator.ReadBit();
            VictimGUID[3] = packet.Translator.ReadBit();
            VictimGUID[2] = packet.Translator.ReadBit();
            VictimGUID[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(VictimGUID, 2);
            packet.Translator.ReadXORByte(VictimGUID, 7);
            packet.Translator.ReadXORByte(AttackerGUID, 0);
            packet.Translator.ReadXORByte(VictimGUID, 5);
            packet.Translator.ReadXORByte(AttackerGUID, 5);
            packet.Translator.ReadXORByte(VictimGUID, 3);
            packet.Translator.ReadXORByte(AttackerGUID, 7);
            packet.Translator.ReadXORByte(AttackerGUID, 1);
            packet.Translator.ReadXORByte(AttackerGUID, 3);
            packet.Translator.ReadXORByte(VictimGUID, 0);
            packet.Translator.ReadXORByte(AttackerGUID, 4);
            packet.Translator.ReadXORByte(AttackerGUID, 6);
            packet.Translator.ReadXORByte(VictimGUID, 1);
            packet.Translator.ReadXORByte(VictimGUID, 6);
            packet.Translator.ReadXORByte(AttackerGUID, 2);
            packet.Translator.ReadXORByte(VictimGUID, 4);

            packet.Translator.WriteGuid("Attacker GUID", AttackerGUID);
            packet.Translator.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var guid = new Byte[8];

            var hitInfo = packet.Translator.ReadInt32E<SpellHitInfo>("HitInfo");
            packet.Translator.ReadPackedGuid("AttackerGUID");
            packet.Translator.ReadPackedGuid("TargetGUID");
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32("OverDamage");

            var subDmgCount = packet.Translator.ReadByte();

            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.Translator.ReadInt32("SchoolMask", i);
                packet.Translator.ReadSingle("Float Damage", i);
                packet.Translator.ReadInt32("Int Damage", i);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.Translator.ReadInt32("Damage Absorbed", i);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.Translator.ReadInt32("Damage Resisted", i);

            var state = packet.Translator.ReadByteE<VictimStates>("VictimState");
            packet.Translator.ReadInt32("Unk Attacker State 0");

            packet.Translator.ReadInt32<SpellId>("Melee Spell ID ");

            var block = 0;
            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                block = packet.Translator.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.Translator.ReadInt32("Rage Gained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.Translator.ReadInt32("Unk Attacker State 3 1");
                packet.Translator.ReadSingle("Unk Attacker State 3 2");
                packet.Translator.ReadSingle("Unk Attacker State 3 3");
                packet.Translator.ReadSingle("Unk Attacker State 3 4");
                packet.Translator.ReadSingle("Unk Attacker State 3 5");
                packet.Translator.ReadSingle("Unk Attacker State 3 6");
                packet.Translator.ReadSingle("Unk Attacker State 3 7");
                packet.Translator.ReadSingle("Unk Attacker State 3 8");
                packet.Translator.ReadSingle("Unk Attacker State 3 9");
                packet.Translator.ReadSingle("Unk Attacker State 3 10");
                packet.Translator.ReadSingle("Unk Attacker State 3 11");

                for (var i = 0; i < 2; ++i)
                {
                    packet.Translator.ReadSingle("Unk1 Float", i);
                    packet.Translator.ReadSingle("Unk2 Float", i);

                }

                packet.Translator.ReadInt32("Unk Attacker State 3 12");
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.Translator.ReadSingle("Unk Float");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK26))
            {
                packet.Translator.ReadInt32("Unk4");
                packet.Translator.ReadInt32("Player current HP");
                packet.Translator.ReadInt32("Unk3");

                packet.Translator.StartBitStream(guid, 4, 5, 6, 7, 0, 2, 3, 1);


                var counter = packet.Translator.ReadBits(21);

                packet.Translator.ReadXORByte(guid, 2);
                packet.Translator.ReadXORByte(guid, 7);
                packet.Translator.ReadXORByte(guid, 0);
                packet.Translator.ReadXORByte(guid, 3);
                packet.Translator.ReadXORByte(guid, 5);
                packet.Translator.ReadXORByte(guid, 4);

                for (var i = 0; i < counter; ++i)
                {
                    packet.Translator.ReadUInt32("unk14", i);
                    packet.Translator.ReadUInt32("unk6", i);
                }

                packet.Translator.ReadXORByte(guid, 1);
                packet.Translator.ReadXORByte(guid, 6);

                packet.Translator.WriteGuid("GUID", guid);
            }
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];
            var powerGUID = new byte[8];

            packet.Translator.StartBitStream(casterGUID, 2, 5, 0, 1);
            targetGUID[1] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            targetGUID[0] = packet.Translator.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 1, 0, 2, 5);
                powerCount = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 7, 3, 4, 6);
            }

            packet.Translator.StartBitStream(targetGUID, 3, 5);
            casterGUID[6] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(targetGUID, 4, 2, 7);
            casterGUID[3] = packet.Translator.ReadBit();
            targetGUID[6] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();

            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadInt32("Current Health");

                packet.Translator.ReadXORByte(powerGUID, 6);
                packet.Translator.ReadXORByte(powerGUID, 0);
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadXORByte(powerGUID, 2);
                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.ReadXORByte(powerGUID, 5);
                packet.Translator.ReadXORByte(powerGUID, 3);

                for (var i = 0; i < powerCount; i++)
                {
                    packet.Translator.ReadInt32("Power Value", i);
                    packet.Translator.ReadUInt32E<PowerType>("Power Type", i);
                }

                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadInt32("Attack power");

                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadXORByte(targetGUID, 3);
            packet.Translator.ReadInt32("Amount");
            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 0);
            packet.Translator.ReadXORByte(targetGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadUInt32E<PowerType>("Power Type");
            packet.Translator.ReadXORByte(targetGUID, 7);
            packet.Translator.ReadXORByte(targetGUID, 2);
            packet.Translator.ReadXORByte(targetGUID, 4);
            packet.Translator.ReadXORByte(targetGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(targetGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 0);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
            packet.Translator.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 3, 7, 2, 5, 4, 1, 0, 6);
            packet.Translator.ParseBitStream(guid, 4, 1, 0, 7, 2, 3, 6, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
