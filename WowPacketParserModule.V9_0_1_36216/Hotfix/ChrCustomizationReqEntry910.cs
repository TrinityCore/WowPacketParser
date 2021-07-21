using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.ChrCustomizationReq, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class ChrCustomizationReqEntry
    {
        public int Flags { get; set; }
        public int ClassMask { get; set; }
        public int AchievementID { get; set; }
        public int OverrideArchive { get; set; }
        public int ItemModifiedAppearanceID { get; set; }
    }
}
