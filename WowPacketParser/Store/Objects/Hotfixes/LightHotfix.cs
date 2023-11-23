using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("light")]
    public sealed record LightHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GameCoordsX")]
        public float? GameCoordsX;

        [DBFieldName("GameCoordsY")]
        public float? GameCoordsY;

        [DBFieldName("GameCoordsZ")]
        public float? GameCoordsZ;

        [DBFieldName("GameFalloffStart")]
        public float? GameFalloffStart;

        [DBFieldName("GameFalloffEnd")]
        public float? GameFalloffEnd;

        [DBFieldName("ContinentID")]
        public short? ContinentID;

        [DBFieldName("LightParamsID", 8)]
        public ushort?[] LightParamsID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("light")]
    public sealed record LightHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GameCoordsX")]
        public float? GameCoordsX;

        [DBFieldName("GameCoordsY")]
        public float? GameCoordsY;

        [DBFieldName("GameCoordsZ")]
        public float? GameCoordsZ;

        [DBFieldName("GameFalloffStart")]
        public float? GameFalloffStart;

        [DBFieldName("GameFalloffEnd")]
        public float? GameFalloffEnd;

        [DBFieldName("ContinentID")]
        public short? ContinentID;

        [DBFieldName("LightParamsID", 8)]
        public ushort?[] LightParamsID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
