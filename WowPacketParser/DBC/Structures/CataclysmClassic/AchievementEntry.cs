using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("Achievement")]
    public sealed class AchievementEntry
    {
        public string Description;
        public string Title;
        public string Reward;
        [Index(false)]
        public uint ID;
        public short InstanceID;
        public sbyte Faction;
        public short Supercedes;
        public short Category;
        public sbyte MinimumCriteria;
        public sbyte Points;
        public int Flags;
        public ushort UiOrder;
        public int IconFileID;
        public uint CriteriaTree;
        public short SharesCriteria;
    }
}
