using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ContentTuningXExpected, HasIndexInData = false)]
    public class ContentTuningXExpectedEntry
    {
        public int ExpectedStatModID { get; set; }
        public int ContentTuningID { get; set; }
    }
}
