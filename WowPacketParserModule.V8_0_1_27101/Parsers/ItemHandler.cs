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
            if (ClientVersion.RemovedInVersion(ClientType.TheWarWithin))
            {
                packet.ReadInt32("AzeriteTier");
                packet.ReadInt32("AzeritePowerID");
            }

            packet.ReadByte("ContainerSlot");
            packet.ReadByte("Slot");

            if (ClientVersion.AddedInVersion(ClientType.TheWarWithin))
            {
                packet.ReadByte("AzeriteTier");
                packet.ReadInt32("AzeritePowerID");
            }
        }

        [Parser(Opcode.CMSG_AZERITE_EMPOWERED_ITEM_VIEWED)]
        public static void HandleItemAzerithEmpoweredItemViewed(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.SMSG_AZERITE_RESPEC_NPC)]
        public static void HandleAzeriteRespecNPC(Packet packet)
        {
            packet.ReadPackedGuid128("ReforgerGUID");
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");

            packet.ReadByte("Slot");

            packet.ReadInt32("SlotInBag");

            packet.ReadUInt32("QuestLogItemID");
            packet.ReadUInt32("Quantity");
            packet.ReadUInt32("QuantityInInventory");
            packet.ReadInt32("DungeonEncounterID");

            packet.ReadUInt32("BattlePetSpeciesID");
            packet.ReadUInt32("BattlePetBreedID");
            packet.ReadUInt32("BattlePetBreedQuality");
            packet.ReadUInt32("BattlePetLevel");

            packet.ReadPackedGuid128("ItemGUID");

            packet.ResetBitReader();

            packet.ReadBit("Pushed");
            packet.ReadBit("Created");
            packet.ReadBits("DisplayText", 3);
            packet.ReadBit("IsBonusRoll");
            packet.ReadBit("IsEncounterLoot");

            Substructures.ItemHandler.ReadItemInstance(packet);
        }
    }
}
