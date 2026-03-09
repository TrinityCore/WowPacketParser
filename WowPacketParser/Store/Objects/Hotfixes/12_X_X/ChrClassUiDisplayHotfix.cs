using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_class_ui_display")]
    public sealed record ChrClassUiDisplayHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrClassesID")]
        public sbyte? ChrClassesID;

        [DBFieldName("AdvGuidePlayerConditionID")]
        public uint? AdvGuidePlayerConditionID;

        [DBFieldName("SplashPlayerConditionID")]
        public uint? SplashPlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
