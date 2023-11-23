using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_spec")]
    public sealed record ItemSpecHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MinLevel")]
        public byte? MinLevel;

        [DBFieldName("MaxLevel")]
        public byte? MaxLevel;

        [DBFieldName("ItemType")]
        public byte? ItemType;

        [DBFieldName("PrimaryStat")]
        public byte? PrimaryStat;

        [DBFieldName("SecondaryStat")]
        public byte? SecondaryStat;

        [DBFieldName("SpecializationID")]
        public ushort? SpecializationID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_spec")]
    public sealed record ItemSpecHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MinLevel")]
        public byte? MinLevel;

        [DBFieldName("MaxLevel")]
        public byte? MaxLevel;

        [DBFieldName("ItemType")]
        public byte? ItemType;

        [DBFieldName("PrimaryStat")]
        public byte? PrimaryStat;

        [DBFieldName("SecondaryStat")]
        public byte? SecondaryStat;

        [DBFieldName("SpecializationID")]
        public ushort? SpecializationID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
