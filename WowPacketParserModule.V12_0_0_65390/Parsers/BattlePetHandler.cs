using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class BattlePetHandler
    {
        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            packet.ReadInt16("Trap");

            var slotsCount = packet.ReadInt32("SlotsCount");
            var petsCount = packet.ReadInt32("PetsCount");

            for (var i = 0; i < slotsCount; i++)
                V6_0_2_19033.Parsers.BattlePetHandler.ReadClientPetBattleSlot(packet, "Slots", i);

            for (var i = 0; i < petsCount; i++)
                V7_0_3_22248.Parsers.BattlePetHandler.ReadClientBattlePet(packet, "Pets", i);

            packet.ResetBitReader();
            packet.ReadBit("HasJournalLock");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            var petsCount = packet.ReadInt32("PetsCount");
            for (var i = 0; i < petsCount; ++i)
                V7_0_3_22248.Parsers.BattlePetHandler.ReadClientBattlePet(packet, i);

            packet.ResetBitReader();
            packet.ReadBit("PetAdded");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_SLOT_UPDATES, ClientVersionBuild.V12_1_0_69214)]
        public static void HandlePetBattleSlotUpdates(Packet packet)
        {
            var petBattleSlotCount = packet.ReadInt32("PetBattleSlotCount");

            for (var i = 0; i < petBattleSlotCount; i++)
                V6_0_2_19033.Parsers.BattlePetHandler.ReadClientPetBattleSlot(packet, "Slots", i);

            packet.ResetBitReader();
            packet.ReadBit("NewSlotUnlocked");
            packet.ReadBit("AutoSlotted");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleBattlePetModifyName(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");

            packet.ResetBitReader();

            var nameLen = packet.ReadBits(7);
            var hasDeclinedNames = packet.ReadBit("HasDeclinedNames");

            packet.ReadWoWString("Name", nameLen);

            if (hasDeclinedNames)
            {
                var declinedNamesLen = new uint[5];
                for (var i = 0; i < 5; i++)
                    declinedNamesLen[i] = packet.ReadBits(7);

                for (var i = 0; i < 5; i++)
                    packet.ReadWoWString("DeclinedNames", declinedNamesLen[i]);
            }
        }
    }
}
