using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.CMSG_USE_ITEM, ClientVersionBuild.V12_0_7_68182)]
        public static void HandleUseItem(Packet packet)
        {
            var useItem = packet.Holder.ClientUseItem = new();
            useItem.PackSlot = packet.ReadByte("PackSlot");
            useItem.ItemSlot = packet.ReadByte("Slot");
            useItem.CastItem = packet.ReadPackedGuid128("CastItem");

            useItem.SpellId = SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_USE_TOY, ClientVersionBuild.V12_0_7_68182)]
        public static void HandleUseToy(Packet packet)
        {
            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }
    }
}
