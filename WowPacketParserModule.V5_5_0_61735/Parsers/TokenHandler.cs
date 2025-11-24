using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class TokenHandler
    {
        [Parser(Opcode.SMSG_COMMERCE_TOKEN_GET_COUNT_RESPONSE)]
        public static void HandleTokenUpdateTokenCountResponse(Packet packet)
        {
            packet.ReadUInt32("UnkInt"); // send CMSG_UPDATE_WOW_TOKEN_COUNT

            packet.ReadUInt32("UnkInt2");

            var count1 = packet.ReadInt32("DistributionCount1");
            var count2 = packet.ReadInt32("DistributionCount2");

            for (int i = 0; i < count1; i++)
                packet.ReadInt64("DistributionID", i);

            for (int i = 0; i < count2; i++)
                packet.ReadInt64("DistributionID", i);
        }

        [Parser(Opcode.SMSG_COMMERCE_TOKEN_UPDATE)]
        public static void HandleCommerceTokenUpdate(Packet packet)
        {
            var count1 = packet.ReadInt32("DistributionCount1");
            var count2 = packet.ReadInt32("DistributionCount2");

            for (int i = 0; i < count1; i++)
                packet.ReadInt64("DistributionID", i);

            for (int i = 0; i < count2; i++)
                packet.ReadInt64("DistributionID", i);
        }

        [Parser(Opcode.SMSG_COMMERCE_TOKEN_GET_MARKET_PRICE_RESPONSE)]
        public static void HandleCommerceTokenGetMarketPriceResponse(Packet packet)
        {
            packet.ReadUInt64("CurrentMarketPrice");
            packet.ReadUInt32("UnkInt");
            packet.ReadUInt32("Result");
            packet.ReadUInt32("AuctionDuration");
        }

        [Parser(Opcode.SMSG_AUCTIONABLE_TOKEN_SELL_AT_MARKET_PRICE_RESPONSE)]
        public static void HandleAuctionableTokenSellAtMarketPriceResponse(Packet packet)
        {
            packet.ReadUInt32("UnkInt");
            packet.ReadUInt32("Result");
        }

        [Parser(Opcode.SMSG_COMMERCE_TOKEN_GET_LOG_RESPONSE)]
        public static void HandleCommerceTokenGetLogResponse(Packet packet)
        {
            packet.ReadInt32("UnkInt");
            packet.ReadUInt32("Result");
            var count = packet.ReadUInt32("TokenCount");
            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt64("DistributionID", i);
                packet.ReadTime64("DateCreated", i);
                packet.ReadUInt64("BuyoutPrice", i);
                packet.ReadUInt32("Owner", i);
                packet.ReadUInt32("EndTime", i);
            }
        }
    }
}
