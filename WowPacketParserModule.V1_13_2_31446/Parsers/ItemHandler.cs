using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_OPEN_CONTAINER)]
        public static void HandleOpenContainer(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }
    }
}
