using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_offset_curve")]
    public sealed record ItemOffsetCurveHotfix1125: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CurveID")]
        public int? CurveID;

        [DBFieldName("Offset")]
        public int? Offset;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
