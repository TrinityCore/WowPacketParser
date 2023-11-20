using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
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
        }

        public static void ReadOptionalCurrency(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<ItemId>("ItemID", indexes);
            packet.ReadInt32("Slot", indexes);
            packet.ReadInt32("Count", indexes);
        }

        public static void ReadSpellMissStatus(Packet packet, params object[] idx)
        {
            var reason = packet.ReadByte("Reason", idx); // TODO enum
            if (reason == 11)
                packet.ReadByte("ReflectStatus", idx);
        }

        public static uint ReadSpellCastRequest(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CastID", idx);

            for (var i = 0; i < 2; i++)
                packet.ReadInt32("Misc", idx, i);

            var spellId = packet.ReadUInt32<SpellId>("SpellID", idx);
            packet.ReadInt32("SpellXSpellVisual", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.ReadPackedGuid128("Guid", idx);

            var optionalReagentCount = packet.ReadUInt32("OptionalReagentCount", idx);
            var optionalCurrenciesCount = packet.ReadUInt32("OptionalCurrenciesCount", idx);
            var removedModificationsCount = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                removedModificationsCount = packet.ReadUInt32("RemovedModificationsCount", idx);

            for (var i = 0; i < optionalReagentCount; ++i)
                ReadOptionalReagent(packet, idx, "OptionalReagent", i);

            for (var j = 0; j < optionalCurrenciesCount; ++j)
                ReadOptionalCurrency(packet, idx, "OptionalCurrency", j);


            packet.ResetBitReader();
            packet.ReadBits("SendCastFlags", 5, idx);
            var hasMoveUpdate = packet.ReadBit("HasMoveUpdate", idx);

            var weightCount = packet.ReadBits("WeightCount", 2, idx);

            var hasCraftingOrderID = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                hasCraftingOrderID = packet.ReadBit("HasCrafingOrderID", idx);

            V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, null, spellId, idx, "Target");

            if (hasMoveUpdate)
                Substructures.MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellWeight(packet, idx, "Weight", i);

            return spellId;
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

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_4_1_47014))
                for (var i = 0; i < missStatusCount; ++i)
                    V6_0_2_19033.Parsers.SpellHandler.ReadSpellMissStatus(packet, idx, "MissStatus", i);

            V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, packetSpellData, spellID, idx, "Target");

            for (var i = 0; i < hitTargetsCount; ++i)
                packetSpellData.HitTargets.Add(packet.ReadPackedGuid128("HitTarget", idx, i));

            for (var i = 0; i < missTargetsCount; ++i)
                packetSpellData.MissedTargets.Add(packet.ReadPackedGuid128("MissTarget", idx, i));

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
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

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            var spellFail = packet.Holder.SpellCastFailed = new();
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = (uint)packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("SpellXSpellVisual");
            spellFail.Success = packet.ReadInt32("Reason") == 0;
            packet.ReadInt32("FailedArg1");
            packet.ReadInt32("FailedArg2");
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

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var spellFail = packet.Holder.SpellFailure = new();
            spellFail.Caster = packet.ReadPackedGuid128("CasterUnit");
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadInt32("SpellXSpellVisual");
            spellFail.Success = packet.ReadByte("Reason") == 0;
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            var spellFail = packet.Holder.SpellFailure = new();
            spellFail.Caster = packet.ReadPackedGuid128("CasterUnit");
            spellFail.CastGuid = packet.ReadPackedGuid128("CastID");
            spellFail.Spell = (uint)packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("SpellXSpellVisual");
            spellFail.Success = packet.ReadInt16("Reason") == 0;
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
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellChannelStartInterruptImmunities(packet, "InterruptImmunities");

            if (hasHealPrediction)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellTargetedHealPrediction(packet, "HealPrediction");
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

                for (var j = 0; j < talentCount; ++j)
                {
                    packet.ReadUInt32("TalentID", idx, i, "TalentInfo", j);
                    packet.ReadByte("Rank", idx, i, "TalentInfo", j);
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

        [Parser(Opcode.SMSG_LEARNED_SPELLS, ClientVersionBuild.V3_4_3_51505)]
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
