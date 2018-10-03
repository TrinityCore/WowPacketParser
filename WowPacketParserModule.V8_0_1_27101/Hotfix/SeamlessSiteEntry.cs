using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SeamlessSite, HasIndexInData = false)]
    public class SeamlessSiteEntry
    {
        public int MapID { get; set; }
    }
}
