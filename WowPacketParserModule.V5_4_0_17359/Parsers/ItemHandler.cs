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
            packet.ReadUInt32E<UnknownFlags>("Mask");
            packet.ReadByteE<ItemClass>("Class");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            var itemGuid = new byte[8];
            var playerGuid = new byte[8];
            itemGuid[3] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            itemGuid[0] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            itemGuid[2] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();
            packet.StartBitStream(itemGuid, 6, 1);
            playerGuid[2] = packet.ReadBit();
            itemGuid[7] = packet.ReadBit();
            packet.StartBitStream(playerGuid, 3, 1);
            itemGuid[5] = packet.ReadBit();
            packet.StartBitStream(playerGuid, 5, 0);
            itemGuid[4] = packet.ReadBit();
            packet.ResetBitReader();

            packet.ReadUInt32("Duration");
            packet.ReadXORBytes(playerGuid, 2, 3);
            packet.ReadXORByte(itemGuid, 7);
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadUInt32("Slot");
            packet.ReadXORByte(itemGuid, 3);
            packet.ReadXORByte(playerGuid, 6);
            packet.ReadXORBytes(itemGuid, 6, 4, 2);
            packet.ReadXORByte(playerGuid, 1);
            packet.ReadXORByte(itemGuid, 5);
            packet.ReadXORBytes(playerGuid, 5, 4, 7);
            packet.ReadXORBytes(itemGuid, 0, 1);

            packet.WriteGuid("Player GUID", playerGuid);
            packet.WriteGuid("Item GUID", itemGuid);
        }
    }
}
