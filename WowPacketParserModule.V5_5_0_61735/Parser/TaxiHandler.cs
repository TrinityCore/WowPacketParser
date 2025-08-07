using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class TaxiHandler
    {
        [Parser(Opcode.SMSG_TAXI_NODE_STATUS)]
        public static void HandleTaxiStatus(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadBits("Status", 2);
        }

        [Parser(Opcode.SMSG_ACTIVATE_TAXI_REPLY)]
        public static void HandleActivateTaxiReply(Packet packet)
        {
            packet.ReadBitsE<TaxiError>("Result", 4);
        }

        [Parser(Opcode.SMSG_SHOW_TAXI_NODES)]
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

        [Parser(Opcode.SMSG_NEW_TAXI_PATH)]
        public static void HandleTaxiNull(Packet packet)
        {
        }
    }
}
