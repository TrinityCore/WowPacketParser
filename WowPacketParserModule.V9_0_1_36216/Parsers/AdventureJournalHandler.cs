using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class AdventureJournalHandler
    {
        [Parser(Opcode.CMSG_ADVENTURE_JOURNAL_UPDATE_SUGGESTIONS)]
        public static void HandleAdventureJournalUpdateSuggestions(Packet packet)
        {
            packet.ReadBit("OnLevelUp");
        }

        public static void ReadAdventureJournalEntry(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("AdventureJournalID", indexes);
            packet.ReadInt32("Priority", indexes);
        }

        [Parser(Opcode.SMSG_ADVENTURE_JOURNAL_DATA_RESPONSE)]
        public static void HandleAdventureJournalDataResponse(Packet packet)
        {
            packet.ReadBit("OnLevelUp");
            var entryCount = packet.ReadUInt32("NumEntries");
            for (var i = 0u; i < entryCount; ++i)
                ReadAdventureJournalEntry(packet, "AdventureJournalEntry", i);
        }
    }
}
