using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spec_set_member")]
    public sealed record SpecSetMemberHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrSpecializationID")]
        public int? ChrSpecializationID;

        [DBFieldName("SpecSetID")]
        public uint? SpecSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spec_set_member")]
    public sealed record SpecSetMemberHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrSpecializationID")]
        public int? ChrSpecializationID;

        [DBFieldName("SpecSetID")]
        public int? SpecSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
