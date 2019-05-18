using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.RewardPack, HasIndexInData = false)]
    public class RewardPackEntry
    {
        public int CharTitleID { get; set; }
        public uint Money { get; set; }
        public sbyte ArtifactXPDifficulty { get; set; }
        public float ArtifactXPMultiplier { get; set; }
        public byte ArtifactXPCategoryID { get; set; }
        public uint TreasurePickerID { get; set; }
    }
}
