using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BattlePetHandler
    {
        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            packet.ReadInt16("TrapLevel");

            var int16 = packet.ReadInt32("SlotsCount");
            var int32 = packet.ReadInt32("PetsCount");

            for (var i = 0; i < int16; i++)
            {
                packet.ReadPackedGuid128("BattlePetGUID", i);

                packet.ReadInt32("CollarID", i);
                packet.ReadByte("SlotIndex", i);

                packet.ResetBitReader();

                packet.ReadBit("Locked", i);
            }

            for (var i = 0; i < int32; i++)
            {
                packet.ReadPackedGuid128("BattlePetGUID", i);

                packet.ReadInt32("SpeciesID", i);
                packet.ReadInt32("DisplayID", i);
                packet.ReadInt32("CollarID", i);

                packet.ReadInt16("BreedID", i);
                packet.ReadInt16("Level", i);
                packet.ReadInt16("Xp", i);
                packet.ReadInt16("BattlePetDBFlags", i);

                packet.ReadInt32("Power", i);
                packet.ReadInt32("Health", i);
                packet.ReadInt32("MaxHealth", i);
                packet.ReadInt32("Speed", i);

                packet.ReadByte("BreedQuality", i);

                packet.ResetBitReader();
                var bits52 = packet.ReadBits(7);
                var bit144 = packet.ReadBit("CustomName", i);

                packet.ReadBit("HasOwnerInfo", i);

                if (bit144)
                {
                    packet.ReadPackedGuid128("Guid", i);
                    packet.ReadInt32("PlayerVirtualRealm", i);
                    packet.ReadInt32("PlayerNativeRealm", i);
                }

                packet.ReadWoWString("CustomName", bits52, i);
            }

            packet.ResetBitReader();

            packet.ReadBit("HasJournalLock");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_NAME_QUERY)]
        public static void HandleBattlePetQuery(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetID");
            packet.ReadPackedGuid128("UnitGUID");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_NAME_QUERY_RESPONSE)]
        public static void HandleBattlePetQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetID");

            packet.ReadInt32("CreatureID");
            packet.ReadTime("Timestamp");

            packet.ResetBitReader();
            var bit40 = packet.ReadBit("Allow");
            if (!bit40)
                return;

            var bits49 = packet.ReadBits(8);
            packet.ReadBit("HasDeclined");

            var bits97 = new uint[5];
            for (int i = 0; i < 5; i++)
                bits97[i] = packet.ReadBits(7);

            for (int i = 0; i < 5; i++)
                packet.ReadWoWString("DeclinedNames", bits97[i]);

            packet.ReadWoWString("Name", bits49);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_DELETE_PET)]
        public static void HandleBattlePetDeletePet(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME)]
        public static void HandleBattlePetModifyName(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");

            packet.ResetBitReader();

            var bits342 = packet.ReadBits(7);
            var bit341 = packet.ReadBit("HasDeclinedNames");

            packet.ReadWoWString("Name", bits342);

            if (bit341)
            {
                var bits97 = new uint[5];
                for (int i = 0; i < 5; i++)
                    bits97[i] = packet.ReadBits(7);

                for (int i = 0; i < 5; i++)
                    packet.ReadWoWString("DeclinedNames", bits97[i]);
            }
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_BATTLE_SLOT)]
        public static void HandleBattlePetSetBattleSlot(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
            packet.ReadByte("SlotIndex");
        }
    }
}
