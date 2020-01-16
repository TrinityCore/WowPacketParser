using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_2_0_30898.Hotfix
{
    [HotfixStructure(DB2Hash.Cfg_Regions, ClientVersionBuild.V8_2_0_30898, HasIndexInData = false)]
    public class Cfg_RegionsEntry
    {
        public string Tag { get; set; }
        public ushort RegionID { get; set; }
        public uint Raidorigin { get; set; }
        public byte RegionGroupMask { get; set; }
        public uint ChallengeOrigin { get; set; }
    }
}
