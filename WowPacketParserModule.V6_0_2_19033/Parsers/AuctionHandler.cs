using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ArenaHandler
    {
        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT)]
        public static void HandleAuctionCommandResult(Packet packet)
        {
            packet.ReadUInt32("Unk");
            packet.ReadEnum<InventoryResult>("BagResult", TypeCode.UInt32);
            packet.ReadUInt32("Unk");
            packet.ReadUInt32("Unk");
            packet.ReadPackedGuid128("Guid");

            // One of the following is MinIncrement and the other is Money, order still unknown
            packet.ReadUInt64("Unk");
            packet.ReadUInt64("Unk");
        }
    }
}