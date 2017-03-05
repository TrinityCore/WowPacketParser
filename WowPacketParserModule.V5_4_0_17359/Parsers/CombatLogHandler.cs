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

            packet.ReadUInt32("Damage");
            packet.ReadUInt32("Resist");
            packet.ReadInt32E<AttackerStateFlags>("Attacker State Flags");
            packet.ReadUInt32("Blocked");
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadInt32("Overkill");
            packet.ReadUInt32("Absorb");
            packet.ReadByte("SchoolMask");

            var hasDebugOutput = packet.ReadBit("Has Debug Output");
            packet.StartBitStream(caster, 1, 7);
            target[1] = packet.ReadBit();

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
                bit2C = !packet.ReadBit();
                bit10 = !packet.ReadBit();
                bit1C = !packet.ReadBit();
                bit24 = !packet.ReadBit();
                bit20 = !packet.ReadBit();
                bit34 = !packet.ReadBit();
                bit18 = !packet.ReadBit();
                bit14 = !packet.ReadBit();
                bit28 = !packet.ReadBit();
                bit30 = !packet.ReadBit();
            }

            packet.ReadBit("bit44");
            packet.StartBitStream(target, 5, 0, 3, 6, 2);
            packet.StartBitStream(caster, 6, 5, 4);
            var hasPawerData = packet.ReadBit();

            var bits8C = 0u;
            if (hasPawerData)
            {
                packet.StartBitStream(powerTargetGUID, 1, 5, 7, 4, 6, 3);
                bits8C = packet.ReadBits(21);
                packet.StartBitStream(powerTargetGUID, 2, 0);
            }

            target[7] = packet.ReadBit();
            caster[3] = packet.ReadBit();
            target[4] = packet.ReadBit();
            packet.StartBitStream(caster, 0, 2);
            packet.ReadBit("bit50");
            packet.ResetBitReader();

            if (hasDebugOutput)
            {
                if (bit28)
                    packet.ReadSingle("float28");
                if (bit20)
                    packet.ReadSingle("float20");
                if (bit24)
                    packet.ReadSingle("float24");
                if (bit1C)
                    packet.ReadSingle("float1C");
                if (bit30)
                    packet.ReadSingle("float30");
                if (bit18)
                    packet.ReadSingle("float18");
                if (bit14)
                    packet.ReadSingle("float14");
                if (bit10)
                    packet.ReadSingle("float10");
                if (bit2C)
                    packet.ReadSingle("float2C");
                if (bit34)
                    packet.ReadSingle("float34");
            }

            packet.ReadXORByte(caster, 7);
            if (hasPawerData)
            {
                packet.ReadXORByte(powerTargetGUID, 3);
                packet.ReadInt32("Attack Power");
                packet.ReadXORBytes(powerTargetGUID, 5, 1, 7);
                for (var i = 0; i < bits8C; ++i)
                {
                    packet.ReadInt32("Value", i);
                    packet.ReadUInt32E<PowerType>("Power type", i);
                }

                packet.ReadXORBytes(powerTargetGUID, 4, 0);
                packet.ReadInt32("Current Health");
                packet.ReadXORByte(powerTargetGUID, 6);
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerTargetGUID, 2);
                packet.WriteGuid("Power Target GUID", powerTargetGUID);
            }

            packet.ReadXORBytes(target, 2, 1);
            packet.ReadXORByte(caster, 1);
            packet.ReadXORBytes(target, 4, 7);
            packet.ReadXORByte(caster, 6);
            packet.ReadXORByte(target, 5);
            packet.ReadXORBytes(caster, 2, 3);
            packet.ReadXORByte(target, 6);
            packet.ReadXORBytes(caster, 0, 4, 5);
            packet.ReadXORBytes(target, 3, 0);

            packet.WriteGuid("CasterGUID", caster);
            packet.WriteGuid("TargetGUID", target);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleRandom(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var powerGUID = new byte[8];

            packet.ReadBit("Critical");
            guid1[5] = packet.ReadBit();
            packet.StartBitStream(guid2, 4, 2, 7, 0);
            var bit14 = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            packet.StartBitStream(guid2, 6, 5);
            guid1[7] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            packet.StartBitStream(guid1, 0, 2, 3);
            var hasPowerData = packet.ReadBit();
            var bit24 = packet.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 0, 4, 6, 2, 5);
                powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 1, 7, 3);
            }
            packet.StartBitStream(guid1, 6, 4);
            guid2[1] = packet.ReadBit();
            packet.ResetBitReader();

            packet.ReadXORByte(guid2, 4);
            if (bit24)
                packet.ReadSingle("float24");

            packet.ReadXORByte(guid1, 6);
            packet.ReadInt32("int2C");
            if (hasPowerData)
            {
                packet.ReadInt32("Spell power");
                packet.ReadInt32("Current health");
                packet.ReadInt32("Attack power");
                packet.ReadXORByte(powerGUID, 3);
                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                    packet.ReadInt32("Value", i);
                }

                packet.ReadXORBytes(powerGUID, 0, 1, 7, 2, 4, 6, 5);
                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadXORBytes(guid2, 6, 0);
            packet.ReadXORBytes(guid1, 1, 3, 7);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadInt32("Damage");
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 2);

            if (bit14)
                packet.ReadSingle("float10");

            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 4);
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid2, 1);
            packet.ReadInt32("Overheal");
            packet.WriteGuid("GUID1", guid1);
            packet.WriteGuid("GUID2", guid2);
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var targetGUID = new byte[8];
            var powerGUID = new byte[8];
            var casterGUID = new byte[8];

            casterGUID[7] = packet.ReadBit();

            var hasPowerData = packet.ReadBit();

            packet.StartBitStream(casterGUID, 3, 5);

            var bits34 = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 4, 6, 7, 1, 0, 2);
                bits34 = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 5, 3);
            }

            casterGUID[6] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            var bits58 = packet.ReadBits(21);
            packet.StartBitStream(casterGUID, 2, 1);
            packet.StartBitStream(targetGUID, 2, 4);
            casterGUID[4] = packet.ReadBit();

            var bit8 = new bool[bits58];
            var hasSpellProto = new bool[bits58];
            var hasAbsorb = new bool[bits58];
            var hasOverDamage = new bool[bits58];

            for (var i = 0; i < bits58; ++i)
            {
                packet.ReadBit(); // fake bit

                hasOverDamage[i] = !packet.ReadBit();
                hasAbsorb[i] = !packet.ReadBit();
                hasSpellProto[i] = !packet.ReadBit();
                bit8[i] = !packet.ReadBit();
            }

            casterGUID[0] = packet.ReadBit();

            packet.StartBitStream(targetGUID, 0, 7, 3, 6, 5);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(casterGUID, 7);

            for (var i = 0; i < bits58; ++i)
            {
                if (bit8[i])
                    packet.ReadInt32("bit8", i);

                var aura = packet.ReadUInt32E<AuraType>("Aura Type", i);

                if (hasOverDamage[i])
                    packet.ReadInt32("Over damage", i);

                if (hasAbsorb[i])
                    packet.ReadInt32("Absorb", i);

                packet.ReadInt32("Damage", i);

                if (hasSpellProto[i])
                    packet.ReadUInt32("Spell Proto", i);
            }

            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(targetGUID, 2);

            if (hasPowerData)
            {
                packet.ReadXORByte(powerGUID, 0);
                packet.ReadXORByte(powerGUID, 3);
                packet.ReadInt32("Attack power");
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadXORByte(powerGUID, 2);
                packet.ReadInt32("Current health");

                for (var i = 0; i < bits34; ++i)
                {
                    packet.ReadInt32("Value", i);
                    packet.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                }

                packet.ReadXORByte(powerGUID, 6);
                packet.ReadXORByte(powerGUID, 7);
                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(casterGUID, 5);

            packet.ReadInt32<SpellId>("Spell ID");

            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 6);

            packet.WriteGuid("Target GUID", targetGUID);
            packet.WriteGuid("Caster GUID", casterGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var AttackerGUID = new byte[8];
            var VictimGUID = new byte[8];

            packet.StartBitStream(VictimGUID, 6, 1);
            packet.StartBitStream(AttackerGUID, 7, 5);
            VictimGUID[2] = packet.ReadBit();
            packet.StartBitStream(AttackerGUID, 2, 1);
            VictimGUID[0] = packet.ReadBit();
            packet.StartBitStream(AttackerGUID, 4, 0);
            packet.StartBitStream(VictimGUID, 4, 7, 5);
            AttackerGUID[3] = packet.ReadBit();
            VictimGUID[3] = packet.ReadBit();
            AttackerGUID[6] = packet.ReadBit();

            packet.ReadXORByte(AttackerGUID, 6);
            packet.ReadXORByte(AttackerGUID, 2);
            packet.ReadXORByte(AttackerGUID, 0);
            packet.ReadXORByte(VictimGUID, 5);
            packet.ReadXORByte(VictimGUID, 6);
            packet.ReadXORByte(VictimGUID, 0);
            packet.ReadXORByte(VictimGUID, 1);
            packet.ReadXORByte(AttackerGUID, 7);
            packet.ReadXORByte(AttackerGUID, 4);
            packet.ReadXORByte(VictimGUID, 7);
            packet.ReadXORByte(AttackerGUID, 3);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(VictimGUID, 2);
            packet.ReadXORByte(VictimGUID, 4);
            packet.ReadXORByte(AttackerGUID, 1);
            packet.ReadXORByte(AttackerGUID, 5);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var AttackerGUID = new byte[8];
            var VictimGUID = new byte[8];

            AttackerGUID[0] = packet.ReadBit();
            VictimGUID[4] = packet.ReadBit();
            AttackerGUID[1] = packet.ReadBit();
            VictimGUID[7] = packet.ReadBit();
            packet.StartBitStream(AttackerGUID, 6, 3);

            packet.ReadBit("Unk bit");

            AttackerGUID[5] = packet.ReadBit();
            packet.StartBitStream(VictimGUID, 1, 0);
            AttackerGUID[7] = packet.ReadBit();

            VictimGUID[6] = packet.ReadBit();
            AttackerGUID[4] = packet.ReadBit();
            AttackerGUID[2] = packet.ReadBit();
            VictimGUID[3] = packet.ReadBit();
            VictimGUID[2] = packet.ReadBit();
            VictimGUID[5] = packet.ReadBit();

            packet.ReadXORByte(VictimGUID, 2);
            packet.ReadXORByte(VictimGUID, 7);
            packet.ReadXORByte(AttackerGUID, 0);
            packet.ReadXORByte(VictimGUID, 5);
            packet.ReadXORByte(AttackerGUID, 5);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(AttackerGUID, 7);
            packet.ReadXORByte(AttackerGUID, 1);
            packet.ReadXORByte(AttackerGUID, 3);
            packet.ReadXORByte(VictimGUID, 0);
            packet.ReadXORByte(AttackerGUID, 4);
            packet.ReadXORByte(AttackerGUID, 6);
            packet.ReadXORByte(VictimGUID, 1);
            packet.ReadXORByte(VictimGUID, 6);
            packet.ReadXORByte(AttackerGUID, 2);
            packet.ReadXORByte(VictimGUID, 4);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var guid = new Byte[8];

            var hitInfo = packet.ReadInt32E<SpellHitInfo>("HitInfo");
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

            var state = packet.ReadByteE<VictimStates>("VictimState");
            packet.ReadInt32("Unk Attacker State 0");

            packet.ReadInt32<SpellId>("Melee Spell ID ");

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

                for (var i = 0; i < 2; ++i)
                {
                    packet.ReadSingle("Unk1 Float", i);
                    packet.ReadSingle("Unk2 Float", i);

                }

                packet.ReadInt32("Unk Attacker State 3 12");
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.ReadSingle("Unk Float");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK26))
            {
                packet.ReadInt32("Unk4");
                packet.ReadInt32("Player current HP");
                packet.ReadInt32("Unk3");

                packet.StartBitStream(guid, 4, 5, 6, 7, 0, 2, 3, 1);


                var counter = packet.ReadBits(21);

                packet.ReadXORByte(guid, 2);
                packet.ReadXORByte(guid, 7);
                packet.ReadXORByte(guid, 0);
                packet.ReadXORByte(guid, 3);
                packet.ReadXORByte(guid, 5);
                packet.ReadXORByte(guid, 4);

                for (var i = 0; i < counter; ++i)
                {
                    packet.ReadUInt32("unk14", i);
                    packet.ReadUInt32("unk6", i);
                }

                packet.ReadXORByte(guid, 1);
                packet.ReadXORByte(guid, 6);

                packet.WriteGuid("GUID", guid);
            }
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];
            var powerGUID = new byte[8];

            packet.StartBitStream(casterGUID, 2, 5, 0, 1);
            targetGUID[1] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 1, 0, 2, 5);
                powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 7, 3, 4, 6);
            }

            packet.StartBitStream(targetGUID, 3, 5);
            casterGUID[6] = packet.ReadBit();
            packet.StartBitStream(targetGUID, 4, 2, 7);
            casterGUID[3] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();

            if (hasPowerData)
            {
                packet.ReadInt32("Spell power");
                packet.ReadInt32("Current Health");

                packet.ReadXORByte(powerGUID, 6);
                packet.ReadXORByte(powerGUID, 0);
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 2);
                packet.ReadXORByte(powerGUID, 7);
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadXORByte(powerGUID, 3);

                for (var i = 0; i < powerCount; i++)
                {
                    packet.ReadInt32("Power Value", i);
                    packet.ReadUInt32E<PowerType>("Power Type", i);
                }

                packet.ReadXORByte(powerGUID, 4);
                packet.ReadInt32("Attack power");

                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadXORByte(targetGUID, 3);
            packet.ReadInt32("Amount");
            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadUInt32E<PowerType>("Power Type");
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(casterGUID, 0);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 3, 7, 2, 5, 4, 1, 0, 6);
            packet.ParseBitStream(guid, 4, 1, 0, 7, 2, 3, 6, 5);

            packet.WriteGuid("Guid", guid);
        }
    }
}
