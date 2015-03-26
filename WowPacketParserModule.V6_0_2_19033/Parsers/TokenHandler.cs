using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TokenHandler
    {
        [Parser(Opcode.CMSG_TOKEN_BUY_TOKEN)]
        public static void HandleTokenBuyToken(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
            packet.ReadPackedGuid128("Buyer");
            packet.ReadUInt64("CurrentMarketPrice");
        }

        [Parser(Opcode.CMSG_TOKEN_CONFIRM_BUY_TOKEN)]
        public static void HandleTokenConfirmBuyToken(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
            packet.ReadUInt32("PendingBuyConfirmations");
            packet.ReadUInt64("GuaranteedPrice");
            packet.ReadBit("Confirmed");
        }

        [Parser(Opcode.CMSG_TOKEN_CONFIRM_SELL_TOKEN)]
        public static void HandleTokenConfirmSellToken(Packet packet)
        {
            packet.ReadPackedGuid128("TokenGuid");
            packet.ReadUInt32("UnkInt32");
            packet.ReadUInt32("PendingBuyConfirmations");
            packet.ReadUInt64("GuaranteedPrice");
            packet.ReadBit("Confirmed");
        }

        [Parser(Opcode.CMSG_TOKEN_SELL_TOKEN)]
        public static void HandleTokenSellToken(Packet packet)
        {
            packet.ReadUInt64("UnkInt64");
            packet.ReadUInt64("CurrentMarketPrice");
            packet.ReadUInt32("UnkInt32");
        }

        [Parser(Opcode.CMSG_TOKEN_CONFIRM_REDEEM_TOKEN)]
        public static void HandleConirmRedeemToken(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
            packet.ReadUInt64("Count");
            packet.ReadPackedGuid128("TokenGuid");
            packet.ReadUInt32("UnkInt32");
            packet.ReadBit("Confirm");
        }

        [Parser(Opcode.CMSG_TOKEN_REDEEM_TOKEN)]
        public static void HandleRedeemToken(Packet packet)
        {
            packet.ReadUInt64("Count");
            packet.ReadUInt32("UnkInt32");
            packet.ReadUInt32("UnkInt32");
        }

        [Parser(Opcode.CMSG_TOKEN_UPDATE_MARKET_PRICE)]
        public static void HandleTokenUpdateMarketPrice(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
        }

        [Parser(Opcode.CMSG_TOKEN_UPDATE_TOKEN_COUNT)]
        public static void HandleTokenUpdateTokenCount(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
        }

        [Parser(Opcode.CMSG_GET_REMAINING_GAME_TIME)]
        public static void HandleGetRemainingGameTime(Packet packet)
        {
            packet.ReadUInt32("UnkInt32");
        }
    }
}
