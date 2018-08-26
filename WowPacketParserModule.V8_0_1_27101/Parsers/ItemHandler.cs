using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ItemHandler
    {
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
