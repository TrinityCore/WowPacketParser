using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.JournalItemXDifficulty, HasIndexInData = false)]
    public class JournalItemXDifficultyEntry
    {
        public byte DifficultyID { get; set; }
        public ushort JournalEncounterItemID { get; set; }
    }
}
