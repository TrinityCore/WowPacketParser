using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using SpellCastFailureReason = WowPacketParser.Enums.Version.V6_1_0_19678.SpellCastFailureReason;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellCastLogData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Health", idx);
            packet.Translator.ReadInt32("AttackPower", idx);
            packet.Translator.ReadInt32("SpellPower", idx);

            var int3 = packet.Translator.ReadInt32("SpellLogPowerData", idx);

            // SpellLogPowerData
            for (var i = 0; i < int3; ++i)
            {
                packet.Translator.ReadInt32("PowerType", idx, i);
                packet.Translator.ReadInt32("Amount", idx, i);
            }

            packet.Translator.ResetBitReader();

            var bit32 = packet.Translator.ReadBit("bit32", idx);

            if (bit32)
                packet.Translator.ReadSingle("Float7", idx);
        }

        public static void ReadSpellTargetData(Packet packet, params object[] idx)
        {
            packet.Translator.ResetBitReader();

            packet.Translator.ReadBitsE<TargetFlag>("Flags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678) ? 23 : 21, idx);
            var hasSrcLoc = packet.Translator.ReadBit("HasSrcLocation", idx);
            var hasDstLoc = packet.Translator.ReadBit("HasDstLocation", idx);
            var hasOrient = packet.Translator.ReadBit("HasOrientation", idx);
            var nameLength = packet.Translator.ReadBits(7);

            packet.Translator.ReadPackedGuid128("Unit", idx);
            packet.Translator.ReadPackedGuid128("Item", idx);

            if (hasSrcLoc)
                ReadLocation(packet, "SrcLocation");

            if (hasDstLoc)
                ReadLocation(packet, "DstLocation");

            if (hasOrient)
                packet.Translator.ReadSingle("Orientation", idx);

            packet.Translator.ReadWoWString("Name", nameLength, idx);
        }

        public static void ReadMissileTrajectoryRequest(Packet packet, params object[] idx)
        {
            packet.Translator.ReadSingle("Pitch", idx);
            packet.Translator.ReadSingle("Speed", idx);
        }

        public static void ReadSpellWeight(Packet packet, params object[] idx)
        {
            packet.Translator.ResetBitReader();
            packet.Translator.ReadBits("Type", 2, idx); // Enum SpellweightTokenTypes
            packet.Translator.ReadInt32("ID", idx);
            packet.Translator.ReadInt32("Quantity", idx);
        }

        public static void ReadLocation(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("Transport", idx);
            packet.Translator.ReadVector3("Location", idx);
        }

        public static void ReadSpellPowerData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Cost", idx);
            packet.Translator.ReadByteE<PowerType>("Type", idx);
        }

        public static void ReadSpellCastRequest(Packet packet, params object[] idx)
        {
            packet.Translator.ReadByte("CastID", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
            {
                for (var i = 0; i < 2; i++)
                    packet.Translator.ReadInt32("Misc", idx, i);
            }

            packet.Translator.ReadInt32<SpellId>("SpellID", idx);
            packet.Translator.ReadInt32(ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? "SpellXSpellVisualID" : "Misc", idx);

            ReadSpellTargetData(packet, idx, "Target");

            ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.Translator.ReadPackedGuid128("Guid", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("SendCastFlags", 5, idx);
            var hasMoveUpdate = packet.Translator.ReadBit("HasMoveUpdate", idx);

            var weightCount = packet.Translator.ReadBits("WeightCount", 2, idx);

            if (hasMoveUpdate)
                MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                ReadSpellWeight(packet, idx, "Weight", i);
        }

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32<SpellId>("Spell ID", i);
            packet.Translator.ReadBit("SuppressMessaging");
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleSendKnownSpells(Packet packet)
        {
            packet.Translator.ReadBit("InitialLogin");
            var knownSpellsCount = packet.Translator.ReadUInt32("KnownSpellsCount");

            for (var i = 0; i < knownSpellsCount; i++)
                packet.Translator.ReadUInt32<SpellId>("KnownSpellId", i);
        }

        [Parser(Opcode.SMSG_PET_CLEAR_SPELLS)]
        public static void HandleSpellZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Category", i);
                packet.Translator.ReadInt32("ModCooldown", i);
            }
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierFlat(Packet packet)
        {
            var modCount = packet.Translator.ReadUInt32("Modifier type count");

            for (var j = 0; j < modCount; ++j)
            {
                packet.Translator.ReadByteE<SpellModOp>("Spell Mod", j);

                var modTypeCount = packet.Translator.ReadUInt32("Count", j);
                for (var i = 0; i < modTypeCount; ++i)
                {
                    packet.Translator.ReadSingle("Amount", j, i);
                    packet.Translator.ReadByte("Spell Mask bitpos", j, i);
                }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            packet.Translator.ReadBit("UpdateAll");
            var guid = packet.Translator.ReadPackedGuid128("UnitGUID");
            var count = packet.Translator.ReadUInt32("AurasCount");

            var auras = new List<Aura>();
            for (var i = 0; i < count; ++i)
            {
                var aura = new Aura();

                packet.Translator.ReadByte("Slot", i);

                packet.Translator.ResetBitReader();
                var hasAura = packet.Translator.ReadBit("HasAura", i);
                if (hasAura)
                {
                    aura.SpellId = (uint)packet.Translator.ReadInt32<SpellId>("SpellID", i);
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                        packet.Translator.ReadUInt32("SpellXSpellVisualID", i);
                    aura.AuraFlags = packet.Translator.ReadByteE<AuraFlagMoP>("Flags", i);
                    packet.Translator.ReadInt32("ActiveFlags", i);
                    aura.Level = packet.Translator.ReadUInt16("CastLevel", i);
                    aura.Charges = packet.Translator.ReadByte("Applications", i);

                    var int72 = packet.Translator.ReadUInt32("Int56 Count", i);
                    var effectCount = packet.Translator.ReadUInt32("Effect Count", i);

                    for (var j = 0; j < int72; ++j)
                        packet.Translator.ReadSingle("Points", i, j);

                    for (var j = 0; j < effectCount; ++j)
                        packet.Translator.ReadSingle("EstimatedPoints", i, j);

                    packet.Translator.ResetBitReader();
                    var hasCasterGUID = packet.Translator.ReadBit("HasCastUnit", i);
                    var hasDuration = packet.Translator.ReadBit("HasDuration", i);
                    var hasMaxDuration = packet.Translator.ReadBit("HasRemaining", i);

                    if (hasCasterGUID)
                        packet.Translator.ReadPackedGuid128("CastUnit", i);

                    aura.Duration = hasDuration ? packet.Translator.ReadInt32("Duration", i) : 0;
                    aura.MaxDuration = hasMaxDuration ? packet.Translator.ReadInt32("Remaining", i) : 0;

                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }
            }

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

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadTalentInfo(Packet packet)
        {
            ReadTalentInfoUpdate(packet, "Info");
        }

        public static void ReadTalentGroupInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("SpecId", idx);
            var talentIDsCount = packet.Translator.ReadInt32("TalentIDsCount", idx);

            for (var i = 0; i < 6; ++i)
                packet.Translator.ReadUInt16("GlyphIDs", idx, i);

            for (var i = 0; i < talentIDsCount; ++i)
                packet.Translator.ReadUInt16("TalentID", idx, i);
        }

        public static void ReadTalentInfoUpdate(Packet packet, params object[] idx)
        {
            packet.Translator.ReadByte("ActiveGroup");

            var talentGroupsCount = packet.Translator.ReadInt32("TalentGroupsCount");
            for (var i = 0; i < talentGroupsCount; ++i)
                ReadTalentGroupInfo(packet, idx, "TalentGroupsCount", i);
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

        public static void ReadMissileTrajectoryResult(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("TravelTime", idx);
            packet.Translator.ReadSingle("Pitch", idx);
        }

        public static void ReadSpellAmmo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("DisplayID", idx);
            packet.Translator.ReadByteE<InventoryType>("InventoryType", idx);
        }

        public static void ReadCreatureImmunities(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("School", idx);
            packet.Translator.ReadUInt32("Value", idx);
        }

        public static void ReadSpellMissStatus(Packet packet, params object[] idx)
        {
            packet.Translator.ResetBitReader();
            var reason = packet.Translator.ReadBits("Reason", 4, idx); // TODO enum
            if (reason == 11)
                packet.Translator.ReadBits("ReflectStatus", 4, idx);
        }

        public static void ReadRuneData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadByte("Start", idx);
            packet.Translator.ReadByte("Count", idx);

            packet.Translator.ResetBitReader();

            var cooldownCount = packet.Translator.ReadBits("CooldownCount", 3, idx);

            for (var i = 0; i < cooldownCount; ++i)
                packet.Translator.ReadByte("Cooldowns", idx, i);
        }

        public static void ReadProjectileVisual(Packet packet, params object[] idx)
        {
            for (var i = 0; i < 2; ++i)
                packet.Translator.ReadInt32("Id", idx, i);
        }

        public static void ReadSpellCastData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID", idx);
            packet.Translator.ReadPackedGuid128("CasterUnit", idx);

            packet.Translator.ReadByte("CastID", idx);

            packet.Translator.ReadInt32<SpellId>("SpellID", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadUInt32("SpellXSpellVisualID", idx);

            packet.Translator.ReadUInt32("CastFlags", idx);
            packet.Translator.ReadUInt32("CastTime", idx);

            var hitTargetsCount = packet.Translator.ReadUInt32("HitTargetsCount", idx);
            var missTargetsCount = packet.Translator.ReadUInt32("MissTargetsCount", idx);
            var missStatusCount = packet.Translator.ReadUInt32("MissStatusCount", idx);

            ReadSpellTargetData(packet, idx, "Target");

            var remainingPowerCount = packet.Translator.ReadUInt32("RemainingPowerCount", idx);

            ReadMissileTrajectoryResult(packet, idx, "MissileTrajectory");

            ReadSpellAmmo(packet, idx, "Ammo");

            packet.Translator.ReadByte("DestLocSpellCastIndex", idx);

            var targetPointsCount = packet.Translator.ReadUInt32("TargetPointsCount", idx);

            ReadCreatureImmunities(packet, idx, "Immunities");

            ReadSpellHealPrediction(packet, idx, "Predict");

            for (var i = 0; i < hitTargetsCount; ++i)
                packet.Translator.ReadPackedGuid128("HitTarget", idx, i);

            for (var i = 0; i < missTargetsCount; ++i)
                packet.Translator.ReadPackedGuid128("MissTarget", idx, i);

            for (var i = 0; i < missStatusCount; ++i)
                ReadSpellMissStatus(packet, idx, "MissStatus", i);

            for (var i = 0; i < remainingPowerCount; ++i)
                ReadSpellPowerData(packet, idx, "RemainingPower", i);

            for (var i = 0; i < targetPointsCount; ++i)
                ReadLocation(packet, idx, "TargetPoints", i);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("CastFlagsEx", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 20 : 18, idx);

            var hasRuneData = packet.Translator.ReadBit("HasRuneData", idx);
            var hasProjectileVisual = ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173) && packet.Translator.ReadBit("HasProjectileVisual", idx);

            if (hasRuneData)
                ReadRuneData(packet, idx, "RemainingRunes");

            if (hasProjectileVisual)
                ReadProjectileVisual(packet, idx, "ProjectileVisual");
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Category");
                packet.Translator.ReadByte("Uses");
            }
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE_UPDATE)]
        public static void HandleWeeklySpellUsageUpdate(Packet packet)
        {
            packet.Translator.ReadInt32("Category");
            packet.Translator.ReadByte("Uses");
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadInt32("TimeRemaining");
        }

        public static void ReadSpellChannelStartInterruptImmunities(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("SchoolImmunities", idx);
            packet.Translator.ReadInt32("Immunities", idx);
        }

        public static void ReadSpellHealPrediction(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Points", idx);
            packet.Translator.ReadByte("Type", idx);
            packet.Translator.ReadPackedGuid128("BeaconGUID", idx);
        }

        public static void ReadSpellTargetedHealPrediction(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID", idx);
            ReadSpellHealPrediction(packet, idx, "Predict");
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("ChannelDuration");

            var hasInterruptImmunities = packet.Translator.ReadBit("HasInterruptImmunities");
            var hasHealPrediction = packet.Translator.ReadBit("HasHealPrediction");

            if (hasInterruptImmunities)
                ReadSpellChannelStartInterruptImmunities(packet, "InterruptImmunities");

            if (hasHealPrediction)
                ReadSpellTargetedHealPrediction(packet, "HealPrediction");
        }

        [Parser(Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS)]
        public static void HandleUpdateChainTargets(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster GUID");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            var count = packet.Translator.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadPackedGuid128("Targets", i);
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCanelAura(Packet packet)
        {
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadPackedGuid128("CasterGUID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Source");
            packet.Translator.ReadPackedGuid128("Target");

            packet.Translator.ReadVector3("TargetPosition");

            packet.Translator.ReadInt32("SpellVisualID");
            packet.Translator.ReadSingle("TravelSpeed");

            packet.Translator.ReadInt16("MissReason");
            packet.Translator.ReadInt16("ReflectStatus");

            packet.Translator.ReadSingle("Orientation");

            packet.Translator.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
            packet.Translator.ReadInt32("KitRecID");
            packet.Translator.ReadInt32("KitType");
            packet.Translator.ReadUInt32("Duration");
        }

        [Parser(Opcode.CMSG_UNLEARN_SKILL)]
        public static void HandleUnlearnSkill(Packet packet)
        {
            packet.Translator.ReadInt32("SkillLine");
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32("Reason");
            packet.Translator.ReadInt32("FailedArg1");
            packet.Translator.ReadInt32("FailedArg2");

            packet.Translator.ReadByte("Cast count");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterUnit");
            packet.Translator.ReadByte("CastID");
            packet.Translator.ReadUInt32<SpellId>("SpellID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadInt32("SpellXSpellVisualID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                packet.Translator.ReadInt16E<SpellCastFailureReason>("Reason");
            else
                packet.Translator.ReadByteE<SpellCastFailureReason>("Reason");
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterUnit");
            packet.Translator.ReadByte("CastID");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                packet.Translator.ReadByteE<SpellCastFailureReason>("Reason");
            else
                packet.Translator.ReadInt16E<SpellCastFailureReason>("Reason");
        }

        [Parser(Opcode.SMSG_REFRESH_SPELL_HISTORY)]
        [Parser(Opcode.SMSG_SEND_SPELL_HISTORY)]
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

                packet.Translator.ResetBitReader();

                var unused622_1 = false;
                var unused622_2 = false;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_2_20444))
                {
                    unused622_1 = packet.Translator.ReadBit();
                    unused622_2 = packet.Translator.ReadBit();
                }

                packet.Translator.ReadBit("OnHold", i);

                if (unused622_1)
                    packet.Translator.ReadUInt32("Unk_622_1", i);

                if (unused622_2)
                    packet.Translator.ReadUInt32("Unk_622_2", i);
            }
        }

        [Parser(Opcode.SMSG_SEND_SPELL_CHARGES)]
        public static void HandleSendSpellCharges(Packet packet)
        {
            var int4 = packet.Translator.ReadInt32("SpellChargeEntryCount");
            for (int i = 0; i < int4; i++)
            {
                packet.Translator.ReadUInt32("Category", i);
                packet.Translator.ReadUInt32("NextRecoveryTime", i);
                packet.Translator.ReadByte("ConsumedCharges", i);
            }
        }

        [Parser(Opcode.SMSG_AURA_POINTS_DEPLETED)]
        public static void HandleAuraPointsDepleted(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("EffectIndex");
        }

        [Parser(Opcode.SMSG_SPELL_MULTISTRIKE_EFFECT)]
        public static void HandleSpellMultistrikeEffect(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadPackedGuid128("Target");
            packet.Translator.ReadInt32<SpellId>("SpellID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadInt32("SpellXSpellVisualID");

            packet.Translator.ReadInt16("ProcCount");
            packet.Translator.ReadInt16("ProcNum");
        }

        [Parser(Opcode.SMSG_SPELL_DISPELL_LOG)]
        public static void HandleSpellDispelLog(Packet packet)
        {
            packet.Translator.ReadBit("IsSteal");
            packet.Translator.ReadBit("IsBreak");
            packet.Translator.ReadPackedGuid128("TargetGUID");
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadUInt32<SpellId>("DispelledBySpellID");
            var dataSize = packet.Translator.ReadUInt32("DispelCount");
            for (var i = 0; i < dataSize; ++i)
            {
                packet.Translator.ResetBitReader();
                packet.Translator.ReadUInt32<SpellId>("SpellID", i);
                packet.Translator.ReadBit("Harmful", i);
                var hasRolled = packet.Translator.ReadBit("HasRolled", i);
                var hasNeeded = packet.Translator.ReadBit("HasNeeded", i);
                if (hasRolled)
                    packet.Translator.ReadUInt32("Rolled", i);
                if (hasNeeded)
                    packet.Translator.ReadUInt32("Needed", i);
            }
        }

        [Parser(Opcode.SMSG_RESUME_CAST_BAR)]
        public static void HandleResumeCastBar(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadPackedGuid128("Target");

            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadUInt32("TimeRemaining");
            packet.Translator.ReadUInt32("TotalTime");

            var result = packet.Translator.ReadBit("HasInterruptImmunities");
            if (result)
            {
                packet.Translator.ReadUInt32("SchoolImmunities");
                packet.Translator.ReadUInt32("Immunities");
            }
        }

        [Parser(Opcode.SMSG_SPELL_DELAYED)]
        public static void HandleSpellDelayed(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadInt32("ActualDelay");
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadByte("CastID");
        }

        [Parser(Opcode.SMSG_DISMOUNT)]
        public static void HandleDismount(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_DISMOUNT_RESULT)]
        public static void HandleDismountResult(Packet packet)
        {
            packet.Translator.ReadUInt32("Result");
        }

        [Parser(Opcode.SMSG_DISPEL_FAILED)]
        public static void HandleDispelFailed(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VictimGUID");
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            var count = packet.Translator.ReadUInt32("FailedSpellsCount");
            for (int i = 0; i < count; i++)
                packet.Translator.ReadUInt32<SpellId>("FailedSpellID");
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadPackedGuid128("Totem");
            packet.Translator.ReadUInt32("Duration");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.CMSG_TOTEM_DESTROYED)]
        public static void HandleTotemDestroyed(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadPackedGuid128("TotemGUID");
        }

        [Parser(Opcode.SMSG_TOTEM_MOVED)]
        public static void HandleTotemMoved(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("NewSlot");
            packet.Translator.ReadPackedGuid128("Totem");
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleCooldownEvent60x(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleCooldownEvent61x(Packet packet)
        {
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_LOSS_OF_CONTROL_AURA_UPDATE)]
        public static void HandleLossOfControlAuraUpdate(Packet packet)
        {
            var count = packet.Translator.ReadInt32("LossOfControlInfoCount");
            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadByte("AuraSlot", i);
                packet.Translator.ReadByte("EffectIndex", i);
                packet.Translator.ReadBits("Type", 8, i);
                packet.Translator.ReadBits("Mechanic", 8, i);
            }
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleClearCooldown60x(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadBit("ClearOnHold");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleClearCooldown61x(Packet packet)
        {
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadBit("ClearOnHold");
            packet.Translator.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWNS)]
        public static void HandleClearCooldowns(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID");
            var count = packet.Translator.ReadInt32("SpellCount");
            for (int i = 0; i < count; i++)
                packet.Translator.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_BREAK_TARGET)]
        public static void HandleBreakTarget(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UnitGUID");
        }

        [Parser(Opcode.SMSG_PLAY_ORPHAN_SPELL_VISUAL)]
        public static void HandlePlayOrphanSpellVisual(Packet packet)
        {
            packet.Translator.ReadVector3("SourceLocation");
            packet.Translator.ReadVector3("SourceOrientation");
            packet.Translator.ReadVector3("TargetLocation");
            packet.Translator.ReadPackedGuid128("Target");
            packet.Translator.ReadInt32("SpellVisualID");
            packet.Translator.ReadSingle("TravelSpeed");
            packet.Translator.ReadSingle("UnkFloat");
            packet.Translator.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.SMSG_CANCEL_ORPHAN_SPELL_VISUAL)]
        public static void HandleCancelOrphanSpellVisual(Packet packet)
        {
            packet.Translator.ReadInt32("SpellVisualID");
        }

        [Parser(Opcode.SMSG_SPELL_INSTAKILL_LOG)]
        public static void HandleSpellInstakillLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Target");
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadByte("Flags");

            var count = packet.Translator.ReadInt32("SpellCooldownsCount");
            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadInt32("SrecID", i);
                packet.Translator.ReadInt32("ForcedCooldown", i);
            }
        }

        [Parser(Opcode.CMSG_GET_MIRROR_IMAGE_DATA)]
        [Parser(Opcode.SMSG_MIRROR_IMAGE_CREATURE_DATA)]
        public static void HandleGetMirrorImageData(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UnitGUID");
            packet.Translator.ReadInt32("DisplayID");
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UnitGUID");
            packet.Translator.ReadInt32("DisplayID");

            packet.Translator.ReadByte("RaceID");
            packet.Translator.ReadByte("Gender");
            packet.Translator.ReadByte("ClassID");
            packet.Translator.ReadByte("BeardVariation");  // SkinID
            packet.Translator.ReadByte("FaceVariation");   // FaceID
            packet.Translator.ReadByte("HairVariation");   // HairStyle
            packet.Translator.ReadByte("HairColor");       // HairColor
            packet.Translator.ReadByte("SkinColor");       // FacialHairStyle

            packet.Translator.ReadPackedGuid128("GuildGUID");

            var count = packet.Translator.ReadInt32("ItemDisplayCount");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt32("ItemDisplayID", i);
        }

        [Parser(Opcode.SMSG_MISSILE_CANCEL)]
        public static void HandleMissileCancel(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("OwnerGUID");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadBit("Reverse");
        }

        [Parser(Opcode.SMSG_MODIFY_COOLDOWN, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleModifyCooldown60x(Packet packet)
        {
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadPackedGuid128("UnitGUID");
            packet.Translator.ReadInt32("DeltaTime");
        }

        [Parser(Opcode.SMSG_MODIFY_COOLDOWN, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleModifyCooldown61x(Packet packet)
        {
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("DeltaTime");
            packet.Translator.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_TARGET)]
        public static void HandleClearTarget(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_SHOW_TRADE_SKILL_RESPONSE)]
        public static void HandleShowTradeSkillResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID");

            packet.Translator.ReadInt32<SpellId>("SpellID");

            var int4 = packet.Translator.ReadInt32("SkillLineCount");
            var int20 = packet.Translator.ReadInt32("SkillRankCount");
            var int36 = packet.Translator.ReadInt32("SkillMaxRankCount");
            var int52 = packet.Translator.ReadInt32("KnownAbilitySpellCount");

            for (int i = 0; i < int4; i++)
                packet.Translator.ReadInt32("SkillLineIDs", i);

            for (int i = 0; i < int20; i++)
                packet.Translator.ReadInt32("SkillRanks", i);

            for (int i = 0; i < int36; i++)
                packet.Translator.ReadInt32("SkillMaxRanks", i);

            for (int i = 0; i < int52; i++)
                packet.Translator.ReadInt32("KnownAbilitySpellIDs", i);
        }

        [Parser(Opcode.SMSG_NOTIFY_MISSILE_TRAJECTORY_COLLISION)]
        public static void HandleNotifyMissileTrajectoryCollision(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadByte("CastID");
            packet.Translator.ReadVector3("CollisionPos");
        }

        [Parser(Opcode.SMSG_MOUNT_RESULT)]
        public static void HandleMountResult(Packet packet)
        {
            packet.Translator.ReadInt32E<MountResult>("Result");
        }

        [Parser(Opcode.SMSG_RESYNC_RUNES)]
        public static void HandleResyncRunes(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadByte("RuneType");
                packet.Translator.ReadByte("Cooldown");
            }
        }

        [Parser(Opcode.SMSG_DISENCHANT_CREDIT)]
        public static void HandleDisenchantCredit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Disenchanter");
            ItemHandler.ReadItemInstance(packet);
        }

        [Parser(Opcode.SMSG_UNLEARNED_SPELLS)]
        public static void HandleUnlearnedSpells(Packet packet)
        {
            var count = packet.Translator.ReadInt32("UnlearnedSpellCount");
            for (int i = 0; i < count; i++)
                packet.Translator.ReadUInt32<SpellId>("SpellID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802))
                packet.Translator.ReadBit("Unk612 1");
        }

        [Parser(Opcode.SMSG_ADD_LOSS_OF_CONTROL)]
        public static void HandleAddLossOfControl(Packet packet)
        {
            packet.Translator.ReadBits("Mechanic", 8);
            packet.Translator.ReadBits("Type", 8);

            packet.Translator.ReadInt32<SpellId>("SpellID");

            packet.Translator.ReadPackedGuid128("Caster");

            packet.Translator.ReadInt32("Duration");
            packet.Translator.ReadInt32("DurationRemaining");
            packet.Translator.ReadInt32("LockoutSchoolMask");
        }

        [Parser(Opcode.SMSG_SET_SPELL_CHARGES, ClientVersionBuild.Zero, ClientVersionBuild.V6_2_0_20173)]
        public static void HandleSetSpellCharges(Packet packet)
        {
            packet.Translator.ReadUInt32("Category");
            packet.Translator.ReadSingle("Count");
            packet.Translator.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_SET_SPELL_CHARGES, ClientVersionBuild.V6_2_0_20173)]
        public static void HandleSetSpellCharges62x(Packet packet)
        {
            packet.Translator.ReadUInt32("Category");
            packet.Translator.ReadUInt32("RecoveryTime");
            packet.Translator.ReadByte("ConsumedCharges");
            packet.Translator.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST)]
        public static void HandleNotifyDestLocSpellCast(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadPackedGuid128("DestTransport");
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadVector3("SourceLoc");
            packet.Translator.ReadVector3("DestLoc");
            packet.Translator.ReadSingle("MissileTrajectoryPitch");
            packet.Translator.ReadSingle("MissileTrajectorySpeed");
            packet.Translator.ReadInt32("TravelTime");
            packet.Translator.ReadByte("DestLocSpellCastIndex");
            packet.Translator.ReadByte("CastID");
        }

        [Parser(Opcode.SMSG_CLEAR_ALL_SPELL_CHARGES, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)] // Bounds NOT confirmed
        public static void HandleClearAllSpellCharges(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.SMSG_CLEAR_ALL_SPELL_CHARGES, ClientVersionBuild.V6_1_0_19678)] // Bounds NOT confirmed
        public static void HandleClearAllSpellCharges610(Packet packet)
        {
            packet.Translator.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_SPELL_CHARGES)]
        public static void HandleClearSpellCharges(Packet packet)
        {
            packet.Translator.ReadBit("IsPet");
            packet.Translator.ReadInt32("Category"); // SpellCategoryEntry->ID
        }

        [Parser(Opcode.SMSG_PLAYER_BOUND)]
        public static void HandlePlayerBound(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("BinderID");
            packet.Translator.ReadUInt32<AreaId>("AreaID");
        }

        [Parser(Opcode.CMSG_CONFIRM_RESPEC_WIPE)]
        public static void HandleConfirmRespecWipe(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("RespecMaster");
            packet.Translator.ReadByte("RespecType");
        }

        [Parser(Opcode.SMSG_RESPEC_WIPE_CONFIRM)]
        public static void HandleRespecWipeConfirm(Packet packet)
        {
            packet.Translator.ReadSByte("RespecType");
            packet.Translator.ReadUInt32("Cost");
            packet.Translator.ReadPackedGuid128("RespecMaster");
        }

        [Parser(Opcode.SMSG_SUPERCEDED_SPELLS)]
        public static void HandleSupercededSpells(Packet packet)
        {
            var spellCount = packet.Translator.ReadInt32("");
            var supercededCount = packet.Translator.ReadInt32("");

            for (int i = 0; i < spellCount; i++)
                packet.Translator.ReadInt32("SpellID", i);

            for (int i = 0; i < supercededCount; i++)
                packet.Translator.ReadInt32("Superceded", i);
        }

        [Parser(Opcode.CMSG_MISSILE_TRAJECTORY_COLLISION)]
        public static void HandleMissileTrajectoryCollision(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadByte("CastID");
            packet.Translator.ReadVector3("CollisionPos");
        }

        [Parser(Opcode.CMSG_LEARN_TALENTS)]
        public static void HandleLearnTalents(Packet packet)
        {
            var talentCount = packet.Translator.ReadInt32("TalentCount");
            for (int i = 0; i < talentCount; i++)
                packet.Translator.ReadInt16("Talents");
        }

        [Parser(Opcode.SMSG_CANCEL_SPELL_VISUAL)]
        public static void HandleCancelSpellVisual(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Source");
            packet.Translator.ReadInt32("SpellVisualID");
        }

        [Parser(Opcode.SMSG_CANCEL_SPELL_VISUAL_KIT)]
        public static void HandleCancelSpellVisualKit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Source");
            packet.Translator.ReadInt32("SpellVisualKitID");
        }

        [Parser(Opcode.SMSG_SPIRIT_HEALER_CONFIRM)]
        public static void HandleSpiritHealerConfirm(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.CMSG_CANCEL_MOD_SPEED_NO_CONTROL_AURAS)]
        public static void HandleCancelModSpeedNoControlAuras(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
        }
    }
}
