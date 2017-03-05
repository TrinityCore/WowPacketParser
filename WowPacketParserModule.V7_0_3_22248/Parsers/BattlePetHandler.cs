using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class BattlePetHandler
    {
        public static void ReadClientBattlePet(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("BattlePetGUID", idx);

            packet.Translator.ReadInt32("SpeciesID", idx);
            packet.Translator.ReadInt32("DisplayID", idx);
            packet.Translator.ReadInt32("CollarID", idx);

            packet.Translator.ReadInt16("BreedID", idx);
            packet.Translator.ReadInt16("Level", idx);
            packet.Translator.ReadInt16("Xp", idx);
            packet.Translator.ReadInt16("BattlePetDBFlags", idx);

            packet.Translator.ReadInt32("Power", idx);
            packet.Translator.ReadInt32("Health", idx);
            packet.Translator.ReadInt32("MaxHealth", idx);
            packet.Translator.ReadInt32("Speed", idx);

            packet.Translator.ReadByte("BreedQuality", idx);

            packet.Translator.ResetBitReader();

            var customNameLength = packet.Translator.ReadBits(7);
            var hasOwnerInfo = packet.Translator.ReadBit("HasOwnerInfo", idx);
            packet.Translator.ReadBit("NoRename", idx);

            packet.Translator.ReadWoWString("CustomName", customNameLength, idx);

            if (hasOwnerInfo)
                V6_0_2_19033.Parsers.BattlePetHandler.ReadClientBattlePetOwnerInfo(packet, "OwnerInfo", idx);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            packet.Translator.ReadInt16("TrapLevel");

            var slotsCount = packet.Translator.ReadInt32("SlotsCount");
            var petsCount = packet.Translator.ReadInt32("PetsCount");
            packet.Translator.ReadInt32("MaxPets");

            packet.Translator.ReadBit("HasJournalLock");
            packet.Translator.ResetBitReader();

            for (var i = 0; i < slotsCount; i++)
                V6_0_2_19033.Parsers.BattlePetHandler.ReadClientPetBattleSlot(packet, i);

            for (var i = 0; i < petsCount; i++)
                ReadClientBattlePet(packet, i);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            var petsCount = packet.Translator.ReadInt32("PetsCount");
            packet.Translator.ReadBit("AddedPet");
            packet.Translator.ResetBitReader();

            for (var i = 0; i < petsCount; ++i)
                ReadClientBattlePet(packet, i);
        }

        [Parser(Opcode.SMSG_PET_BATTLE_SLOT_UPDATES)]
        public static void HandlePetBattleSlotUpdates(Packet packet)
        {
            var petBattleSlotCount = packet.Translator.ReadInt32("PetBattleSlotCount");

            packet.Translator.ReadBit("NewSlotUnlocked");
            packet.Translator.ReadBit("AutoSlotted");

            for (int i = 0; i < petBattleSlotCount; i++)
                V6_0_2_19033.Parsers.BattlePetHandler.ReadClientPetBattleSlot(packet, i, "PetBattleSlot");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_ERROR)]
        public static void HandleBattlePetError(Packet packet)
        {
            packet.Translator.ReadBits("Result", 3);
            packet.Translator.ReadInt32("CreatureID");
        }
    }
}
