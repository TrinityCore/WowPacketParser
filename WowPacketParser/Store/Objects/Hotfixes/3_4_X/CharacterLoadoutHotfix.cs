using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("character_loadout")]
    public sealed record CharacterLoadoutHotfix340: IDataModel
    {
        [DBFieldName("Racemask")]
        public long? Racemask;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrClassID")]
        public sbyte? ChrClassID;

        [DBFieldName("Purpose")]
        public int? Purpose;

        [DBFieldName("ItemContext")]
        public sbyte? ItemContext;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("character_loadout")]
    public sealed record CharacterLoadoutHotfix344: IDataModel
    {
        [DBFieldName("Racemask")]
        public long? Racemask;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrClassID")]
        public sbyte? ChrClassID;

        [DBFieldName("Purpose")]
        public int? Purpose;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
