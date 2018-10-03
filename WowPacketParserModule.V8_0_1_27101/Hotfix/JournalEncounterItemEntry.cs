using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.JournalEncounterItem)]
    public class JournalEncounterItemEntry
    {
        public uint ID { get; set; }
        public ushort JournalEncounterID { get; set; }
        public uint ItemID { get; set; }
        public sbyte FactionMask { get; set; }
        public byte Flags { get; set; }
        public sbyte DifficultyMask { get; set; }
    }
}
