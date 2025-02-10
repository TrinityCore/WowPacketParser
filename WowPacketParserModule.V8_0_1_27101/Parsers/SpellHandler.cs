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

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellTargetData(Packet packet, PacketSpellData packetSpellData, uint spellID, params object[] idx)
        {
            packet.ResetBitReader();

            if (ClientVersion.AddedInVersion(ClientType.Dragonflight) || ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                packet.ReadBitsE<TargetFlag>("Flags", 28, idx);
            else if (ClientVersion.IsWotLKClientVersionBuild(ClientVersion.Build))
                packet.ReadBitsE<TargetFlag>("Flags", 27, idx);
            else if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_5_29683))
                packet.ReadBitsE<TargetFlag>("Flags", 26, idx);
            else
                packet.ReadBitsE<TargetFlag>("Flags", 25, idx);

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
                V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "SrcLocation");

            Vector3? dstLocation = null;
            if (hasDstLoc)
            {
                dstLocation = V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "DstLocation");
                if (packetSpellData != null)
                    packetSpellData.DstLocation = dstLocation;
            }

            if (hasOrient)
            {
                var orientation = packet.ReadSingle("Orientation", idx);
                if (packetSpellData != null)
                    packetSpellData.DstOrientation = orientation;
            }

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
                        var isValidDBTarget = (short[] implicitTargets, Targets type) =>
                        {
                            foreach (var implicitTarget in implicitTargets)
                                if ((Targets)implicitTarget == type)
                                    return true;

                            return false;
                        };
                        var isSingleDBTarget = isValidDBTarget(effect.ImplicitTarget, Targets.TARGET_DEST_DB);
                        var isMultipleDBTarget = isValidDBTarget(effect.ImplicitTarget, Targets.TARGET_DEST_NEARBY_DB);
                        if (isSingleDBTarget || isMultipleDBTarget)
                        {
                            string effectHelper = $"Spell: { StoreGetters.GetName(StoreNameType.Spell, (int)spellID) } Effect {effect.EffectIndex}: { effect.Effect } ({ (SpellEffects)effect.Effect })";

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

                            if (isSingleDBTarget)
                            {
                                if (!Storage.SpellTargetPositions.ContainsKey(spellTargetPosition))
                                {
                                    spellTargetPosition.OrderIndex = "0";
                                    Storage.SpellTargetPositions.Add(spellTargetPosition);
                                }
                            }
                            else
                                Storage.SpellTargetMultiplePositions.Add(spellID, spellTargetPosition, packet.TimeSpan);
                        }
                    }
                }
            }

            packet.ReadWoWString("Name", nameLength, idx);
        }

        public static void ReadTalentGroupInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SpecId", idx);

            var talentIDsCount = packet.ReadUInt32("TalentIDsCount", idx);
            var pvpTalentIDsCount = packet.ReadUInt32("PvPTalentIDsCount", idx);

            for (var i = 0; i < talentIDsCount; ++i)
                packet.ReadUInt16("TalentID", idx, i);

            for (var i = 0; i < pvpTalentIDsCount; ++i)
            {
                packet.ReadUInt16("PvPTalentID", idx, i);
                packet.ReadByte("Slot", idx, i);
            }
        }

        public static void ReadTalentInfoUpdate(Packet packet, params object[] idx)
        {
            packet.ReadByte("ActiveGroup", idx);
            packet.ReadInt32("PrimarySpecialization", idx);

            var talentGroupsCount = packet.ReadUInt32("TalentGroupsCount", idx);
            for (var i = 0; i < talentGroupsCount; ++i)
                ReadTalentGroupInfo(packet, idx, "TalentGroupsCount", i);
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadUpdateTalentData(Packet packet)
        {
            ReadTalentInfoUpdate(packet, "Info");
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

            packetSpellData.AmmoDisplayId =  packet.ReadInt32("Ammo.DisplayID", idx);

            packet.ReadByte("DestLocSpellCastIndex", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadCreatureImmunities(packet, idx, "Immunities");

            V6_0_2_19033.Parsers.SpellHandler.ReadSpellHealPrediction(packet, idx, "Predict");

            packet.ResetBitReader();

            var hitTargetsCount = packet.ReadBits("HitTargetsCount", 16, idx);
            var missTargetsCount = packet.ReadBits("MissTargetsCount", 16, idx);
            uint hitStatusCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                hitStatusCount = packet.ReadBits("HitStatusCount", 16, idx);
            var missStatusCount = packet.ReadBits("MissStatusCount", 16, idx);
            var remainingPowerCount = packet.ReadBits("RemainingPowerCount", 9, idx);

            var hasRuneData = packet.ReadBit("HasRuneData", idx);
            var targetPointsCount = packet.ReadBits("TargetPointsCount", 16, idx);

            for (var i = 0; i < missStatusCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellMissStatus(packet, idx, "MissStatus", i);

            ReadSpellTargetData(packet, packetSpellData, spellID, idx, "Target");

            for (var i = 0; i < hitTargetsCount; ++i)
                packetSpellData.HitTargets.Add(packet.ReadPackedGuid128("HitTarget", idx, i));

            for (var i = 0; i < missTargetsCount; ++i)
                packetSpellData.MissedTargets.Add(packet.ReadPackedGuid128("MissTarget", idx, i));

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                for (var i = 0; i < hitStatusCount; ++i)
                    packet.ReadByte("HitStatus", idx, i);

            for (var i = 0; i < remainingPowerCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellPowerData(packet, idx, "RemainingPower", i);

            if (hasRuneData)
                V7_0_3_22248.Parsers.SpellHandler.ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                packetSpellData.TargetPoints.Add(V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "TargetPoints", i));

            return packetSpellData;
        }

        public static void ReadSpellCastLogData(Packet packet, params object[] idx)
        {
            packet.ReadInt64("Health", idx);
            packet.ReadInt32("AttackPower", idx);
            packet.ReadInt32("SpellPower", idx);
            packet.ReadInt32("Armor", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_5_57171))
            {
                packet.ReadInt32("Unknown_1105_1", idx);
                packet.ReadInt32("Unknown_1105_2", idx);
            }

            packet.ResetBitReader();

            var spellLogPowerDataCount = packet.ReadBits("SpellLogPowerData", 9, idx);

            // SpellLogPowerData
            for (var i = 0; i < spellLogPowerDataCount; ++i)
            {
                if (ClientVersion.RemovedInVersion(ClientType.TheWarWithin))
                    packet.ReadInt32E<PowerType>("PowerType", idx, i);
                else
                    packet.ReadByteE<PowerType>("PowerType", idx, i);
                packet.ReadInt32("Amount", idx, i);
                packet.ReadInt32("Cost", idx, i);
            }
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

        public static void ReadContentTuningParams(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();

            packet.ReadInt16("PlayerLevelDelta", idx);
            packet.ReadUInt16("PlayerItemLevel", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724) && !ClientVersion.IsClassicVanillaClientVersionBuild(ClientVersion.Build))
                packet.ReadUInt16("TargetItemLevel", idx);
            packet.ReadUInt16("ScalingHealthItemLevelCurveID", idx);
            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);
            packet.ReadByte("TargetMinScalingLevel", idx);
            packet.ReadByte("TargetMaxScalingLevel", idx);
            packet.ReadSByte("TargetScalingLevelDelta", idx);
            packet.ReadBits("Type", 4, idx);
            packet.ReadBit("ScalesWithItemLevel", idx);
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
                    packet.ReadInt32("SpellXSpellVisualID", i);
                    var flags = packet.ReadByteE<AuraFlagMoP>("Flags", i);
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
                        ReadContentTuningParams(packet, i, "ContentTuning");

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

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.ReadPackedGuid128("Source");
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("Transport");

            packet.ReadVector3("TargetPosition");

            packet.ReadInt32("SpellVisualID");
            packet.ReadSingle("TravelSpeed");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadUInt16("HitReason");
            packet.ReadUInt16("MissReason");
            packet.ReadUInt16("ReflectStatus");

            packet.ReadSingle("LaunchDelay");
            packet.ReadSingle("MinDuration");

            packet.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.SMSG_PLAY_ORPHAN_SPELL_VISUAL)]
        public static void HandlePlayOrphanSpellVisual(Packet packet)
        {
            packet.ReadVector3("SourceLocation");
            packet.ReadVector3("SourceRotation");
            packet.ReadVector3("TargetLocation");
            packet.ReadPackedGuid128("Target");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                packet.ReadPackedGuid128("TargetTransport");
            packet.ReadInt32("SpellVisualID");
            packet.ReadSingle("TravelSpeed");
            packet.ReadSingle("LaunchDelay");
            packet.ReadSingle("MinDuration");
            packet.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.CMSG_CANCEL_CHANNELLING)]
        public static void HandleCancelChanneling(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("Reason");
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

        [Parser(Opcode.SMSG_SEND_SPELL_CHARGES)] // already since 7.?.?
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

        [Parser(Opcode.SMSG_RESUME_CAST)]
        public static void HandleResumeCast(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32("SpellXSpellVisualID");
            packet.ReadPackedGuid128("CastID");
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_INTERRUPT_POWER_REGEN)]
        public static void HandleInterruptPowerRegen(Packet packet)
        {
            packet.ReadUInt32("PowerTypeEnum");
        }
    }
}
