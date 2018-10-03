using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrString, HasIndexInData = false)]
    public class GarrStringEntry
    {
        public string Text { get; set; }
    }
}
