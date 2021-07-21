using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.ContentTuning, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class ContentTuningEntry
    {
        public int Flags { get; set; }
        public int ExpansionID { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int MinLevelType { get; set; }
        public int MaxLevelType { get; set; }
        public int TargetLevelDelta { get; set; }
        public int TargetLevelMaxDelta { get; set; }
        public int TargetLevelMin { get; set; }
        public int TargetLevelMax { get; set; }
        public int MinItemLevel { get; set; }
    }
}
