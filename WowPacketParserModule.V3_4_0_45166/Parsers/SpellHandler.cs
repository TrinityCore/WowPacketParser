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

            for (var i = 0; i < optionalReagentCount; ++i)
                ReadOptionalReagent(packet, idx, "OptionalReagent", i);

            for (var j = 0; j < optionalCurrenciesCount; ++j)
                ReadOptionalCurrency(packet, idx, "OptionalCurrency", j);


            packet.ResetBitReader();
            packet.ReadBits("SendCastFlags", 5, idx);
            var hasMoveUpdate = packet.ReadBit("HasMoveUpdate", idx);

            var weightCount = packet.ReadBits("WeightCount", 2, idx);

            V7_0_3_22248.Parsers.SpellHandler.ReadSpellTargetData(packet, null, spellId, idx, "Target");

            if (hasMoveUpdate)
                V7_0_3_22248.Parsers.MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellWeight(packet, idx, "Weight", i);

            return spellId;
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            ReadSpellCastRequest(packet, "Cast");
        }
    }
}
