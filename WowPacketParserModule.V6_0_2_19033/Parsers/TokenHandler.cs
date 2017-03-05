using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TokenHandler
    {
        [Parser(Opcode.CMSG_BUY_WOW_TOKEN_START)]
        public static void HandleTokenBuyToken(Packet packet)
        {
            packet.Translator.ReadUInt32("UnkInt32");
            packet.Translator.ReadPackedGuid128("Buyer");
            packet.Translator.ReadUInt64("CurrentMarketPrice");
        }

        [Parser(Opcode.CMSG_BUY_WOW_TOKEN_CONFIRM)]
        public static void HandleTokenConfirmBuyToken(Packet packet)
        {
            packet.Translator.ReadUInt32("UnkInt32");
            packet.Translator.ReadUInt32("PendingBuyConfirmations");
            packet.Translator.ReadUInt64("GuaranteedPrice");
            packet.Translator.ReadBit("Confirmed");
        }

        [Parser(Opcode.CMSG_SELL_WOW_TOKEN_CONFIRM)]
        public static void HandleTokenConfirmSellToken(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TokenGuid");
            packet.Translator.ReadUInt32("UnkInt32");
            packet.Translator.ReadUInt32("PendingBuyConfirmations");
            packet.Translator.ReadUInt64("GuaranteedPrice");
            packet.Translator.ReadBit("Confirmed");
        }

        [Parser(Opcode.CMSG_SELL_WOW_TOKEN_START)]
        public static void HandleTokenSellToken(Packet packet)
        {
            packet.Translator.ReadUInt64("UnkInt64");
            packet.Translator.ReadUInt64("CurrentMarketPrice");
            packet.Translator.ReadUInt32("UnkInt32");
        }

        [Parser(Opcode.CMSG_REDEEM_WOW_TOKEN_CONFIRM)]
        public static void HandleConirmRedeemToken(Packet packet)
        {
            packet.Translator.ReadUInt32("UnkInt32");
            packet.Translator.ReadUInt64("Count");
            packet.Translator.ReadPackedGuid128("TokenGuid");
            packet.Translator.ReadUInt32("UnkInt32");
            packet.Translator.ReadBit("Confirm");
        }

        [Parser(Opcode.CMSG_REDEEM_WOW_TOKEN_START)]
        public static void HandleRedeemToken(Packet packet)
        {
            packet.Translator.ReadUInt64("Count");
            packet.Translator.ReadUInt32("UnkInt32");
            packet.Translator.ReadUInt32("UnkInt32");
        }

        [Parser(Opcode.CMSG_REQUEST_WOW_TOKEN_MARKET_PRICE)]
        public static void HandleTokenUpdateMarketPrice(Packet packet)
        {
            packet.Translator.ReadUInt32("UnkInt");
        }

        [Parser(Opcode.SMSG_REQUEST_WOW_TOKEN_MARKET_PRICE_RESPONSE)]
        public static void HandleTokenUpdateMarketPriceResponse(Packet packet)
        {
            packet.Translator.ReadUInt64("CurrentMarketPrice");
            packet.Translator.ReadUInt32("UnkInt"); // send CMSG_REQUEST_WOW_TOKEN_MARKET_PRICE
            packet.Translator.ReadUInt32("Result");
            packet.Translator.ReadUInt32("CurrentMarketPriceDuration");
        }

        [Parser(Opcode.CMSG_UPDATE_WOW_TOKEN_COUNT)]
        public static void HandleTokenUpdateTokenCount(Packet packet)
        {
            packet.Translator.ReadUInt32("UnkInt");
        }

        [Parser(Opcode.SMSG_UPDATE_WOW_TOKEN_COUNT_RESPONSE)]
        public static void HandleTokenUpdateTokenCountResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("UnkInt"); // send CMSG_UPDATE_WOW_TOKEN_COUNT

            packet.Translator.ReadUInt32("UnkInt2");

            var count1 = packet.Translator.ReadInt32("DistributionCount1");
            var count2 = packet.Translator.ReadInt32("DistributionCount2");

            for (int i = 0; i < count1; i++)
                packet.Translator.ReadInt64("DistributionID", i);

            for (int i = 0; i < count2; i++)
                packet.Translator.ReadInt64("DistributionID", i);
        }

        [Parser(Opcode.CMSG_GET_REMAINING_GAME_TIME)]
        public static void HandleGetRemainingGameTime(Packet packet)
        {
            packet.Translator.ReadUInt32("UnkInt32");
        }

        [Parser(Opcode.SMSG_TOKEN_UNK1)]
        public static void HandleTokenUnk1(Packet packet)
        {
            var count1 = packet.Translator.ReadInt32("DistributionCount1");
            var count2 = packet.Translator.ReadInt32("DistributionCount2");

            for (int i = 0; i < count1; i++)
                packet.Translator.ReadInt64("DistributionID", i);

            for (int i = 0; i < count2; i++)
                packet.Translator.ReadInt64("DistributionID", i);
        }

        [Parser(Opcode.CMSG_UPDATE_WOW_TOKEN_AUCTIONABLE_LIST)]
        public static void HandleUpdateListedAuctionableTokens(Packet packet)
        {
            packet.Translator.ReadUInt32("UnkInt");
        }

        [Parser(Opcode.SMSG_UPDATE_WOW_TOKEN_AUCTIONABLE_LIST_RESPONSE)]
        public static void HandleUpdateListedAuctionableTokensResponse(Packet packet)
        {
            packet.Translator.ReadInt32("UnkInt"); // send CMSG_UPDATE_WOW_TOKEN_AUCTIONABLE_LIST
            packet.Translator.ReadUInt32("Result");
            var count = packet.Translator.ReadUInt32("TokenCount");
            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt64("DistributionID", i);
                packet.Translator.ReadTime("DateCreated", i);
                packet.Translator.ReadUInt32("Owner", i);
                packet.Translator.ReadUInt64("BuyoutPrice", i);
                packet.Translator.ReadUInt32("EndTime", i);
            }
        }

        [Parser(Opcode.SMSG_TOKEN_SELL_RESULT)]
        public static void HandleTokenSellResult(Packet packet)
        {
            packet.Translator.ReadUInt32("UnkInt");
            packet.Translator.ReadUInt32("Result");
        }
    }
}
