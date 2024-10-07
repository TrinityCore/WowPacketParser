using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("area_trigger")]
    public sealed record AreaTriggerHotfix1100 : IDataModel
    {
        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("PhaseUseFlags")]
        public int? PhaseUseFlags;

        [DBFieldName("PhaseID")]
        public short? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public short? PhaseGroupID;

        [DBFieldName("Radius")]
        public float? Radius;

        [DBFieldName("BoxLength")]
        public float? BoxLength;

        [DBFieldName("BoxWidth")]
        public float? BoxWidth;

        [DBFieldName("BoxHeight")]
        public float? BoxHeight;

        [DBFieldName("BoxYaw")]
        public float? BoxYaw;

        [DBFieldName("ShapeType")]
        public sbyte? ShapeType;

        [DBFieldName("ShapeID")]
        public short? ShapeID;

        [DBFieldName("AreaTriggerActionSetID")]
        public int? AreaTriggerActionSetID;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
