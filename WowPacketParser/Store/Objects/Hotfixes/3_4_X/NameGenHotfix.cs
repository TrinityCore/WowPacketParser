using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("name_gen")]
    public sealed record NameGenHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("RaceID")]
        public byte? RaceID;

        [DBFieldName("Sex")]
        public byte? Sex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("name_gen")]
    public sealed record NameGenHotfix344: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("RaceID")]
        public sbyte? RaceID;

        [DBFieldName("Sex")]
        public sbyte? Sex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
