using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChrClassUIDisplay, HasIndexInData = false)]
    public class ChrClassUIDisplayEntry
    {
        public byte ChrClassesID { get; set; }
        public uint AdvGuidePlayerConditionID { get; set; }
        public uint SplashPlayerConditionID { get; set; }
    }
}
