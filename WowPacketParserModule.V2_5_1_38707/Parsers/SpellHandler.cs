using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V2_5_1_38707.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellCastData(Packet packet, params object[] idx)
        {
            var dbdata = new PacketSpellData();
            packet.ReadPackedGuid128("CasterGUID", idx);
            dbdata.Caster = packet.ReadPackedGuid128("CasterUnit", idx);

            packet.ReadPackedGuid128("CastID", idx);
            packet.ReadPackedGuid128("OriginalCastID", idx);

            dbdata.Spell = packet.ReadUInt32<SpellId>("SpellID", idx);
            packet.ReadUInt32("SpellXSpellVisualID", idx);

            dbdata.Flags = packet.ReadUInt32("CastFlags", idx);
            dbdata.Flags2 = packet.ReadUInt32("CastFlagsEx", idx);
            dbdata.CastTime = packet.ReadUInt32("CastTime", idx);

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

            for (var i = 0; i < missStatusCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellMissStatus(packet, idx, "MissStatus", i);

            V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, dbdata, dbdata.Spell, idx, "Target");

            for (var i = 0; i < hitTargetsCount; ++i)
                dbdata.HitTargets.Add(packet.ReadPackedGuid128("HitTarget", idx, i));

            for (var i = 0; i < missTargetsCount; ++i)
                dbdata.MissedTargets.Add(packet.ReadPackedGuid128("MissTarget", idx, i));

            for (var i = 0; i < remainingPowerCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellPowerData(packet, idx, "RemainingPower", i);

            if (hasRuneData)
                V7_0_3_22248.Parsers.SpellHandler.ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "TargetPoints", i);

            if (hasAmmoDisplayId)
                dbdata.AmmoDisplayId = packet.ReadInt32("AmmoDisplayId", idx);

            if (hasAmmoInventoryType)
                dbdata.AmmoInventoryType = (uint)packet.ReadByteE<InventoryType>("InventoryType", idx);
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

            packet.ResetBitReader();
            var hasLog = packet.ReadBit();
            if (hasLog)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "LogData");
        }

        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifier(Packet packet)
        {
            var modCount = packet.ReadUInt32("SpellModifierCount");
            for (var i = 0; i < modCount; ++i)
            {
                packet.ReadByteE<SpellModOp>("SpellMod", i);

                var dataCount = packet.ReadUInt32("SpellModifierDataCount", i);
                for (var j = 0; j < dataCount; ++j)
                {
                    packet.ReadInt32("Amount", j, i);
                    packet.ReadByte("SpellMask", j, i);
                }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            packet.ReadBit("UpdateAll");
            var count = packet.ReadBits("AurasCount", 9);

            var auras = new List<Aura>();
            for (var i = 0; i < count; ++i)
            {
                var aura = new Aura();

                aura.Slot = packet.ReadByte("Slot", i);

                packet.ResetBitReader();
                var hasAura = packet.ReadBit("HasAura", i);
                if (hasAura)
                {
                    packet.ReadPackedGuid128("CastID", i);
                    aura.SpellId = (uint)packet.ReadInt32<SpellId>("SpellID", i);
                    packet.ReadInt32("SpellXSpellVisualID", i);
                    aura.AuraFlags = packet.ReadUInt16E<AuraFlagMoP>("Flags", i);
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
                        WowPacketParserModule.V9_0_1_36216.Parsers.CombatLogHandler.ReadContentTuningParams(packet, i, "ContentTuning");

                    if (hasCastUnit)
                        aura.CasterGuid = packet.ReadPackedGuid128("CastUnit", i);

                    aura.Duration = hasDuration ? packet.ReadInt32("Duration", i) : 0;
                    aura.MaxDuration = hasRemaining ? packet.ReadInt32("Remaining", i) : 0;

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

        [Parser(Opcode.SMSG_RESUME_CAST)]
        public static void HandleResumeCast(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32("SpellXSpellVisualID");
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
            packet.ReadInt32("SpellXSpellVisualID");
            packet.ReadUInt32("TimeRemaining");
            packet.ReadUInt32("TotalTime");

            var result = packet.ReadBit("HasInterruptImmunities");
            if (result)
            {
                packet.ReadUInt32("SchoolImmunities");
                packet.ReadUInt32("Immunities");
            }
        }

        [Parser(Opcode.CMSG_LEARN_TALENT)]
        public static void HandleLearnTalent(Packet packet)
        {
            packet.ReadInt32("TalentID");
            packet.ReadUInt16("Rank?");
        }
    }
}
