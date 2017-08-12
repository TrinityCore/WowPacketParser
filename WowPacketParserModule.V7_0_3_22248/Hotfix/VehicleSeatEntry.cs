using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.VehicleSeat, HasIndexInData = false)]
    public class VehicleSeatEntry
    {
        [HotfixArray(3)]
        public uint[] Flags { get; set; }
        public float AttachmentOffsetX { get; set; }
        public float AttachmentOffsetY { get; set; }
        public float AttachmentOffsetZ { get; set; }
        public float EnterPreDelay { get; set; }
        public float EnterSpeed { get; set; }
        public float EnterGravity { get; set; }
        public float EnterMinDuration { get; set; }
        public float EnterMaxDuration { get; set; }
        public float EnterMinArcHeight { get; set; }
        public float EnterMaxArcHeight { get; set; }
        public float ExitPreDelay { get; set; }
        public float ExitSpeed { get; set; }
        public float ExitGravity { get; set; }
        public float ExitMinDuration { get; set; }
        public float ExitMaxDuration { get; set; }
        public float ExitMinArcHeight { get; set; }
        public float ExitMaxArcHeight { get; set; }
        public float PassengerYaw { get; set; }
        public float PassengerPitch { get; set; }
        public float PassengerRoll { get; set; }
        public float VehicleEnterAnimDelay { get; set; }
        public float VehicleExitAnimDelay { get; set; }
        public float CameraEnteringDelay { get; set; }
        public float CameraEnteringDuration { get; set; }
        public float CameraExitingDelay { get; set; }
        public float CameraExitingDuration { get; set; }
        public float CameraOffsetX { get; set; }
        public float CameraOffsetY { get; set; }
        public float CameraOffsetZ { get; set; }
        public float CameraPosChaseRate { get; set; }
        public float CameraFacingChaseRate { get; set; }
        public float CameraEnteringZoom { get; set; }
        public float CameraSeatZoomMin { get; set; }
        public float CameraSeatZoomMax { get; set; }
        public uint UISkinFileDataID { get; set; }
        public short EnterAnimStart { get; set; }
        public short EnterAnimLoop { get; set; }
        public short RideAnimStart { get; set; }
        public short RideAnimLoop { get; set; }
        public short RideUpperAnimStart { get; set; }
        public short RideUpperAnimLoop { get; set; }
        public short ExitAnimStart { get; set; }
        public short ExitAnimLoop { get; set; }
        public short ExitAnimEnd { get; set; }
        public short VehicleEnterAnim { get; set; }
        public short VehicleExitAnim { get; set; }
        public short VehicleRideAnimLoop { get; set; }
        public ushort EnterAnimKitID { get; set; }
        public ushort RideAnimKitID { get; set; }
        public ushort ExitAnimKitID { get; set; }
        public ushort VehicleEnterAnimKitID { get; set; }
        public ushort VehicleRideAnimKitID { get; set; }
        public ushort VehicleExitAnimKitID { get; set; }
        public ushort CameraModeID { get; set; }
        public sbyte AttachmentID { get; set; }
        public sbyte PassengerAttachmentID { get; set; }
        public sbyte VehicleEnterAnimBone { get; set; }
        public sbyte VehicleExitAnimBone { get; set; }
        public sbyte VehicleRideAnimLoopBone { get; set; }
        public byte VehicleAbilityDisplay { get; set; }
        public uint EnterUISoundID { get; set; }
        public uint ExitUISoundID { get; set; }
    }
}
