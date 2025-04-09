using System;
using System.Collections.Generic;
using WowPacketParser.DBC;
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
        public static Vector3 ReadLocation(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Transport", idx);
            return packet.ReadVector3("Location", idx);
        }

        public static void ReadSpellWeight(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBits("Type", 2, idx); // Enum SpellweightTokenTypes
            packet.ReadInt32("ID", idx);
            packet.ReadInt32("Quantity", idx);
        }

        public static void ReadOptionalReagent(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<ItemId>("ItemID", indexes);
            packet.ReadInt32("DataSlotIndex", indexes);
            packet.ReadInt32("Quantity", indexes);

            if (packet.ReadBit())
                packet.ReadByte("Unknown_1000", indexes);
        }

        public static void ReadMissileTrajectoryRequest(Packet packet, params object[] idx)
        {
            packet.ReadSingle("Pitch", idx);
            packet.ReadSingle("Speed", idx);
        }

        public static void ReadSpellTargetData(Packet packet, PacketSpellData packetSpellData, uint spellID, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBitsE<TargetFlag>("Flags", 28, idx);

            var hasSrcLoc = packet.ReadBit("HasSrcLocation", idx);
            var hasDstLoc = packet.ReadBit("HasDstLocation", idx);
            var hasOrient = packet.ReadBit("HasOrientation", idx);
            var hasMapID = packet.ReadBit("hasMapID ", idx);
            var nameLength = packet.ReadBits(7);

            var targetUnit = packet.ReadPackedGuid128("Unit", idx);
            if (packetSpellData != null)
                packetSpellData.TargetUnit = targetUnit;
            packet.ReadPackedGuid128("Item", idx);

            if (hasSrcLoc)
                ReadLocation(packet, idx, "SrcLocation");

            Vector3? dstLocation = null;
            if (hasDstLoc)
            {
                ReadLocation(packet, idx, "DstLocation");
                if (packetSpellData != null)
                    packetSpellData.DstLocation = dstLocation;
            }

            if (hasOrient)
                packet.ReadSingle("Orientation", idx);

            int mapID = -1;
            if (hasMapID)
                mapID = (ushort)packet.ReadInt32("MapID", idx);

            if (Settings.UseDBC && dstLocation != null && mapID != -1)
            {
                for (uint i = 0; i < 32; i++)
                {
                    var tuple = Tuple.Create(spellID, i);
                    if (DBC.SpellEffectStores.ContainsKey(tuple))
                    {
                        var effect = DBC.SpellEffectStores[tuple];
                        if ((Targets)effect.ImplicitTarget[0] == Targets.TARGET_DEST_DB || (Targets)effect.ImplicitTarget[1] == Targets.TARGET_DEST_DB)
                        {
                            string effectHelper = $"Spell: {StoreGetters.GetName(StoreNameType.Spell, (int)spellID)} Efffect: {effect.Effect} ({(SpellEffects)effect.Effect})";

                            var spellTargetPosition = new SpellTargetPosition
                            {
                                ID = spellID,
                                EffectIndex = (byte)i,
                                PositionX = dstLocation.Value.X,
                                PositionY = dstLocation.Value.Y,
                                PositionZ = dstLocation.Value.Z,
                                MapID = (ushort)mapID,
                                EffectHelper = effectHelper
                            };

                            if (!Storage.SpellTargetPositions.ContainsKey(spellTargetPosition))
                                Storage.SpellTargetPositions.Add(spellTargetPosition);
                        }
                    }
                }
            }

            packet.ReadWoWString("Name", nameLength, idx);
        }

        public static void ReadOptionalCurrency(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("CurrencyID", indexes);
            packet.ReadInt32("Count", indexes);
        }

        public static uint ReadSpellCastRequest(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CastID", idx);

            for (var i = 0; i < 2; i++)
                packet.ReadInt32("Misc", idx, i);

            var spellId = packet.ReadUInt32<SpellId>("SpellID", idx);
            packet.ReadInt32("SpellXSpellVisual", idx);

            ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.ReadPackedGuid128("CraftingNPC", idx);

            var optionalCurrenciesCount = packet.ReadUInt32("OptionalCurrenciesCount", idx);
            var optionalReagentsCount = packet.ReadUInt32("OptionalReagentsCount", idx);
            var removedModificationsCount = packet.ReadUInt32("RemovedModificationsCount", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                packet.ReadByte("CraftingFlags", idx);

            for (var j = 0; j < optionalCurrenciesCount; ++j)
                ReadOptionalCurrency(packet, idx, "OptionalCurrency", j);

            packet.ResetBitReader();
            packet.ReadBits("SendCastFlags", 5, idx);
            var hasMoveUpdate = packet.ReadBit("HasMoveUpdate", idx);
            var weightCount = packet.ReadBits("WeightCount", 2, idx);
            var hasCraftingOrderID = packet.ReadBit("HasCrafingOrderID", idx);

            ReadSpellTargetData(packet, null, spellId, idx, "Target");

            if (hasCraftingOrderID)
                packet.ReadUInt64("CraftingOrderID", idx);

            for (var i = 0; i < optionalReagentsCount; ++i)
                ReadOptionalReagent(packet, idx, "OptionalReagent", i);

            for (var i = 0; i < removedModificationsCount; ++i)
                ReadOptionalReagent(packet, idx, "RemovedModifications", i);

            if (hasMoveUpdate)
                MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                ReadSpellWeight(packet, idx, "Weight", i);

            return spellId;
        }

        public static void ReadSpellCastLogData(Packet packet, params object[] idx)
        {
            packet.ReadInt64("Health", idx);
            packet.ReadInt32("AttackPower", idx);
            packet.ReadInt32("SpellPower", idx);
            packet.ReadInt32("Armor", idx);

            packet.ResetBitReader();

            var spellLogPowerDataCount = packet.ReadBits("SpellLogPowerData", 9, idx);

            // SpellLogPowerData
            for (var i = 0; i < spellLogPowerDataCount; ++i)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                    packet.ReadByte("PowerType", idx, i);
                else
                    packet.ReadInt32("PowerType", idx, i);

                packet.ReadInt32("Amount", idx, i);
                packet.ReadInt32("Cost", idx, i);
            }
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

        public static void ReadCreatureImmunities(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("School", idx);
            packet.ReadUInt32("Value", idx);
        }

        public static void ReadMissileTrajectoryResult(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("TravelTime", idx);
            packet.ReadSingle("Pitch", idx);
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

            ReadMissileTrajectoryResult(packet, idx, "MissileTrajectory");

            packet.ReadByte("DestLocSpellCastIndex", idx);

            ReadCreatureImmunities(packet, idx, "Immunities");

            ReadSpellHealPrediction(packet, idx, "Predict");

            packet.ResetBitReader();

            var hitTargetsCount = packet.ReadBits("HitTargetsCount", 16, idx);
            var missTargetsCount = packet.ReadBits("MissTargetsCount", 16, idx);
            var missStatusCount = packet.ReadBits("MissStatusCount", 16, idx);
            var remainingPowerCount = packet.ReadBits("RemainingPowerCount", 9, idx);

            var hasRuneData = packet.ReadBit("HasRuneData", idx);
            var targetPointsCount = packet.ReadBits("TargetPointsCount", 16, idx);
            var hasAmmoDisplayId = packet.ReadBit("HasAmmoDisplayId", idx);
            var hasAmmoInventoryType = packet.ReadBit("HasAmmoInventoryType", idx);

            ReadSpellTargetData(packet, packetSpellData, spellID, idx, "Target");

            for (var i = 0; i < hitTargetsCount; ++i)
                packetSpellData.HitTargets.Add(packet.ReadPackedGuid128("HitTarget", idx, i));

            for (var i = 0; i < missTargetsCount; ++i)
                packetSpellData.MissedTargets.Add(packet.ReadPackedGuid128("MissTarget", idx, i));

            for (var i = 0; i < missStatusCount; ++i)
                ReadSpellMissStatus(packet, idx, "MissStatus", i);

            for (var i = 0; i < remainingPowerCount; ++i)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                    ReadSpellPowerData441(packet, idx, "RemainingPower", i);
                else
                    ReadSpellPowerData(packet, idx, "RemainingPower", i);
            }

            if (hasRuneData)
                ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                packetSpellData.TargetPoints.Add(ReadLocation(packet, idx, "TargetPoints", i));

            if (hasAmmoDisplayId)
                packetSpellData.AmmoDisplayId = packet.ReadInt32("AmmoDisplayId", idx);

            if (hasAmmoInventoryType)
                packetSpellData.AmmoInventoryType = (uint)packet.ReadInt32E<InventoryType>("InventoryType", idx);

            return packetSpellData;
        }

        public static void ReadSpellPowerData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Cost", idx);
            packet.ReadByteE<PowerType>("Type", idx);
        }

        public static void ReadSpellPowerData441(Packet packet, params object[] idx)
        {
            packet.ReadByteE<PowerType>("Type", idx);
            packet.ReadInt32("Cost", idx);
        }

        public static void ReadRuneData(Packet packet, params object[] indexes)
        {
            packet.ReadByte("Start", indexes);
            packet.ReadByte("Count", indexes);

            var cooldownCount = packet.ReadUInt32("CooldownCount", indexes);
            for (var i = 0; i < cooldownCount; ++i)
                packet.ReadByte("Cooldown", indexes);
        }

        [Parser(Opcode.SMSG_CANCEL_ORPHAN_SPELL_VISUAL)]
        public static void HandleCancelOrphanSpellVisual(Packet packet)
        {
            packet.ReadInt32("SpellVisualID");
        }

        [Parser(Opcode.SMSG_CANCEL_SPELL_VISUAL)]
        public static void HandleCancelSpellVisual(Packet packet)
        {
            packet.ReadPackedGuid128("Source");
            packet.ReadInt32("SpellVisualID");
        }

        [Parser(Opcode.SMSG_CANCEL_SPELL_VISUAL_KIT)]
        public static void HandleCancelSpellVisualKit(Packet packet)
        {
            packet.ReadPackedGuid128("Source");
            packet.ReadInt32("SpellVisualKitID");
            packet.ReadBit("MountedVisual");
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
                packet.ReadUInt32("PrimarySpecialization", idx, i);

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

        public static void ReadGlyphBinding(Packet packet, params object[] index)
        {
            packet.ReadUInt32("SpellID", index);
            packet.ReadUInt16("GlyphID", index);
        }

        [Parser(Opcode.SMSG_ACTIVE_GLYPHS)]
        public static void HandleActiveGlyphs(Packet packet)
        {
            var count = packet.ReadUInt32("GlyphsCount");
            for (int i = 0; i < count; i++)
                ReadGlyphBinding(packet, i);
            packet.ResetBitReader();
            packet.ReadBit("IsFullUpdate");
        }

        [Parser(Opcode.SMSG_ADD_LOSS_OF_CONTROL)]
        public static void HandleAddLossOfControl(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("Caster");

            packet.ReadUInt32("Duration");
            packet.ReadUInt32("DurationRemaining");
            packet.ReadUInt32E<SpellSchoolMask>("LockoutSchoolMask");

            packet.ReadByteE<SpellMechanic>("Mechanic");
            packet.ReadByte("Type");
        }

        [Parser(Opcode.SMSG_ADD_RUNE_POWER)]
        public static void HandleAddRunePower(Packet packet)
        {
            packet.ReadUInt32("RuneMask");
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
        [Parser(Opcode.SMSG_AURA_UPDATE, ClientBranch.Cata)]
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
                    packet.ReadInt32("SpellXSpellVisualID", i);
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
                        CombatLogHandler.ReadContentTuningParams(packet, i, "ContentTuning");

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

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE, ClientBranch.Classic)]
        public static void HandleAuraUpdateClassic(Packet packet)
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

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V1_15_7_60000))
                    auraEntry.Slot = packet.ReadUInt16("Slot", i);
                else
                    auraEntry.Slot = packet.ReadByte("Slot", i);

                packet.ResetBitReader();
                var hasAura = packet.ReadBit("HasAura", i);
                auraEntry.Remove = !hasAura;
                if (hasAura)
                {
                    packet.ReadPackedGuid128("CastID", i);
                    aura.SpellId = auraEntry.Spell = (uint)packet.ReadInt32<SpellId>("SpellID", i);
                    packet.ReadInt32("SpellXSpellVisualID", i);
                    var flags = packet.ReadUInt16E<AuraFlagClassic>("Flags", i);
                    aura.AuraFlags = flags;
                    auraEntry.Flags = flags.ToUniversal();
                    packet.ReadUInt32("ActiveFlags", i);
                    aura.Level = packet.ReadUInt16("CastLevel", i);
                    aura.Charges = packet.ReadByte("Applications", i);
                    packet.ReadInt32("ContentTuningID", i);
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V1_15_7_60000))
                        packet.ReadVector3("DstLocation", i);

                    packet.ResetBitReader();

                    var hasCastUnit = packet.ReadBit("HasCastUnit", i);
                    var hasDuration = packet.ReadBit("HasDuration", i);
                    var hasRemaining = packet.ReadBit("HasRemaining", i);

                    var hasTimeMod = packet.ReadBit("HasTimeMod", i);

                    var pointsCount = packet.ReadBits("PointsCount", 6, i);
                    var effectCount = packet.ReadBits("EstimatedPoints", 6, i);

                    var hasContentTuning = packet.ReadBit("HasContentTuning", i);

                    if (hasContentTuning)
                        CombatLogHandler.ReadContentTuningParams(packet, i, "ContentTuning");

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
                ReadSpellCastLogData(packet, "LogData");
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32("TimeRemaining");
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var spellFail = packet.Holder.SpellFailure = new();
            spellFail.Caster = packet.ReadPackedGuid128("CasterUnit");
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadInt32("SpellXSpellVisualID");
            spellFail.Success = packet.ReadByte("Reason") == 0;
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            var spellFail = packet.Holder.SpellFailure = new();
            spellFail.Caster = packet.ReadPackedGuid128("CasterUnit");
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = (uint)packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("SpellXSpellVisualID");
            spellFail.Success = packet.ReadInt16("Reason") == 0;
        }

        [Parser(Opcode.SMSG_SPELL_DISPELL_LOG)]
        public static void HandleSpellDispelLog(Packet packet)
        {
            packet.ReadBit("IsSteal");
            packet.ReadBit("IsBreak");
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadUInt32<SpellId>("DispelledBySpellID");
            var dataSize = packet.ReadUInt32("DispelCount");
            for (var i = 0; i < dataSize; ++i)
            {
                packet.ResetBitReader();
                packet.ReadUInt32<SpellId>("SpellID", i);
                packet.ReadBit("Harmful", i);
                var hasRolled = packet.ReadBit("HasRolled", i);
                var hasNeeded = packet.ReadBit("HasNeeded", i);
                if (hasRolled)
                    packet.ReadUInt32("Rolled", i);
                if (hasNeeded)
                    packet.ReadUInt32("Needed", i);
            }
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            var playSpellVisualKit = packet.Holder.PlaySpellVisualKit = new();
            playSpellVisualKit.Unit = packet.ReadPackedGuid128("Unit");
            playSpellVisualKit.KitRecId = packet.ReadInt32("KitRecID");
            playSpellVisualKit.KitType = packet.ReadInt32("KitType");
            playSpellVisualKit.Duration = packet.ReadUInt32("Duration");
            packet.ReadBit("MountedVisual");
        }

        [Parser(Opcode.SMSG_SPELL_DELAYED)]
        public static void HandleSpellDelayed(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadInt32("ActualDelay");
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            var spellFail = packet.Holder.SpellCastFailed = new();
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = (uint)packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("SpellXSpellVisualID");
            spellFail.Success = packet.ReadInt32("Reason") == 0;
            packet.ReadInt32("FailedArg1");
            packet.ReadInt32("FailedArg2");
        }

        [Parser(Opcode.SMSG_SPELL_PREPARE)]
        public static void SpellPrepare(Packet packet)
        {
            packet.ReadPackedGuid128("ClientCastID");
            packet.ReadPackedGuid128("ServerCastID");
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandleCancelCast(Packet packet)
        {
            packet.ReadPackedGuid128("CastID");
            packet.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_RESYNC_RUNES)]
        public static void HandleResyncRunes(Packet packet)
        {
            ReadRuneData(packet, "RuneData");
        }

        [Parser(Opcode.SMSG_CONVERT_RUNE)]
        public static void HandleConvertRune(Packet packet)
        {
            ReadRuneData(packet, "RuneData");

            packet.ReadUInt32("Index");
            packet.ReadUInt32("Rune");
        }

        [Parser(Opcode.CMSG_SET_PRIMARY_TALENT_TREE)]
        public static void HandleSetPrimaryTalentTree(Packet packet)
        {
            packet.ReadInt32("TabIndex");
        }

        [Parser(Opcode.CMSG_LEARN_TALENT)]
        public static void HandleLearnTalent(Packet packet)
        {
            packet.ReadUInt32("TalentID");
            packet.ReadUInt16("Rank");
        }

        [Parser(Opcode.SMSG_CLEAR_ALL_SPELL_CHARGES)]
        public static void HandleClearAllSpellCharges(Packet packet)
        {
            packet.ReadBit("Unused_440");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN)]
        public static void HandleClearCooldown(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadBit("ClearOnHold");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWNS)]
        public static void HandleClearCooldowns(Packet packet)
        {
            var count = packet.ReadInt32("SpellCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32<SpellId>("SpellID", i);

            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_SPELL_CHARGES)]
        public static void HandleClearSpellCharges(Packet packet)
        {
            packet.ReadInt32("Category");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_TARGET)]
        public static void HandleClearTarget(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_DISPEL_FAILED)]
        public static void HandleDispelFailed(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("VictimGUID");
            packet.ReadUInt32("SpellID");

            var failedSpellsCount = packet.ReadUInt32();
            for (int i = 0; i < failedSpellsCount; i++)
                packet.ReadUInt32<SpellId>("FailedSpellID", i);
        }

        [Parser(Opcode.SMSG_INTERRUPT_POWER_REGEN)]
        public static void HandleInterruptPowerRegen(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                packet.ReadByteE<PowerType>("PowerType");
            else
                packet.ReadInt32E<PowerType>("PowerType");
        }

        [Parser(Opcode.SMSG_LEARN_TALENT_FAILED)]
        public static void HandleLearnTalentFailed(Packet packet)
        {
            packet.ReadBits("Reason", 4);
            packet.ReadInt32("SpellID");
            var count = packet.ReadUInt32("TalentCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt16("Talent");
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("DisplayID");
            packet.ReadByte("RaceID");
            packet.ReadByte("Gender");
            packet.ReadByte("ClassID");
            var customizationCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("GuildGUID");
            var itemDisplayCount = packet.ReadInt32("ItemDisplayCount");

            for (var j = 0u; j < customizationCount; ++j)
                CharacterHandler.ReadChrCustomizationChoice(packet, "Customizations", j);

            for (var i = 0; i < itemDisplayCount; i++)
                packet.ReadInt32("ItemDisplayID", i);
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_CREATURE_DATA)]
        public static void HandleMirrorImageCreatureData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("DisplayID");
            packet.ReadInt32("SpellVisualKitID"); // Unused
        }

        [Parser(Opcode.SMSG_MISSILE_CANCEL)]
        public static void HandleMissileCancel(Packet packet)
        {
            packet.ReadPackedGuid128("OwnerGUID");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadBit("Reverse");
        }

        [Parser(Opcode.SMSG_MODIFY_COOLDOWN)]
        public static void HandleModifyCooldown(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("DeltaTime");
            packet.ReadBit("IsPet");
            packet.ReadBit("WithoutCategoryCooldown");
        }

        [Parser(Opcode.SMSG_MOUNT_RESULT)]
        public static void HandleMountResult(Packet packet)
        {
            packet.ReadInt32E<MountResult>("Result");
        }

        [Parser(Opcode.SMSG_NOTIFY_MISSILE_TRAJECTORY_COLLISION)]
        public static void HandleNotifyMissileTrajectoryCollision(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("CastID");
            packet.ReadVector3("CollisionPos");
        }

        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        public static void HandlePetCastFailed(Packet packet)
        {
            var spellFail = packet.Holder.SpellCastFailed = new();
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = (uint)packet.ReadInt32<SpellId>("SpellID");
            spellFail.Success = packet.ReadInt32("Reason") == 0;
            packet.ReadInt32("FailedArg1");
            packet.ReadInt32("FailedArg2");
        }

        [Parser(Opcode.SMSG_PLAYER_BOUND)]
        public static void HandlePlayerBound(Packet packet)
        {
            packet.ReadPackedGuid128("BinderID");
            packet.ReadUInt32<AreaId>("AreaID");
        }

        [Parser(Opcode.SMSG_PLAY_ORPHAN_SPELL_VISUAL)]
        public static void HandlePlayOrphanSpellVisual(Packet packet)
        {
            packet.ReadVector3("SourceLocation");
            packet.ReadVector3("SourceOrientation");
            packet.ReadVector3("TargetLocation");
            packet.ReadPackedGuid128("Target");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                packet.ReadPackedGuid128("TargetTransport");
            packet.ReadInt32("SpellVisualID");
            packet.ReadSingle("TravelSpeed");
            packet.ReadSingle("LaunchDelay");
            packet.ReadSingle("MinDuration");
            packet.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.ReadPackedGuid128("Source");
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("Transport");

            packet.ReadVector3("TargetPosition");

            packet.ReadInt32("SpellVisualID");
            packet.ReadSingle("TravelSpeed");
            packet.ReadUInt16("HitReason");
            packet.ReadUInt16("MissReason");
            packet.ReadUInt16("ReflectStatus");

            packet.ReadSingle("LaunchDelay");
            packet.ReadSingle("MinDuration");

            packet.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.SMSG_RESPEC_WIPE_CONFIRM)]
        public static void HandleRespecWipeConfirm(Packet packet)
        {
            packet.ReadSByte("RespecType");
            packet.ReadUInt32("Cost");
            packet.ReadPackedGuid128("RespecMaster");
        }

        [Parser(Opcode.SMSG_RESURRECT_REQUEST)]
        public static void HandleResurrectRequest(Packet packet)
        {
            packet.ReadPackedGuid128("ResurrectOffererGUID");

            packet.ReadUInt32("ResurrectOffererVirtualRealmAddress");
            packet.ReadUInt32("PetNumber");
            packet.ReadInt32<SpellId>("SpellID");

            var len = packet.ReadBits(11);

            packet.ReadBit("UseTimer");
            packet.ReadBit("Sickness");

            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierFlat(Packet packet)
        {
            var modCount = packet.ReadUInt32("SpellModifierCount");

            for (var j = 0; j < modCount; ++j)
            {
                packet.ReadByteE<SpellModOp>("SpellMod", j);

                var modTypeCount = packet.ReadUInt32("SpellModifierDataCount", j);
                for (var i = 0; i < modTypeCount; ++i)
                {
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                        packet.ReadSingle("ModifierValue", j, i);
                    else
                        packet.ReadInt32("ModifierValue", j, i);

                    packet.ReadByte("ClassIndex", j, i);
                }
            }
        }

        [Parser(Opcode.SMSG_SET_SPELL_CHARGES)]
        public static void HandleSetSpellCharges(Packet packet)
        {
            packet.ReadInt32("Category");
            packet.ReadInt32("RecoveryTime");
            packet.ReadByte("ConsumedCharges");
            packet.ReadSingle("ChargeModRate");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("Caster");
            packet.ReadByte("Flags");

            var count = packet.ReadInt32("SpellCooldownsCount");
            for (int i = 0; i < count; i++)
            {
                var spellId = packet.ReadInt32("SrecID", i);
                var time = packet.ReadInt32("ForcedCooldown", i);
                packet.ReadSingle("ModRate", i);
                WowPacketParser.Parsing.Parsers.SpellHandler.FillSpellListCooldown((uint)spellId, time, guid.GetEntry());
            }
        }

        [Parser(Opcode.SMSG_SPELL_VISUAL_LOAD_SCREEN)]
        public static void HandleSpellVisualLoadScreen(Packet packet)
        {
            packet.ReadInt32("SpellVisualKitID");
            packet.ReadInt32("Delay");
        }

        [Parser(Opcode.SMSG_SUPERCEDED_SPELLS)]
        public static void HandleSupercededSpells(Packet packet)
        {
            var spellCount = packet.ReadUInt32();

            for (var i = 0; i < spellCount; ++i)
                ReadLearnedSpellInfo(packet, "ClientLearnedSpellData", i);
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadPackedGuid128("Totem");
            packet.ReadUInt32("Duration");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadSingle("TimeMod");

            packet.ResetBitReader();
            packet.ReadBit("CannotDismiss");
        }

        [Parser(Opcode.SMSG_TOTEM_MOVED)]
        public static void HandleTotemMoved(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadByte("NewSlot");
            packet.ReadPackedGuid128("Totem");
        }

        [Parser(Opcode.SMSG_UNLEARNED_SPELLS)]
        public static void HandleUnlearnedSpells(Packet packet)
        {
            var count = packet.ReadInt32("UnlearnedSpellCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32<SpellId>("SpellID");

                packet.ReadBit("SuppressMessaging");
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCanelAura(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("CasterGUID");
        }

        [Parser(Opcode.CMSG_CANCEL_CHANNELLING)]
        public static void HandleCancelChanneling(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("Reason");
        }

        [Parser(Opcode.CMSG_CANCEL_TEMP_ENCHANTMENT)]
        public static void HandleCancelTempEnchantment(Packet packet)
        {
            packet.ReadUInt32("Slot");
        }

        [Parser(Opcode.CMSG_CONFIRM_RESPEC_WIPE)]
        public static void HandleConfirmRespecWipe(Packet packet)
        {
            packet.ReadPackedGuid128("RespecMaster");
            packet.ReadByte("RespecType");
        }

        [Parser(Opcode.CMSG_GET_MIRROR_IMAGE_DATA)]
        public static void HandleGetMirrorImageData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("DisplayID");
        }

        [Parser(Opcode.CMSG_KEYBOUND_OVERRIDE)]
        public static void HandleKeyboundOverride(Packet packet)
        {
            packet.ReadUInt16("OverrideID");
        }

        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS)]
        public static void HandleLearnPreviewTalents(Packet packet)
        {
            var talentCount = packet.ReadUInt32("TalentCount");
            packet.ReadInt32("TabIndex");

            for (int i = 0; i < talentCount; i++)
            {
                packet.ReadInt32("TalentID", i);
                packet.ReadInt32("Rank", i);
            }
        }

        [Parser(Opcode.CMSG_MISSILE_TRAJECTORY_COLLISION)]
        public static void HandleMissileTrajectoryCollision(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("CastID");
            packet.ReadVector3("CollisionPos");
        }

        [Parser(Opcode.CMSG_PET_CAST_SPELL)]
        public static void HandlePetCastSpell(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_SELF_RES)]
        public static void HandleSelfRes(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.CMSG_TOTEM_DESTROYED)]
        public static void HandleTotemDestroyed(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadPackedGuid128("TotemGUID");
        }

        [Parser(Opcode.CMSG_UNLEARN_SKILL)]
        public static void HandleUnlearnSkill(Packet packet)
        {
            packet.ReadInt32("SkillLine");
        }

        [Parser(Opcode.CMSG_UPDATE_MISSILE_TRAJECTORY)]
        public static void HandleUpdateMissileTrajectory(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadPackedGuid128("CastID");
            packet.ReadUInt16("MoveMsgID");
            packet.ReadInt32("SpellID");
            packet.ReadSingle("Pitch");
            packet.ReadSingle("Speed");
            packet.ReadVector3("FirePos");
            packet.ReadVector3("ImpactPos");

            packet.ResetBitReader();
            var hasStatus = packet.ReadBit("HasStatus");
            if (hasStatus)
                Substructures.MovementHandler.ReadMovementStats(packet, "Status");
        }

        [Parser(Opcode.SMSG_ON_CANCEL_EXPECTED_RIDE_VEHICLE_AURA)]
        [Parser(Opcode.CMSG_CANCEL_AUTO_REPEAT_SPELL)]
        [Parser(Opcode.CMSG_CANCEL_GROWTH_AURA)]
        [Parser(Opcode.CMSG_CANCEL_MOUNT_AURA)]
        [Parser(Opcode.CMSG_CANCEL_QUEUED_SPELL)]
        public static void HandleSpellNull(Packet packet)
        {
        }
    }
}
