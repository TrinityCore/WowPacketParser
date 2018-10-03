using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ContentTuningDescription, HasIndexInData = false)]
    public class ContentTuningDescriptionEntry
    {
        public string Description { get; set; }
    }
}
    