using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellHealPrediction(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Points", idx);
            packet.ReadByte("Type", idx);
            packet.ReadPackedGuid128("BeaconGUID", idx);
        }

        public static void ReadSpellChannelStartInterruptImmunities(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SchoolImmunities", idx);
            packet.ReadInt32("Immunities", idx);
        }

        public static void ReadSpellTargetedHealPrediction(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("TargetGUID", idx);
            ReadSpellHealPrediction(packet, idx, "Predict");
        }

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

        [Parser(Opcode.SMSG_SPELL_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("SpellXSpellVisual");
            packet.ReadInt32("ChannelDuration");

            var hasInterruptImmunities = packet.ReadBit("HasInterruptImmunities");
            var hasHealPrediction = packet.ReadBit("HasHealPrediction");

            if (hasInterruptImmunities)
                ReadSpellChannelStartInterruptImmunities(packet, "InterruptImmunities");

            if (hasHealPrediction)
                ReadSpellTargetedHealPrediction(packet, "HealPrediction");
        }

        public static void ReadTalentInfoUpdate(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("UnspentTalentPoints", idx);

            // This is always 0 or 1 (index)
            // Random values if IsPetTalents (probably uninitialized on serverside)
            packet.ReadByte("ActiveSpecGroup", idx);
            var specCount = packet.ReadUInt32("SpecCount", idx);

            for (var i = 0; i < specCount; ++i)
            {
                packet.ReadByte("TalentCount", idx, i);                     // Blizzard doing blizzard things
                var talentCount = packet.ReadUInt32("TalentCount", idx, i); // Blizzard doing blizzard things
                packet.ReadByte("GlyphCount", idx, i);                      // Blizzard doing blizzard things - Random values if IsPetTalents (probably uninitialized on serverside)
                var glyphCount = packet.ReadUInt32("GlyphCount", idx, i);   // Blizzard doing blizzard things
                // This is 0 (without dualspec learnt) and 1 or 2 with dualspec learnt
                // SpecID 0 and 1 = Index 0 (SpecGroup)
                // SpecID 2 = Index 1 (SpecGroup)
                packet.ReadByte("SpecID", idx, i);
                packet.ReadUInt32("Unk440", idx, i);

                for (var j = 0; j < talentCount; ++j)
                {
                    packet.ReadUInt32("TalentID", idx, i, "TalentInfo", j);
                    packet.ReadUInt32("Rank", idx, i, "TalentInfo", j);
                }

                for (var k = 0; k < glyphCount; ++k)
                {
                    packet.ReadUInt16("Glyph", idx, i, "GlyphInfo", k);
                }
            }

            packet.ReadBit("IsPetTalents", idx);
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadUpdateTalentData(Packet packet)
        {
            ReadTalentInfoUpdate(packet);
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleSendKnownSpells(Packet packet)
        {
            packet.ReadBit("InitialLogin");
            var knownSpells = packet.ReadUInt32("KnownSpellsCount");
            var favoriteSpells = packet.ReadUInt32("FavoriteSpellsCount");

            for (var i = 0; i < knownSpells; i++)
                packet.ReadUInt32<SpellId>("KnownSpellId", i);

            for (var i = 0; i < favoriteSpells; i++)
                packet.ReadUInt32<SpellId>("FavoriteSpellId", i);
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var unleanrSpells = packet.ReadUInt32("UnlearnSpellsCount");

            for (var i = 0; i < unleanrSpells; i++)
                packet.ReadUInt32<SpellId>("KnownSpellId", i);
        }

        [Parser(Opcode.SMSG_REFRESH_SPELL_HISTORY)]
        [Parser(Opcode.SMSG_SEND_SPELL_HISTORY)]
        public static void HandleSendSpellHistory(Packet packet)
        {
            var int4 = packet.ReadInt32("SpellHistoryEntryCount");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadUInt32<SpellId>("SpellID", i);
                packet.ReadUInt32("ItemID", i);
                packet.ReadUInt32("Category", i);
                packet.ReadInt32("RecoveryTime", i);
                packet.ReadInt32("CategoryRecoveryTime", i);
                packet.ReadSingle("ModRate", i);

                packet.ResetBitReader();
                var unused622_1 = packet.ReadBit();
                var unused622_2 = packet.ReadBit();

                packet.ReadBit("OnHold", i);

                if (unused622_1)
                    packet.ReadUInt32("Unk_622_1", i);

                if (unused622_2)
                    packet.ReadUInt32("Unk_622_2", i);
            }
        }

        [Parser(Opcode.SMSG_SEND_SPELL_CHARGES)]
        public static void HandleSendSpellCharges(Packet packet)
        {
            var int4 = packet.ReadInt32("SpellChargeEntryCount");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadUInt32("Category", i);
                packet.ReadUInt32("NextRecoveryTime", i);
                packet.ReadSingle("ChargeModRate", i);
                packet.ReadByte("ConsumedCharges", i);
            }
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
    }
}
