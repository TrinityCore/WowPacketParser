using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PvpTier, HasIndexInData = false)]
    public class PvpTierEntry
    {
        public string Name { get; set; }
        public short PrevTier { get; set; }
        public short NextTier { get; set; }
        public int MinRating { get; set; }
        public int MaxRating { get; set; }
        public sbyte BracketID { get; set; }
        public sbyte Rank { get; set; }
        public int RankIcon { get; set; }
    }
}
