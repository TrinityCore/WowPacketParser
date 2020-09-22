using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TokenHandler
    {
        [Parser(Opcode.CMSG_CONSUMABLE_TOKEN_BUY)]
        public static void HandleConsumableTokenBuy(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
            packet.ReadPackedGuid128("Buyer");
            packet.ReadUInt64("CurrentMarketPrice");
        }

        [Parser(Opcode.CMSG_CONSUMABLE_TOKEN_BUY_AT_MARKET_PRICE)]
        public static void HandleConsumableTokenBuyAtMarketPrice(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
            packet.ReadUInt32("PendingBuyConfirmations");
            packet.ReadUInt64("GuaranteedPrice");
            packet.ReadBit("Confirmed");
        }

        [Parser(Opcode.CMSG_AUCTIONABLE_TOKEN_SELL_AT_MARKET_PRICE)]
        public static void HandleAuctionableTokenSellAtMarketPrice(Packet packet)
        {
            packet.ReadPackedGuid128("TokenGuid");
            packet.ReadUInt32("UnkInt32");
            packet.ReadUInt32("PendingBuyConfirmations");
            packet.ReadUInt64("GuaranteedPrice");
            packet.ReadBit("Confirmed");
        }

        [Parser(Opcode.CMSG_AUCTIONABLE_TOKEN_SELL)]
        public static void HandleAuctionableTokenSell(Packet packet)
        {
            packet.ReadUInt64("UnkInt64");
            packet.ReadUInt64("CurrentMarketPrice");
            packet.ReadUInt32("UnkInt32");
        }

        [Parser(Opcode.CMSG_CONSUMABLE_TOKEN_REDEEM_CONFIRMATION)]
        public static void HandleConsumableTokenRedeemConfirmation(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
            packet.ReadUInt64("Count");
            packet.ReadPackedGuid128("TokenGuid");
            packet.ReadUInt32("UnkInt32");
            packet.ReadBit("Confirm");
        }

        [Parser(Opcode.CMSG_CONSUMABLE_TOKEN_REDEEM)]
        public static void HandleConsumableTokenRedeem(Packet packet)
        {
            packet.ReadUInt64("Count");
            packet.ReadUInt32("UnkInt32");
            packet.ReadUInt32("UnkInt32");
        }

        [Parser(Opcode.CMSG_COMMERCE_TOKEN_GET_MARKET_PRICE)]
        public static void HandleCommerceTokenGetMarketPrice(Packet packet)
        {
            packet.ReadUInt32("UnkInt");
        }

        [Parser(Opcode.SMSG_COMMERCE_TOKEN_GET_MARKET_PRICE_RESPONSE)]
        public static void HandleCommerceTokenGetMarketPriceResponse(Packet packet)
        {
            packet.ReadUInt64("CurrentMarketPrice");
            packet.ReadUInt32("UnkInt"); // send CMSG_REQUEST_WOW_TOKEN_MARKET_PRICE
            packet.ReadUInt32("Result");
            packet.ReadUInt32("AuctionDuration");
        }

        [Parser(Opcode.CMSG_COMMERCE_TOKEN_GET_COUNT)]
        public static void HandleCommerceTokenGetCount(Packet packet)
        {
            packet.ReadUInt32("UnkInt");
        }

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

        [Parser(Opcode.CMSG_GET_REMAINING_GAME_TIME)]
        public static void HandleGetRemainingGameTime(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
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

        [Parser(Opcode.CMSG_COMMERCE_TOKEN_GET_LOG)]
        public static void HandleCommerceTokenGetLog(Packet packet)
        {
            packet.ReadUInt32("UnkInt");
        }

        [Parser(Opcode.SMSG_COMMERCE_TOKEN_GET_LOG_RESPONSE)]
        public static void HandleCommerceTokenGetLogResponse(Packet packet)
        {
            packet.ReadInt32("UnkInt"); // send CMSG_UPDATE_WOW_TOKEN_AUCTIONABLE_LIST
            packet.ReadUInt32("Result");
            var count = packet.ReadUInt32("TokenCount");
            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt64("DistributionID", i);
                packet.ReadTime("DateCreated", i);
                packet.ReadUInt32("Owner", i);
                packet.ReadUInt64("BuyoutPrice", i);
                packet.ReadUInt32("EndTime", i);
            }
        }

        [Parser(Opcode.SMSG_AUCTIONABLE_TOKEN_SELL_AT_MARKET_PRICE_RESPONSE)]
        public static void HandleAuctionableTokenSellAtMarketPriceResponse(Packet packet)
        {
            packet.ReadUInt32("UnkInt");
            packet.ReadUInt32("Result");
        }
    }
}
