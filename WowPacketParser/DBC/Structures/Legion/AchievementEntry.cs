namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("Achievement")]

    public sealed class AchievementEntry
    {
        public string Title;
        public string Description;
        public string Reward;
        public int Flags;
        public short InstanceID;
        public short Supercedes;
        public short Category;
        public short UiOrder;
        public short SharesCriteria;
        public sbyte Faction;
        public sbyte Points;
        public sbyte MinimumCriteria;
        public uint ID;
        public int IconFileDataID;
        public uint CriteriaTree;
    }
}
