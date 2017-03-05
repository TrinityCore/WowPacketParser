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
            packet.ReadUInt32E<UnknownFlags>("Mask");
            packet.ReadByteE<ItemClass>("Class");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            var itemGuid = new byte[8];
            var playerGuid = new byte[8];
            packet.StartBitStream(itemGuid, 2, 4);
            playerGuid[4] = packet.ReadBit();
            itemGuid[5] = packet.ReadBit();
            packet.StartBitStream(playerGuid, 3, 5);
            packet.StartBitStream(itemGuid, 7, 0, 6);
            packet.StartBitStream(playerGuid, 6, 2, 0, 1);
            itemGuid[1] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            itemGuid[3] = packet.ReadBit();
            packet.ResetBitReader();

            packet.ReadXORBytes(playerGuid, 1, 7);
            packet.ReadXORBytes(itemGuid, 4, 7);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadXORBytes(itemGuid, 1, 2);
            packet.ReadXORBytes(playerGuid, 4, 2, 0);
            packet.ReadXORBytes(itemGuid, 0, 5);
            packet.ReadUInt32("Duration");
            packet.ReadUInt32("Slot");
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORBytes(itemGuid, 3, 6);
            packet.ReadXORByte(playerGuid, 6);

            packet.WriteGuid("Player GUID", playerGuid);
            packet.WriteGuid("Item GUID", itemGuid);
        }
    }
}
