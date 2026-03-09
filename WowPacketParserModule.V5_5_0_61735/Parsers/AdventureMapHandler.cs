using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class AdventureMapHandler
    {
        public static void ReadAdventureJournalEntry(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("AdventureJournalID", indexes);
            packet.ReadInt32("Priority", indexes);
        }

        [Parser(Opcode.SMSG_PLAYER_IS_ADVENTURE_MAP_POI_VALID)]
        public static void HandlePlayerIsAdventureMapPOIValid(Packet packet)
        {
            packet.ReadInt32("AdventureMapPoiID");
            packet.ResetBitReader();
            packet.ReadBit("IsVisible");
        }

        [Parser(Opcode.SMSG_ADVENTURE_JOURNAL_DATA_RESPONSE)]
        public static void HandleAdventureJournalDataResponse(Packet packet)
        {
            packet.ReadBit("OnLevelUp");
            var entryCount = packet.ReadUInt32("NumEntries");
            for (var i = 0u; i < entryCount; ++i)
                ReadAdventureJournalEntry(packet, "AdventureJournalEntry", i);
        }

        [Parser(Opcode.CMSG_CHECK_IS_ADVENTURE_MAP_POI_VALID)]
        public static void HandleCheckIsAdventureMapPOIValid(Packet packet)
        {
            packet.ReadUInt32("AdventureMapPoiID");
        }

        [Parser(Opcode.CMSG_ADVENTURE_MAP_START_QUEST)]
        public static void HandleAdventureMapStartQuest(Packet packet)
        {
            packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.CMSG_ADVENTURE_JOURNAL_OPEN_QUEST)]
        public static void HandleAdventureZero(Packet packet)
        {
        }
    }
}
