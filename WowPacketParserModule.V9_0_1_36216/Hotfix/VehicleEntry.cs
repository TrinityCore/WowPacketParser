using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.Vehicle, HasIndexInData = false)]
    public class VehicleEntry
    {
        public int Flags { get; set; }
        public byte FlagsB { get; set; }
        public float TurnSpeed { get; set; }
        public float PitchSpeed { get; set; }
        public float PitchMin { get; set; }
        public float PitchMax { get; set; }
        public float MouseLookOffsetPitch { get; set; }
        public float CameraFadeDistScalarMin { get; set; }
        public float CameraFadeDistScalarMax { get; set; }
        public float CameraPitchOffset { get; set; }
        public float FacingLimitRight { get; set; }
        public float FacingLimitLeft { get; set; }
        public float CameraYawOffset { get; set; }
        public ushort VehicleUIIndicatorID { get; set; }
        public int MissileTargetingID { get; set; }
        public ushort VehiclePOITypeID { get; set; }
        [HotfixArray(8)]
        public ushort[] SeatID { get; set; }
        [HotfixArray(3)]
        public ushort[] PowerDisplayID { get; set; }
    }
}
