using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class TaxiHandler
    {
        [Parser(Opcode.SMSG_NEW_TAXI_PATH)]
        public static void HandleTaxiNull(Packet packet)
        {
            packet.ReadInt32("TaxiNodesID");
        }
    }
}
