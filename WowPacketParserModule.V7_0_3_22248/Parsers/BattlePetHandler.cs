using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class BattlePetHandler
    {
        public static void ReadClientBattlePet(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("BattlePetGUID", idx);

            packet.ReadInt32("SpeciesID", idx);
            packet.ReadInt32("DisplayID", idx);
            packet.ReadInt32("CollarID", idx);

            packet.ReadInt16("BreedID", idx);
            packet.ReadInt16("Level", idx);
            packet.ReadInt16("Xp", idx);
            packet.ReadInt16("BattlePetDBFlags", idx);

            packet.ReadInt32("Power", idx);
            packet.ReadInt32("Health", idx);
            packet.ReadInt32("MaxHealth", idx);
            packet.ReadInt32("Speed", idx);

            packet.ReadByte("BreedQuality", idx);

            packet.ResetBitReader();

            var customNameLength = packet.ReadBits(7);
            var hasOwnerInfo = packet.ReadBit("HasOwnerInfo", idx);
            packet.ReadBit("NoRename", idx);

            packet.ReadWoWString("CustomName", customNameLength, idx);

            if (hasOwnerInfo)
                V6_0_2_19033.Parsers.BattlePetHandler.ReadClientBattlePetOwnerInfo(packet, "OwnerInfo", idx);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            packet.ReadInt16("TrapLevel");

            var slotsCount = packet.ReadInt32("SlotsCount");
            var petsCount = packet.ReadInt32("PetsCount");
            packet.ReadInt32("MaxPets");

            packet.ReadBit("HasJournalLock");
            packet.ResetBitReader();

            for (var i = 0; i < slotsCount; i++)
                V6_0_2_19033.Parsers.BattlePetHandler.ReadClientPetBattleSlot(packet, i);

            for (var i = 0; i < petsCount; i++)
                ReadClientBattlePet(packet, i);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            var petsCount = packet.ReadInt32("PetsCount");
            packet.ReadBit("AddedPet");
            packet.ResetBitReader();

            for (var i = 0; i < petsCount; ++i)
                ReadClientBattlePet(packet, i);
        }

        [Parser(Opcode.SMSG_PET_BATTLE_SLOT_UPDATES)]
        public static void HandlePetBattleSlotUpdates(Packet packet)
        {
            var petBattleSlotCount = packet.ReadInt32("PetBattleSlotCount");

            packet.ReadBit("NewSlotUnlocked");
            packet.ReadBit("AutoSlotted");

            for (int i = 0; i < petBattleSlotCount; i++)
                V6_0_2_19033.Parsers.BattlePetHandler.ReadClientPetBattleSlot(packet, i, "PetBattleSlot");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_ERROR)]
        public static void HandleBattlePetError(Packet packet)
        {
            packet.ReadBits("Result", 3);
            packet.ReadInt32("CreatureID");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME, ClientVersionBuild.V7_2_0_23826)]
        public static void HandleBattlePetModifyName(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");

            packet.ResetBitReader();

            var nameLen = packet.ReadBits(7);
            var hasDeclinedNames = packet.ReadBit("HasDeclinedNames");

            if (hasDeclinedNames)
            {
                var declinedNamesLen = new uint[5];
                for (int i = 0; i < 5; i++)
                    declinedNamesLen[i] = packet.ReadBits(7);

                for (int i = 0; i < 5; i++)
                    packet.ReadWoWString("DeclinedNames", declinedNamesLen[i]);
            }

            packet.ReadWoWString("Name", nameLen);
        }
    }
}
