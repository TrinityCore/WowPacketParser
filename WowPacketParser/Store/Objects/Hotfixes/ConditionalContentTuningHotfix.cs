using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("conditional_content_tuning")]
    public sealed record ConditionalContentTuningHotfix1000 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("RedirectContentTuningID")]
        public int? RedirectContentTuningID;

        [DBFieldName("RedirectFlag")]
        public int? RedirectFlag;

        [DBFieldName("ParentContentTuningID")]
        public uint? ParentContentTuningID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("conditional_content_tuning")]
    public sealed record ConditionalContentTuningHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("RedirectContentTuningID")]
        public int? RedirectContentTuningID;

        [DBFieldName("RedirectFlag")]
        public int? RedirectFlag;

        [DBFieldName("ParentContentTuningID")]
        public int? ParentContentTuningID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
