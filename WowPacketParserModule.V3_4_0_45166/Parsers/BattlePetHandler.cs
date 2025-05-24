using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class BattlePetHandler
    {
        public static void ReadClientBattlePetOwnerInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);
            packet.ReadUInt32("PlayerVirtualRealm", idx);
            packet.ReadUInt32("PlayerNativeRealm", idx);
        }

        public static void ReadClientBattlePet(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("BattlePetGUID", idx);

            packet.ReadInt32("SpeciesID", idx);
            packet.ReadInt32("CreatureID", idx);
            packet.ReadInt32("DisplayID", idx);

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
                ReadClientBattlePetOwnerInfo(packet, "OwnerInfo", idx);
        }

        public static void ReadClientPetBattleSlot(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("BattlePetGUID", idx);

            packet.ReadInt32("CollarID", idx);
            packet.ReadByte("SlotIndex", idx);

            packet.ResetBitReader();

            packet.ReadBit("Locked", idx);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_ERROR, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetError(Packet packet)
        {
            packet.ReadBits("Result", 4);
            packet.ReadInt32("CreatureID");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            packet.ReadInt16("TrapLevel");

            var slotsCount = packet.ReadInt32("SlotsCount");
            var petsCount = packet.ReadInt32("PetsCount");

            packet.ReadBit("HasJournalLock");
            packet.ResetBitReader();

            for (var i = 0; i < slotsCount; i++)
                V6_0_2_19033.Parsers.BattlePetHandler.ReadClientPetBattleSlot(packet, i);

            for (var i = 0; i < petsCount; i++)
                ReadClientBattlePet(packet, i);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            var petsCount = packet.ReadInt32("PetsCount");
            packet.ReadBit("AddedPet");
            packet.ResetBitReader();

            for (var i = 0; i < petsCount; ++i)
                ReadClientBattlePet(packet, i);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SUMMON, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PET_DELETED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetDeletePet(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_SLOT_UPDATES, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetBattleSlotUpdates(Packet packet)
        {
            var petBattleSlotCount = packet.ReadInt32("PetBattleSlotCount");

            packet.ReadBit("NewSlotUnlocked");
            packet.ReadBit("AutoSlotted");

            for (int i = 0; i < petBattleSlotCount; i++)
                ReadClientPetBattleSlot(packet, i, "PetBattleSlot");
        }

        [Parser(Opcode.SMSG_QUERY_BATTLE_PET_NAME_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetID");

            packet.ReadInt32("CreatureID");
            packet.ReadTime64("Timestamp");

            packet.ResetBitReader();
            var nonEmpty = packet.ReadBit("Allow");
            if (!nonEmpty)
                return;

            var nameLength = packet.ReadBits(8);

            packet.ReadBit("HasDeclined");
            var declinedNameLengths = new uint[5];
            for (int i = 0; i < 5; i++)
                declinedNameLengths[i] = packet.ReadBits(7);

            for (int i = 0; i < 5; i++)
                packet.ReadWoWString("DeclinedNames", declinedNameLengths[i]);

            packet.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_CLEAR_FANFARE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetClearFanfare(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_BATTLE_SLOT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetSetBattleSlot(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
            packet.ReadByte("SlotIndex");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_FLAGS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetSetFlags(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
            packet.ReadUInt16("Flags");
            packet.ReadBits("ControlType", 2);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_UPDATE_NOTIFY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetUpdateNotify(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }

        [Parser(Opcode.CMSG_QUERY_BATTLE_PET_NAME, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetQuery(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetID");
            packet.ReadPackedGuid128("UnitGUID");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_DENIED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PET_REQUEST_JOURNAL, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_BATTLE_PET_REQUEST_JOURNAL_LOCK, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlePetZero(Packet packet)
        {
        }
    }
}
