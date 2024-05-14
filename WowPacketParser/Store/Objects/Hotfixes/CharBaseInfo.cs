using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("char_base_info")]
    public sealed record CharBaseInfoHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceID")]
        public sbyte? RaceID;

        [DBFieldName("ClassID")]
        public sbyte? ClassID;

        [DBFieldName("OtherFactionRaceID")]
        public int? OtherFactionRaceID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
