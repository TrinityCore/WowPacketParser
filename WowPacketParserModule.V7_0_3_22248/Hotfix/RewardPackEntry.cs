using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.RewardPack, HasIndexInData = false)]
    public class RewardPackEntry
    {
        public uint Money { get; set; }
        public float ArtifactXPMultiplier { get; set; }
        public byte ArtifactXPDifficulty { get; set; }
        public byte ArtifactCategoryID { get; set; }
        public uint TitleID { get; set; }
        public uint Unused { get; set; }
    }
}
