using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class CombatLogHandler
    {
        public static void ReadContentTuningParams(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();

            packet.ReadSingle("PlayerItemLevel", idx);
            packet.ReadSingle("TargetItemLevel", idx);
            packet.ReadInt16("PlayerLevelDelta", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49407))
                packet.ReadUInt32("ScalingHealthItemLevelCurveID", idx);
            else
                packet.ReadUInt16("ScalingHealthItemLevelCurveID", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_7_61491))
            {
                packet.ReadUInt32("Unused1117", idx);
                packet.ReadUInt32("ScalingHealthPrimaryStatCurveID", idx);
            }

            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_1_0_39185))
            {
                packet.ReadByte("TargetMinScalingLevel", idx);
                packet.ReadByte("TargetMaxScalingLevel", idx);
            }

            packet.ReadSByte("TargetScalingLevelDelta", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_2_36639))
                packet.ReadUInt32("Flags", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
            {
                packet.ReadInt32("PlayerContentTuningID", idx);
                packet.ReadInt32("TargetContentTuningID", idx);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_7_45114))
                packet.ReadInt32("TargetHealingContentTuningID", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_7_61491))
                packet.ReadSingle("PlayerPrimaryStatToExpectedRatio", idx);

            packet.ReadBits("Type", 4, idx);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_0_2_36639))
                packet.ReadBit("ScalesWithItemLevel", idx);
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

            packet.ResetBitReader();

            packet.ReadBit("Crit", idx);
            var hasDebugData = packet.ReadBit("HasPeriodicAuraLogEffectDebugInfo", idx);
            var hasContentTuning = packet.ReadBit("HasContentTuning", idx);

            if (hasContentTuning)
                ReadContentTuningParams(packet, idx, "ContentTuning");

            if (hasDebugData)
            {
                packet.ReadSingle("CritRollMade", idx);
                packet.ReadSingle("CritRollNeeded", idx);
            }
        }

        public static void ReadCombatLogContentTuning(Packet packet, params object[] idx)
        {
            packet.ReadByte("Type", idx);
            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_1_0_39185))
            {
                packet.ReadByte("TargetMinScalingLevel", idx);
                packet.ReadByte("TargetMaxScalingLevel", idx);
            }

            packet.ReadInt16("PlayerLevelDelta", idx);
            packet.ReadSByte("TargetScalingLevelDelta", idx);
            packet.ReadSingle("PlayerItemLevel", idx);
            packet.ReadSingle("TargetItemLevel", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49407))
                packet.ReadUInt32("ScalingHealthItemLevelCurveID", idx);
            else
                packet.ReadUInt16("ScalingHealthItemLevelCurveID", idx);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_0_2_36639))
                packet.ReadByte("ScalesWithItemLevel", idx);
            else
                packet.ReadUInt32("Flags", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
            {
                packet.ReadInt32("PlayerContentTuningID", idx);
                packet.ReadInt32("TargetContentTuningID", idx);
            }
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
                packet.ReadSingle("Unk Attacker State 3 10", indexes);
                packet.ReadSingle("Unk Attacker State 3 11", indexes);
                packet.ReadInt32("Unk Attacker State 3 12", indexes);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.ReadSingle("Unk Float", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                ReadContentTuningParams(packet, indexes, "ContentTuning");
            else
                ReadCombatLogContentTuning(packet, indexes, "ContentTuning");
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.ReadPackedGuid128("Me");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("CastID");

            packet.ReadInt32<SpellId>("SpellID");
            SpellHandler.ReadSpellCastVisual(packet, "Visual");
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
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");

            if (hasDebugData)
                V8_0_1_27101.Parsers.CombatLogHandler.ReadSpellNonMeleeDebugData(packet, "DebugData");

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
                ReadPeriodicAuraLogEffectData(packet, "PeriodicAuraLogEffectData");

            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");
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
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet);

            if (hasCritRollMade)
                packet.ReadSingle("CritRollMade");

            if (hasCritRollNeeded)
                packet.ReadSingle("CritRollNeeded");

            if (hasContentTuning)
                ReadContentTuningParams(packet, "ContentTuning");
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var hasLogData = packet.ReadBit("HasLogData");

            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet);

            packet.ReadInt32("Size");

            ReadAttackRoundInfo(packet, "AttackRoundInfo");
        }

        [Parser(Opcode.SMSG_ATTACK_SWING_LANDED_LOG)]
        public static void HandleAttackswingLandedLog(Packet packet)
        {
            V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet);

            packet.ReadInt32("Size");

            ReadAttackRoundInfo(packet, "AttackRoundInfo");
        }
    }
}
