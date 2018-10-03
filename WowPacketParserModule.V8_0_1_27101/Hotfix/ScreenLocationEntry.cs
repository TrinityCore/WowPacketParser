using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ScreenLocation, HasIndexInData = false)]
    public class ScreenLocationEntry
    {
        public string Name { get; set; }
    }
}
