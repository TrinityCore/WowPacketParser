using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GlobalStrings, HasIndexInData = false)]
    public class GlobalStringsEntry
    {
        public string BaseTag { get; set; }
        public string TagText { get; set; }
        public byte Flags { get; set; }
    }
}
