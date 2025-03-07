using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class CombatLogHandler
    {
        public static void ReadSpellSupportInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CasterGUID", idx);
            packet.ReadInt32<SpellId>("SpellID", idx);
            packet.ReadInt32("Amount", idx);
            packet.ReadSingle("Percentage", idx);
        }

        public static void ReadCombatWorldTextViewerInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("ViewerGUID", idx);
            packet.ResetBitReader();
            var hasColorType = packet.ReadBit("HasColorType", idx);
            var hasScaleType = packet.ReadBit("HasScaleType", idx);

            if (hasColorType)
                packet.ReadByte("ColorType", idx);

            if (hasScaleType)
                packet.ReadByte("ScaleType", idx);
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
                V9_0_1_36216.Parsers.CombatLogHandler.ReadContentTuningParams(packet, idx, "ContentTuning");

            if (hasDebugData)
            {
                packet.ReadSingle("CritRollMade", idx);
                packet.ReadSingle("CritRollNeeded", idx);
            }
        }

        [Parser(Opcode.SMSG_SPELL_ABSORB_LOG, ClientVersionBuild.V10_1_5_50232)]
        public static void HandleSpellAbsorbLog(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker");
            packet.ReadPackedGuid128("Victim");

            packet.ReadInt32<SpellId>("AbsorbedSpellID");
            packet.ReadInt32<SpellId>("AbsorbSpellID");
            packet.ReadPackedGuid128("Caster");
            packet.ReadInt32("Absorbed");
            packet.ReadInt32("OriginalDamage"); // OriginalDamage (before HitResult -> BeforeCrit and Armor etc)

            var supportInfosCount = packet.ReadUInt32();
            for (var i = 0; i < supportInfosCount; i++)
                ReadSpellSupportInfo(packet, "Supporters", i);

            packet.ResetBitReader();
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_3_0_33062))
                packet.ReadBit("Crit");

            var bit100 = packet.ReadBit("HasLogData");
            if (bit100)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_ABSORB_LOG, ClientVersionBuild.V10_1_5_50232)]
        public static void HandleSpellHealAbsorbLog(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker");
            packet.ReadPackedGuid128("Victim");
            packet.ReadInt32<SpellId>("AbsorbedSpellID");
            packet.ReadInt32<SpellId>("AbsorbSpellID");
            packet.ReadPackedGuid128("Caster");
            packet.ReadInt32("Absorbed");
            packet.ReadInt32("OriginalHeal");

            var supportInfosCount = packet.ReadUInt32("SupportInfosCount");
            for (var i = 0; i < supportInfosCount; i++)
                ReadSpellSupportInfo(packet, "SupportInfo", i);

            packet.ResetBitReader();
            packet.ReadBit("Unk");
            var hasLogData = packet.ReadBit("HasLogData");
            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG, ClientVersionBuild.V10_1_5_50232)]
        public static void HandleSpellPeriodicAuraLog(Packet packet)
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
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG, ClientVersionBuild.V10_1_5_50232)]
        public static void HandleSpellHealLog(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("Health");
            packet.ReadInt32("OriginalHeal");
            packet.ReadInt32("OverHeal");
            packet.ReadInt32("Absorbed");
            var supportInfosCount = packet.ReadUInt32("SupportInfosCount");
            for (var i = 0; i < supportInfosCount; i++)
                ReadSpellSupportInfo(packet, "SupportInfo", i);

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
                V9_0_1_36216.Parsers.CombatLogHandler.ReadContentTuningParams(packet, "ContentTuning");
        }


        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, ClientVersionBuild.V10_1_5_50232)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.ReadPackedGuid128("Me");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("CastID");
            packet.ReadInt32<SpellId>("SpellID");
            V9_0_1_36216.Parsers.SpellHandler.ReadSpellCastVisual(packet, "Visual");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OriginalDamage");
            packet.ReadInt32("OverKill");
            packet.ReadByte("SchoolMask");
            packet.ReadInt32("Absorbed");
            packet.ReadInt32("Resisted");
            packet.ReadInt32("ShieldBlock");
            var worldTextViewersCount = packet.ReadUInt32("WorldTextViewersCount");
            var supportInfosCount = packet.ReadUInt32("SupportInfosCount");
            for (var i = 0; i < supportInfosCount; i++)
                ReadSpellSupportInfo(packet, "SupportInfo", i);

            packet.ResetBitReader();
            packet.ReadBit("Periodic");
            packet.ReadBitsE<AttackerStateFlags>("Flags", 7);
            var hasDebugData = packet.ReadBit("HasDebugData");
            var hasLogData = packet.ReadBit("HasLogData");
            var hasContentTuning = packet.ReadBit("HasContentTuning");

            for (var i = 0; i < worldTextViewersCount; i++)
                ReadCombatWorldTextViewerInfo(packet, "WorldTextViewer", i);

            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");

            if (hasDebugData)
                V8_0_1_27101.Parsers.CombatLogHandler.ReadSpellNonMeleeDebugData(packet, "DebugData");

            if (hasContentTuning)
                V9_0_1_36216.Parsers.CombatLogHandler.ReadContentTuningParams(packet, "ContentTuning");
        }

        [Parser(Opcode.SMSG_ATTACK_SWING_LANDED_LOG, ClientVersionBuild.V10_1_5_50232)]
        public static void HandleAttackswingLandedLog(Packet packet)
        {
            var supportInfosCount = packet.ReadUInt32("SupportInfosCount");
            for (var i = 0; i < supportInfosCount; i++)
                ReadSpellSupportInfo(packet, "SupportInfo", i);

            var hasLogData = packet.ReadBit("HasLogData");
            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet);

            packet.ReadInt32("Size");

            V9_0_1_36216.Parsers.CombatLogHandler.ReadAttackRoundInfo(packet, "AttackRoundInfo");
        }
    }
}
