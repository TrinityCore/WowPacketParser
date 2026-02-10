using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("flight_capability")]
    public sealed record FlightCapabilityHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AirFriction")]
        public float? AirFriction;

        [DBFieldName("MaxVel")]
        public float? MaxVel;

        [DBFieldName("Unknown1000_2")]
        public float? Unknown1000_2;

        [DBFieldName("DoubleJumpVelMod")]
        public float? DoubleJumpVelMod;

        [DBFieldName("LiftCoefficient")]
        public float? LiftCoefficient;

        [DBFieldName("GlideStartMinHeight")]
        public float? GlideStartMinHeight;

        [DBFieldName("AddImpulseMaxSpeed")]
        public float? AddImpulseMaxSpeed;

        [DBFieldName("BankingRateMin")]
        public float? BankingRateMin;

        [DBFieldName("BankingRateMax")]
        public float? BankingRateMax;

        [DBFieldName("PitchingRateDownMin")]
        public float? PitchingRateDownMin;

        [DBFieldName("PitchingRateDownMax")]
        public float? PitchingRateDownMax;

        [DBFieldName("PitchingRateUpMin")]
        public float? PitchingRateUpMin;

        [DBFieldName("PitchingRateUpMax")]
        public float? PitchingRateUpMax;

        [DBFieldName("TurnVelocityThresholdMin")]
        public float? TurnVelocityThresholdMin;

        [DBFieldName("TurnVelocityThresholdMax")]
        public float? TurnVelocityThresholdMax;

        [DBFieldName("SurfaceFriction")]
        public float? SurfaceFriction;

        [DBFieldName("OverMaxDeceleration")]
        public float? OverMaxDeceleration;

        [DBFieldName("Unknown1000_17")]
        public float? Unknown1000_17;

        [DBFieldName("Unknown1000_18")]
        public float? Unknown1000_18;

        [DBFieldName("Unknown1000_19")]
        public float? Unknown1000_19;

        [DBFieldName("Unknown1000_20")]
        public float? Unknown1000_20;

        [DBFieldName("Unknown1000_21")]
        public float? Unknown1000_21;

        [DBFieldName("LaunchSpeedCoefficient")]
        public float? LaunchSpeedCoefficient;

        [DBFieldName("VigorRegenMaxVelCoefficient")]
        public float? VigorRegenMaxVelCoefficient;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
