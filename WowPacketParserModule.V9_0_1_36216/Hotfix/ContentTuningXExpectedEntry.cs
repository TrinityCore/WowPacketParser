using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ContentTuningXExpected, HasIndexInData = false)]
    public class ContentTuningXExpectedEntry
    {
        public int ExpectedStatModID { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_1_0_39185, true)]
        public int MythicPlusSeasonID { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_1_0_39185, false)]
        public int MinMythicPlusSeasonID { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_1_0_39185, false)]
        public int MaxMythicPlusSeasonID { get; set; }
        public uint ContentTuningID { get; set; }
    }
}
