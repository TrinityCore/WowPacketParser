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

            itemGUID[4] = packet.Translator.ReadBit();
            itemGUID[0] = packet.Translator.ReadBit();
            playerGUID[3] = packet.Translator.ReadBit();
            itemGUID[3] = packet.Translator.ReadBit();
            playerGUID[2] = packet.Translator.ReadBit();
            playerGUID[6] = packet.Translator.ReadBit();
            playerGUID[7] = packet.Translator.ReadBit();
            itemGUID[1] = packet.Translator.ReadBit();
            playerGUID[4] = packet.Translator.ReadBit();
            itemGUID[6] = packet.Translator.ReadBit();
            itemGUID[5] = packet.Translator.ReadBit();
            playerGUID[0] = packet.Translator.ReadBit();
            itemGUID[2] = packet.Translator.ReadBit();
            playerGUID[5] = packet.Translator.ReadBit();
            playerGUID[1] = packet.Translator.ReadBit();
            itemGUID[7] = packet.Translator.ReadBit();
            packet.Translator.ReadInt32("Slot");
            packet.Translator.ReadXORByte(playerGUID, 4);
            packet.Translator.ReadXORByte(playerGUID, 2);
            packet.Translator.ReadXORByte(itemGUID, 5);
            packet.Translator.ReadXORByte(itemGUID, 4);
            packet.Translator.ReadXORByte(playerGUID, 6);
            packet.Translator.ReadXORByte(itemGUID, 1);
            packet.Translator.ReadXORByte(playerGUID, 0);
            packet.Translator.ReadXORByte(playerGUID, 1);
            packet.Translator.ReadXORByte(itemGUID, 6);
            packet.Translator.ReadXORByte(itemGUID, 2);
            packet.Translator.ReadXORByte(playerGUID, 7);
            packet.Translator.ReadXORByte(itemGUID, 0);
            packet.Translator.ReadXORByte(itemGUID, 3);
            packet.Translator.ReadXORByte(itemGUID, 7);
            packet.Translator.ReadXORByte(playerGUID, 3);
            packet.Translator.ReadXORByte(playerGUID, 5);
            packet.Translator.ReadInt32("Duration");

            packet.Translator.WriteGuid("Player GUID", playerGUID);
            packet.Translator.WriteGuid("Item GUID", itemGUID);
        }

        [Parser(Opcode.CMSG_GET_ITEM_PURCHASE_DATA)]
        public static void HandleItemRefundInfo(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 0, 3, 2, 7, 4, 5, 6);
            packet.Translator.ParseBitStream(guid, 3, 7, 5, 1, 0, 6, 4, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
