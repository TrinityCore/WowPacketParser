using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class CombatLogHandler
    {
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
                V10_0_0_46181.Parsers.CombatLogHandler.ReadSpellSupportInfo(packet, "SupportInfo", i, idx);

            packet.ResetBitReader();
            packet.ReadBit("Crit", idx);
            var hasDebugData = packet.ReadBit("HasDebugInfo", idx);
            var hasContentTuning = packet.ReadBit("HasContentTuning", idx);

            if (hasDebugData)
            {
                packet.ReadSingle("CritRollMade", idx);
                packet.ReadSingle("CritRollNeeded", idx);
            }

            if (hasContentTuning)
                V9_0_1_36216.Parsers.CombatLogHandler.ReadContentTuningParams(packet, idx, "ContentTuning");
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleSpellPeriodicAuraLog(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadPackedGuid128("CasterGUID");

            packet.ReadInt32<SpellId>("SpellID");

            var periodicAuraLogEffectCount = packet.ReadUInt32("PeriodicAuraLogEffectCount");

            for (var i = 0; i < periodicAuraLogEffectCount; i++)
                ReadPeriodicAuraLogEffectData(packet, "PeriodicAuraLogEffectData", i);

            packet.ResetBitReader();
            var hasLogData = packet.ReadBit("HasLogData");

            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG, ClientVersionBuild.V12_1_0_69214)]
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
                V10_0_0_46181.Parsers.CombatLogHandler.ReadSpellSupportInfo(packet, "SupportInfo", i);

            packet.ResetBitReader();
            packet.ReadBit("Crit");
            var hasCritRollMade = packet.ReadBit("HasCritRollMade");
            var hasCritRollNeeded = packet.ReadBit("HasCritRollNeeded");
            var hasLogData = packet.ReadBit("HasLogData");
            var hasContentTuning = packet.ReadBit("HasContentTuning");

            if (hasCritRollMade)
                packet.ReadSingle("CritRollMade");

            if (hasCritRollNeeded)
                packet.ReadSingle("CritRollNeeded");

            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet);

            if (hasContentTuning)
                V9_0_1_36216.Parsers.CombatLogHandler.ReadContentTuningParams(packet, "ContentTuning");
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_ABSORB_LOG, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleSpellHealAbsorbLog(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("AbsorbCaster");
            packet.ReadPackedGuid128("Healer");
            packet.ReadInt32<SpellId>("AbsorbedSpellID");
            packet.ReadInt32<SpellId>("AbsorbSpellID");
            packet.ReadInt32("Absorbed");
            packet.ReadInt32("OriginalHeal");

            packet.ResetBitReader();
            var hasLogData = packet.ReadBit("HasLogData");
            var hasContentTuning = packet.ReadBit("HasContentTuning");

            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");

            if (hasContentTuning)
                V9_0_1_36216.Parsers.CombatLogHandler.ReadContentTuningParams(packet, "ContentTuning");
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, ClientVersionBuild.V12_1_0_69214)]
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
            packet.ReadInt32<SpellId>("ReflectingSpellID");
            packet.ReadInt32E<AttackerStateFlags>("Flags");
            var worldTextViewersCount = packet.ReadUInt32("WorldTextViewersCount");
            var supportInfosCount = packet.ReadUInt32("SupportInfosCount");

            for (var i = 0; i < worldTextViewersCount; i++)
                V10_0_0_46181.Parsers.CombatLogHandler.ReadCombatWorldTextViewerInfo(packet, "WorldTextViewer", i);

            for (var i = 0; i < supportInfosCount; i++)
                V10_0_0_46181.Parsers.CombatLogHandler.ReadSpellSupportInfo(packet, "SupportInfo", i);

            packet.ResetBitReader();
            packet.ReadBit("Periodic");
            var hasDebugData = packet.ReadBit("HasDebugData");
            var hasLogData = packet.ReadBit("HasLogData");
            var hasContentTuning = packet.ReadBit("HasContentTuning");

            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");

            if (hasDebugData)
                V8_0_1_27101.Parsers.CombatLogHandler.ReadSpellNonMeleeDebugData(packet, "DebugData");

            if (hasContentTuning)
                V9_0_1_36216.Parsers.CombatLogHandler.ReadContentTuningParams(packet, "ContentTuning");
        }

        [Parser(Opcode.SMSG_SPELL_MISS_LOG, ClientVersionBuild.V12_1_0_69214)]
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

            packet.ResetBitReader();
            packet.ReadBit("HideFromCombatLog");
        }
    }
}
