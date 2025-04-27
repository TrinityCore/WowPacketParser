using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_target_position")]
    public sealed record SpellTargetPosition : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("EffectIndex", true)]
        public byte? EffectIndex;

        [DBFieldName("OrderIndex", TargetedDatabaseFlag.SinceTheWarWithin, true, true)]
        public int? OrderIndex;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("PositionX")]
        public float? PositionX;

        [DBFieldName("PositionY")]
        public float? PositionY;

        [DBFieldName("PositionZ")]
        public float? PositionZ;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public string EffectHelper;
    }

    [DBTableName("spell_target_position", TargetedDatabaseFlag.SinceTheWarWithin)]
    public sealed record SpellTargetPositionMulti : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("EffectIndex", true)]
        public byte? EffectIndex;

        [DBFieldName("OrderIndex", true, true)]
        public string OrderIndex;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("PositionX")]
        public float? PositionX;

        [DBFieldName("PositionY")]
        public float? PositionY;

        [DBFieldName("PositionZ")]
        public float? PositionZ;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public string EffectHelper;
    }
}
