using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TaxiHandler
    {
        [Parser(Opcode.CMSG_TAXI_NODE_STATUS_QUERY)]
        [Parser(Opcode.CMSG_TAXI_QUERY_AVAILABLE_NODES)]
        [Parser(Opcode.CMSG_ENABLE_TAXI_NODE)]
        public static void HandleTaxiStatusQuery(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_TAXI_NODE_STATUS)]
        public static void HandleTaxiStatus(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("Known");
        }

        [Parser(Opcode.SMSG_SHOW_TAXI_NODES, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleShowTaxiNodes(Packet packet)
        {
            var u = packet.Translator.ReadUInt32("Unk UInt32 1");
            if (u != 0)
            {
                packet.Translator.ReadGuid("GUID");
                packet.Translator.ReadUInt32("Node ID");
            }
            var i = 0;
            while (packet.CanRead())
                packet.Translator.ReadUInt64("NodeMask", i++);
        }

        [Parser(Opcode.SMSG_SHOW_TAXI_NODES, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleShowTaxiNodes434(Packet packet)
        {
            var u = packet.Translator.ReadUInt32("Unk UInt32 1");
            if (u != 0)
            {
                packet.Translator.ReadGuid("GUID");
                packet.Translator.ReadUInt32("Node ID");
            }

            var count = packet.Translator.ReadInt32("Count");
            for (int i = 0; i < count; ++i)
                packet.Translator.ReadByte("NodeMask", i);
        }

        [Parser(Opcode.CMSG_ACTIVATE_TAXI)]
        public static void HandleActivateTaxi(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Node 1 ID");
            packet.Translator.ReadUInt32("Node 2 ID");
        }

        [Parser(Opcode.SMSG_ACTIVATE_TAXI_REPLY)]
        public static void HandleActivateTaxiReply(Packet packet)
        {
            packet.Translator.ReadUInt32E<TaxiError>("Result");
        }

        [Parser(Opcode.CMSG_ACTIVATE_TAXI_EXPRESS)]
        public static void HandleActivateTaxiExpress(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.Translator.ReadUInt32("Cost");

            var count = packet.Translator.ReadUInt32("Node Count");
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadUInt32("Node ID", i);

        }

        [Parser(Opcode.CMSG_SET_TAXI_BENCHMARK_MODE)]
        public static void HandleSetTaxiBenchmarkMode(Packet packet)
        {
            packet.Translator.ReadBool("Activate");
        }

        [Parser(Opcode.SMSG_NEW_TAXI_PATH)]
        public static void HandleTaxiNull(Packet packet)
        {
        }

        //Missing Feedback
        //[Parser(Opcode.CMSG_TAXICLEARALLNODES)]
        //[Parser(Opcode.CMSG_TAXIENABLEALLNODES)]
        //[Parser(Opcode.CMSG_TAXISHOWNODES)]
        //[Parser(Opcode.CMSG_TAXICLEARNODE)]
        //[Parser(Opcode.CMSG_TAXIENABLENODE)]
    }
}
