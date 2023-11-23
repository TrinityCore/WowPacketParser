using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("vehicle_seat")]
    public sealed record VehicleSeatHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AttachmentOffsetX")]
        public float? AttachmentOffsetX;

        [DBFieldName("AttachmentOffsetY")]
        public float? AttachmentOffsetY;

        [DBFieldName("AttachmentOffsetZ")]
        public float? AttachmentOffsetZ;

        [DBFieldName("CameraOffsetX")]
        public float? CameraOffsetX;

        [DBFieldName("CameraOffsetY")]
        public float? CameraOffsetY;

        [DBFieldName("CameraOffsetZ")]
        public float? CameraOffsetZ;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FlagsB")]
        public int? FlagsB;

        [DBFieldName("FlagsC")]
        public int? FlagsC;

        [DBFieldName("AttachmentID")]
        public sbyte? AttachmentID;

        [DBFieldName("EnterPreDelay")]
        public float? EnterPreDelay;

        [DBFieldName("EnterSpeed")]
        public float? EnterSpeed;

        [DBFieldName("EnterGravity")]
        public float? EnterGravity;

        [DBFieldName("EnterMinDuration")]
        public float? EnterMinDuration;

        [DBFieldName("EnterMaxDuration")]
        public float? EnterMaxDuration;

        [DBFieldName("EnterMinArcHeight")]
        public float? EnterMinArcHeight;

        [DBFieldName("EnterMaxArcHeight")]
        public float? EnterMaxArcHeight;

        [DBFieldName("EnterAnimStart")]
        public int? EnterAnimStart;

        [DBFieldName("EnterAnimLoop")]
        public int? EnterAnimLoop;

        [DBFieldName("RideAnimStart")]
        public int? RideAnimStart;

        [DBFieldName("RideAnimLoop")]
        public int? RideAnimLoop;

        [DBFieldName("RideUpperAnimStart")]
        public int? RideUpperAnimStart;

        [DBFieldName("RideUpperAnimLoop")]
        public int? RideUpperAnimLoop;

        [DBFieldName("ExitPreDelay")]
        public float? ExitPreDelay;

        [DBFieldName("ExitSpeed")]
        public float? ExitSpeed;

        [DBFieldName("ExitGravity")]
        public float? ExitGravity;

        [DBFieldName("ExitMinDuration")]
        public float? ExitMinDuration;

        [DBFieldName("ExitMaxDuration")]
        public float? ExitMaxDuration;

        [DBFieldName("ExitMinArcHeight")]
        public float? ExitMinArcHeight;

        [DBFieldName("ExitMaxArcHeight")]
        public float? ExitMaxArcHeight;

        [DBFieldName("ExitAnimStart")]
        public int? ExitAnimStart;

        [DBFieldName("ExitAnimLoop")]
        public int? ExitAnimLoop;

        [DBFieldName("ExitAnimEnd")]
        public int? ExitAnimEnd;

        [DBFieldName("VehicleEnterAnim")]
        public short? VehicleEnterAnim;

        [DBFieldName("VehicleEnterAnimBone")]
        public sbyte? VehicleEnterAnimBone;

        [DBFieldName("VehicleExitAnim")]
        public short? VehicleExitAnim;

        [DBFieldName("VehicleExitAnimBone")]
        public sbyte? VehicleExitAnimBone;

        [DBFieldName("VehicleRideAnimLoop")]
        public short? VehicleRideAnimLoop;

        [DBFieldName("VehicleRideAnimLoopBone")]
        public sbyte? VehicleRideAnimLoopBone;

        [DBFieldName("PassengerAttachmentID")]
        public sbyte? PassengerAttachmentID;

        [DBFieldName("PassengerYaw")]
        public float? PassengerYaw;

        [DBFieldName("PassengerPitch")]
        public float? PassengerPitch;

        [DBFieldName("PassengerRoll")]
        public float? PassengerRoll;

        [DBFieldName("VehicleEnterAnimDelay")]
        public float? VehicleEnterAnimDelay;

        [DBFieldName("VehicleExitAnimDelay")]
        public float? VehicleExitAnimDelay;

        [DBFieldName("VehicleAbilityDisplay")]
        public sbyte? VehicleAbilityDisplay;

        [DBFieldName("EnterUISoundID")]
        public uint? EnterUISoundID;

        [DBFieldName("ExitUISoundID")]
        public uint? ExitUISoundID;

        [DBFieldName("UiSkinFileDataID")]
        public int? UiSkinFileDataID;

        [DBFieldName("CameraEnteringDelay")]
        public float? CameraEnteringDelay;

        [DBFieldName("CameraEnteringDuration")]
        public float? CameraEnteringDuration;

        [DBFieldName("CameraExitingDelay")]
        public float? CameraExitingDelay;

        [DBFieldName("CameraExitingDuration")]
        public float? CameraExitingDuration;

        [DBFieldName("CameraPosChaseRate")]
        public float? CameraPosChaseRate;

        [DBFieldName("CameraFacingChaseRate")]
        public float? CameraFacingChaseRate;

        [DBFieldName("CameraEnteringZoom")]
        public float? CameraEnteringZoom;

        [DBFieldName("CameraSeatZoomMin")]
        public float? CameraSeatZoomMin;

        [DBFieldName("CameraSeatZoomMax")]
        public float? CameraSeatZoomMax;

        [DBFieldName("EnterAnimKitID")]
        public short? EnterAnimKitID;

        [DBFieldName("RideAnimKitID")]
        public short? RideAnimKitID;

        [DBFieldName("ExitAnimKitID")]
        public short? ExitAnimKitID;

        [DBFieldName("VehicleEnterAnimKitID")]
        public short? VehicleEnterAnimKitID;

        [DBFieldName("VehicleRideAnimKitID")]
        public short? VehicleRideAnimKitID;

        [DBFieldName("VehicleExitAnimKitID")]
        public short? VehicleExitAnimKitID;

        [DBFieldName("CameraModeID")]
        public short? CameraModeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("vehicle_seat")]
    public sealed record VehicleSeatHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AttachmentOffsetX")]
        public float? AttachmentOffsetX;

        [DBFieldName("AttachmentOffsetY")]
        public float? AttachmentOffsetY;

        [DBFieldName("AttachmentOffsetZ")]
        public float? AttachmentOffsetZ;

        [DBFieldName("CameraOffsetX")]
        public float? CameraOffsetX;

        [DBFieldName("CameraOffsetY")]
        public float? CameraOffsetY;

        [DBFieldName("CameraOffsetZ")]
        public float? CameraOffsetZ;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FlagsB")]
        public int? FlagsB;

        [DBFieldName("FlagsC")]
        public int? FlagsC;

        [DBFieldName("AttachmentID")]
        public sbyte? AttachmentID;

        [DBFieldName("EnterPreDelay")]
        public float? EnterPreDelay;

        [DBFieldName("EnterSpeed")]
        public float? EnterSpeed;

        [DBFieldName("EnterGravity")]
        public float? EnterGravity;

        [DBFieldName("EnterMinDuration")]
        public float? EnterMinDuration;

        [DBFieldName("EnterMaxDuration")]
        public float? EnterMaxDuration;

        [DBFieldName("EnterMinArcHeight")]
        public float? EnterMinArcHeight;

        [DBFieldName("EnterMaxArcHeight")]
        public float? EnterMaxArcHeight;

        [DBFieldName("EnterAnimStart")]
        public int? EnterAnimStart;

        [DBFieldName("EnterAnimLoop")]
        public int? EnterAnimLoop;

        [DBFieldName("RideAnimStart")]
        public int? RideAnimStart;

        [DBFieldName("RideAnimLoop")]
        public int? RideAnimLoop;

        [DBFieldName("RideUpperAnimStart")]
        public int? RideUpperAnimStart;

        [DBFieldName("RideUpperAnimLoop")]
        public int? RideUpperAnimLoop;

        [DBFieldName("ExitPreDelay")]
        public float? ExitPreDelay;

        [DBFieldName("ExitSpeed")]
        public float? ExitSpeed;

        [DBFieldName("ExitGravity")]
        public float? ExitGravity;

        [DBFieldName("ExitMinDuration")]
        public float? ExitMinDuration;

        [DBFieldName("ExitMaxDuration")]
        public float? ExitMaxDuration;

        [DBFieldName("ExitMinArcHeight")]
        public float? ExitMinArcHeight;

        [DBFieldName("ExitMaxArcHeight")]
        public float? ExitMaxArcHeight;

        [DBFieldName("ExitAnimStart")]
        public int? ExitAnimStart;

        [DBFieldName("ExitAnimLoop")]
        public int? ExitAnimLoop;

        [DBFieldName("ExitAnimEnd")]
        public int? ExitAnimEnd;

        [DBFieldName("VehicleEnterAnim")]
        public short? VehicleEnterAnim;

        [DBFieldName("VehicleEnterAnimBone")]
        public sbyte? VehicleEnterAnimBone;

        [DBFieldName("VehicleExitAnim")]
        public short? VehicleExitAnim;

        [DBFieldName("VehicleExitAnimBone")]
        public sbyte? VehicleExitAnimBone;

        [DBFieldName("VehicleRideAnimLoop")]
        public short? VehicleRideAnimLoop;

        [DBFieldName("VehicleRideAnimLoopBone")]
        public sbyte? VehicleRideAnimLoopBone;

        [DBFieldName("PassengerAttachmentID")]
        public sbyte? PassengerAttachmentID;

        [DBFieldName("PassengerYaw")]
        public float? PassengerYaw;

        [DBFieldName("PassengerPitch")]
        public float? PassengerPitch;

        [DBFieldName("PassengerRoll")]
        public float? PassengerRoll;

        [DBFieldName("VehicleEnterAnimDelay")]
        public float? VehicleEnterAnimDelay;

        [DBFieldName("VehicleExitAnimDelay")]
        public float? VehicleExitAnimDelay;

        [DBFieldName("VehicleAbilityDisplay")]
        public sbyte? VehicleAbilityDisplay;

        [DBFieldName("EnterUISoundID")]
        public uint? EnterUISoundID;

        [DBFieldName("ExitUISoundID")]
        public uint? ExitUISoundID;

        [DBFieldName("UiSkinFileDataID")]
        public int? UiSkinFileDataID;

        [DBFieldName("UiSkin")]
        public int? UiSkin;

        [DBFieldName("CameraEnteringDelay")]
        public float? CameraEnteringDelay;

        [DBFieldName("CameraEnteringDuration")]
        public float? CameraEnteringDuration;

        [DBFieldName("CameraExitingDelay")]
        public float? CameraExitingDelay;

        [DBFieldName("CameraExitingDuration")]
        public float? CameraExitingDuration;

        [DBFieldName("CameraPosChaseRate")]
        public float? CameraPosChaseRate;

        [DBFieldName("CameraFacingChaseRate")]
        public float? CameraFacingChaseRate;

        [DBFieldName("CameraEnteringZoom")]
        public float? CameraEnteringZoom;

        [DBFieldName("CameraSeatZoomMin")]
        public float? CameraSeatZoomMin;

        [DBFieldName("CameraSeatZoomMax")]
        public float? CameraSeatZoomMax;

        [DBFieldName("EnterAnimKitID")]
        public short? EnterAnimKitID;

        [DBFieldName("RideAnimKitID")]
        public short? RideAnimKitID;

        [DBFieldName("ExitAnimKitID")]
        public short? ExitAnimKitID;

        [DBFieldName("VehicleEnterAnimKitID")]
        public short? VehicleEnterAnimKitID;

        [DBFieldName("VehicleRideAnimKitID")]
        public short? VehicleRideAnimKitID;

        [DBFieldName("VehicleExitAnimKitID")]
        public short? VehicleExitAnimKitID;

        [DBFieldName("CameraModeID")]
        public short? CameraModeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("vehicle_seat")]
    public sealed record VehicleSeatHotfix342: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AttachmentOffsetX")]
        public float? AttachmentOffsetX;

        [DBFieldName("AttachmentOffsetY")]
        public float? AttachmentOffsetY;

        [DBFieldName("AttachmentOffsetZ")]
        public float? AttachmentOffsetZ;

        [DBFieldName("CameraOffsetX")]
        public float? CameraOffsetX;

        [DBFieldName("CameraOffsetY")]
        public float? CameraOffsetY;

        [DBFieldName("CameraOffsetZ")]
        public float? CameraOffsetZ;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FlagsB")]
        public int? FlagsB;

        [DBFieldName("FlagsC")]
        public int? FlagsC;

        [DBFieldName("AttachmentID")]
        public sbyte? AttachmentID;

        [DBFieldName("EnterPreDelay")]
        public float? EnterPreDelay;

        [DBFieldName("EnterSpeed")]
        public float? EnterSpeed;

        [DBFieldName("EnterGravity")]
        public float? EnterGravity;

        [DBFieldName("EnterMinDuration")]
        public float? EnterMinDuration;

        [DBFieldName("EnterMaxDuration")]
        public float? EnterMaxDuration;

        [DBFieldName("EnterMinArcHeight")]
        public float? EnterMinArcHeight;

        [DBFieldName("EnterMaxArcHeight")]
        public float? EnterMaxArcHeight;

        [DBFieldName("EnterAnimStart")]
        public int? EnterAnimStart;

        [DBFieldName("EnterAnimLoop")]
        public int? EnterAnimLoop;

        [DBFieldName("RideAnimStart")]
        public int? RideAnimStart;

        [DBFieldName("RideAnimLoop")]
        public int? RideAnimLoop;

        [DBFieldName("RideUpperAnimStart")]
        public int? RideUpperAnimStart;

        [DBFieldName("RideUpperAnimLoop")]
        public int? RideUpperAnimLoop;

        [DBFieldName("ExitPreDelay")]
        public float? ExitPreDelay;

        [DBFieldName("ExitSpeed")]
        public float? ExitSpeed;

        [DBFieldName("ExitGravity")]
        public float? ExitGravity;

        [DBFieldName("ExitMinDuration")]
        public float? ExitMinDuration;

        [DBFieldName("ExitMaxDuration")]
        public float? ExitMaxDuration;

        [DBFieldName("ExitMinArcHeight")]
        public float? ExitMinArcHeight;

        [DBFieldName("ExitMaxArcHeight")]
        public float? ExitMaxArcHeight;

        [DBFieldName("ExitAnimStart")]
        public int? ExitAnimStart;

        [DBFieldName("ExitAnimLoop")]
        public int? ExitAnimLoop;

        [DBFieldName("ExitAnimEnd")]
        public int? ExitAnimEnd;

        [DBFieldName("VehicleEnterAnim")]
        public short? VehicleEnterAnim;

        [DBFieldName("VehicleEnterAnimBone")]
        public sbyte? VehicleEnterAnimBone;

        [DBFieldName("VehicleExitAnim")]
        public short? VehicleExitAnim;

        [DBFieldName("VehicleExitAnimBone")]
        public sbyte? VehicleExitAnimBone;

        [DBFieldName("VehicleRideAnimLoop")]
        public short? VehicleRideAnimLoop;

        [DBFieldName("VehicleRideAnimLoopBone")]
        public sbyte? VehicleRideAnimLoopBone;

        [DBFieldName("PassengerAttachmentID")]
        public sbyte? PassengerAttachmentID;

        [DBFieldName("PassengerYaw")]
        public float? PassengerYaw;

        [DBFieldName("PassengerPitch")]
        public float? PassengerPitch;

        [DBFieldName("PassengerRoll")]
        public float? PassengerRoll;

        [DBFieldName("VehicleEnterAnimDelay")]
        public float? VehicleEnterAnimDelay;

        [DBFieldName("VehicleExitAnimDelay")]
        public float? VehicleExitAnimDelay;

        [DBFieldName("VehicleAbilityDisplay")]
        public sbyte? VehicleAbilityDisplay;

        [DBFieldName("EnterUISoundID")]
        public uint? EnterUISoundID;

        [DBFieldName("ExitUISoundID")]
        public uint? ExitUISoundID;

        [DBFieldName("UiSkinFileDataID")]
        public int? UiSkinFileDataID;

        [DBFieldName("UiSkin")]
        public int? UiSkin;

        [DBFieldName("CameraEnteringDelay")]
        public float? CameraEnteringDelay;

        [DBFieldName("CameraEnteringDuration")]
        public float? CameraEnteringDuration;

        [DBFieldName("CameraExitingDelay")]
        public float? CameraExitingDelay;

        [DBFieldName("CameraExitingDuration")]
        public float? CameraExitingDuration;

        [DBFieldName("CameraPosChaseRate")]
        public float? CameraPosChaseRate;

        [DBFieldName("CameraFacingChaseRate")]
        public float? CameraFacingChaseRate;

        [DBFieldName("CameraEnteringZoom")]
        public float? CameraEnteringZoom;

        [DBFieldName("CameraSeatZoomMin")]
        public float? CameraSeatZoomMin;

        [DBFieldName("CameraSeatZoomMax")]
        public float? CameraSeatZoomMax;

        [DBFieldName("EnterAnimKitID")]
        public short? EnterAnimKitID;

        [DBFieldName("RideAnimKitID")]
        public short? RideAnimKitID;

        [DBFieldName("ExitAnimKitID")]
        public short? ExitAnimKitID;

        [DBFieldName("VehicleEnterAnimKitID")]
        public short? VehicleEnterAnimKitID;

        [DBFieldName("VehicleRideAnimKitID")]
        public short? VehicleRideAnimKitID;

        [DBFieldName("VehicleExitAnimKitID")]
        public short? VehicleExitAnimKitID;

        [DBFieldName("CameraModeID")]
        public short? CameraModeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
