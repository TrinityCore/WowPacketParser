using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
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
            itemGuid[3] = packet.Translator.ReadBit();
            playerGuid[4] = packet.Translator.ReadBit();
            itemGuid[0] = packet.Translator.ReadBit();
            playerGuid[7] = packet.Translator.ReadBit();
            itemGuid[2] = packet.Translator.ReadBit();
            playerGuid[6] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(itemGuid, 6, 1);
            playerGuid[2] = packet.Translator.ReadBit();
            itemGuid[7] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(playerGuid, 3, 1);
            itemGuid[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(playerGuid, 5, 0);
            itemGuid[4] = packet.Translator.ReadBit();
            packet.Translator.ResetBitReader();

            packet.Translator.ReadUInt32("Duration");
            packet.Translator.ReadXORBytes(playerGuid, 2, 3);
            packet.Translator.ReadXORByte(itemGuid, 7);
            packet.Translator.ReadXORByte(playerGuid, 0);
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadXORByte(itemGuid, 3);
            packet.Translator.ReadXORByte(playerGuid, 6);
            packet.Translator.ReadXORBytes(itemGuid, 6, 4, 2);
            packet.Translator.ReadXORByte(playerGuid, 1);
            packet.Translator.ReadXORByte(itemGuid, 5);
            packet.Translator.ReadXORBytes(playerGuid, 5, 4, 7);
            packet.Translator.ReadXORBytes(itemGuid, 0, 1);

            packet.Translator.WriteGuid("Player GUID", playerGuid);
            packet.Translator.WriteGuid("Item GUID", itemGuid);
        }
    }
}
