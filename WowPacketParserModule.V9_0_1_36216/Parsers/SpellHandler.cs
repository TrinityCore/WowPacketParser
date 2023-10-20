using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellCastVisual(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("SpellXSpellVisualID", indexes);
            packet.ReadInt32("ScriptVisualID", indexes);
        }

        public static void ReadOptionalReagent(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<ItemId>("ItemID", indexes);
            packet.ReadInt32("Slot", indexes);
            if(ClientVersion.RemovedInVersion(ClientVersionBuild.V9_1_0_39185))
                packet.ReadInt32("Count", indexes);
        }

        public static void ReadOptionalCurrency(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<ItemId>("ItemID", indexes);
            packet.ReadInt32("Slot", indexes);
            packet.ReadInt32("Count", indexes);
        }

        public static uint ReadSpellCastRequest(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CastID", idx);

            for (var i = 0; i < 2; i++)
                packet.ReadInt32("Misc", idx, i);

            var spellId = packet.ReadUInt32<SpellId>("SpellID", idx);

            ReadSpellCastVisual(packet, idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.ReadPackedGuid128("CraftingNPC", idx);

            var optionalCurrenciesCount = packet.ReadUInt32("OptionalCurrenciesCount", idx);
            var optionalReagentsCount = packet.ReadUInt32("OptionalReagentsCount", idx);
            var removedModificationsCount = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49407))
                removedModificationsCount = packet.ReadUInt32("RemovedModificationsCount", idx);

            for (var j = 0; j < optionalCurrenciesCount; ++j)
                ReadOptionalCurrency(packet, idx, "OptionalCurrency", j);

            packet.ResetBitReader();
            packet.ReadBits("SendCastFlags", 5, idx);
            var hasMoveUpdate = packet.ReadBit("HasMoveUpdate", idx);
            var weightCount = packet.ReadBits("WeightCount", 2, idx);
            var hasCraftingOrderID = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                hasCraftingOrderID = packet.ReadBit("HasCrafingOrderID", idx);

            V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, null, spellId, idx, "Target");

            if (hasCraftingOrderID)
                packet.ReadUInt64("CraftingOrderID", idx);

            for (var i = 0; i < optionalReagentsCount; ++i)
                ReadOptionalReagent(packet, idx, "OptionalReagent", i);

            for (var i = 0; i < removedModificationsCount; ++i)
                ReadOptionalReagent(packet, idx, "RemovedModifications", i);

            if (hasMoveUpdate)
                Substructures.MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellWeight(packet, idx, "Weight", i);

            return spellId;
        }

        public static void ReadSpellMissStatus_10_1_7(Packet packet, params object[] idx)
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
            ReadSpellCastVisual(packet, idx, "Visual");

            packetSpellData.Flags = packet.ReadUInt32("CastFlags", idx);
            packetSpellData.Flags2 = packet.ReadUInt32("CastFlagsEx", idx);
            packetSpellData.CastTime = packet.ReadUInt32("CastTime", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryResult(packet, idx, "MissileTrajectory");

            packetSpellData.AmmoDisplayId = packet.ReadInt32("Ammo.DisplayID", idx);

            packet.ReadByte("DestLocSpellCastIndex", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadCreatureImmunities(packet, idx, "Immunities");

            V6_0_2_19033.Parsers.SpellHandler.ReadSpellHealPrediction(packet, idx, "Predict");

            packet.ResetBitReader();

            var hitTargetsCount = packet.ReadBits("HitTargetsCount", 16, idx);
            var missTargetsCount = packet.ReadBits("MissTargetsCount", 16, idx);
            var hitStatusCount = packet.ReadBits("HitStatusCount", 16, idx);
            var missStatusCount = packet.ReadBits("MissStatusCount", 16, idx);
            var remainingPowerCount = packet.ReadBits("RemainingPowerCount", 9, idx);

            var hasRuneData = packet.ReadBit("HasRuneData", idx);
            var targetPointsCount = packet.ReadBits("TargetPointsCount", 16, idx);

            if (ClientVersion.RemovedInVersion(ClientBranch.Retail, ClientVersionBuild.V10_1_7_51187))
                for (var i = 0; i < missStatusCount; ++i)
                    V6_0_2_19033.Parsers.SpellHandler.ReadSpellMissStatus(packet, idx, "MissStatus", i);

            V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, packetSpellData, spellID, idx, "Target");

            for (var i = 0; i < hitTargetsCount; ++i)
                packetSpellData.HitTargets.Add(packet.ReadPackedGuid128("HitTarget", idx, i));

            for (var i = 0; i < missTargetsCount; ++i)
                packetSpellData.MissedTargets.Add(packet.ReadPackedGuid128("MissTarget", idx, i));

            for (var i = 0; i < hitStatusCount; ++i)
                packet.ReadByte("HitStatus", idx, i);

            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V10_1_7_51187))
                for (var i = 0; i < missStatusCount; ++i)
                    ReadSpellMissStatus_10_1_7(packet, idx, "MissStatus", i);

            for (var i = 0; i < remainingPowerCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellPowerData(packet, idx, "RemainingPower", i);

            if (hasRuneData)
                V7_0_3_22248.Parsers.SpellHandler.ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                packetSpellData.TargetPoints.Add(V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "TargetPoints", i));

            return packetSpellData;
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            PacketSpellStart packetSpellStart = new();
            packetSpellStart.Data = ReadSpellCastData(packet, "Cast");
            packet.Holder.SpellStart = packetSpellStart;
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            PacketSpellGo packetSpellGo = new();
            packetSpellGo.Data = ReadSpellCastData(packet, "Cast");
            packet.Holder.SpellGo = packetSpellGo;

            packet.ResetBitReader();

            var hasLogData = packet.ReadBit();
            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "LogData");
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
                    var flags = packet.ReadUInt16E<AuraFlagMoP>("Flags", i);
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

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            var spellFail = packet.Holder.SpellCastFailed = new();
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = (uint)packet.ReadInt32<SpellId>("SpellID");
            ReadSpellCastVisual(packet, "Visual");
            spellFail.Success = packet.ReadInt32("Reason") == 0;
            packet.ReadInt32("FailedArg1");
            packet.ReadInt32("FailedArg2");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            var spellFail = packet.Holder.SpellFailure = new();
            spellFail.Caster = packet.ReadPackedGuid128("CasterUnit");
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = (uint)packet.ReadInt32<SpellId>("SpellID");
            ReadSpellCastVisual(packet, "Visual");
            spellFail.Success = packet.ReadInt16("Reason") == 0;
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var spellFail = packet.Holder.SpellFailure = new();
            spellFail.Caster = packet.ReadPackedGuid128("CasterUnit");
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = packet.ReadUInt32<SpellId>("SpellID");
            ReadSpellCastVisual(packet, "Visual");
            spellFail.Success = packet.ReadByte("Reason") == 0;
        }

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            var spellCount = packet.ReadUInt32("SpellCount");
            var favoriteSpellCount = packet.ReadUInt32("FavoriteSpellCount");
            packet.ReadUInt32("SpecializationID");

            for (var i = 0; i < spellCount; ++i)
                packet.ReadInt32<SpellId>("SpellID", i);

            for (var i = 0; i < favoriteSpellCount; ++i)
                packet.ReadInt32<SpellId>("FavoriteSpellID", i);

            packet.ReadBit("SuppressMessaging");
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_START, ClientVersionBuild.V7_2_0_23826)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32<SpellId>("SpellID");
            ReadSpellCastVisual(packet, "Visual");
            packet.ReadInt32("ChannelDuration");

            var hasInterruptImmunities = packet.ReadBit("HasInterruptImmunities");
            var hasHealPrediction = packet.ReadBit("HasHealPrediction");

            if (hasInterruptImmunities)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellChannelStartInterruptImmunities(packet, "InterruptImmunities");

            if (hasHealPrediction)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellTargetedHealPrediction(packet, "HealPrediction");
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

        [Parser(Opcode.CMSG_UPDATE_SPELL_VISUAL)]
        public static void HandleUpdateSpellVisual(Packet packet)
        {
            packet.ReadUInt32("SpellID");
            ReadSpellCastVisual(packet, "Visual");
            packet.ReadPackedGuid128("GUID");
        }

        [Parser(Opcode.SMSG_RESUME_CAST)]
        public static void HandleResumeCast(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            ReadSpellCastVisual(packet, "Visual");
            packet.ReadPackedGuid128("CastID");
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32<SpellId>("SpellID");

        }

        [Parser(Opcode.SMSG_RESUME_CAST_BAR)]
        public static void HandleResumeCastBar(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadPackedGuid128("Target");

            packet.ReadUInt32<SpellId>("SpellID");
            ReadSpellCastVisual(packet, "Visual");
            packet.ReadUInt32("TimeRemaining");
            packet.ReadUInt32("TotalTime");

            var result = packet.ReadBit("HasInterruptImmunities");
            if (result)
            {
                packet.ReadUInt32("SchoolImmunities");
                packet.ReadUInt32("Immunities");
            }
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_CREATURE_DATA)]
        public static void HandleGetMirrorImageData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("DisplayID");
            packet.ReadInt32("SpellVisualKitID");
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("DisplayID");
            packet.ReadInt32("SpellVisualKitID");

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

        [Parser(Opcode.CMSG_PET_CAST_SPELL)]
        public static void HandlePetCastSpell(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_INTERRUPT_POWER_REGEN)]
        public static void HandleInterruptPowerRegen(Packet packet)
        {
            packet.ReadUInt32E<PowerType>("PowerType");
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
            if (packet.ReadBit())
                CombatLogHandler.ReadContentTuningParams(packet, "ContentTuning");
        }
    }
}
