using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            var itemGUID = new byte[8];
            var playerGUID = new byte[8];

            itemGUID[4] = packet.ReadBit();
            itemGUID[0] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            itemGUID[3] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            itemGUID[1] = packet.ReadBit();
            playerGUID[4] = packet.ReadBit();
            itemGUID[6] = packet.ReadBit();
            itemGUID[5] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            itemGUID[2] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            itemGUID[7] = packet.ReadBit();
            packet.ReadInt32("Slot");
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(itemGUID, 5);
            packet.ReadXORByte(itemGUID, 4);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(itemGUID, 1);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(itemGUID, 6);
            packet.ReadXORByte(itemGUID, 2);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(itemGUID, 0);
            packet.ReadXORByte(itemGUID, 3);
            packet.ReadXORByte(itemGUID, 7);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadInt32("Duration");

            packet.WriteGuid("Player GUID", playerGUID);
            packet.WriteGuid("Item GUID", itemGUID);
        }

        [Parser(Opcode.CMSG_ITEM_REFUND_INFO)]
        public static void HandleItemRefundInfo(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 4, 6, 3, 2, 1, 7, 5);
            packet.ParseBitStream(guid, 5, 3, 7, 2, 1, 6, 0, 4);

            packet.WriteGuid("Guid", guid);
        }
    }
}
