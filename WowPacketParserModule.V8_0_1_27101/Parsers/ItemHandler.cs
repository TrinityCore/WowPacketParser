using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ItemHandler
    {
        public static int ReadItemInstance815(Packet packet, params object[] indexes)
        {
            var itemId = packet.ReadInt32<ItemId>("ItemID", indexes);

            packet.ResetBitReader();
            var hasBonuses = packet.ReadBit("HasItemBonus", indexes);
            var hasModifications = packet.ReadBit("HasModifications", indexes);
            if (hasBonuses)
            {
                packet.ReadByte("Context", indexes);

                var bonusCount = packet.ReadUInt32();
                for (var j = 0; j < bonusCount; ++j)
                    packet.ReadInt32("BonusListID", indexes, j);
            }

            if (hasModifications)
            {
                var mask = packet.ReadUInt32();
                for (var j = 0; mask != 0; mask >>= 1, ++j)
                    if ((mask & 1) != 0)
                        packet.ReadUInt32(((ItemModifier)j).ToString(), indexes);
            }

            packet.ResetBitReader();

            return itemId;
        }

        [Parser(Opcode.CMSG_AZERITE_EMPOWERED_ITEM_SELECT_POWER)]
        public static void HandleItemAzerithEmpoweredItemSelectPower(Packet packet)
        {
            packet.ReadInt32("AzeriteTier");
            packet.ReadInt32("AzeritePowerID");
            packet.ReadByte("ContainerSlot");
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_AZERITE_EMPOWERED_ITEM_VIEWED)]
        public static void HandleItemAzerithEmpoweredItemViewed(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }
    }
}
