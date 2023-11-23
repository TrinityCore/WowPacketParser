using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_bonus_tree_node")]
    public sealed record ItemBonusTreeNodeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("ChildItemBonusTreeID")]
        public ushort? ChildItemBonusTreeID;

        [DBFieldName("ChildItemBonusListID")]
        public ushort? ChildItemBonusListID;

        [DBFieldName("ChildItemLevelSelectorID")]
        public ushort? ChildItemLevelSelectorID;

        [DBFieldName("ChildItemBonusListGroupID")]
        public int? ChildItemBonusListGroupID;

        [DBFieldName("IblGroupPointsModSetID")]
        public int? IblGroupPointsModSetID;

        [DBFieldName("ParentItemBonusTreeID")]
        public uint? ParentItemBonusTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_bonus_tree_node")]
    public sealed record ItemBonusTreeNodeHotfix1010 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("ChildItemBonusTreeID")]
        public ushort? ChildItemBonusTreeID;

        [DBFieldName("ChildItemBonusListID")]
        public ushort? ChildItemBonusListID;

        [DBFieldName("ChildItemLevelSelectorID")]
        public ushort? ChildItemLevelSelectorID;

        [DBFieldName("ChildItemBonusListGroupID")]
        public int? ChildItemBonusListGroupID;

        [DBFieldName("IblGroupPointsModSetID")]
        public int? IblGroupPointsModSetID;

        [DBFieldName("MinMythicPlusLevel")]
        public int? MinMythicPlusLevel;

        [DBFieldName("MaxMythicPlusLevel")]
        public int? MaxMythicPlusLevel;

        [DBFieldName("ParentItemBonusTreeID")]
        public uint? ParentItemBonusTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_bonus_tree_node")]
    public sealed record ItemBonusTreeNodeHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("ChildItemBonusTreeID")]
        public ushort? ChildItemBonusTreeID;

        [DBFieldName("ChildItemBonusListID")]
        public ushort? ChildItemBonusListID;

        [DBFieldName("ChildItemLevelSelectorID")]
        public ushort? ChildItemLevelSelectorID;

        [DBFieldName("ParentItemBonusTreeID")]
        public int? ParentItemBonusTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
