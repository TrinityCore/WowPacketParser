using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

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

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.ReadPackedGuid128("CraftingNPC", idx);

            var optionalCurrenciesCount = packet.ReadUInt32("OptionalCurrenciesCount", idx);
            var optionalReagentsCount = packet.ReadUInt32("OptionalReagentsCount", idx);
            var removedModificationsCount = packet.ReadUInt32("RemovedModificationsCount", idx);

            packet.ReadByte("CraftingFlags", idx);

            for (var j = 0; j < optionalCurrenciesCount; ++j)
                V9_0_1_36216.Parsers.SpellHandler.ReadOptionalCurrency(packet, idx, "ExtraCurrencyCosts", j);

            packet.ResetBitReader();

            var hasReceiveTime = packet.ReadBit("HasReceiveTime", idx);
            var hasMoveUpdate = packet.ReadBit("HasMoveUpdate", idx);
            var weightCount = packet.ReadBits("WeightCount", 2, idx);
            var hasCraftingOrderID = packet.ReadBit("HasCrafingOrderID", idx);

            V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(packet, null, spellId, idx, "Target");

            if (hasReceiveTime)
                packet.ReadUInt32("ReceiveTime", idx);

            if (hasCraftingOrderID)
                packet.ReadUInt64("CraftingOrderID", idx);

            for (var i = 0; i < optionalReagentsCount; ++i)
                V9_0_1_36216.Parsers.SpellHandler.ReadOptionalReagent(packet, idx, "CraftingReagents", i);

            for (var i = 0; i < removedModificationsCount; ++i)
                V9_0_1_36216.Parsers.SpellHandler.ReadOptionalReagent(packet, idx, "RemovedReagents", i);

            if (hasMoveUpdate)
                Substructures.MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellWeight(packet, idx, "Weight", i);

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
    }
}
