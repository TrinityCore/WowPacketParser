using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class CombatLogHandler
    {
        public static void ReadSpellNonMeleeDebugData(Packet packet, params object[] idx)
        {
            packet.ReadSingle("CritRoll", idx);
            packet.ReadSingle("CritNeeded", idx);
            packet.ReadSingle("HitRoll", idx);
            packet.ReadSingle("HitNeeded", idx);
            packet.ReadSingle("MissChance", idx);
            packet.ReadSingle("DodgeChance", idx);
            packet.ReadSingle("ParryChance", idx);
            packet.ReadSingle("BlockChance", idx);
            packet.ReadSingle("GlanceChance", idx);
            packet.ReadSingle("CrushChance", idx);
        }

        public static void ReadCombatLogContentTuning(Packet packet, params object[] idx)
        {
            packet.ReadByte("Type", idx);
            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);
            packet.ReadByte("TargetMinScalingLevel", idx);
            packet.ReadByte("TargetMaxScalingLevel", idx);
            packet.ReadInt16("PlayerLevelDelta", idx);
            packet.ReadSByte("TargetScalingLevelDelta", idx);
            packet.ReadUInt16("PlayerItemLevel", idx);
            packet.ReadUInt16("TargetItemLevel", idx);
            packet.ReadUInt16("ScalingHealthItemLevelCurveID", idx);
            packet.ReadByte("ScalesWithItemLevel", idx);
        }

        public static void ReadAttackRoundInfo(Packet packet, params object[] indexes)
        {
            var hitInfo = packet.ReadInt32E<SpellHitInfo>("HitInfo", indexes);

            packet.ReadPackedGuid128("AttackerGUID", indexes);
            packet.ReadPackedGuid128("TargetGUID", indexes);

            packet.ReadInt32("Damage", indexes);
            packet.ReadInt32("OriginalDamage", indexes);
            packet.ReadInt32("OverDamage", indexes);

            var subDmgCount = packet.ReadBool("HasSubDmg", indexes);
            if (subDmgCount)
            {
                packet.ReadInt32("SchoolMask", indexes);
                packet.ReadSingle("FloatDamage", indexes);
                packet.ReadInt32("IntDamage", indexes);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    packet.ReadInt32("DamageAbsorbed", indexes);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    packet.ReadInt32("DamageResisted", indexes);
            }

            packet.ReadByteE<VictimStates>("VictimState", indexes);
            packet.ReadInt32("AttackerState", indexes);

            packet.ReadInt32<SpellId>("MeleeSpellID", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.ReadInt32("BlockAmount", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("RageGained", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.ReadInt32("Unk Attacker State 3 1", indexes);
                packet.ReadSingle("Unk Attacker State 3 2", indexes);
                packet.ReadSingle("Unk Attacker State 3 3", indexes);
                packet.ReadSingle("Unk Attacker State 3 4", indexes);
                packet.ReadSingle("Unk Attacker State 3 5", indexes);
                packet.ReadSingle("Unk Attacker State 3 6", indexes);
                packet.ReadSingle("Unk Attacker State 3 7", indexes);
                packet.ReadSingle("Unk Attacker State 3 8", indexes);
                packet.ReadSingle("Unk Attacker State 3 9", indexes);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                {
                    for (var j = 0; j < 5; j++)
                    {
                        packet.ReadSingle("Unk Attacker State 3 10", j, indexes);
                        packet.ReadSingle("Unk Attacker State 3 11", j, indexes);
                    }
                }
                else
                {
                    packet.ReadSingle("Unk Attacker State 3 10", indexes);
                    packet.ReadSingle("Unk Attacker State 3 11", indexes);
                }
                packet.ReadInt32("Unk Attacker State 3 12", indexes);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.ReadSingle("Unk Float", indexes);

            ReadCombatLogContentTuning(packet, indexes, "ContentTuning");
        }

        public static void ReadContentTuningParams(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();

            packet.ReadUInt16("PlayerItemLevel", idx);
            packet.ReadInt16("PlayerLevelDelta", idx);
            packet.ReadUInt32("TargetItemLevel", idx);
            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);
            packet.ReadByte("TargetMinScalingLevel", idx);
            packet.ReadByte("TargetMaxScalingLevel", idx);
            packet.ReadSByte("TargetScalingLevelDelta", idx);
            packet.ReadBits("Type", 4, idx);
            packet.ReadBit("ScalesWithItemLevel", idx);
        }

        public static void ReadSpellSupportInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CasterGUID", idx);
            packet.ReadInt32<SpellId>("SpellID", idx);
            packet.ReadInt32("Amount", idx);
            packet.ReadSingle("Percentage", idx);
        }

        public static void ReadPeriodicAuraLogEffectData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Effect", idx);
            packet.ReadInt32("Amount", idx);
            packet.ReadInt32("OriginalDamage", idx);
            packet.ReadInt32("OverHealOrKill", idx);
            packet.ReadInt32("SchoolMaskOrPower", idx);
            packet.ReadInt32("AbsorbedOrAmplitude", idx);
            packet.ReadInt32("Resisted", idx);
            var supportInfosCount = packet.ReadUInt32("SupportInfosCount", idx);
            for (var i = 0; i < supportInfosCount; i++)
                ReadSpellSupportInfo(packet, "SupportInfo", i, idx);

            packet.ResetBitReader();
            packet.ReadBit("Crit", idx);
            var hasDebugData = packet.ReadBit("HasDebugInfo", idx);
            var hasContentTuning = packet.ReadBit("HasContentTuning", idx);
            if (hasContentTuning)
                ReadContentTuningParams(packet, idx, "ContentTuning");

            if (hasDebugData)
            {
                packet.ReadSingle("CritRollMade", idx);
                packet.ReadSingle("CritRollNeeded", idx);
            }
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var hasLogData = packet.ReadBit("HasLogData");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet);

            packet.ReadInt32("Size");

            ReadAttackRoundInfo(packet, "AttackRoundInfo");
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.ReadPackedGuid128("Me");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("CastID");

            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("SpellXSpellVisual");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OriginalDamage");
            packet.ReadInt32("OverKill");

            packet.ReadByte("SchoolMask");

            packet.ReadInt32("Absorbed");
            packet.ReadInt32("Resisted");
            packet.ReadInt32("ShieldBlock");

            packet.ResetBitReader();

            packet.ReadBit("Periodic");

            packet.ReadBitsE<AttackerStateFlags>("Flags", 7);

            var hasDebugData = packet.ReadBit("HasDebugData");
            var hasLogData = packet.ReadBit("HasLogData");
            var hasContentTuning = packet.ReadBit("HasContentTuning");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");

            if (hasDebugData)
                ReadSpellNonMeleeDebugData(packet, "DebugData");

            if (hasContentTuning)
                ReadContentTuningParams(packet, "ContentTuning");
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandleSpellPeriodicAuraLog720(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadPackedGuid128("CasterGUID");

            packet.ReadInt32<SpellId>("SpellID");

            var periodicAuraLogEffectCount = packet.ReadUInt32("PeriodicAuraLogEffectCount");

            packet.ResetBitReader();
            var hasLogData = packet.ReadBit("HasLogData");

            for (var i = 0; i < periodicAuraLogEffectCount; i++)
                ReadPeriodicAuraLogEffectData(packet, "PeriodicAuraLogEffectData", i);

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadPackedGuid128("CasterGUID");

            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("Health");
            packet.ReadInt32("OriginalHeal");
            packet.ReadInt32("OverHeal");
            packet.ReadInt32("Absorbed");

            packet.ResetBitReader();

            packet.ReadBit("Crit");
            var hasCritRollMade = packet.ReadBit("HasCritRollMade");
            var hasCritRollNeeded = packet.ReadBit("HasCritRollNeeded");
            var hasLogData = packet.ReadBit("HasLogData");
            var hasContentTuning = packet.ReadBit("HasContentTuning");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet);

            if (hasCritRollMade)
                packet.ReadSingle("CritRollMade");

            if (hasCritRollNeeded)
                packet.ReadSingle("CritRollNeeded");

            if (hasContentTuning)
                ReadContentTuningParams(packet, "ContentTuning");
        }

        [Parser(Opcode.SMSG_SPELL_EXECUTE_LOG)]
        public static void HandleSpellLogExecute(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");

            packet.ReadInt32<SpellId>("SpellID");

            var int16 = packet.ReadInt32("EffectsCount");
            for (var i = 0; i < int16; i++)
            {
                packet.ReadInt32("Effect", i);

                var int4 = packet.ReadInt32("PowerDrainTargetsCount", i);
                var int20 = packet.ReadInt32("ExtraAttacksTargetsCount", i);
                var int36 = packet.ReadInt32("DurabilityDamageTargetsCount", i);
                var int52 = packet.ReadInt32("GenericVictimTargetsCount", i);
                var int68 = packet.ReadInt32("TradeSkillTargetsCount", i);
                var int84 = packet.ReadInt32("FeedPetTargetsCount", i);

                // ClientSpellLogEffectPowerDrainParams
                for (var j = 0; j < int4; j++)
                {
                    packet.ReadPackedGuid128("Victim");
                    packet.ReadInt32("Points");
                    packet.ReadInt32("PowerType");
                    packet.ReadSingle("Amplitude");
                }

                // ClientSpellLogEffectExtraAttacksParams
                for (var j = 0; j < int20; j++)
                {
                    packet.ReadPackedGuid128("Victim", i, j);
                    packet.ReadInt32("NumAttacks", i, j);
                }

                // ClientSpellLogEffectDurabilityDamageParams
                for (var j = 0; j < int36; j++)
                {
                    packet.ReadPackedGuid128("Victim", i, j);
                    packet.ReadInt32<ItemId>("ItemID", i, j);
                    packet.ReadInt32("Amount", i, j);
                }

                // ClientSpellLogEffectGenericVictimParams
                for (var j = 0; j < int52; j++)
                    packet.ReadPackedGuid128("Victim", i, j);

                // ClientSpellLogEffectTradeSkillItemParams
                for (var j = 0; j < int68; j++)
                    packet.ReadInt32<ItemId>("ItemID", i, j);

                // ClientSpellLogEffectFeedPetParams
                for (var j = 0; j < int84; j++)
                    packet.ReadInt32<ItemId>("ItemID", i, j);
            }

            var bit160 = packet.ReadBit("HasLogData");
            if (bit160)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("TargetGUID");

            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadUInt32E<PowerType>("Type");

            packet.ReadInt32("Amount");
            packet.ReadInt32("OverEnergize");

            packet.ResetBitReader();

            var bit100 = packet.ReadBit("HasLogData");
            if (bit100)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");

            packet.ReadByteE<EnvironmentDamage>("Type");

            packet.ReadInt32("Amount");
            packet.ReadInt32("Resisted");
            packet.ReadInt32("Absorbed");

            packet.ResetBitReader();
            var bit76 = packet.ReadBit("HasLogData");
            if (bit76)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_PARTY_KILL_LOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadPackedGuid128("VictimGUID");
        }

        [Parser(Opcode.SMSG_PROC_RESIST)]
        public static void HandleProcResist(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("Target");

            packet.ReadInt32<SpellId>("SpellID");

            var bit20 = packet.ReadBit("HasRolled");    // unconfirmed order
            var bit32 = packet.ReadBit("HasNeeded");    // unconfirmed order

            if (bit20)
                packet.ReadSingle("Rolled");            // unconfirmed order

            if (bit32)
                packet.ReadSingle("Needed");            // unconfirmed order
        }

        [Parser(Opcode.SMSG_SPELL_ABSORB_LOG)]
        public static void HandleSpellAbsorbLog(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");
            packet.ReadPackedGuid128("Caster");

            packet.ReadInt32("InterruptedSpellID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("ShieldTargetGUID?");
            packet.ReadInt32("Absorbed");
            packet.ReadInt32("OriginalDamage"); // OriginalDamage (before HitResult -> BeforeCrit and Armor etc)

            packet.ResetBitReader();
            packet.ReadBit("UnkBit");
            var hasLogData = packet.ReadBit("HasLogData");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_DAMAGE_SHIELD)]
        public static void ReadSpellDamageShield(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker");
            packet.ReadPackedGuid128("Defender");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("TotalDamage");
            packet.ReadInt32("OriginalDamage");
            packet.ReadInt32("OverKill");
            packet.ReadInt32("SchoolMask");
            packet.ReadInt32("LogAbsorbed");

            packet.ResetBitReader();

            var hasLogData = packet.ReadBit("HasLogData");
            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_ABSORB_LOG)]
        public static void HandleSpellHealAbsorbLog(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("AbsorbCaster");
            packet.ReadPackedGuid128("Healer");
            packet.ReadInt32("AbsorbSpellID");
            packet.ReadInt32("AbsorbedSpellID");
            packet.ReadInt32("Absorbed");
            packet.ReadInt32("OriginalHeal");
            var hasContentTuning = packet.ReadBit("HasContentTuning");

            if (hasContentTuning)
                ReadContentTuningParams(packet, "ContentTuning");
        }

        [Parser(Opcode.SMSG_SPELL_INSTAKILL_LOG)]
        public static void HandleSpellInstakillLog(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("Caster");
            packet.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_SPELL_INTERRUPT_LOG)]
        public static void HandleSpellInterruptLog(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("Victim");

            packet.ReadInt32<SpellId>("InterruptedSpellID");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_SPELL_MISS_LOG)]
        public static void HandleSpellMissLog(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("Caster");

            var spellLogMissEntryCount = packet.ReadInt32("SpellLogMissEntryCount");
            for (int i = 0; i < spellLogMissEntryCount; i++)
            {
                packet.ReadPackedGuid128("Victim", i);
                packet.ReadByte("MissReason", i);

                packet.ResetBitReader();

                var hasSpellLogMissDebug = packet.ReadBit("HasSpellLogMissDebug", i);
                if (hasSpellLogMissDebug)
                {
                    packet.ReadSingle("HitRoll", i);
                    packet.ReadSingle("HitRollNeededHitRollNeeded", i);
                }
            }
        }

        [Parser(Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE)]
        public static void HandleSpellOrDamageImmune(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("VictimGUID");

            packet.ReadInt32<SpellId>("SpellID");

            packet.ReadBit("IsPeriodic");
        }

        [Parser(Opcode.CMSG_SET_ADVANCED_COMBAT_LOGGING)]
        public static void HandleSetAdvancedCombatLogging(Packet packet)
        {
            packet.ReadBit("Enable");
        }
    }
}
