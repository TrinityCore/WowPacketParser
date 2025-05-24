using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class TaxiHandler
    {
        [Parser(Opcode.SMSG_ACTIVATE_TAXI_REPLY)]
        public static void HandleActivateTaxiReply(Packet packet)
        {
            packet.ReadBitsE<TaxiError>("Result", 4);
        }

        [Parser(Opcode.SMSG_SHOW_TAXI_NODES, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_TAXI_NODE_STATUS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTaxiStatus(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadBits("Status", 2);
        }

        [Parser(Opcode.CMSG_ACTIVATE_TAXI, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleActivateTaxi(Packet packet)
        {
            packet.ReadPackedGuid128("Vendor");
            packet.ReadUInt32("Node");
            packet.ReadUInt32("GroundMountID");
            packet.ReadUInt32("FlyingMountID");
        }

        [Parser(Opcode.CMSG_ENABLE_TAXI_NODE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleEnableTaxiNode(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.CMSG_SET_TAXI_BENCHMARK_MODE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetTaxiBenchmarkMode(Packet packet)
        {
            packet.ReadBool("Activate");
        }

        [Parser(Opcode.CMSG_TAXI_NODE_STATUS_QUERY, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_TAXI_QUERY_AVAILABLE_NODES, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTaxinodeStatusQuery(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
        }

        [Parser(Opcode.SMSG_NEW_TAXI_PATH, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_TAXI_REQUEST_EARLY_LANDING, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTaxiNull(Packet packet)
        {
        }
    }
}
