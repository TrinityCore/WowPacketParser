using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_bonus_list_level_delta")]
    public sealed record ItemBonusListLevelDeltaHotfix1000: IDataModel
    {
        [DBFieldName("ItemLevelDelta")]
        public short? ItemLevelDelta;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_bonus_list_level_delta")]
    public sealed record ItemBonusListLevelDeltaHotfix340: IDataModel
    {
        [DBFieldName("ItemLevelDelta")]
        public short? ItemLevelDelta;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
