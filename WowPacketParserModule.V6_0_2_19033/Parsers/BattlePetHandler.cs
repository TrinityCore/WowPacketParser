using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BattlePetHandler
    {
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
    }
}
