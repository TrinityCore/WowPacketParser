using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("modified_crafting_item")]
    public sealed record ModifiedCraftingItemHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ModifiedCraftingReagentItemID")]
        public int? ModifiedCraftingReagentItemID;

        [DBFieldName("CraftingQualityID")]
        public int? CraftingQualityID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
