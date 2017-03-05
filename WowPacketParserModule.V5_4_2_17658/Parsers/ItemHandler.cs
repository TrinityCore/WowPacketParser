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
            packet.ReadUInt32E<UnknownFlags>("Mask");
            packet.ReadByteE<ItemClass>("Class");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            var playerGuid = new byte[8];
            var itemGuid = new byte[8];

            playerGuid[0] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            itemGuid[3] = packet.ReadBit();
            itemGuid[4] = packet.ReadBit();
            playerGuid[2] = packet.ReadBit();
            itemGuid[1] = packet.ReadBit();
            itemGuid[5] = packet.ReadBit();
            playerGuid[5] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            itemGuid[6] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();
            itemGuid[7] = packet.ReadBit();
            playerGuid[1] = packet.ReadBit();
            itemGuid[2] = packet.ReadBit();
            playerGuid[3] = packet.ReadBit();
            itemGuid[0] = packet.ReadBit();

            packet.ReadUInt32("Slot");

            packet.ReadXORByte(itemGuid, 1);
            packet.ReadXORByte(playerGuid, 4);
            packet.ReadXORByte(playerGuid, 7);
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(itemGuid, 6);
            packet.ReadXORByte(itemGuid, 5);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadXORByte(playerGuid, 6);
            packet.ReadXORByte(itemGuid, 7);
            packet.ReadXORByte(itemGuid, 3);
            packet.ReadXORByte(itemGuid, 4);
            packet.ReadXORByte(itemGuid, 2);
            packet.ReadXORByte(playerGuid, 1);
            packet.ReadXORByte(itemGuid, 0);
            packet.ReadXORByte(playerGuid, 0);

            packet.ReadUInt32("Duration");

            packet.WriteGuid("Player GUID", playerGuid);
            packet.WriteGuid("Item GUID", itemGuid);
        }
    }
}
