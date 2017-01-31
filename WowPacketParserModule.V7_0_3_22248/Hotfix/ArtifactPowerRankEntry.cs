using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ArtifactPowerRank, HasIndexInData = false)]
    public class ArtifactPowerRankEntry
    {
        public uint SpellID { get; set; }
        public float Value { get; set; }
        public ushort ArtifactPowerID { get; set; }
        public ushort Unknown { get; set; }
        public byte Rank { get; set; }
    }
}
