using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("vehicle")]
    public sealed record VehicleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FlagsB")]
        public byte? FlagsB;

        [DBFieldName("TurnSpeed")]
        public float? TurnSpeed;

        [DBFieldName("PitchSpeed")]
        public float? PitchSpeed;

        [DBFieldName("PitchMin")]
        public float? PitchMin;

        [DBFieldName("PitchMax")]
        public float? PitchMax;

        [DBFieldName("MouseLookOffsetPitch")]
        public float? MouseLookOffsetPitch;

        [DBFieldName("CameraFadeDistScalarMin")]
        public float? CameraFadeDistScalarMin;

        [DBFieldName("CameraFadeDistScalarMax")]
        public float? CameraFadeDistScalarMax;

        [DBFieldName("CameraPitchOffset")]
        public float? CameraPitchOffset;

        [DBFieldName("FacingLimitRight")]
        public float? FacingLimitRight;

        [DBFieldName("FacingLimitLeft")]
        public float? FacingLimitLeft;

        [DBFieldName("CameraYawOffset")]
        public float? CameraYawOffset;

        [DBFieldName("VehicleUIIndicatorID")]
        public ushort? VehicleUIIndicatorID;

        [DBFieldName("MissileTargetingID")]
        public int? MissileTargetingID;

        [DBFieldName("VehiclePOITypeID")]
        public ushort? VehiclePOITypeID;

        [DBFieldName("SeatID", 8)]
        public ushort?[] SeatID;

        [DBFieldName("PowerDisplayID", 3)]
        public ushort?[] PowerDisplayID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("vehicle")]
    public sealed record VehicleHotfix1010 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FlagsB")]
        public int? FlagsB;

        [DBFieldName("TurnSpeed")]
        public float? TurnSpeed;

        [DBFieldName("PitchSpeed")]
        public float? PitchSpeed;

        [DBFieldName("PitchMin")]
        public float? PitchMin;

        [DBFieldName("PitchMax")]
        public float? PitchMax;

        [DBFieldName("MouseLookOffsetPitch")]
        public float? MouseLookOffsetPitch;

        [DBFieldName("CameraFadeDistScalarMin")]
        public float? CameraFadeDistScalarMin;

        [DBFieldName("CameraFadeDistScalarMax")]
        public float? CameraFadeDistScalarMax;

        [DBFieldName("CameraPitchOffset")]
        public float? CameraPitchOffset;

        [DBFieldName("FacingLimitRight")]
        public float? FacingLimitRight;

        [DBFieldName("FacingLimitLeft")]
        public float? FacingLimitLeft;

        [DBFieldName("CameraYawOffset")]
        public float? CameraYawOffset;

        [DBFieldName("VehicleUIIndicatorID")]
        public ushort? VehicleUIIndicatorID;

        [DBFieldName("MissileTargetingID")]
        public int? MissileTargetingID;

        [DBFieldName("VehiclePOITypeID")]
        public ushort? VehiclePOITypeID;

        [DBFieldName("SeatID", 8)]
        public ushort?[] SeatID;

        [DBFieldName("PowerDisplayID", 3)]
        public ushort?[] PowerDisplayID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("vehicle")]
    public sealed record VehicleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FlagsB")]
        public byte? FlagsB;

        [DBFieldName("TurnSpeed")]
        public float? TurnSpeed;

        [DBFieldName("PitchSpeed")]
        public float? PitchSpeed;

        [DBFieldName("PitchMin")]
        public float? PitchMin;

        [DBFieldName("PitchMax")]
        public float? PitchMax;

        [DBFieldName("MouseLookOffsetPitch")]
        public float? MouseLookOffsetPitch;

        [DBFieldName("CameraFadeDistScalarMin")]
        public float? CameraFadeDistScalarMin;

        [DBFieldName("CameraFadeDistScalarMax")]
        public float? CameraFadeDistScalarMax;

        [DBFieldName("CameraPitchOffset")]
        public float? CameraPitchOffset;

        [DBFieldName("FacingLimitRight")]
        public float? FacingLimitRight;

        [DBFieldName("FacingLimitLeft")]
        public float? FacingLimitLeft;

        [DBFieldName("CameraYawOffset")]
        public float? CameraYawOffset;

        [DBFieldName("VehicleUIIndicatorID")]
        public ushort? VehicleUIIndicatorID;

        [DBFieldName("MissileTargetingID")]
        public int? MissileTargetingID;

        [DBFieldName("UiLocomotionType")]
        public byte? UiLocomotionType;

        [DBFieldName("SeatID", 8)]
        public ushort?[] SeatID;

        [DBFieldName("PowerDisplayID", 3)]
        public ushort?[] PowerDisplayID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("vehicle")]
    public sealed record VehicleHotfix342: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FlagsB")]
        public int? FlagsB;

        [DBFieldName("TurnSpeed")]
        public float? TurnSpeed;

        [DBFieldName("PitchSpeed")]
        public float? PitchSpeed;

        [DBFieldName("PitchMin")]
        public float? PitchMin;

        [DBFieldName("PitchMax")]
        public float? PitchMax;

        [DBFieldName("MouseLookOffsetPitch")]
        public float? MouseLookOffsetPitch;

        [DBFieldName("CameraFadeDistScalarMin")]
        public float? CameraFadeDistScalarMin;

        [DBFieldName("CameraFadeDistScalarMax")]
        public float? CameraFadeDistScalarMax;

        [DBFieldName("CameraPitchOffset")]
        public float? CameraPitchOffset;

        [DBFieldName("FacingLimitRight")]
        public float? FacingLimitRight;

        [DBFieldName("FacingLimitLeft")]
        public float? FacingLimitLeft;

        [DBFieldName("CameraYawOffset")]
        public float? CameraYawOffset;

        [DBFieldName("VehicleUIIndicatorID")]
        public ushort? VehicleUIIndicatorID;

        [DBFieldName("MissileTargetingID")]
        public int? MissileTargetingID;

        [DBFieldName("VehiclePOITypeID")]
        public ushort? VehiclePOITypeID;

        [DBFieldName("UiLocomotionType")]
        public int? UiLocomotionType;

        [DBFieldName("SeatID", 8)]
        public ushort?[] SeatID;

        [DBFieldName("PowerDisplayID", 3)]
        public ushort?[] PowerDisplayID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
