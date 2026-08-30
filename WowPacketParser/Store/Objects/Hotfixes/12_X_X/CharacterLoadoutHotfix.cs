using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("character_loadout")]
    public sealed record CharacterLoadoutHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("ChrClassID")]
        public sbyte? ChrClassID;

        [DBFieldName("Purpose")]
        public int? Purpose;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("character_loadout")]
    public sealed record CharacterLoadoutHotfix1205: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("ChrClassID")]
        public sbyte? ChrClassID;

        [DBFieldName("Purpose")]
        public int? Purpose;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("RaceMask_1")]
        public int? RaceMask_1;

        [DBFieldName("RaceMask_2")]
        public int? RaceMask_2;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("character_loadout")]
    public sealed record CharacterLoadoutHotfix1207: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrClassID")]
        public sbyte? ChrClassID;

        [DBFieldName("Purpose")]
        public int? Purpose;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("RaceMask", 2)]
        public int?[] RaceMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
