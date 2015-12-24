namespace WowPacketParser.DBC.Structures
{
    public sealed class AchievementEntry
    {
         public int ID;
         public int  Faction;
         public int  MapID;
         public uint Supercedes;
         public string Title;
         public string Description;
         public uint Category;
         public uint Points;
         public uint UIOrder;
         public uint Flags;
         public uint IconID;
         public string Reward;
         public uint MinimumCriteria;
         public uint SharesCriteria;
         public uint CriteriaTree;
    }
}
