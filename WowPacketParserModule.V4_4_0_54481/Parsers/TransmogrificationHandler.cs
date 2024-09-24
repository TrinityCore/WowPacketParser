using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
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
