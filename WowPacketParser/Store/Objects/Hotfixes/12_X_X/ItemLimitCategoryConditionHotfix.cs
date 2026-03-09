using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_limit_category_condition")]
    public sealed record ItemLimitCategoryConditionHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AddQuantity")]
        public sbyte? AddQuantity;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("ParentItemLimitCategoryID")]
        public uint? ParentItemLimitCategoryID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
