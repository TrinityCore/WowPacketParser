using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_reforge")]
    public sealed record ItemReforgeHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SourceStat")]
        public ushort? SourceStat;

        [DBFieldName("SourceMultiplier")]
        public float? SourceMultiplier;

        [DBFieldName("TargetStat")]
        public ushort? TargetStat;

        [DBFieldName("TargetMultiplier")]
        public float? TargetMultiplier;

        [DBFieldName("LegacyItemReforgeID")]
        public ushort? LegacyItemReforgeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
