using System;
using System.Collections.Generic;
using System.Linq;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class SpellHandler1158
    {
        public static void ReadPetFlags(Packet packet, params object[] idx)
        {
            packet.ReadByteE<CommandState>("CommandState");
            packet.ReadByte("Flag");
            packet.ReadByteE<ReactState>("ReactState");
        }

        public static void ReadPetSpellCooldownData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SpellID", idx);
            packet.ReadInt32("Duration", idx);
            packet.ReadInt32("CategoryDuration", idx);
            packet.ReadSingle("ModRate", idx);
            packet.ReadInt16("Category", idx);
        }

        public static void ReadPetSpellHistoryData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("CategoryID", idx);
            packet.ReadInt32("RecoveryTime", idx);
            packet.ReadSingle("ChargeModRate", idx);
            packet.ReadSByte("ConsumedCharges", idx);
        }

        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE, ClientBranch.Classic)]
        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE, ClientBranch.TBC)]
        public static void HandlePetSpells(Packet packet)
        {
            var petGuid = packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt16("CreatureFamily");
            packet.ReadInt16("Specialization");
            packet.ReadInt32("TimeLimit");

            ReadPetFlags(packet, "PetModeAndOrders");

            const int maxCreatureSpells = 10;
            for (var i = 0; i < maxCreatureSpells; i++) // Read pet / vehicle spell ids
            {
                var (slot, spellId) = V6_0_2_19033.Parsers.PetHandler.ReadPetAction(packet, "ActionButtons", i);

                if (spellId == 0)
                    continue;

                if (slot == 7 && spellId != 2 || slot == 6 && spellId < 10)
                    continue;

                // pets do not have npc entry available in sniff - skip
                if (petGuid.GetHighType() == HighGuidType.Pet)
                    continue;

                var operationName = "";
                if (slot == 7 && spellId == 2)
                    operationName = "Attack";
                else
                    operationName = StoreGetters.GetName(StoreNameType.Spell, (int)spellId, false);

                var potentialKey = (int)(petGuid.GetEntry() * 100 + CreatureSpellList.ConvertDifficultyToIdx(CoreParsers.MovementHandler.CurrentDifficultyID));
                if (Storage.CreatureSpellLists.Where(p => p.Item1.Id == potentialKey && p.Item1.SpellId == spellId).SingleOrDefault() == null)
                    Storage.CreatureSpellLists.Add(new CreatureSpellList
                    {
                        Id = potentialKey,
                        Position = i,
                        SpellId = (int)spellId,
                        Comments = StoreGetters.GetName(StoreNameType.Unit, (int)petGuid.GetEntry(), false) + " - " + operationName
                    });
            }

            var actionsCount = packet.ReadInt32("ActionsCount");
            var cooldownsCount = packet.ReadUInt32("CooldownsCount");
            var spellHistoryCount = packet.ReadUInt32("SpellHistoryCount");

            for (int i = 0; i < actionsCount; i++)
                V6_0_2_19033.Parsers.PetHandler.ReadPetAction(packet, i, "Actions");

            for (int i = 0; i < cooldownsCount; i++)
                ReadPetSpellCooldownData(packet, i, "PetSpellCooldown");


            for (int i = 0; i < spellHistoryCount; i++)
                ReadPetSpellHistoryData(packet, i, "PetSpellHistory");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE, ClientBranch.TBC)]
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

                auraEntry.Slot = packet.ReadUInt16("Slot", i);

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
                    packet.ReadVector3("DstLocation", i);

                    packet.ResetBitReader();

                    var hasCastUnit = packet.ReadBit("HasCastUnit", i);
                    var hasCastItem = ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_5_64796) && packet.ReadBit("HasCastItem", i);
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

                    if (hasCastItem)
                        packet.ReadPackedGuid128("CastItem", i);

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

        public static void ReadSpellCastLogData(Packet packet, params object[] idx)
        {
            packet.ReadInt64("Health", idx);
            packet.ReadInt32("AttackPower", idx);
            packet.ReadInt32("SpellPower", idx);
            packet.ReadInt32("Armor", idx);
            packet.ReadInt32("Unknown_1105_1", idx);
            packet.ReadInt32("Unknown_1105_2", idx);

            packet.ResetBitReader();

            var spellLogPowerDataCount = packet.ReadBits("SpellLogPowerData", 9, idx);

            // SpellLogPowerData
            for (var i = 0; i < spellLogPowerDataCount; ++i)
            {
                packet.ReadByteE<PowerType>("PowerType", idx, i);
                packet.ReadInt32("Amount", idx, i);
                packet.ReadInt32("Cost", idx, i);
            }
        }

        public static void ReadMissileTrajectoryResult(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("TravelTime", idx);
            packet.ReadSingle("Pitch", idx);
        }

        public static void ReadCreatureImmunities(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("School", idx);
            packet.ReadUInt32("Value", idx);
        }

        public static void ReadSpellHealPrediction(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Points", idx);
            if (ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_5_64796))
                packet.ReadInt32("Type", idx);
            else
                packet.ReadByte("Type", idx);
            packet.ReadPackedGuid128("BeaconGUID", idx);
        }

        public static Vector3 ReadLocation(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Transport", idx);
            return packet.ReadVector3("Location", idx);
        }

        public static void ReadSpellTargetData(Packet packet, PacketSpellData packetSpellData, uint spellID, params object[] idx)
        {
            packet.ResetBitReader();

            if (ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_5_64796))
                packet.ReadUInt32E<TargetFlag>("Flags", idx);
            else
                packet.ReadBitsE<TargetFlag>("Flags", 28, idx);

            if (ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_5_64796))
            {
                var targetUnit = packet.ReadPackedGuid128("Unit", idx);
                if (packetSpellData != null)
                    packetSpellData.TargetUnit = targetUnit;
                packet.ReadPackedGuid128("Item", idx);
            }

            if (ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_5_64796))
            {
                packet.ReadPackedGuid128("HousingGUID", idx);
                packet.ReadBit("HousingIsResident", idx);
            }

            var hasSrcLoc = packet.ReadBit("HasSrcLocation", idx);
            var hasDstLoc = packet.ReadBit("HasDstLocation", idx);
            var hasOrient = packet.ReadBit("HasOrientation", idx);
            var hasMapID = packet.ReadBit("hasMapID ", idx);
            var nameLength = packet.ReadBits(7);

            if (ClientVersion.RemovedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_5_64796))
            {
                var targetUnit = packet.ReadPackedGuid128("Unit", idx);
                if (packetSpellData != null)
                    packetSpellData.TargetUnit = targetUnit;
                packet.ReadPackedGuid128("Item", idx);
            }

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

        public static void ReadSpellMissStatus(Packet packet, params object[] idx)
        {
            var reason = packet.ReadByte("Reason", idx); // TODO enum
            if (reason == 11)
                packet.ReadByte("ReflectStatus", idx);
        }

        public static void ReadSpellPowerData(Packet packet, params object[] idx)
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
            packet.ReadUInt32("CastFlagsEx2", idx);
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
                ReadSpellPowerData(packet, idx, "RemainingPower", i);

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

        [Parser(Opcode.SMSG_SPELL_GO, ClientBranch.Classic)]
        [Parser(Opcode.SMSG_SPELL_GO, ClientBranch.TBC)]
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
    }
}
