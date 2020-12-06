using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_2_36639
{
    public class ArtifactPower : IArtifactPower
    {
        public short ArtifactPowerID { get; set; }
        public byte PurchasedRank { get; set; }
        public byte CurrentRankWithBonus { get; set; }
    }
}

