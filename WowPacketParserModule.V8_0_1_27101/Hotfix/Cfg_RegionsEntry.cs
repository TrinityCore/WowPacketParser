using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Cfg_Regions, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_2_0_30898, HasIndexInData = false)]
    public class Cfg_RegionsEntry
    {
        public string Tag { get; set; }
        public ushort RegionID { get; set; }
        public uint Raidorigin { get; set; }
        public byte RegionGroupMask { get; set; }
        public uint ChallengeOrigin { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        [HotfixArray(2)]
        public int[] ChallengeTimeOffset { get; set; }
    }
}
