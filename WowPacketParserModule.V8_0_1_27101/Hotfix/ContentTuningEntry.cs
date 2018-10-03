using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ContentTuning)]
    public class ContentTuningEntry
    {
        public int ID { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int Flags { get; set; }
        public int ExpectedStatModID { get; set; }
        public int DifficultyESMID { get; set; }
    }
}
