using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class TransmogrificationHandler
    {
        public static void ReadTransmogOutfitDataInfo(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadByte("SetType", indexes);
            packet.ReadUInt32("Icon", indexes);
            var nameLength = packet.ReadBits(8);
            packet.ReadBit("SituationsEnabled", indexes);
            packet.ReadWoWString("Name", nameLength, indexes);
        }

        public static void ReadTransmogOutfitSituationInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("SituationID", indexes);
            packet.ReadUInt32("SpecID", indexes);
            packet.ReadUInt32("LoadoutID", indexes);
            packet.ReadUInt32("EquipmentSetID", indexes);
        }

        public static void ReadTransmogOutfitSlotData(Packet packet, params object[] indexes)
        {
            packet.ReadSByte("Slot", indexes);
            packet.ReadByte("SlotOption", indexes);
            packet.ReadUInt32("ItemModifiedAppearanceID", indexes);
            packet.ReadByte("AppearanceDisplayType", indexes);
            packet.ReadUInt32("SpellItemEnchantmentID", indexes);
            packet.ReadByte("IllusionDisplayType", indexes);
            packet.ReadUInt32("Flags", indexes);
        }

        [Parser(Opcode.CMSG_TRANSMOG_OUTFIT_NEW)]
        public static void HandleTransmogOutfitNew(Packet packet)
        {
            packet.ReadPackedGuid128("Npc");
            packet.ReadByte("Source");
            ReadTransmogOutfitDataInfo(packet, "Info");
        }

        [Parser(Opcode.SMSG_TRANSMOG_OUTFIT_NEW_ENTRY_ADDED)]
        public static void HandleTransmogOutfitNewEntryAdded(Packet packet)
        {
            packet.ReadUInt32("TransmogOutfitID");
        }

        [Parser(Opcode.CMSG_TRANSMOG_OUTFIT_UPDATE_INFO)]
        public static void HandleTransmogOutfitUpdateInfo(Packet packet)
        {
            packet.ReadUInt32("TransmogOutfitID");
            packet.ReadPackedGuid128("Npc");
            ReadTransmogOutfitDataInfo(packet, "Info");
        }

        [Parser(Opcode.SMSG_TRANSMOG_OUTFIT_INFO_UPDATED)]
        public static void HandleTransmogOutfitInfoUpdated(Packet packet)
        {
            packet.ReadUInt32("TransmogOutfitID");
            ReadTransmogOutfitDataInfo(packet, "OutfitInfo");
        }

        [Parser(Opcode.CMSG_TRANSMOG_OUTFIT_UPDATE_SITUATIONS)]
        public static void HandleTransmogOutfitUpdateSituations(Packet packet)
        {
            packet.ReadUInt32("OutfitID");
            packet.ReadPackedGuid128("Npc");
            var situationsCount = packet.ReadUInt32();

            for (var i = 0u; i < situationsCount; ++i)
                ReadTransmogOutfitSituationInfo(packet, "Situations", i);

            packet.ResetBitReader();
            packet.ReadBit("SituationsEnabled");
        }

        [Parser(Opcode.SMSG_TRANSMOG_OUTFIT_SITUATIONS_UPDATED)]
        public static void HandleTransmogOutfitSituationsUpdated(Packet packet)
        {
            packet.ReadUInt32("TransmogOutfitID");
            var situationsCount = packet.ReadUInt32();

            for (var i = 0u; i < situationsCount; ++i)
                ReadTransmogOutfitSituationInfo(packet, "Situations", i);

            packet.ResetBitReader();
            packet.ReadBit("SituationsEnabled");
        }

        [Parser(Opcode.CMSG_TRANSMOG_OUTFIT_UPDATE_SLOTS)]
        public static void HandleTransmogOutfitUpdateSlots(Packet packet)
        {
            packet.ReadUInt32("OutfitID");
            var slotCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("Npc");
            packet.ReadUInt64("Cost");

            for (var i = 0u; i < slotCount; ++i)
                ReadTransmogOutfitSlotData(packet, "Slots", i);

            packet.ResetBitReader();
            packet.ReadBit("UseAvailableDiscount");
        }

        [Parser(Opcode.SMSG_TRANSMOG_OUTFIT_SLOTS_UPDATED)]
        public static void HandleTransmogOutfitSlotsUpdated(Packet packet)
        {
            packet.ReadUInt32("TransmogOutfitID");
            var slotCount = packet.ReadUInt32();

            for (var i = 0u; i < slotCount; ++i)
                ReadTransmogOutfitSlotData(packet, "Slots", i);
        }
    }
}
