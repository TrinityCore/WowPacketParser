using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class BlackMarketHandler
    {
        [Parser(Opcode.SMSG_BLACK_MARKET_REQUEST_ITEMS_RESULT, ClientVersionBuild.V9_0_5_37503)]
        public static void HandleBlackMarketRequestItemsResult(Packet packet)
        {
            packet.ReadTime64("LastUpdateID");
            var count = packet.ReadInt32("ItemsCount");

            for (var i = 0; i < count; ++i)
                V7_0_3_22248.Parsers.BlackMarketHandler.ReadBlackMarketItem(packet, "Items", i);
        }
    }
}
