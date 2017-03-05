using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellCastRequest(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("CastID", idx);

            for (var i = 0; i < 2; i++)
                packet.Translator.ReadInt32("Misc", idx, i);

            packet.Translator.ReadInt32<SpellId>("SpellID", idx);
            packet.Translator.ReadInt32("SpellXSpellVisualID", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.Translator.ReadPackedGuid128("Guid", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("SendCastFlags", 5, idx);
            var hasMoveUpdate = packet.Translator.ReadBit("HasMoveUpdate", idx);

            var weightCount = packet.Translator.ReadBits("WeightCount", 2, idx);

            ReadSpellTargetData(packet, idx, "Target");

            if (hasMoveUpdate)
                MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellWeight(packet, idx, "Weight", i);
        }

        public static void ReadSpellCastData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID", idx);
            packet.Translator.ReadPackedGuid128("CasterUnit", idx);

            packet.Translator.ReadPackedGuid128("CastID", idx);
            packet.Translator.ReadPackedGuid128("OriginalCastID", idx);

            packet.Translator.ReadInt32<SpellId>("SpellID", idx);
            packet.Translator.ReadUInt32("SpellXSpellVisualID", idx);

            packet.Translator.ReadUInt32("CastFlags", idx);
            packet.Translator.ReadUInt32("CastTime", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryResult(packet, idx, "MissileTrajectory");

            packet.Translator.ReadInt32("Ammo.DisplayID", idx);

            packet.Translator.ReadByte("DestLocSpellCastIndex", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadCreatureImmunities(packet, idx, "Immunities");

            V6_0_2_19033.Parsers.SpellHandler.ReadSpellHealPrediction(packet, idx, "Predict");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("CastFlagsEx", 22, idx);
            var hitTargetsCount = packet.Translator.ReadBits("HitTargetsCount", 16, idx);
            var missTargetsCount = packet.Translator.ReadBits("MissTargetsCount", 16, idx);
            var missStatusCount = packet.Translator.ReadBits("MissStatusCount", 16, idx);
            var remainingPowerCount = packet.Translator.ReadBits("RemainingPowerCount", 9, idx);

            var hasRuneData = packet.Translator.ReadBit("HasRuneData", idx);

            var targetPointsCount = packet.Translator.ReadBits("TargetPointsCount", 16, idx);

            for (var i = 0; i < missStatusCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellMissStatus(packet, idx, "MissStatus", i);

            ReadSpellTargetData(packet, idx, "Target");

            for (var i = 0; i < hitTargetsCount; ++i)
                packet.Translator.ReadPackedGuid128("HitTarget", idx, i);

            for (var i = 0; i < missTargetsCount; ++i)
                packet.Translator.ReadPackedGuid128("MissTarget", idx, i);

            for (var i = 0; i < remainingPowerCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellPowerData(packet, idx, "RemainingPower", i);

            if (hasRuneData)
                ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "TargetPoints", i);
        }

        public static void ReadSpellTargetData(Packet packet, params object[] idx)
        {
            packet.Translator.ResetBitReader();

            packet.Translator.ReadBitsE<TargetFlag>("Flags", 25, idx);
            var hasSrcLoc = packet.Translator.ReadBit("HasSrcLocation", idx);
            var hasDstLoc = packet.Translator.ReadBit("HasDstLocation", idx);
            var hasOrient = packet.Translator.ReadBit("HasOrientation", idx);
            var hasMapID = packet.Translator.ReadBit("hasMapID ", idx);
            var nameLength = packet.Translator.ReadBits(7);

            packet.Translator.ReadPackedGuid128("Unit", idx);
            packet.Translator.ReadPackedGuid128("Item", idx);

            if (hasSrcLoc)
                V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, "SrcLocation");

            if (hasDstLoc)
                V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, "DstLocation");

            if (hasOrient)
                packet.Translator.ReadSingle("Orientation", idx);

            if (hasMapID)
                packet.Translator.ReadInt32("MapID", idx);

            packet.Translator.ReadWoWString("Name", nameLength, idx);
        }

        public static void ReadRuneData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadByte("Start", idx);
            packet.Translator.ReadByte("Count", idx);

            packet.Translator.ResetBitReader();

            var cooldownCount = packet.Translator.ReadInt32("CooldownCount", idx);

            for (var i = 0; i < cooldownCount; ++i)
                packet.Translator.ReadByte("Cooldowns", idx, i);
        }

        public static void ReadSandboxScalingData(Packet packet, params object[] idx)
        {
            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("Type", 3, idx);
            packet.Translator.ReadInt16("PlayerLevelDelta", idx);
            packet.Translator.ReadByte("TargetLevel", idx);
            packet.Translator.ReadByte("Expansion", idx);
            packet.Translator.ReadByte("Class", idx);
            packet.Translator.ReadByte("TargetMinScalingLevel", idx);
            packet.Translator.ReadByte("TargetMaxScalingLevel", idx);
            packet.Translator.ReadSByte("TargetScalingLevelDelta", idx);
        }

        public static void ReadSpellCastLogData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt64("Health", idx);
            packet.Translator.ReadInt32("AttackPower", idx);
            packet.Translator.ReadInt32("SpellPower", idx);

            packet.Translator.ResetBitReader();

            var spellLogPowerDataCount = packet.Translator.ReadBits("SpellLogPowerData", 9, idx);

            // SpellLogPowerData
            for (var i = 0; i < spellLogPowerDataCount; ++i)
            {
                packet.Translator.ReadInt32("PowerType", idx, i);
                packet.Translator.ReadInt32("Amount", idx, i);
                packet.Translator.ReadInt32("Cost", idx, i);
            }
        }

        public static void ReadTalentInfoUpdate(Packet packet, params object[] idx)
        {
            packet.Translator.ReadByte("ActiveGroup", idx);
            packet.Translator.ReadInt32("PrimarySpecialization", idx);

            var talentGroupsCount = packet.Translator.ReadInt32("TalentGroupsCount", idx);
            for (var i = 0; i < talentGroupsCount; ++i)
                ReadTalentGroupInfo(packet, idx, "TalentGroupsCount", i);
        }

        public static void ReadTalentGroupInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("SpecId", idx);

            var talentIDsCount = packet.Translator.ReadInt32("TalentIDsCount", idx);
            var pvpTalentIDsCount = packet.Translator.ReadInt32("PvPTalentIDsCount", idx);

            for (var i = 0; i < talentIDsCount; ++i)
                packet.Translator.ReadUInt16("TalentID", idx, i);

            for (var i = 0; i < pvpTalentIDsCount; ++i)
                packet.Translator.ReadUInt16("PvPTalentID", idx, i);
        }

        [Parser(Opcode.SMSG_SPELL_PREPARE)]
        public static void SpellPrepare(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ClientCastID");
            packet.Translator.ReadPackedGuid128("ServerCastID");
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_PET_CAST_SPELL)]
        public static void HandlePetCastSpell(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PetGUID");
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            ReadSpellCastData(packet, "Cast");
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            ReadSpellCastData(packet, "Cast");

            packet.Translator.ResetBitReader();

            var hasLogData = packet.Translator.ReadBit();
            if (hasLogData)
                ReadSpellCastLogData(packet, "LogData");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            packet.Translator.ReadBit("UpdateAll");
            var count = packet.Translator.ReadBits("AurasCount", 9);

            var auras = new List<Aura>();
            for (var i = 0; i < count; ++i)
            {
                var aura = new Aura();

                packet.Translator.ReadByte("Slot", i);

                packet.Translator.ResetBitReader();
                var hasAura = packet.Translator.ReadBit("HasAura", i);
                if (hasAura)
                {
                    packet.Translator.ReadPackedGuid128("CastID", i);
                    aura.SpellId = (uint)packet.Translator.ReadInt32<SpellId>("SpellID", i);
                    packet.Translator.ReadInt32("SpellXSpellVisualID", i);
                    aura.AuraFlags = packet.Translator.ReadByteE<AuraFlagMoP>("Flags", i);
                    packet.Translator.ReadInt32("ActiveFlags", i);
                    aura.Level = packet.Translator.ReadUInt16("CastLevel", i);
                    aura.Charges = packet.Translator.ReadByte("Applications", i);

                    packet.Translator.ResetBitReader();

                    var hasCastUnit = packet.Translator.ReadBit("HasCastUnit", i);
                    var hasDuration = packet.Translator.ReadBit("HasDuration", i);
                    var hasRemaining = packet.Translator.ReadBit("HasRemaining", i);

                    var hasTimeMod = packet.Translator.ReadBit("HasTimeMod", i);

                    var pointsCount = packet.Translator.ReadBits("PointsCount", 6, i);
                    var effectCount = packet.Translator.ReadBits("EstimatedPoints", 6, i);

                    var hasSandboxScaling = packet.Translator.ReadBit("HasSandboxScaling", i);

                    if (hasCastUnit)
                        packet.Translator.ReadPackedGuid128("CastUnit", i);

                    aura.Duration = hasDuration ? (int)packet.Translator.ReadUInt32("Duration", i) : 0;
                    aura.MaxDuration = hasRemaining ? (int)packet.Translator.ReadUInt32("Remaining", i) : 0;

                    if (hasTimeMod)
                        packet.Translator.ReadSingle("TimeMod");

                    for (var j = 0; j < pointsCount; ++j)
                        packet.Translator.ReadSingle("Points", i, j);

                    for (var j = 0; j < effectCount; ++j)
                        packet.Translator.ReadSingle("EstimatedPoints", i, j);

                    if (hasSandboxScaling)
                        ReadSandboxScalingData(packet, "SandboxScalingData", i);

                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }
            }

            var guid = packet.Translator.ReadPackedGuid128("UnitGUID");

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
            packet.Translator.ReadPackedGuid128("CastID");
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("SpellXSpellVisualID");
            packet.Translator.ReadInt32("Reason");
            packet.Translator.ReadInt32("FailedArg1");
            packet.Translator.ReadInt32("FailedArg2");
        }

        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        public static void HandlePetCastFailed(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CastID");
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("Reason");
            packet.Translator.ReadInt32("FailedArg1");
            packet.Translator.ReadInt32("FailedArg2");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterUnit");
            packet.Translator.ReadPackedGuid128("CastID");
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadUInt32("SpellXSpellVisualID");
            packet.Translator.ReadInt16E<SpellCastFailureReason>("Reason");
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterUnit");
            packet.Translator.ReadPackedGuid128("CastID");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadUInt32("SpellXSpellVisualID");
            packet.Translator.ReadByteE<SpellCastFailureReason>("Reason");
        }

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            var spellCount = packet.Translator.ReadUInt32("SpellCount");
            var favoriteSpellCount = packet.Translator.ReadUInt32("FavoriteSpellCount");

            for (var i = 0; i < spellCount; ++i)
                packet.Translator.ReadInt32<SpellId>("SpellID", i);

            for (var i = 0; i < favoriteSpellCount; ++i)
                packet.Translator.ReadInt32<SpellId>("FavoriteSpellID", i);

            packet.Translator.ReadBit("SuppressMessaging");
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadUpdateTalentData(Packet packet)
        {
            ReadTalentInfoUpdate(packet, "Info");
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleSendKnownSpells(Packet packet)
        {
            packet.Translator.ReadBit("InitialLogin");
            var knownSpells = packet.Translator.ReadUInt32("KnownSpellsCount");
            var favoriteSpells = packet.Translator.ReadUInt32("FavoriteSpellsCount");

            for (var i = 0; i < knownSpells; i++)
                packet.Translator.ReadUInt32<SpellId>("KnownSpellId", i);

            for (var i = 0; i < favoriteSpells; i++)
                packet.Translator.ReadUInt32<SpellId>("FavoriteSpellId", i);
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandleCancelCast(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CastID");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.CMSG_LEARN_TALENTS)]
        public static void HandleLearnTalents(Packet packet)
        {
            var talentCount = packet.Translator.ReadBits("TalentCount", 6);
            for (int i = 0; i < talentCount; i++)
                packet.Translator.ReadUInt16("Talents");
        }

        [Parser(Opcode.SMSG_RESYNC_RUNES)]
        public static void HandleResyncRunes(Packet packet)
        {
            packet.Translator.ReadByte("Start");
            packet.Translator.ReadByte("Count");

            var cooldownCount = packet.Translator.ReadUInt32("CooldownCount");
            for (var i = 0; i < cooldownCount; ++i)
                packet.Translator.ReadByte("Cooldown");
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleSpellCooldown(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadByte("Flags");

            var count = packet.Translator.ReadInt32("SpellCooldownsCount");
            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadInt32("SrecID", i);
                packet.Translator.ReadInt32("ForcedCooldown", i);
                packet.Translator.ReadSingle("ModRate", i);
            }
        }


        [Parser(Opcode.SMSG_REFRESH_SPELL_HISTORY, ClientVersionBuild.V7_1_0_22900)]
        [Parser(Opcode.SMSG_SEND_SPELL_HISTORY, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleSendSpellHistory(Packet packet)
        {
            var int4 = packet.Translator.ReadInt32("SpellHistoryEntryCount");
            for (int i = 0; i < int4; i++)
            {
                packet.Translator.ReadUInt32<SpellId>("SpellID", i);
                packet.Translator.ReadUInt32("ItemID", i);
                packet.Translator.ReadUInt32("Category", i);
                packet.Translator.ReadInt32("RecoveryTime", i);
                packet.Translator.ReadInt32("CategoryRecoveryTime", i);
                packet.Translator.ReadSingle("ModRate", i);

                packet.Translator.ResetBitReader();
                var unused622_1 = packet.Translator.ReadBit();
                var unused622_2 = packet.Translator.ReadBit();

                packet.Translator.ReadBit("OnHold", i);

                if (unused622_1)
                    packet.Translator.ReadUInt32("Unk_622_1", i);

                if (unused622_2)
                    packet.Translator.ReadUInt32("Unk_622_2", i);
            }
        }


        [Parser(Opcode.SMSG_SEND_SPELL_CHARGES, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleSendSpellCharges(Packet packet)
        {
            var int4 = packet.Translator.ReadInt32("SpellChargeEntryCount");
            for (int i = 0; i < int4; i++)
            {
                packet.Translator.ReadUInt32("Category", i);
                packet.Translator.ReadUInt32("NextRecoveryTime", i);
                packet.Translator.ReadByte("ConsumedCharges", i);
                packet.Translator.ReadSingle("ChargeModRate", i);

                packet.Translator.ReadBit("IsPet");
            }
        }

        [Parser(Opcode.SMSG_RESURRECT_REQUEST, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleResurrectRequest(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ResurrectOffererGUID");

            packet.Translator.ReadUInt32("ResurrectOffererVirtualRealmAddress");
            packet.Translator.ReadUInt32("PetNumber");
            packet.Translator.ReadInt32<SpellId>("SpellID");

            var len = packet.Translator.ReadBits(11);

            packet.Translator.ReadBit("UseTimer");
            packet.Translator.ReadBit("Sickness");

            packet.Translator.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleTotemCreated(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadPackedGuid128("Totem");
            packet.Translator.ReadUInt32("Duration");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadSingle("TimeMod");

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("CannotDismiss");
        }
    }
}
