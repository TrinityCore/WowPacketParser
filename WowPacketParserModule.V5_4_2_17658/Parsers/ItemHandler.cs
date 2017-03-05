using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
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
            var playerGuid = new byte[8];
            var itemGuid = new byte[8];

            playerGuid[0] = packet.Translator.ReadBit();
            playerGuid[7] = packet.Translator.ReadBit();
            itemGuid[3] = packet.Translator.ReadBit();
            itemGuid[4] = packet.Translator.ReadBit();
            playerGuid[2] = packet.Translator.ReadBit();
            itemGuid[1] = packet.Translator.ReadBit();
            itemGuid[5] = packet.Translator.ReadBit();
            playerGuid[5] = packet.Translator.ReadBit();
            playerGuid[4] = packet.Translator.ReadBit();
            itemGuid[6] = packet.Translator.ReadBit();
            playerGuid[6] = packet.Translator.ReadBit();
            itemGuid[7] = packet.Translator.ReadBit();
            playerGuid[1] = packet.Translator.ReadBit();
            itemGuid[2] = packet.Translator.ReadBit();
            playerGuid[3] = packet.Translator.ReadBit();
            itemGuid[0] = packet.Translator.ReadBit();

            packet.Translator.ReadUInt32("Slot");

            packet.Translator.ReadXORByte(itemGuid, 1);
            packet.Translator.ReadXORByte(playerGuid, 4);
            packet.Translator.ReadXORByte(playerGuid, 7);
            packet.Translator.ReadXORByte(playerGuid, 2);
            packet.Translator.ReadXORByte(playerGuid, 3);
            packet.Translator.ReadXORByte(itemGuid, 6);
            packet.Translator.ReadXORByte(itemGuid, 5);
            packet.Translator.ReadXORByte(playerGuid, 5);
            packet.Translator.ReadXORByte(playerGuid, 6);
            packet.Translator.ReadXORByte(itemGuid, 7);
            packet.Translator.ReadXORByte(itemGuid, 3);
            packet.Translator.ReadXORByte(itemGuid, 4);
            packet.Translator.ReadXORByte(itemGuid, 2);
            packet.Translator.ReadXORByte(playerGuid, 1);
            packet.Translator.ReadXORByte(itemGuid, 0);
            packet.Translator.ReadXORByte(playerGuid, 0);

            packet.Translator.ReadUInt32("Duration");

            packet.Translator.WriteGuid("Player GUID", playerGuid);
            packet.Translator.WriteGuid("Item GUID", itemGuid);
        }
    }
}
