using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_2_0_30898
{
    public class ArtifactPower : IArtifactPower
    {
        public short ArtifactPowerID { get; set; }
        public byte PurchasedRank { get; set; }
        public byte CurrentRankWithBonus { get; set; }
    }
}

