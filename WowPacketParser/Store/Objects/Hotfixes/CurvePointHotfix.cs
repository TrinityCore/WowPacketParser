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

    [Hotfix]
    [DBTableName("curve_point")]
    public sealed record CurvePointHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("CurveID")]
        public ushort? CurveID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("curve_point")]
    public sealed record CurvePointHotfix341: IDataModel
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
    public sealed record CurvePointHotfix342: IDataModel
    {
        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosPreSquishX")]
        public float? PosPreSquishX;

        [DBFieldName("PosPreSquishY")]
        public float? PosPreSquishY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CurveID")]
        public int? CurveID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
