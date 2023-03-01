﻿using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class SpellHandler
    {
        public static void ReadLearnedSpellInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<SpellId>("SpellID", indexes);
            packet.ReadBit("IsFavorite", indexes);
            var hasField8 = packet.ReadBit();
            var hasSuperceded = packet.ReadBit();
            var hasTraitDefinition = packet.ReadBit();
            packet.ResetBitReader();

            if (hasField8)
                packet.ReadInt32("field_8", indexes);

            if (hasSuperceded)
                packet.ReadInt32<SpellId>("Superceded", indexes);

            if (hasTraitDefinition)
                packet.ReadInt32("TraitDefinitionID", indexes);
        }

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            var spellCount = packet.ReadUInt32();
            packet.ReadUInt32("SpecializationID");
            packet.ReadBit("SuppressMessaging");
            packet.ResetBitReader();

            for (var i = 0; i < spellCount; ++i)
                ReadLearnedSpellInfo(packet, "ClientLearnedSpellData", i);
        }

        [Parser(Opcode.SMSG_SUPERCEDED_SPELLS)]
        public static void HandleSupercededSpells(Packet packet)
        {
            var spellCount = packet.ReadUInt32();

            for (var i = 0; i < spellCount; ++i)
                ReadLearnedSpellInfo(packet, "ClientLearnedSpellData", i);
        }

        [Parser(Opcode.SMSG_SPELL_EMPOWER_SET_STAGE)]
        public static void HandleEmpowerSetStage(Packet packet)
        {
            packet.ReadPackedGuid128("CastGUID");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadUInt32("StageId");
        }

        [Parser(Opcode.SMSG_SPELL_EMPOWER_UPDATE)]
        public static void HandleEmpowerUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("CastGUID");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32("TimeRemaining");
            var stageCount = packet.ReadUInt32("RemainingStageCount");
            packet.ResetBitReader();
            packet.ReadBit("UnkBit");

            for (int i = 0; i < stageCount; i++)
                packet.ReadUInt32("Duration", "Stage", i);
        }

        [Parser(Opcode.CMSG_SPELL_EMPOWER_RELEASE)]
        [Parser(Opcode.CMSG_SPELL_EMPOWER_RESTART)]
        public static void HandleEmpowerRelease(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellId");
        }

   
        [Parser(Opcode.CMSG_SET_EMPOWER_MIN_HOLD_STAGE_PERCENT)]
        public static void HandleHolStagePct(Packet packet)
        {
            packet.ReadSingle("MinHoldPct");
        }

        [Parser(Opcode.SMSG_SPELL_EMPOWER_START)]
        public static void HandleEmpowerStart(Packet packet)
        {
            packet.ReadPackedGuid128("CastGUID");
            packet.ReadPackedGuid128("CasterGUID");

            var playersCount = packet.ReadUInt32("TargetsCount");
            packet.ReadInt32<SpellId>("SpellId");

            V9_0_1_36216.Parsers.SpellHandler.ReadSpellCastVisual(packet, "Visual");

            packet.ReadUInt32("Duration");
            packet.ReadUInt32("FirstStageDuration");
            packet.ReadUInt32("FinalStageDuration");
            var stageCount = packet.ReadUInt32("StageCount");

            for (var i = 0; i < playersCount; ++i)
                packet.ReadPackedGuid128("Guid", "Targets", i);

            for (var i = 0; i < stageCount; ++i)
                packet.ReadUInt32("Duration", "Stage", i);

            packet.ResetBitReader();
            var hasInterruptImmunities = packet.ReadBit("HasInterruptImmunities");
            var hasHealPrediction = packet.ReadBit("HasHealPrediction");

            if (hasInterruptImmunities)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellChannelStartInterruptImmunities(packet, "InterruptImmunities");

            if (hasHealPrediction)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellTargetedHealPrediction(packet, "HealPrediction");
        }
    }
}
