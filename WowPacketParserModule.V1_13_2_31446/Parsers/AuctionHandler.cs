using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class AuctionHandler
    {
        [Parser(Opcode.SMSG_AUCTION_HELLO_RESPONSE)]
        public static void HandleServerAuctionHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadUInt32("AuctionHouseID"); // Unsure
            packet.ReadBit("OpenForBusiness");

            CoreParsers.NpcHandler.LastGossipOption.Guid = null;
            CoreParsers.NpcHandler.LastGossipOption.MenuId = null;
            CoreParsers.NpcHandler.LastGossipOption.OptionIndex = null;
            CoreParsers.NpcHandler.LastGossipOption.ActionMenuId = null;
            CoreParsers.NpcHandler.LastGossipOption.ActionMenuId = null;

            CoreParsers.NpcHandler.TempGossipOptionPOI.Guid = null;
            CoreParsers.NpcHandler.TempGossipOptionPOI.MenuId = null;
            CoreParsers.NpcHandler.TempGossipOptionPOI.OptionIndex = null;
            CoreParsers.NpcHandler.TempGossipOptionPOI.ActionMenuId = null;
            CoreParsers.NpcHandler.TempGossipOptionPOI.ActionMenuId = null;
        }
    }
}
