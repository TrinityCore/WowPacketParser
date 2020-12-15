using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.MountXDisplay, HasIndexInData = false)]
    public class MountXDisplayEntry
    {
        public int CreatureDisplayInfoID { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint MountID { get; set; }
    }
}
