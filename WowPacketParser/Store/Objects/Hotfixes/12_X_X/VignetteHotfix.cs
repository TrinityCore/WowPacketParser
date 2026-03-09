using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("vignette")]
    public sealed record VignetteHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("VisibleTrackingQuestID")]
        public uint? VisibleTrackingQuestID;

        [DBFieldName("QuestFeedbackEffectID")]
        public uint? QuestFeedbackEffectID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("MaxHeight")]
        public float? MaxHeight;

        [DBFieldName("MinHeight")]
        public float? MinHeight;

        [DBFieldName("VignetteType")]
        public sbyte? VignetteType;

        [DBFieldName("RewardQuestID")]
        public int? RewardQuestID;

        [DBFieldName("UiWidgetSetID")]
        public int? UiWidgetSetID;

        [DBFieldName("UiMapPinInfoID")]
        public int? UiMapPinInfoID;

        [DBFieldName("ObjectiveType")]
        public sbyte? ObjectiveType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("vignette_locale")]
    public sealed record VignetteLocaleHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
