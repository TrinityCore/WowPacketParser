using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_class_ui_display")]
    public sealed record ChrClassUiDisplayHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrClassesID")]
        public byte? ChrClassesID;

        [DBFieldName("AdvGuidePlayerConditionID")]
        public uint? AdvGuidePlayerConditionID;

        [DBFieldName("SplashPlayerConditionID")]
        public uint? SplashPlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_class_ui_display")]
    public sealed record ChrClassUiDisplayHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrClassesID")]
        public byte? ChrClassesID;

        [DBFieldName("AdvGuidePlayerConditionID")]
        public uint? AdvGuidePlayerConditionID;

        [DBFieldName("SplashPlayerConditionID")]
        public uint? SplashPlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
