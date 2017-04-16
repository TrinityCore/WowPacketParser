using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.MountXDisplay, ClientVersionBuild.V7_0_3_22248, HasIndexInData = false)]
    public class MountXDisplayEntry
    {
        public uint MountID { get; set; }
        public uint DisplayID { get; set; }
        public uint PlayerConditionID { get; set; }
    }
}