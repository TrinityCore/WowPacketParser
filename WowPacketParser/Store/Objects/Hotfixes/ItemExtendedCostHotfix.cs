using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_extended_cost")]
    public sealed record ItemExtendedCostHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RequiredArenaRating")]
        public ushort? RequiredArenaRating;

        [DBFieldName("ArenaBracket")]
        public sbyte? ArenaBracket;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("MinFactionID")]
        public byte? MinFactionID;

        [DBFieldName("MinReputation")]
        public int? MinReputation;

        [DBFieldName("RequiredAchievement")]
        public byte? RequiredAchievement;

        [DBFieldName("ItemID", 5)]
        public int?[] ItemID;

        [DBFieldName("ItemCount", 5)]
        public ushort?[] ItemCount;

        [DBFieldName("CurrencyID", 5)]
        public ushort?[] CurrencyID;

        [DBFieldName("CurrencyCount", 5)]
        public uint?[] CurrencyCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_extended_cost")]
    public sealed record ItemExtendedCostHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RequiredArenaRating")]
        public ushort? RequiredArenaRating;

        [DBFieldName("ArenaBracket")]
        public sbyte? ArenaBracket;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("MinFactionID")]
        public byte? MinFactionID;

        [DBFieldName("MinReputation")]
        public byte? MinReputation;

        [DBFieldName("RequiredAchievement")]
        public byte? RequiredAchievement;

        [DBFieldName("ItemID", 5)]
        public int?[] ItemID;

        [DBFieldName("ItemCount", 5)]
        public ushort?[] ItemCount;

        [DBFieldName("CurrencyID", 5)]
        public ushort?[] CurrencyID;

        [DBFieldName("CurrencyCount", 5)]
        public uint?[] CurrencyCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("item_extended_cost")]
    public sealed record ItemExtendedCostHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RequiredArenaRating")]
        public ushort? RequiredArenaRating;

        [DBFieldName("ArenaBracket")]
        public sbyte? ArenaBracket;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("MinFactionID")]
        public byte? MinFactionID;

        [DBFieldName("MinReputation")]
        public int? MinReputation;

        [DBFieldName("RequiredAchievement")]
        public byte? RequiredAchievement;

        [DBFieldName("ItemID", 5)]
        public int?[] ItemID;

        [DBFieldName("ItemCount", 5)]
        public ushort?[] ItemCount;

        [DBFieldName("CurrencyID", 5)]
        public ushort?[] CurrencyID;

        [DBFieldName("CurrencyCount", 5)]
        public uint?[] CurrencyCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
