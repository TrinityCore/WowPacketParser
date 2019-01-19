using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.VehicleSeat, HasIndexInData = false)]
    public class VehicleSeatEntry
    {
        [HotfixArray(3)]
        public float[] AttachmentOffset { get; set; }
        [HotfixArray(3)]
        public float[] CameraOffset { get; set; }
        public uint Flags { get; set; }
        public uint FlagsB { get; set; }
        public uint FlagsC { get; set; }
        public sbyte AttachmentID { get; set; }
        public float EnterPreDelay { get; set; }
        public float EnterSpeed { get; set; }
        public float EnterGravity { get; set; }
        public float EnterMinDuration { get; set; }
        public float EnterMaxDuration { get; set; }
        public float EnterMinArcHeight { get; set; }
        public float EnterMaxArcHeight { get; set; }
        public int EnterAnimStart { get; set; }
        public int EnterAnimLoop { get; set; }
        public int RideAnimStart { get; set; }
        public int RideAnimLoop { get; set; }
        public int RideUpperAnimStart { get; set; }
        public int RideUpperAnimLoop { get; set; }
        public float ExitPreDelay { get; set; }
        public float ExitSpeed { get; set; }
        public float ExitGravity { get; set; }
        public float ExitMinDuration { get; set; }
        public float ExitMaxDuration { get; set; }
        public float ExitMinArcHeight { get; set; }
        public float ExitMaxArcHeight { get; set; }
        public int ExitAnimStart { get; set; }
        public int ExitAnimLoop { get; set; }
        public int ExitAnimEnd { get; set; }
        public short VehicleEnterAnim { get; set; }
        public sbyte VehicleEnterAnimBone { get; set; }
        public short VehicleExitAnim { get; set; }
        public sbyte VehicleExitAnimBone { get; set; }
        public short VehicleRideAnimLoop { get; set; }
        public sbyte VehicleRideAnimLoopBone { get; set; }
        public sbyte PassengerAttachmentID { get; set; }
        public float PassengerYaw { get; set; }
        public float PassengerPitch { get; set; }
        public float PassengerRoll { get; set; }
        public float VehicleEnterAnimDelay { get; set; }
        public float VehicleExitAnimDelay { get; set; }
        public sbyte VehicleAbilityDisplay { get; set; }
        public uint EnterUISoundID { get; set; }
        public uint ExitUISoundID { get; set; }
        public int UiSkinFileDataID { get; set; }
        public float CameraEnteringDelay { get; set; }
        public float CameraEnteringDuration { get; set; }
        public float CameraExitingDelay { get; set; }
        public float CameraExitingDuration { get; set; }
        public float CameraPosChaseRate { get; set; }
        public float CameraFacingChaseRate { get; set; }
        public float CameraEnteringZoom { get; set; }
        public float CameraSeatZoomMin { get; set; }
        public float CameraSeatZoomMax { get; set; }
        public short EnterAnimKitID { get; set; }
        public short RideAnimKitID { get; set; }
        public short ExitAnimKitID { get; set; }
        public short VehicleEnterAnimKitID { get; set; }
        public short VehicleRideAnimKitID { get; set; }
        public short VehicleExitAnimKitID { get; set; }
        public short CameraModeID { get; set; }
    }
}
