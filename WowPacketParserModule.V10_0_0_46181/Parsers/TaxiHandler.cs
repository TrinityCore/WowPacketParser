using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class TaxiHandler
    {
        [Parser(Opcode.SMSG_SHOW_TAXI_NODES, ClientVersionBuild.V7_3_0_24920)]
        public static void HandleShowTaxiNodes(Packet packet)
        {
            var hasWindowInfo = packet.ReadBit("HasWindowInfo");
            var canLandNodesCount = packet.ReadUInt32();
            var canUseNodesCount = packet.ReadUInt32();

            if (hasWindowInfo)
            {
                packet.ReadPackedGuid128("UnitGUID");
                packet.ReadUInt32("CurrentNode");
            }

            for (var i = 0u; i < canLandNodesCount; ++i)
                packet.ReadUInt64("CanLandNodes", i);

            for (var i = 0u; i < canUseNodesCount; ++i)
                packet.ReadUInt64("CanUseNodes", i);

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }
    }
}
