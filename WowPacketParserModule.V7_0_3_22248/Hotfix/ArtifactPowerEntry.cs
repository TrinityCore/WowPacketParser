using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactPower)]
    public class ArtifactPowerEntry
    {
        [HotfixArray(2)]
        public float[] Pos { get; set; }
        public byte ArtifactID { get; set; }
        public byte Flags { get; set; }
        public byte MaxRank { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public byte ArtifactTier { get; set; }
        public uint ID { get; set; }
        public int RelicType { get; set; }
    }
}
