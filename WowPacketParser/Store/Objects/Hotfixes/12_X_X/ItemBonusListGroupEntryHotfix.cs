using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_bonus_list_group_entry")]
    public sealed record ItemBonusListGroupEntryHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemBonusListGroupID")]
        public uint? ItemBonusListGroupID;

        [DBFieldName("ItemBonusListID")]
        public int? ItemBonusListID;

        [DBFieldName("ItemLevelSelectorID")]
        public int? ItemLevelSelectorID;

        [DBFieldName("SequenceValue")]
        public int? SequenceValue;

        [DBFieldName("ItemExtendedCostID")]
        public int? ItemExtendedCostID;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ItemLogicalCostGroupID")]
        public int? ItemLogicalCostGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
