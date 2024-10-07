using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("ui_splash_screen")]
    public sealed record UiSplashScreenHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Header")]
        public string Header;

        [DBFieldName("TopLeftFeatureTitle")]
        public string TopLeftFeatureTitle;

        [DBFieldName("TopLeftFeatureDesc")]
        public string TopLeftFeatureDesc;

        [DBFieldName("BottomLeftFeatureTitle")]
        public string BottomLeftFeatureTitle;

        [DBFieldName("BottomLeftFeatureDesc")]
        public string BottomLeftFeatureDesc;

        [DBFieldName("RightFeatureTitle")]
        public string RightFeatureTitle;

        [DBFieldName("RightFeatureDesc")]
        public string RightFeatureDesc;

        [DBFieldName("AllianceQuestID")]
        public int? AllianceQuestID;

        [DBFieldName("HordeQuestID")]
        public int? HordeQuestID;

        [DBFieldName("ScreenType")]
        public byte? ScreenType;

        [DBFieldName("TextureKitID")]
        public int? TextureKitID;

        [DBFieldName("SoundKitID")]
        public int? SoundKitID;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("CharLevelConditionID")]
        public int? CharLevelConditionID;

        [DBFieldName("RequiredTimeEventPassed")]
        public int? RequiredTimeEventPassed;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_splash_screen_locale")]
    public sealed record UiSplashScreenLocaleHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Header_lang")]
        public string HeaderLang;

        [DBFieldName("TopLeftFeatureTitle_lang")]
        public string TopLeftFeatureTitleLang;

        [DBFieldName("TopLeftFeatureDesc_lang")]
        public string TopLeftFeatureDescLang;

        [DBFieldName("BottomLeftFeatureTitle_lang")]
        public string BottomLeftFeatureTitleLang;

        [DBFieldName("BottomLeftFeatureDesc_lang")]
        public string BottomLeftFeatureDescLang;

        [DBFieldName("RightFeatureTitle_lang")]
        public string RightFeatureTitleLang;

        [DBFieldName("RightFeatureDesc_lang")]
        public string RightFeatureDescLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
