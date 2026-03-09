using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_bonus_tree_node")]
    public sealed record ItemBonusTreeNodeHotfix1100 : IDataModel
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
    public sealed record ItemBonusTreeNodeHotfix1102 : IDataModel
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
    public sealed record ItemBonusTreeNodeHotfix1125 : IDataModel
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

        [DBFieldName("ItemCreationContextGroupID")]
        public int? ItemCreationContextGroupID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ParentItemBonusTreeID")]
        public uint? ParentItemBonusTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
