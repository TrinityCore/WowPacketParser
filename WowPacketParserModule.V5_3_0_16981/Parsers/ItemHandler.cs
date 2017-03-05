using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.Translator.ReadUInt32E<UnknownFlags>("Mask");
            packet.Translator.ReadByteE<ItemClass>("Class");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            var itemGuid = new byte[8];
            var playerGuid = new byte[8];
            packet.Translator.StartBitStream(itemGuid, 2, 4);
            playerGuid[4] = packet.Translator.ReadBit();
            itemGuid[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(playerGuid, 3, 5);
            packet.Translator.StartBitStream(itemGuid, 7, 0, 6);
            packet.Translator.StartBitStream(playerGuid, 6, 2, 0, 1);
            itemGuid[1] = packet.Translator.ReadBit();
            playerGuid[7] = packet.Translator.ReadBit();
            itemGuid[3] = packet.Translator.ReadBit();
            packet.Translator.ResetBitReader();

            packet.Translator.ReadXORBytes(playerGuid, 1, 7);
            packet.Translator.ReadXORBytes(itemGuid, 4, 7);
            packet.Translator.ReadXORByte(playerGuid, 5);
            packet.Translator.ReadXORBytes(itemGuid, 1, 2);
            packet.Translator.ReadXORBytes(playerGuid, 4, 2, 0);
            packet.Translator.ReadXORBytes(itemGuid, 0, 5);
            packet.Translator.ReadUInt32("Duration");
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadXORByte(playerGuid, 3);
            packet.Translator.ReadXORBytes(itemGuid, 3, 6);
            packet.Translator.ReadXORByte(playerGuid, 6);

            packet.Translator.WriteGuid("Player GUID", playerGuid);
            packet.Translator.WriteGuid("Item GUID", itemGuid);
        }
    }
}
