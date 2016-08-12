using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Achievement)]
    public class AchievementEntry
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public uint Flags { get; set; }
        public string Reward { get; set; }
        public short MapID { get; set; }
        public ushort Supercedes { get; set; }
        public ushort Category { get; set; }
        public ushort UIOrder { get; set; }
        public ushort IconID { get; set; }
        public ushort SharesCriteria { get; set; }
        public ushort CriteriaTree { get; set; }
        public sbyte Faction { get; set; }
        public byte Points { get; set; }
        public byte MinimumCriteria { get; set; }
        public uint ID { get; set; }
    }
}
