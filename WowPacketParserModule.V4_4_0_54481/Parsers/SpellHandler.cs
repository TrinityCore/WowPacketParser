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
        public static void ReadSpellCastVisual(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("SpellXSpellVisualID", indexes);
        }

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

        public static void ReadSpellMissStatus(Packet packet, params object[] idx)
        {
            var reason = packet.ReadByte("Reason", idx); // TODO enum
            if (reason == 11)
                packet.ReadByte("ReflectStatus", idx);
        }

        public static PacketSpellData ReadSpellCastData(Packet packet, params object[] idx)
        {
            var packetSpellData = new PacketSpellData();
            packet.ReadPackedGuid128("CasterGUID", idx);
            packetSpellData.Caster = packet.ReadPackedGuid128("CasterUnit", idx);

            packetSpellData.CastGuid = packet.ReadPackedGuid128("CastID", idx);
            packet.ReadPackedGuid128("OriginalCastID", idx);

            var spellID = packetSpellData.Spell = packet.ReadUInt32<SpellId>("SpellID", idx);
            packet.ReadUInt32("SpellXSpellVisualID", idx);

            packetSpellData.Flags = packet.ReadUInt32("CastFlags", idx);
            packetSpellData.Flags2 = packet.ReadUInt32("CastFlagsEx", idx);
            packetSpellData.CastTime = packet.ReadUInt32("CastTime", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryResult(packet, idx, "MissileTrajectory");

            packet.ReadByte("DestLocSpellCastIndex", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadCreatureImmunities(packet, idx, "Immunities");

            V6_0_2_19033.Parsers.SpellHandler.ReadSpellHealPrediction(packet, idx, "Predict");

            packet.ResetBitReader();

            var hitTargetsCount = packet.ReadBits("HitTargetsCount", 16, idx);
            var missTargetsCount = packet.ReadBits("MissTargetsCount", 16, idx);
            var missStatusCount = packet.ReadBits("MissStatusCount", 16, idx);
            var remainingPowerCount = packet.ReadBits("RemainingPowerCount", 9, idx);

            var hasRuneData = packet.ReadBit("HasRuneData", idx);
            var targetPointsCount = packet.ReadBits("TargetPointsCount", 16, idx);
            var hasAmmoDisplayId = packet.ReadBit("HasAmmoDisplayId", idx);
            var hasAmmoInventoryType = packet.ReadBit("HasAmmoInventoryType", idx);

            V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, packetSpellData, spellID, idx, "Target");

            for (var i = 0; i < hitTargetsCount; ++i)
                packetSpellData.HitTargets.Add(packet.ReadPackedGuid128("HitTarget", idx, i));

            for (var i = 0; i < missTargetsCount; ++i)
                packetSpellData.MissedTargets.Add(packet.ReadPackedGuid128("MissTarget", idx, i));

            for (var i = 0; i < missStatusCount; ++i)
                ReadSpellMissStatus(packet, idx, "MissStatus", i);

            for (var i = 0; i < remainingPowerCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellPowerData(packet, idx, "RemainingPower", i);

            if (hasRuneData)
                V7_0_3_22248.Parsers.SpellHandler.ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                packetSpellData.TargetPoints.Add(V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "TargetPoints", i));

            if (hasAmmoDisplayId)
                packetSpellData.AmmoDisplayId = packet.ReadInt32("AmmoDisplayId", idx);

            if (hasAmmoInventoryType)
                packetSpellData.AmmoInventoryType = (uint)packet.ReadInt32E<InventoryType>("InventoryType", idx);

            return packetSpellData;
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

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            PacketAuraUpdate packetAuraUpdate = packet.Holder.AuraUpdate = new();
            packet.ReadBit("UpdateAll");
            var count = packet.ReadBits("AurasCount", 9);

            var auras = new List<Aura>();
            for (var i = 0; i < count; ++i)
            {
                var auraEntry = new PacketAuraUpdateEntry();
                packetAuraUpdate.Updates.Add(auraEntry);
                var aura = new Aura();

                auraEntry.Slot = packet.ReadByte("Slot", i);

                packet.ResetBitReader();
                var hasAura = packet.ReadBit("HasAura", i);
                auraEntry.Remove = !hasAura;
                if (hasAura)
                {
                    packet.ReadPackedGuid128("CastID", i);
                    aura.SpellId = auraEntry.Spell = (uint)packet.ReadInt32<SpellId>("SpellID", i);
                    ReadSpellCastVisual(packet, i, "Visual");
                    var flags = packet.ReadUInt16E<AuraFlagClassic>("Flags", i);
                    aura.AuraFlags = flags;
                    auraEntry.Flags = flags.ToUniversal();
                    packet.ReadUInt32("ActiveFlags", i);
                    aura.Level = packet.ReadUInt16("CastLevel", i);
                    aura.Charges = packet.ReadByte("Applications", i);
                    packet.ReadInt32("ContentTuningID", i);

                    packet.ResetBitReader();

                    var hasCastUnit = packet.ReadBit("HasCastUnit", i);
                    var hasDuration = packet.ReadBit("HasDuration", i);
                    var hasRemaining = packet.ReadBit("HasRemaining", i);

                    var hasTimeMod = packet.ReadBit("HasTimeMod", i);

                    var pointsCount = packet.ReadBits("PointsCount", 6, i);
                    var effectCount = packet.ReadBits("EstimatedPoints", 6, i);

                    var hasContentTuning = packet.ReadBit("HasContentTuning", i);

                    if (hasContentTuning)
                        V9_0_1_36216.Parsers.CombatLogHandler.ReadContentTuningParams(packet, i, "ContentTuning");

                    if (hasCastUnit)
                        auraEntry.CasterUnit = packet.ReadPackedGuid128("CastUnit", i);

                    aura.Duration = hasDuration ? packet.ReadInt32("Duration", i) : 0;
                    aura.MaxDuration = hasRemaining ? packet.ReadInt32("Remaining", i) : 0;

                    if (hasDuration)
                        auraEntry.Duration = aura.Duration;

                    if (hasRemaining)
                        auraEntry.Remaining = aura.MaxDuration;

                    if (hasTimeMod)
                        packet.ReadSingle("TimeMod");

                    for (var j = 0; j < pointsCount; ++j)
                        packet.ReadSingle("Points", i, j);

                    for (var j = 0; j < effectCount; ++j)
                        packet.ReadSingle("EstimatedPoints", i, j);

                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }
            }

            var guid = packet.ReadPackedGuid128("UnitGUID");
            packetAuraUpdate.Unit = guid;

            if (Storage.Objects.ContainsKey(guid))
            {
                var unit = Storage.Objects[guid].Item1 as Unit;
                if (unit != null)
                {
                    // If this is the first packet that sends auras
                    // (hopefully at spawn time) add it to the "Auras" field,
                    // if not create another row of auras in AddedAuras
                    // (similar to ChangedUpdateFields)

                    if (unit.Auras == null)
                        unit.Auras = auras;
                    else
                        unit.AddedAuras.Add(auras);
                }
            }
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            ReadSpellCastData(packet, "Cast");
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            PacketSpellGo packetSpellGo = new();
            packetSpellGo.Data = ReadSpellCastData(packet, "Cast");
            packet.Holder.SpellGo = packetSpellGo;

            packet.ResetBitReader();
            var hasLog = packet.ReadBit();
            if (hasLog)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "LogData");
        }
    }
}
