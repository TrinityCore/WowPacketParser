using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("content_tuning_x_label")]
    public sealed record ContentTuningXLabelHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("LabelID")]
        public int? LabelID;

        [DBFieldName("ContentTuningID")]
        public uint? ContentTuningID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
