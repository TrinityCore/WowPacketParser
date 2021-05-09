using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChrClassUiDisplay, HasIndexInData = false)]
    public class ChrClassUIDisplayEntry
    {
        public byte ChrClassesID { get; set; }
        public uint AdvGuidePlayerConditionID { get; set; }
        public uint SplashPlayerConditionID { get; set; }

    }
}
