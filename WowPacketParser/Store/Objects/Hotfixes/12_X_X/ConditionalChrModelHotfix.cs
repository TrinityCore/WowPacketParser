using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("conditional_chr_model")]
    public sealed record ConditionalChrModelHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrModelID")]
        public uint? ChrModelID;

        [DBFieldName("ChrCustomizationReqID")]
        public int? ChrCustomizationReqID;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ChrCustomizationCategoryID")]
        public int? ChrCustomizationCategoryID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
