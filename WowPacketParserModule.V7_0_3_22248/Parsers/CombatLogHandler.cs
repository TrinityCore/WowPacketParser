using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class CombatLogHandler
    {
        public static void ReadAttackRoundInfo(Packet packet, params object[] indexes)
        {
            var hitInfo = packet.Translator.ReadInt32E<SpellHitInfo>("HitInfo", indexes);

            packet.Translator.ReadPackedGuid128("AttackerGUID", indexes);
            packet.Translator.ReadPackedGuid128("TargetGUID", indexes);

            packet.Translator.ReadInt32("Damage", indexes);
            packet.Translator.ReadInt32("OverDamage", indexes);

            var subDmgCount = packet.Translator.ReadBool("HasSubDmg", indexes);
            if (subDmgCount)
            {
                packet.Translator.ReadInt32("SchoolMask", indexes);
                packet.Translator.ReadSingle("FloatDamage", indexes);
                packet.Translator.ReadInt32("IntDamage", indexes);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    packet.Translator.ReadInt32("DamageAbsorbed", indexes);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    packet.Translator.ReadInt32("DamageResisted", indexes);
            }

            packet.Translator.ReadByteE<VictimStates>("VictimState", indexes);
            packet.Translator.ReadInt32("AttackerState", indexes);

            packet.Translator.ReadInt32<SpellId>("MeleeSpellID", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.Translator.ReadInt32("BlockAmount", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.Translator.ReadInt32("RageGained", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.Translator.ReadInt32("Unk Attacker State 3 1", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 2", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 3", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 4", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 5", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 6", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 7", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 8", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 9", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 10", indexes);
                packet.Translator.ReadSingle("Unk Attacker State 3 11", indexes);
                packet.Translator.ReadInt32("Unk Attacker State 3 12", indexes);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.Translator.ReadSingle("Unk Float", indexes);

            ReadSandboxScalingData(packet, indexes, "SandboxScalingData");
        }

        public static void ReadSandboxScalingData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadByte("Type", idx);
            packet.Translator.ReadByte("TargetLevel", idx);
            packet.Translator.ReadByte("Expansion", idx);
            packet.Translator.ReadByte("Class", idx);
            packet.Translator.ReadByte("TargetMinScalingLevel", idx);
            packet.Translator.ReadByte("TargetMaxScalingLevel", idx);
            packet.Translator.ReadInt16("PlayerLevelDelta", idx);
            packet.Translator.ReadSByte("TargetScalingLevelDelta", idx);
        }

        public static void ReadPeriodicAuraLogEffectData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Effect", idx);
            packet.Translator.ReadInt32("Amount", idx);
            packet.Translator.ReadInt32("OverHealOrKill", idx);
            packet.Translator.ReadInt32("SchoolMaskOrPower", idx);
            packet.Translator.ReadInt32("AbsorbedOrAmplitude", idx);
            packet.Translator.ReadInt32("Resisted", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Crit", idx);
            var hasDebugData = packet.Translator.ReadBit("HasPeriodicAuraLogEffectDebugInfo", idx);
            var hasSandboxScaling = packet.Translator.ReadBit("HasSandboxScaling", idx);

            if (hasDebugData)
            {
                packet.Translator.ReadSingle("CritRollMade", idx);
                packet.Translator.ReadSingle("CritRollNeeded", idx);
            }

            if (hasSandboxScaling)
                ReadSandboxScalingData(packet, idx, "SandboxScalingData");
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Me");
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadPackedGuid128("CastID");

            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32("OverKill");

            packet.Translator.ReadByte("SchoolMask");

            packet.Translator.ReadInt32("ShieldBlock");
            packet.Translator.ReadInt32("Resisted");
            packet.Translator.ReadInt32("Absorbed");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Periodic");

            packet.Translator.ReadBitsE<AttackerStateFlags>("Flags", 7);

            var hasDebugData = packet.Translator.ReadBit("HasDebugData");
            var hasLogData = packet.Translator.ReadBit("HasLogData");
            var hasSandboxScaling = packet.Translator.ReadBit("HasSandboxScaling");

            if (hasDebugData)
            {
                packet.Translator.ReadSingle("CritRoll");
                packet.Translator.ReadSingle("CritNeeded");
                packet.Translator.ReadSingle("HitRoll");
                packet.Translator.ReadSingle("HitNeeded");
                packet.Translator.ReadSingle("MissChance");
                packet.Translator.ReadSingle("DodgeChance");
                packet.Translator.ReadSingle("ParryChance");
                packet.Translator.ReadSingle("BlockChance");
                packet.Translator.ReadSingle("GlanceChance");
                packet.Translator.ReadSingle("CrushChance");
            }

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");

            if (hasSandboxScaling)
                ReadSandboxScalingData(packet, "SandboxScalingData");
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var hasLogData = packet.Translator.ReadBit("HasLogData");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet);

            packet.Translator.ReadInt32("Size");

            ReadAttackRoundInfo(packet, "AttackRoundInfo");
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandleSpellPeriodicAuraLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
            packet.Translator.ReadPackedGuid128("CasterGUID");

            packet.Translator.ReadInt32<SpellId>("SpellID");

            var periodicAuraLogEffectCount = packet.Translator.ReadInt32("PeriodicAuraLogEffectCount");
            for (var i = 0; i < periodicAuraLogEffectCount; i++)
                ReadPeriodicAuraLogEffectData(packet, "PeriodicAuraLogEffectData");

            packet.Translator.ResetBitReader();

            var hasLogData = packet.Translator.ReadBit("HasLogData");
            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
            packet.Translator.ReadPackedGuid128("CasterGUID");

            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("Health");
            packet.Translator.ReadInt32("OverHeal");
            packet.Translator.ReadInt32("Absorbed");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Crit");
            var hasCritRollMade = packet.Translator.ReadBit("HasCritRollMade");
            var hasCritRollNeeded = packet.Translator.ReadBit("HasCritRollNeeded");
            var hasLogData = packet.Translator.ReadBit("HasLogData");
            var hasSandboxScaling = packet.Translator.ReadBit("HasLogData");

            if (hasCritRollMade)
                packet.Translator.ReadSingle("CritRollMade");

            if (hasCritRollNeeded)
                packet.Translator.ReadSingle("CritRollNeeded");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet);


            if (hasSandboxScaling)
                ReadSandboxScalingData(packet, "SandboxScalingData");
        }
    }
}
