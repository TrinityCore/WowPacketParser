using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_BUY_FAILED)]
        public static void HandleBuyFailed(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32<ItemId>("Muid");
            packet.ReadByteE<BuyResult>("Reason");
        }

        [Parser(Opcode.SMSG_BUY_SUCCEEDED)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32("Muid");
            packet.ReadInt32("NewQuantity");
            packet.ReadUInt32("QuantityBought");
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadUInt32E<UnknownFlags>("ProficiencyMask");
            packet.ReadByteE<ItemClass>("ProficiencyClass");
        }

        [Parser(Opcode.CMSG_REFORGE_ITEM)]
        public static void HandleReforgeItem(Packet packet)
        {
            packet.ReadPackedGuid128("ReforgerGUID");
            packet.ReadInt32("ContainerId");
            packet.ReadInt32("SlotNum");
            packet.ReadInt32("ItemReforgeRecId");
        }

        [Parser(Opcode.SMSG_ITEM_EXPIRE_PURCHASE_REFUND)]
        public static void HandleItemExpirePurchaseRefund(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_USE_TOY)]
        public static void HandleUseToy(Packet packet)
        {
            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_BAG_CLEANUP_FINISHED)]
        public static void HandleItemZero(Packet packet)
        {
        }
    }
}
