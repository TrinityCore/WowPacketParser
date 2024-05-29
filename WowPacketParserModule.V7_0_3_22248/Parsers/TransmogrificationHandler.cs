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

        [Parser(Opcode.SMSG_ACCOUNT_TRANSMOG_SET_FAVORITES_UPDATE)]
        [Parser(Opcode.SMSG_ACCOUNT_TRANSMOG_UPDATE)]
        public static void HandleAccountTransmogUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");
            packet.ReadBit("IsSetFavorite");
            var count = packet.ReadUInt32("FavoriteAppearancesCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32("ItemModifiedAppearanceId", "FavoriteAppearances", i);
        }

        [Parser(Opcode.SMSG_TRANSMOGRIFY_NPC)]
        public static void HandleTransmogrifyNPC(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_ACCOUNT_COSMETIC_ADDED)]
        public static void HandleAccountCosmeticAdded(Packet packet)
        {
            packet.ReadInt32("ItemModifiedAppearanceID");
        }

        [Parser(Opcode.CMSG_ADD_ACCOUNT_COSMETIC)]
        public static void HandleAddAccountCosmetic(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }
    }
}
