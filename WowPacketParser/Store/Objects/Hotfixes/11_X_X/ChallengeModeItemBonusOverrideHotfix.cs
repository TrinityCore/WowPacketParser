using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("challenge_mode_item_bonus_override")]
    public sealed record ChallengeModeItemBonusOverrideHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemBonusTreeGroupID")]
        public int? ItemBonusTreeGroupID;

        [DBFieldName("DstItemBonusTreeID")]
        public int? DstItemBonusTreeID;

        [DBFieldName("Value")]
        public int? Value;

        [DBFieldName("RequiredTimeEventPassed")]
        public int? RequiredTimeEventPassed;

        [DBFieldName("SrcItemBonusTreeID")]
        public uint? SrcItemBonusTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("challenge_mode_item_bonus_override")]
    public sealed record ChallengeModeItemBonusOverrideHotfix1110 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemBonusTreeGroupID")]
        public int? ItemBonusTreeGroupID;

        [DBFieldName("DstItemBonusTreeID")]
        public int? DstItemBonusTreeID;

        [DBFieldName("Value")]
        public int? Value;

        [DBFieldName("RequiredTimeEventPassed")]
        public int? RequiredTimeEventPassed;

        [DBFieldName("RequiredTimeEventNotPassed")]
        public int? RequiredTimeEventNotPassed;

        [DBFieldName("SrcItemBonusTreeID")]
        public uint? SrcItemBonusTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
