using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ContentTuningXExpected, HasIndexInData = false)]
    public class ContentTuningXExpectedEntry
    {
        public int ExpectedStatModID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_3_0_33062, false)]
        public int MythicPlusSeasonID { get; set; }
        public int ContentTuningID { get; set; }
    }
}
