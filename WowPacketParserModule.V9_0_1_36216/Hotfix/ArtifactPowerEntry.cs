using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactPower)]
    public class ArtifactPowerEntry
    {
        [HotfixArray(2, true)]
        public float[] DisplayPos { get; set; }
        public uint ID { get; set; }
        public byte ArtifactID { get; set; }
        public byte MaxPurchasableRank { get; set; }
        public int Label { get; set; }
        public byte Flags { get; set; }
        public byte Tier { get; set; }
    }
}
