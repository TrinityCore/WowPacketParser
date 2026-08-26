using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class SpellHandler
    {
        public static uint ReadSpellCastRequest(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CastID", idx);
            packet.ReadByte("SendCastFlags", idx);

            for (var i = 0; i < 3; i++)
                packet.ReadInt32("Misc", idx, i);

            var spellId = packet.ReadUInt32<SpellId>("SpellID", idx);

            V9_0_1_36216.Parsers.SpellHandler.ReadSpellCastVisual(packet, idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V12_1_0_69214))
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, null, spellId, idx, "Target");

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.ReadPackedGuid128("CraftingNPC", idx);

            var optionalCurrenciesCount = packet.ReadUInt32("OptionalCurrenciesCount", idx);
            var optionalReagentsCount = packet.ReadUInt32("OptionalReagentsCount", idx);
            var removedModificationsCount = packet.ReadUInt32("RemovedModificationsCount", idx);

            packet.ReadByte("CraftingFlags", idx);

            for (var j = 0; j < optionalCurrenciesCount; ++j)
                V9_0_1_36216.Parsers.SpellHandler.ReadOptionalCurrency(packet, idx, "ExtraCurrencyCosts", j);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V12_1_0_69214))
            {
                for (var i = 0; i < optionalReagentsCount; ++i)
                    V9_0_1_36216.Parsers.SpellHandler.ReadOptionalReagent(packet, idx, "CraftingReagents", i);

                for (var i = 0; i < removedModificationsCount; ++i)
                    V9_0_1_36216.Parsers.SpellHandler.ReadOptionalReagent(packet, idx, "RemovedReagents", i);
            }

            packet.ResetBitReader();

            var hasReceiveTime = packet.ReadBit("HasReceiveTime", idx);
            var hasMoveUpdate = packet.ReadBit("HasMoveUpdate", idx);
            var weightCount = packet.ReadBits("WeightCount", 2, idx);
            var hasCraftingOrderId = packet.ReadBit("HasCrafingOrderID", idx);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V12_1_0_69214))
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, null, spellId, idx, "Target");

            if (hasReceiveTime)
                packet.ReadUInt32("ReceiveTime", idx);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V12_1_0_69214))
            {
                if (hasCraftingOrderId)
                    packet.ReadUInt64("CraftingOrderID", idx);

                for (var i = 0; i < optionalReagentsCount; ++i)
                    V9_0_1_36216.Parsers.SpellHandler.ReadOptionalReagent(packet, idx, "CraftingReagents", i);

                for (var i = 0; i < removedModificationsCount; ++i)
                    V9_0_1_36216.Parsers.SpellHandler.ReadOptionalReagent(packet, idx, "RemovedReagents", i);
            }

            if (hasMoveUpdate)
                Substructures.MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellWeight(packet, idx, "Weight", i);

            if (hasCraftingOrderId && ClientVersion.AddedInVersion(ClientVersionBuild.V12_1_0_69214))
                packet.ReadUInt64("CraftingOrderID", idx);

            return spellId;
        }

        [Parser(Opcode.CMSG_CAST_SPELL, ClientVersionBuild.V12_0_7_68182)]
        public static void HandleCastSpell(Packet packet)
        {
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_PET_CAST_SPELL, ClientVersionBuild.V12_0_7_68182)]
        public static void HandlePetCastSpell(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            ReadSpellCastRequest(packet, "Cast");
        }

        public static PacketSpellData ReadSpellCastData(Packet packet, params object[] idx)
        {
            var packetSpellData = new PacketSpellData();
            packet.ReadPackedGuid128("CasterGUID", idx);
            packetSpellData.Caster = packet.ReadPackedGuid128("CasterUnit", idx);

            packetSpellData.CastGuid = packet.ReadPackedGuid128("CastID", idx);
            packet.ReadPackedGuid128("OriginalCastID", idx);

            var spellId = packetSpellData.Spell = packet.ReadUInt32<SpellId>("SpellID", idx);
            V9_0_1_36216.Parsers.SpellHandler.ReadSpellCastVisual(packet, idx, "Visual");

            packetSpellData.Flags = packet.ReadUInt32("CastFlags", idx);
            packetSpellData.Flags2 = packet.ReadUInt32("CastFlagsEx", idx);
            packet.ReadUInt32("CastFlagsEx2", idx);

            packetSpellData.CastTime = packet.ReadUInt32("CastTime", idx);

            V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, packetSpellData, spellId, idx, "Target");
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

            for (var i = 0; i < hitTargetsCount; ++i)
                packetSpellData.HitTargets.Add(packet.ReadPackedGuid128("HitTarget", idx, i));

            for (var i = 0; i < missTargetsCount; ++i)
                packetSpellData.MissedTargets.Add(packet.ReadPackedGuid128("MissTarget", idx, i));

            for (var i = 0; i < hitStatusCount; ++i)
                packet.ReadByte("HitStatus", idx, i);

            for (var i = 0; i < missStatusCount; ++i)
                V9_0_1_36216.Parsers.SpellHandler.ReadSpellMissStatus_10_1_7(packet, idx, "MissStatus", i);

            for (var i = 0; i < remainingPowerCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellPowerData(packet, idx, "RemainingPower", i);

            if (hasRuneData)
                V7_0_3_22248.Parsers.SpellHandler.ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                packetSpellData.TargetPoints.Add(V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "TargetPoints", i));

            return packetSpellData;
        }

        [Parser(Opcode.SMSG_SPELL_START, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleSpellStart(Packet packet)
        {
            PacketSpellStart packetSpellStart = new();
            packetSpellStart.Data = ReadSpellCastData(packet, "Cast");
            packet.Holder.SpellStart = packetSpellStart;
        }

        [Parser(Opcode.SMSG_SPELL_GO, ClientVersionBuild.V12_1_0_69214)]
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

        public static void ReadLearnedSpellInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<SpellId>("SpellID", indexes);

            packet.ResetBitReader();
            packet.ReadBit("IsFavorite", indexes);
            var hasEquipableSpellInvSlot = packet.ReadBit();
            var hasSuperceded = packet.ReadBit();
            var hasTraitDefinition = packet.ReadBit();

            if (hasEquipableSpellInvSlot)
                packet.ReadInt32("EquipableSpellInvSlot", indexes);

            if (hasSuperceded)
                packet.ReadInt32<SpellId>("Superceded", indexes);

            if (hasTraitDefinition)
                packet.ReadInt32("TraitDefinitionID", indexes);
        }

        [Parser(Opcode.SMSG_LEARNED_SPELLS, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleLearnedSpells(Packet packet)
        {
            var spellCount = packet.ReadUInt32();
            packet.ReadUInt32("SpecializationID");
            packet.ReadInt32("MinActionBarSlot");

            for (var i = 0; i < spellCount; ++i)
                ReadLearnedSpellInfo(packet, "ClientLearnedSpellData", i);

            packet.ResetBitReader();
            packet.ReadBit("SuppressMessaging");
            packet.ReadBit("TraitGrantedByAura");
        }

        [Parser(Opcode.SMSG_SPELL_VISUAL_LOAD_SCREEN, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleSpellVisualLoadScreen(Packet packet)
        {
            packet.ReadInt32("SpellVisualKitID");
            packet.ReadInt32("Duration");
            packet.ReadInt32("Delay");
            packet.ReadBit("Unknown_1210");
        }
    }
}
