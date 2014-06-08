using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class TaxiHandler
    {
        [Parser(Opcode.CMSG_TAXINODE_STATUS_QUERY)]
        public static void HandleTaxiStatusQuery(Packet packet)
        {
            var guid = packet.StartBitStream(0, 1, 6, 4, 5, 2, 3, 7);
            packet.ParseBitStream(guid, 4, 1, 5, 0, 2, 7, 6, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_TAXIQUERYAVAILABLENODES)]
        public static void HandleTaxiQuery(Packet packet)
        {
            var guid = packet.StartBitStream(1, 3, 0, 5, 7, 4, 6, 2);
            packet.ParseBitStream(guid, 4, 1, 7, 3, 0, 2, 5, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TAXINODE_STATUS)]
        public static void HandleTaxiStatus(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Known");
        }

        [Parser(Opcode.SMSG_SHOWTAXINODES, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleShowTaxiNodes(Packet packet)
        {
            var u = packet.ReadUInt32("Unk UInt32 1");
            if (u != 0)
            {
                packet.ReadGuid("GUID");
                packet.ReadUInt32("Node ID");
            }
            var i = 0;
            while (packet.CanRead())
                packet.ReadUInt64("NodeMask", i++);
        }

        [Parser(Opcode.SMSG_SHOWTAXINODES, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleShowTaxiNodes434(Packet packet)
        {
            var u = packet.ReadUInt32("Unk UInt32 1");
            if (u != 0)
            {
                packet.ReadGuid("GUID");
                packet.ReadUInt32("Node ID");
            }

            var count = packet.ReadInt32("Count");
            for (int i = 0; i < count; ++i)
                packet.ReadByte("NodeMask", i);
        }

        [Parser(Opcode.CMSG_ACTIVATETAXI)]
        public static void HandleActivateTaxi(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Node 1 ID");
            packet.ReadUInt32("Node 2 ID");
        }

        [Parser(Opcode.SMSG_ACTIVATETAXIREPLY)]
        public static void HandleActivateTaxiReply(Packet packet)
        {
            packet.ReadEnum<TaxiError>("Result", TypeCode.UInt32);
        }

        [Parser(Opcode.CMSG_ACTIVATETAXIEXPRESS)]
        public static void HandleActivateTaxiExpress(Packet packet)
        {
            packet.ReadGuid("GUID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadUInt32("Cost");

            var count = packet.ReadUInt32("Node Count");
            for (var i = 0; i < count; ++i)
                packet.ReadUInt32("Node ID", i);

        }

        [Parser(Opcode.CMSG_SET_TAXI_BENCHMARK_MODE)]
        public static void HandleSetTaxiBenchmarkMode(Packet packet)
        {
            packet.ReadBoolean("Activate");
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
