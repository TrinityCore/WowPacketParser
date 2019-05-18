using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Cfg_Regions, HasIndexInData = false)]
    public class Cfg_RegionsEntry
    {
        public string Tag { get; set; }
        public ushort RegionID { get; set; }
        public uint Raidorigin { get; set; }
        public byte RegionGroupMask { get; set; }
        public uint ChallengeOrigin { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        [HotfixArray(2)]
        public int[] Unknown1 { get; set; }
    }
}
