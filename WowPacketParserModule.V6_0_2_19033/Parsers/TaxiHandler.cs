using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TaxiHandler
    {
        [Parser(Opcode.CMSG_ACTIVATETAXI)]
        public static void HandleActivateTaxi(Packet packet)
        {
            packet.ReadPackedGuid128("Vendor");
            packet.ReadUInt32("StartNode");
            packet.ReadUInt32("DestNode");
        }

        [Parser(Opcode.CMSG_ACTIVATETAXIEXPRESS)]
        public static void HandleActivateTaxiExpress(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");

            var count = packet.ReadUInt32("PathNodesCount");
            for (var i = 0; i < count; ++i)
                packet.ReadUInt32("PathNodes", i);
        }
    }
}