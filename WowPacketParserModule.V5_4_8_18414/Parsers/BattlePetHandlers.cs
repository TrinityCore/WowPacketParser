using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class BattlePetHandler
    {
        [Parser(Opcode.CMSG_BATTLE_PET_DELETE_PET)]
        public static void HandleBattlePetDeletePet(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 6, 2, 4, 0, 7, 1);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 4, 1, 5, 0, 7, 2, 3, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME)]
        public static void HandleBattlePetModifyName(Packet packet)
        {
            var guid = new byte[8];
            guid[5] = packet.ReadBit(); // 93
            guid[7] = packet.ReadBit(); // 95
            guid[3] = packet.ReadBit(); // 91
            guid[0] = packet.ReadBit(); // 88
            guid[6] = packet.ReadBit(); // 94
            var len = packet.ReadBits("Len", 7);
            guid[2] = packet.ReadBit(); // 90
            guid[1] = packet.ReadBit(); // 89
            var hasDeclinedNames = packet.ReadBit("hasDeclNames"); // 421
            guid[4] = packet.ReadBit(); // 92

            var declinedNamesLen = new uint[5];

            if (hasDeclinedNames)
                for ( var i = 5; i > 0; i--)
                    declinedNamesLen[i-1] = packet.ReadBits(7);

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 3, 0, 6, 1, 5, 2, 4, 7);
            packet.WriteGuid("Guid", guid);

            packet.ReadWoWString("Nick", len);

            if (hasDeclinedNames)
                for (var i = 5; i > 0; i--)
                    packet.ReadWoWString("declNames", declinedNamesLen[i - 1], i);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_NAME_QUERY)]
        public static void HandleBattlePetNameQuery(Packet packet)
        {
            var petName = new byte[8];
            var petGuid = new byte[8];

            petGuid[2] = packet.ReadBit(); // 26
            petName[6] = packet.ReadBit(); // 22
            petName[3] = packet.ReadBit(); // 19
            petGuid[3] = packet.ReadBit(); // 27
            petName[7] = packet.ReadBit(); // 23
            petGuid[4] = packet.ReadBit(); // 28
            petGuid[1] = packet.ReadBit(); // 25
            petGuid[0] = packet.ReadBit(); // 24
            petName[0] = packet.ReadBit(); // 16
            petGuid[7] = packet.ReadBit(); // 31
            petGuid[5] = packet.ReadBit(); // 29
            petGuid[6] = packet.ReadBit(); // 30
            petName[1] = packet.ReadBit(); // 17 
            petName[2] = packet.ReadBit(); // 18
            petName[5] = packet.ReadBit(); // 21
            petName[4] = packet.ReadBit(); // 20

            packet.ResetBitReader();

            packet.ReadXORByte(petGuid, 5);
            packet.ReadXORByte(petName, 1);
            packet.ReadXORByte(petGuid, 0);
            packet.ReadXORByte(petName, 4);
            packet.ReadXORByte(petGuid, 3);
            packet.ReadXORByte(petName, 3);
            packet.ReadXORByte(petGuid, 1);
            packet.ReadXORByte(petGuid, 6);
            packet.ReadXORByte(petName, 6);
            packet.ReadXORByte(petName, 0);
            packet.ReadXORByte(petName, 2);
            packet.ReadXORByte(petGuid, 7);
            packet.ReadXORByte(petGuid, 2);
            packet.ReadXORByte(petName, 7);
            packet.ReadXORByte(petGuid, 4);
            packet.ReadXORByte(petName, 5);

            packet.WriteGuid("petName", petName);
            packet.WriteGuid("petGuid", petGuid);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_BATTLE_SLOT)]
        public static void HandleBattlePetSetBattleSlot(Packet packet)
        {
            packet.ReadByte("Slot");
            var guid = packet.StartBitStream(4, 6, 5, 7, 3, 1, 0, 2);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 1, 3, 5, 0, 7, 6, 4, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_FLAGS)]
        public static void HandleBattlePetSetFlags(Packet packet)
        {
            packet.ReadInt32("Flags");
            var guid = new byte[8];
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            packet.ReadBits("Mode", 2);
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 4, 0, 7, 3, 1, 6, 2, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SUMMON_COMPANION)]
        public static void HandleBattlePetSummonCompanion(Packet packet)
        {
            var guid = packet.StartBitStream(3, 2, 5, 0, 7, 1, 6, 4);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 6, 7, 3, 5, 0, 4, 1, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_DELETED)]
        public static void HandleBattlePetDeleted(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 7, 6, 1, 5, 2, 3);
            packet.ParseBitStream(guid, 6, 1, 7, 0, 4, 3, 5, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED)]
        public static void HandleBattlePetJournalLockAcquired(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_DENIED)]
        public static void HandleBattlePetJournalLockDenied(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_BATTLE_PET_QUERY_NAME_RESPONSE)]
        public static void HandleBattlePetQueryNameResponse(Packet packet)
        {
            packet.ReadInt64("Petentry");
            packet.ReadTime("Time");
            packet.ReadInt32("SpeciesId");
            var declinedNamesLen = new uint[5];
            var hasDeclinedNames = packet.ReadBit("hasDeclNames");
            var len = 0u;
            if (hasDeclinedNames)
            {
                len = packet.ReadBits(8);
                for (var i = 5; i > 0; i--)
                    declinedNamesLen[i - 1] = packet.ReadBits(7);
                packet.ReadBit("unk");
            }

            if (hasDeclinedNames)
            {
                for (var i = 5; i > 0; i--)
                    packet.ReadWoWString("declNames", declinedNamesLen[i - 1], i);
                packet.ReadWoWString("Name", len);
            }
        }

        [Parser(Opcode.SMSG_BATTLE_PET_SLOT_UPDATE)]
        public static void HandleBattlePetSlotUpdate(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
