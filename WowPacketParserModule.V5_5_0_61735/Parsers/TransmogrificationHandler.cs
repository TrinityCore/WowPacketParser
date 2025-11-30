using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class TransmogrificationHandler
    {
        public static void ReadTransmogrifyItem(Packet packet, params object[] index)
        {
            packet.ReadInt32("ItemModifiedAppearanceID");
            packet.ReadUInt32("Slot");
            packet.ReadInt32("SpellItemEnchantmentId");
            packet.ReadInt32("SecondaryItemModifiedAppearanceID");
        }

        [Parser(Opcode.SMSG_ACCOUNT_TRANSMOG_UPDATE)]
        public static void HandleAccountTransmogUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");
            packet.ReadBit("IsSetFavorite");
            var count = packet.ReadUInt32("FavoriteAppearancesCount");
            var newCount = packet.ReadUInt32("NewAppearancesCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32("ItemModifiedAppearanceId", "FavoriteAppearances", i);
            for (int i = 0; i < newCount; i++)
                packet.ReadUInt32("ItemModifiedAppearanceId", "NewAppearances", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_TRANSMOG_SET_FAVORITES_UPDATE)]
        public static void HandleAccountTransmogSetFavoritesUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");
            packet.ReadBit("IsSetFavorite");
            var count = packet.ReadUInt32("FavoriteAppearancesCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32("ItemModifiedAppearanceId", "FavoriteAppearances", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_COSMETIC_ADDED)]
        public static void HandleAccountCosmeticAdded(Packet packet)
        {
            packet.ReadInt32("ItemModifiedAppearanceID");
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
    }
}
