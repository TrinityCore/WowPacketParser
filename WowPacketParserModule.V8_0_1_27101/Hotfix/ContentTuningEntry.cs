using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ContentTuning)]
    public class ContentTuningEntry
    {
        public uint ID { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int Flags { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_2_0_30898, true)]
        public int ExpectedStatModID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_2_0_30898, true)]
        public int DifficultyESMID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_2_0_30898, false)]
        public int ExpansionID { get; set; }
    }
}
