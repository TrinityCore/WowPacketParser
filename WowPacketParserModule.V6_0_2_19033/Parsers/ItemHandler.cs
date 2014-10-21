using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Item Guid");
            packet.ReadUInt32("Duration");
            packet.ReadUInt32("Slot");
            packet.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.CMSG_ITEM_REFUND_INFO)]
        public static void HandleItemRefundInfo(Packet packet)
        {
            packet.ReadPackedGuid128("Item Guid");
        }
    }
}
