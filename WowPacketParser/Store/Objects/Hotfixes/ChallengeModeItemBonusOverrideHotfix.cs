using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("challenge_mode_item_bonus_override")]
    public sealed record ChallengeModeItemBonusOverrideHotfix1007 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemBonusTreeGroupID")]
        public int? ItemBonusTreeGroupID;

        [DBFieldName("DstItemBonusTreeID")]
        public int? DstItemBonusTreeID;

        [DBFieldName("Type")]
        public sbyte? Type;

        [DBFieldName("Value")]
        public int? Value;

        [DBFieldName("MythicPlusSeasonID")]
        public int? MythicPlusSeasonID;

        [DBFieldName("PvPSeasonID")]
        public int? PvPSeasonID;

        [DBFieldName("SrcItemBonusTreeID")]
        public uint? SrcItemBonusTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
