using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SoulbindConduitRank, HasIndexInData = false)]
    public class SoulbindConduitRankEntry
    {
        public int RankIndex { get; set; }
        public int SpellID { get; set; }
        public float AuraPointsOverride { get; set; }
        public uint SoulbindConduitID { get; set; }
    }
}
