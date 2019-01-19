using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactPower)]
    public class ArtifactPowerEntry
    {
        [HotfixArray(2)]
        public float[] DisplayPos { get; set; }
        public int ID { get; set; }
        public byte ArtifactID { get; set; }
        public byte MaxPurchasableRank { get; set; }
        public int Label { get; set; }
        public byte Flags { get; set; }
        public byte Tier { get; set; }
    }
}
