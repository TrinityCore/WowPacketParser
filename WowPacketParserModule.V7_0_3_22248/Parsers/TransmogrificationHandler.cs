using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class TransmogrificationHandler
    {
        public static void ReadTransmogrifyItem(Packet packet, params object[] index)
        {
            packet.ReadInt32("ItemModifiedAppearanceID");
            packet.ReadUInt32("Slot");
            packet.ReadInt32("SpellItemEnchantmentId");
        }

        [Parser(Opcode.CMSG_TRANSMOGRIFY_ITEMS)]
        public static void HandleTransmogrifyItems(Packet packet)
        {
            var itemCount = packet.ReadInt32("ItemsCount");
            packet.ReadPackedGuid128("Npc");

            for (int i = 0; i < itemCount; i++)
                ReadTransmogrifyItem(packet, i);

            packet.ResetBitReader();
            packet.ReadBit("CurrentSpecOnly");
        }

        [Parser(Opcode.SMSG_TRANSMOG_SET_COLLECTION_UPDATE)]
        [Parser(Opcode.SMSG_TRANSMOG_COLLECTION_UPDATE)]
        public static void HandleTransmogCollectionUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");
            packet.ReadBit("IsSetFavorite");
            var count = packet.ReadUInt32("FavoriteAppearancesCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32("ItemModifiedAppearanceId");
        }

        [Parser(Opcode.SMSG_OPEN_TRANSMOGRIFIER)]
        public static void HandleOpenTransmogrifier(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }
    }
}
