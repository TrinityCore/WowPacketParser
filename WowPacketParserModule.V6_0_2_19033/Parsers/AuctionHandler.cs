using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AuctionHandler
    {
        [Parser(Opcode.CMSG_AUCTION_HELLO)]
        public static void HandleClientAuctionHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_AUCTION_HELLO)]
        public static void HandleServerAuctionHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadBit("OpenForBusiness");
        }

        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT)]
        public static void HandleAuctionCommandResult(Packet packet)
        {
            packet.ReadUInt32("AuctionItemID");
            packet.ReadEnum<AuctionHouseAction>("Command", TypeCode.UInt32);
            packet.ReadEnum<AuctionHouseError>("ErrorCode", TypeCode.UInt32);
            packet.ReadUInt32("BagResult");
            packet.ReadPackedGuid128("Guid");

            // One of the following is MinIncrement and the other is Money, order still unknown
            packet.ReadUInt64("MinIncrement");
            packet.ReadUInt64("Money");
        }
    }
}