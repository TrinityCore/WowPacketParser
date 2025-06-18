using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using SpellCastFailureReason = WowPacketParser.Enums.Version.V6_1_0_19678.SpellCastFailureReason;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellCastLogData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Health", idx);
            packet.ReadInt32("AttackPower", idx);
            packet.ReadInt32("SpellPower", idx);

            var int3 = packet.ReadInt32("SpellLogPowerData", idx);

            // SpellLogPowerData
            for (var i = 0; i < int3; ++i)
            {
                packet.ReadInt32("PowerType", idx, i);
                packet.ReadInt32("Amount", idx, i);
            }

            packet.ResetBitReader();

            var bit32 = packet.ReadBit("bit32", idx);

            if (bit32)
                packet.ReadSingle("Float7", idx);
        }

        public static void ReadSpellTargetData(Packet packet, PacketSpellData spellData, params object[] idx)
        {
            packet.ResetBitReader();

            packet.ReadBitsE<TargetFlag>("Flags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678) ? 23 : 21, idx);
            var hasSrcLoc = packet.ReadBit("HasSrcLocation", idx);
            var hasDstLoc = packet.ReadBit("HasDstLocation", idx);
            var hasOrient = packet.ReadBit("HasOrientation", idx);
            var nameLength = packet.ReadBits(7);

            var targetUnit = packet.ReadPackedGuid128("Unit", idx);
            if (spellData != null)
                spellData.TargetUnit = targetUnit;
            packet.ReadPackedGuid128("Item", idx);

            if (hasSrcLoc)
                ReadLocation(packet, idx, "SrcLocation");

            if (hasDstLoc)
            {
                var dstLocation = ReadLocation(packet, idx, "DstLocation");
                if (spellData != null)
                    spellData.DstLocation = dstLocation;
            }

            if (hasOrient)
            {
                var orientation = packet.ReadSingle("Orientation", idx);
                if (spellData != null)
                    spellData.DstOrientation = orientation;
            }

            packet.ReadWoWString("Name", nameLength, idx);
        }

        public static void ReadMissileTrajectoryRequest(Packet packet, params object[] idx)
        {
            packet.ReadSingle("Pitch", idx);
            packet.ReadSingle("Speed", idx);
        }

        public static void ReadSpellWeight(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBits("Type", 2, idx); // Enum SpellweightTokenTypes
            packet.ReadInt32("ID", idx);
            packet.ReadInt32("Quantity", idx);
        }

        public static Vector3 ReadLocation(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Transport", idx);
            return packet.ReadVector3("Location", idx);
        }

        public static void ReadSpellPowerData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Cost", idx);
            packet.ReadByteE<PowerType>("Type", idx);
        }

        public static uint ReadSpellCastRequest(Packet packet, params object[] idx)
        {
            packet.ReadByte("CastID", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
            {
                for (var i = 0; i < 2; i++)
                    packet.ReadInt32("Misc", idx, i);
            }

            uint spellId = packet.ReadUInt32<SpellId>("SpellID", idx);
            packet.ReadInt32(ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? "SpellXSpellVisualID" : "Misc", idx);

            ReadSpellTargetData(packet, null, idx, "Target");

            ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.ReadPackedGuid128("Guid", idx);

            packet.ResetBitReader();

            packet.ReadBits("SendCastFlags", 5, idx);
            var hasMoveUpdate = packet.ReadBit("HasMoveUpdate", idx);

            var weightCount = packet.ReadBits("WeightCount", 2, idx);

            if (hasMoveUpdate)
                Substructures.MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                ReadSpellWeight(packet, idx, "Weight", i);

            return spellId;
        }

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            var count = packet.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
                packet.ReadInt32<SpellId>("Spell ID", i);
            packet.ReadBit("SuppressMessaging");
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleSendKnownSpells(Packet packet)
        {
            packet.ReadBit("InitialLogin");
            var knownSpellsCount = packet.ReadUInt32("KnownSpellsCount");

            for (var i = 0; i < knownSpellsCount; i++)
                packet.ReadUInt32<SpellId>("KnownSpellId", i);
        }

        [Parser(Opcode.SMSG_PET_CLEAR_SPELLS)]
        public static void HandleSpellZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Category", i);
                packet.ReadInt32("ModCooldown", i);
            }
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierFlat(Packet packet)
        {
            var modCount = packet.ReadUInt32("Modifier type count");

            for (var j = 0; j < modCount; ++j)
            {
                packet.ReadByteE<SpellModOp>("Spell Mod", j);

                var modTypeCount = packet.ReadUInt32("Count", j);
                for (var i = 0; i < modTypeCount; ++i)
                {
                    packet.ReadSingle("Amount", j, i);
                    packet.ReadByte("Spell Mask bitpos", j, i);
                }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            PacketAuraUpdate packetAuraUpdate = packet.Holder.AuraUpdate = new();
            packet.ReadBit("UpdateAll");
            var guid = packet.ReadPackedGuid128("UnitGUID");
            packetAuraUpdate.Unit = guid;
            var count = packet.ReadUInt32("AurasCount");

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
                    aura.SpellId = auraEntry.Spell = (uint)packet.ReadInt32<SpellId>("SpellID", i);
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                        packet.ReadUInt32("SpellXSpellVisualID", i);
                    var flags = packet.ReadByteE<AuraFlagMoP>("Flags", i);
                    aura.AuraFlags = flags;
                    auraEntry.Flags = flags.ToUniversal();
                    packet.ReadInt32("ActiveFlags", i);
                    aura.Level = packet.ReadUInt16("CastLevel", i);
                    aura.Charges = packet.ReadByte("Applications", i);

                    var int72 = packet.ReadUInt32("Int56 Count", i);
                    var effectCount = packet.ReadUInt32("Effect Count", i);

                    for (var j = 0; j < int72; ++j)
                        packet.ReadSingle("Points", i, j);

                    for (var j = 0; j < effectCount; ++j)
                        packet.ReadSingle("EstimatedPoints", i, j);

                    packet.ResetBitReader();
                    var hasCasterGUID = packet.ReadBit("HasCastUnit", i);
                    var hasDuration = packet.ReadBit("HasDuration", i);
                    var hasMaxDuration = packet.ReadBit("HasRemaining", i);

                    if (hasCasterGUID)
                        auraEntry.CasterUnit = packet.ReadPackedGuid128("CastUnit", i);

                    aura.Duration = hasDuration ? packet.ReadInt32("Duration", i) : 0;
                    aura.MaxDuration = hasMaxDuration ? packet.ReadInt32("Remaining", i) : 0;

                    if (hasDuration)
                        auraEntry.Duration = aura.Duration;

                    if (hasMaxDuration)
                        auraEntry.Remaining = aura.MaxDuration;

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
            packet.ReadUInt32("SpecId", idx);
            var talentIDsCount = packet.ReadInt32("TalentIDsCount", idx);

            for (var i = 0; i < 6; ++i)
                packet.ReadUInt16("GlyphIDs", idx, i);

            for (var i = 0; i < talentIDsCount; ++i)
                packet.ReadUInt16("TalentID", idx, i);
        }

        public static void ReadTalentInfoUpdate(Packet packet, params object[] idx)
        {
            packet.ReadByte("ActiveGroup");

            var talentGroupsCount = packet.ReadInt32("TalentGroupsCount");
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
            packet.ReadPackedGuid128("PetGUID");
            ReadSpellCastRequest(packet, "Cast");
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
                ReadSpellCastLogData(packet, "LogData");
        }

        public static void ReadMissileTrajectoryResult(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("TravelTime", idx);
            packet.ReadSingle("Pitch", idx);
        }

        public static void ReadSpellAmmo(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("DisplayID", idx);
            packet.ReadByteE<InventoryType>("InventoryType", idx);
        }

        public static void ReadCreatureImmunities(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("School", idx);
            packet.ReadUInt32("Value", idx);
        }

        public static void ReadSpellMissStatus(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            var reason = packet.ReadBits("Reason", 4, idx); // TODO enum
            if (reason == 11)
                packet.ReadBits("ReflectStatus", 4, idx);
        }

        public static void ReadRuneData(Packet packet, params object[] idx)
        {
            packet.ReadByte("Start", idx);
            packet.ReadByte("Count", idx);

            packet.ResetBitReader();

            var cooldownCount = packet.ReadBits("CooldownCount", 3, idx);

            for (var i = 0; i < cooldownCount; ++i)
                packet.ReadByte("Cooldowns", idx, i);
        }

        public static void ReadProjectileVisual(Packet packet, params object[] idx)
        {
            for (var i = 0; i < 2; ++i)
                packet.ReadInt32("Id", idx, i);
        }

        public static PacketSpellData ReadSpellCastData(Packet packet, params object[] idx)
        {
            var packetSpellData = new PacketSpellData();
            packet.ReadPackedGuid128("CasterGUID", idx);
            packetSpellData.Caster = packet.ReadPackedGuid128("CasterUnit", idx);

            packetSpellData.CastId = packet.ReadByte("CastID", idx);

            packetSpellData.Spell = (uint)packet.ReadInt32<SpellId>("SpellID", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.ReadUInt32("SpellXSpellVisualID", idx);

            packetSpellData.Flags = packet.ReadUInt32("CastFlags", idx);
            packetSpellData.CastTime = packet.ReadUInt32("CastTime", idx);

            var hitTargetsCount = packet.ReadUInt32("HitTargetsCount", idx);
            var missTargetsCount = packet.ReadUInt32("MissTargetsCount", idx);
            var missStatusCount = packet.ReadUInt32("MissStatusCount", idx);

            ReadSpellTargetData(packet, packetSpellData, idx, "Target");

            var remainingPowerCount = packet.ReadUInt32("RemainingPowerCount", idx);

            ReadMissileTrajectoryResult(packet, idx, "MissileTrajectory");

            ReadSpellAmmo(packet, idx, "Ammo");

            packet.ReadByte("DestLocSpellCastIndex", idx);

            var targetPointsCount = packet.ReadUInt32("TargetPointsCount", idx);

            ReadCreatureImmunities(packet, idx, "Immunities");

            ReadSpellHealPrediction(packet, idx, "Predict");

            for (var i = 0; i < hitTargetsCount; ++i)
                packetSpellData.HitTargets.Add(packet.ReadPackedGuid128("HitTarget", idx, i));

            for (var i = 0; i < missTargetsCount; ++i)
                packetSpellData.MissedTargets.Add(packet.ReadPackedGuid128("MissTarget", idx, i));

            for (var i = 0; i < missStatusCount; ++i)
                ReadSpellMissStatus(packet, idx, "MissStatus", i);

            for (var i = 0; i < remainingPowerCount; ++i)
                ReadSpellPowerData(packet, idx, "RemainingPower", i);

            for (var i = 0; i < targetPointsCount; ++i)
                packetSpellData.TargetPoints.Add(ReadLocation(packet, idx, "TargetPoints", i));

            packet.ResetBitReader();

            packetSpellData.Flags2 = packet.ReadBits("CastFlagsEx", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 20 : 18, idx);

            var hasRuneData = packet.ReadBit("HasRuneData", idx);
            var hasProjectileVisual = ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173) && packet.ReadBit("HasProjectileVisual", idx);

            if (hasRuneData)
                ReadRuneData(packet, idx, "RemainingRunes");

            if (hasProjectileVisual)
                ReadProjectileVisual(packet, idx, "ProjectileVisual");

            return packetSpellData;
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Category");
                packet.ReadByte("Uses");
            }
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE_UPDATE)]
        public static void HandleWeeklySpellUsageUpdate(Packet packet)
        {
            packet.ReadInt32("Category");
            packet.ReadByte("Uses");
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32("TimeRemaining");
        }

        public static void ReadSpellChannelStartInterruptImmunities(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SchoolImmunities", idx);
            packet.ReadInt32("Immunities", idx);
        }

        public static void ReadSpellHealPrediction(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Points", idx);
            packet.ReadByte("Type", idx);
            packet.ReadPackedGuid128("BeaconGUID", idx);
        }

        public static void ReadSpellTargetedHealPrediction(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("TargetGUID", idx);
            ReadSpellHealPrediction(packet, idx, "Predict");
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("ChannelDuration");

            var hasInterruptImmunities = packet.ReadBit("HasInterruptImmunities");
            var hasHealPrediction = packet.ReadBit("HasHealPrediction");

            if (hasInterruptImmunities)
                ReadSpellChannelStartInterruptImmunities(packet, "InterruptImmunities");

            if (hasHealPrediction)
                ReadSpellTargetedHealPrediction(packet, "HealPrediction");
        }

        [Parser(Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS)]
        public static void HandleUpdateChainTargets(Packet packet)
        {
            packet.ReadPackedGuid128("Caster GUID");
            packet.ReadUInt32<SpellId>("SpellID");
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                packet.ReadPackedGuid128("Targets", i);
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCanelAura(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("CasterGUID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.ReadPackedGuid128("Source");
            packet.ReadPackedGuid128("Target");

            packet.ReadVector3("TargetPosition");

            packet.ReadInt32("SpellVisualID");
            packet.ReadSingle("TravelSpeed");

            packet.ReadInt16("MissReason");
            packet.ReadInt16("ReflectStatus");

            packet.ReadSingle("Orientation");

            packet.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            var playSpellVisualKit = packet.Holder.PlaySpellVisualKit = new();
            playSpellVisualKit.Unit = packet.ReadPackedGuid128("Unit");
            playSpellVisualKit.KitRecId = packet.ReadInt32("KitRecID");
            playSpellVisualKit.KitType = packet.ReadInt32("KitType");
            playSpellVisualKit.Duration = packet.ReadUInt32("Duration");
        }

        [Parser(Opcode.CMSG_UNLEARN_SKILL)]
        public static void HandleUnlearnSkill(Packet packet)
        {
            packet.ReadInt32("SkillLine");
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            var spellFail = packet.Holder.SpellCastFailed = new();
            spellFail.Spell = (uint)packet.ReadInt32<SpellId>("SpellID");
            spellFail.Success = packet.ReadInt32("Reason") == 0;
            packet.ReadInt32("FailedArg1");
            packet.ReadInt32("FailedArg2");

            spellFail.CastId = packet.ReadByte("Cast count");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            var spellFail = packet.Holder.SpellFailure = new();
            spellFail.Caster = packet.ReadPackedGuid128("CasterUnit");
            spellFail.CastId = packet.ReadByte("CastID");
            spellFail.Spell = packet.ReadUInt32<SpellId>("SpellID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.ReadInt32("SpellXSpellVisualID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                spellFail.Success = packet.ReadInt16E<SpellCastFailureReason>("Reason") == SpellCastFailureReason.Success;
            else
                spellFail.Success = packet.ReadByteE<SpellCastFailureReason>("Reason") == SpellCastFailureReason.Success;
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var spellFail = packet.Holder.SpellFailure = new();
            spellFail.Caster = packet.ReadPackedGuid128("CasterUnit");
            spellFail.CastId = packet.ReadByte("CastID");
            spellFail.Spell = packet.ReadUInt32<SpellId>("SpellID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                spellFail.Success = packet.ReadByteE<SpellCastFailureReason>("Reason") == SpellCastFailureReason.Success;
            else
                spellFail.Success = packet.ReadInt16E<SpellCastFailureReason>("Reason") == SpellCastFailureReason.Success;
        }

        [Parser(Opcode.SMSG_REFRESH_SPELL_HISTORY)]
        [Parser(Opcode.SMSG_SEND_SPELL_HISTORY)]
        public static void HandleSendSpellHistory(Packet packet)
        {
            var int4 = packet.ReadInt32("SpellHistoryEntryCount");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadUInt32<SpellId>("SpellID", i);
                packet.ReadUInt32<ItemId>("ItemID", i);
                packet.ReadUInt32("Category", i);
                packet.ReadInt32("RecoveryTime", i);
                packet.ReadInt32("CategoryRecoveryTime", i);

                packet.ResetBitReader();

                var unused622_1 = false;
                var unused622_2 = false;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_2_20444))
                {
                    unused622_1 = packet.ReadBit();
                    unused622_2 = packet.ReadBit();
                }

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
                packet.ReadByte("ConsumedCharges", i);
            }
        }

        [Parser(Opcode.SMSG_AURA_POINTS_DEPLETED)]
        public static void HandleAuraPointsDepleted(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_0_59347))
                packet.ReadUInt16("Slot");
            else
                packet.ReadByte("Slot");

            packet.ReadByte("EffectIndex");
        }

        [Parser(Opcode.SMSG_SPELL_MULTISTRIKE_EFFECT)]
        public static void HandleSpellMultistrikeEffect(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32<SpellId>("SpellID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.ReadInt32("SpellXSpellVisualID");

            packet.ReadInt16("ProcCount");
            packet.ReadInt16("ProcNum");
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

        [Parser(Opcode.SMSG_RESUME_CAST_BAR)]
        public static void HandleResumeCastBar(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadPackedGuid128("Target");

            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadUInt32("TimeRemaining");
            packet.ReadUInt32("TotalTime");

            var result = packet.ReadBit("HasInterruptImmunities");
            if (result)
            {
                packet.ReadUInt32("SchoolImmunities");
                packet.ReadUInt32("Immunities");
            }
        }

        [Parser(Opcode.SMSG_SPELL_DELAYED)]
        public static void HandleSpellDelayed(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadInt32("ActualDelay");
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadByte("CastID");
        }

        [Parser(Opcode.SMSG_DISMOUNT)]
        public static void HandleDismount(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_DISMOUNT_RESULT)]
        public static void HandleDismountResult(Packet packet)
        {
            packet.ReadUInt32("Result");
        }

        [Parser(Opcode.SMSG_DISPEL_FAILED)]
        public static void HandleDispelFailed(Packet packet)
        {
            packet.ReadPackedGuid128("VictimGUID");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadUInt32<SpellId>("SpellID");
            var count = packet.ReadUInt32("FailedSpellsCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32<SpellId>("FailedSpellID");
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadPackedGuid128("Totem");
            packet.ReadUInt32("Duration");
            packet.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.CMSG_TOTEM_DESTROYED)]
        public static void HandleTotemDestroyed(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadPackedGuid128("TotemGUID");
        }

        [Parser(Opcode.SMSG_TOTEM_MOVED)]
        public static void HandleTotemMoved(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadByte("NewSlot");
            packet.ReadPackedGuid128("Totem");
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleCooldownEvent60x(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleCooldownEvent61x(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_LOSS_OF_CONTROL_AURA_UPDATE)]
        public static void HandleLossOfControlAuraUpdate(Packet packet)
        {
            var count = packet.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                packet.ReadByte("AuraSlot", i);
                packet.ReadByte("EffectIndex", i);
                packet.ReadBitsE<LossOfControlType>("LocType", 8, i);
                packet.ReadBitsE<SpellMechanic>("Mechanic", 8, i);
            }
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleClearCooldown60x(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadBit("ClearOnHold");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleClearCooldown61x(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadBit("ClearOnHold");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWNS, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleClearCooldowns(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            var count = packet.ReadInt32("SpellCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWNS, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleClearCooldowns612(Packet packet)
        {
            var count = packet.ReadInt32("SpellCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32<SpellId>("SpellID");

            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_BREAK_TARGET)]
        public static void HandleBreakTarget(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
        }

        [Parser(Opcode.SMSG_PLAY_ORPHAN_SPELL_VISUAL)]
        public static void HandlePlayOrphanSpellVisual(Packet packet)
        {
            packet.ReadVector3("SourceLocation");
            packet.ReadVector3("SourceOrientation");
            packet.ReadVector3("TargetLocation");
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32("SpellVisualID");
            packet.ReadSingle("TravelSpeed");
            packet.ReadSingle("UnkFloat");
            packet.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.SMSG_CANCEL_ORPHAN_SPELL_VISUAL)]
        public static void HandleCancelOrphanSpellVisual(Packet packet)
        {
            packet.ReadInt32("SpellVisualID");
        }

        [Parser(Opcode.SMSG_SPELL_INSTAKILL_LOG)]
        public static void HandleSpellInstakillLog(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("Caster");
            packet.ReadUInt32<SpellId>("SpellID");
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
                WowPacketParser.Parsing.Parsers.SpellHandler.FillSpellListCooldown((uint)spellId, time, guid.GetEntry());
            }
        }

        [Parser(Opcode.CMSG_GET_MIRROR_IMAGE_DATA)]
        [Parser(Opcode.SMSG_MIRROR_IMAGE_CREATURE_DATA)]
        public static void HandleGetMirrorImageData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_5_47777))
                packet.ReadInt32("DisplayID");
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("DisplayID");

            packet.ReadByte("RaceID");
            packet.ReadByte("Gender");
            packet.ReadByte("ClassID");
            packet.ReadByte("BeardVariation");  // SkinID
            packet.ReadByte("FaceVariation");   // FaceID
            packet.ReadByte("HairVariation");   // HairStyle
            packet.ReadByte("HairColor");       // HairColor
            packet.ReadByte("SkinColor");       // FacialHairStyle

            packet.ReadPackedGuid128("GuildGUID");

            var count = packet.ReadInt32("ItemDisplayCount");
            for (var i = 0; i < count; i++)
                packet.ReadInt32("ItemDisplayID", i);
        }

        [Parser(Opcode.SMSG_MISSILE_CANCEL)]
        public static void HandleMissileCancel(Packet packet)
        {
            packet.ReadPackedGuid128("OwnerGUID");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadBit("Reverse");
        }

        [Parser(Opcode.SMSG_MODIFY_COOLDOWN, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleModifyCooldown60x(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("DeltaTime");
        }

        [Parser(Opcode.SMSG_MODIFY_COOLDOWN, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleModifyCooldown61x(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("DeltaTime");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_TARGET)]
        public static void HandleClearTarget(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_SHOW_TRADE_SKILL_RESPONSE)]
        public static void HandleShowTradeSkillResponse(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");

            packet.ReadInt32<SpellId>("SpellID");

            var int4 = packet.ReadInt32("SkillLineCount");
            var int20 = packet.ReadInt32("SkillRankCount");
            var int36 = packet.ReadInt32("SkillMaxRankCount");
            var int52 = packet.ReadInt32("KnownAbilitySpellCount");

            for (int i = 0; i < int4; i++)
                packet.ReadInt32("SkillLineIDs", i);

            for (int i = 0; i < int20; i++)
                packet.ReadInt32("SkillRanks", i);

            for (int i = 0; i < int36; i++)
                packet.ReadInt32("SkillMaxRanks", i);

            for (int i = 0; i < int52; i++)
                packet.ReadInt32("KnownAbilitySpellIDs", i);
        }

        [Parser(Opcode.SMSG_NOTIFY_MISSILE_TRAJECTORY_COLLISION)]
        public static void HandleNotifyMissileTrajectoryCollision(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadByte("CastID");
            packet.ReadVector3("CollisionPos");
        }

        [Parser(Opcode.SMSG_MOUNT_RESULT)]
        public static void HandleMountResult(Packet packet)
        {
            packet.ReadInt32E<MountResult>("Result");
        }

        [Parser(Opcode.SMSG_RESYNC_RUNES)]
        public static void HandleResyncRunes(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadByte("RuneType");
                packet.ReadByte("Cooldown");
            }
        }

        [Parser(Opcode.SMSG_DISENCHANT_CREDIT)]
        public static void HandleDisenchantCredit(Packet packet)
        {
            packet.ReadPackedGuid128("Disenchanter");
            Substructures.ItemHandler.ReadItemInstance(packet);
        }

        [Parser(Opcode.SMSG_UNLEARNED_SPELLS)]
        public static void HandleUnlearnedSpells(Packet packet)
        {
            var count = packet.ReadInt32("UnlearnedSpellCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32<SpellId>("SpellID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802))
                packet.ReadBit("Unk612 1");
        }

        [Parser(Opcode.SMSG_ADD_LOSS_OF_CONTROL)]
        public static void HandleAddLossOfControl(Packet packet)
        {
            packet.ReadBits("Mechanic", 8);
            packet.ReadBits("Type", 8);

            packet.ReadInt32<SpellId>("SpellID");

            packet.ReadPackedGuid128("Caster");

            packet.ReadInt32("Duration");
            packet.ReadInt32("DurationRemaining");
            packet.ReadInt32("LockoutSchoolMask");
        }

        [Parser(Opcode.SMSG_SET_SPELL_CHARGES, ClientVersionBuild.Zero, ClientVersionBuild.V6_2_0_20173)]
        public static void HandleSetSpellCharges(Packet packet)
        {
            packet.ReadUInt32("Category");
            packet.ReadSingle("Count");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_SET_SPELL_CHARGES, ClientVersionBuild.V6_2_0_20173)]
        public static void HandleSetSpellCharges62x(Packet packet)
        {
            packet.ReadUInt32("Category");
            packet.ReadUInt32("RecoveryTime");
            packet.ReadByte("ConsumedCharges");
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST)]
        public static void HandleNotifyDestLocSpellCast(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("DestTransport");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadVector3("SourceLoc");
            packet.ReadVector3("DestLoc");
            packet.ReadSingle("MissileTrajectoryPitch");
            packet.ReadSingle("MissileTrajectorySpeed");
            packet.ReadInt32("TravelTime");
            packet.ReadByte("DestLocSpellCastIndex");
            packet.ReadByte("CastID");
        }

        [Parser(Opcode.SMSG_CLEAR_ALL_SPELL_CHARGES, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)] // Bounds NOT confirmed
        public static void HandleClearAllSpellCharges(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.SMSG_CLEAR_ALL_SPELL_CHARGES, ClientVersionBuild.V6_1_0_19678)] // Bounds NOT confirmed
        public static void HandleClearAllSpellCharges610(Packet packet)
        {
            packet.ReadBit("IsPet");
        }

        [Parser(Opcode.SMSG_CLEAR_SPELL_CHARGES)]
        public static void HandleClearSpellCharges(Packet packet)
        {
            packet.ReadBit("IsPet");
            packet.ReadInt32("Category"); // SpellCategoryEntry->ID
        }

        [Parser(Opcode.SMSG_PLAYER_BOUND)]
        public static void HandlePlayerBound(Packet packet)
        {
            packet.ReadPackedGuid128("BinderID");
            packet.ReadUInt32<AreaId>("AreaID");
        }

        [Parser(Opcode.CMSG_CONFIRM_RESPEC_WIPE)]
        public static void HandleConfirmRespecWipe(Packet packet)
        {
            packet.ReadPackedGuid128("RespecMaster");
            packet.ReadByte("RespecType");
        }

        [Parser(Opcode.SMSG_RESPEC_WIPE_CONFIRM)]
        public static void HandleRespecWipeConfirm(Packet packet)
        {
            packet.ReadSByte("RespecType");
            packet.ReadUInt32("Cost");
            packet.ReadPackedGuid128("RespecMaster");
        }

        [Parser(Opcode.SMSG_SUPERCEDED_SPELLS)]
        public static void HandleSupercededSpells(Packet packet)
        {
            var spellCount = packet.ReadInt32("");
            var supercededCount = packet.ReadInt32("");

            for (int i = 0; i < spellCount; i++)
                packet.ReadInt32("SpellID", i);

            for (int i = 0; i < supercededCount; i++)
                packet.ReadInt32("Superceded", i);
        }

        [Parser(Opcode.CMSG_MISSILE_TRAJECTORY_COLLISION)]
        public static void HandleMissileTrajectoryCollision(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadByte("CastID");
            packet.ReadVector3("CollisionPos");
        }

        [Parser(Opcode.CMSG_LEARN_TALENTS)]
        public static void HandleLearnTalents(Packet packet)
        {
            var talentCount = packet.ReadInt32("TalentCount");
            for (int i = 0; i < talentCount; i++)
                packet.ReadInt16("Talents");
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
        }

        [Parser(Opcode.SMSG_SPIRIT_HEALER_CONFIRM)]
        public static void HandleSpiritHealerConfirm(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.CMSG_CANCEL_MOD_SPEED_NO_CONTROL_AURAS)]
        public static void HandleCancelModSpeedNoControlAuras(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
        }

        [Parser(Opcode.CMSG_UPDATE_MISSILE_TRAJECTORY)]
        public static void HandleUpdateMissileTrajectory(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
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
    }
}
