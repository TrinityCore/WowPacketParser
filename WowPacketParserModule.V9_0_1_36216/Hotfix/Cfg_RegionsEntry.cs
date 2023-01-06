using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.CfgRegions, HasIndexInData = false)]
    public class Cfg_RegionsEntry
    {
        public string Tag { get; set; }
        public ushort RegionID { get; set; }
        public uint Raidorigin { get; set; }
        public byte RegionGroupMask { get; set; }
        public uint ChallengeOrigin { get; set; }
    }
}
