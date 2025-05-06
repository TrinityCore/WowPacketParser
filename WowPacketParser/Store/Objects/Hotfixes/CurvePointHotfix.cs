using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("curve_point")]
    public sealed record CurvePointHotfix1000: IDataModel
    {
        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PreSLSquishPosX")]
        public float? PreSLSquishPosX;

        [DBFieldName("PreSLSquishPosY")]
        public float? PreSLSquishPosY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CurveID")]
        public ushort? CurveID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("curve_point")]
    public sealed record CurvePointHotfix1010 : IDataModel
    {
        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PreSLSquishPosX")]
        public float? PreSLSquishPosX;

        [DBFieldName("PreSLSquishPosY")]
        public float? PreSLSquishPosY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CurveID")]
        public int? CurveID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("curve_point")]
    public sealed record CurvePointHotfix1020 : IDataModel
    {
        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PreSLSquishPosX")]
        public float? PreSLSquishPosX;

        [DBFieldName("PreSLSquishPosY")]
        public float? PreSLSquishPosY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CurveID")]
        public uint? CurveID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
