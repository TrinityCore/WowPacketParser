using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Vehicle, HasIndexInData = false)]
    public class VehicleEntry
    {
        public uint Flags { get; set; }
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
        public float MsslTrgtTurnLingering { get; set; }
        public float MsslTrgtPitchLingering { get; set; }
        public float MsslTrgtMouseLingering { get; set; }
        public float MsslTrgtEndOpacity { get; set; }
        public float MsslTrgtArcSpeed { get; set; }
        public float MsslTrgtArcRepeat { get; set; }
        public float MsslTrgtArcWidth { get; set; }
        [HotfixArray(2)]
        public float[] MsslTrgtImpactRadius { get; set; }
        public string MsslTrgtArcTexture { get; set; }
        public string MsslTrgtImpactTexture { get; set; }
        [HotfixArray(2)]
        public string[] MsslTrgtImpactModel { get; set; }
        public float CameraYawOffset { get; set; }
        public float MsslTrgtImpactTexRadius { get; set; }
        [HotfixArray(8)]
        public ushort[] SeatID { get; set; }
        public ushort VehicleUIIndicatorID { get; set; }
        [HotfixArray(3)]
        public ushort[] PowerDisplayID { get; set; }
        public byte FlagsB { get; set; }
        public byte UILocomotionType { get; set; }
    }
}