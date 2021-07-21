using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.PvpTier)]
    public class PvpTierEntry
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public short MinRating { get; set; }
        public short MaxRating { get; set; }
        public int PrevTier { get; set; }
        public int NextTier { get; set; }
        public sbyte BracketID { get; set; }
        public sbyte Rank { get; set; }
        public int RankIconFileDataID { get; set; }
    }
}
