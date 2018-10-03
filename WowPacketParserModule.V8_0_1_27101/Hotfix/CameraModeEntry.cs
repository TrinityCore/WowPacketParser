using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CameraMode, HasIndexInData = false)]
    public class CameraModeEntry
    {
        [HotfixArray(3)]
        public float[] PositionOffset { get; set; }
        [HotfixArray(3)]
        public float[] TargetOffset { get; set; }
        public sbyte Type { get; set; }
        public int Flags { get; set; }
        public float PositionSmoothing { get; set; }
        public float RotationSmoothing { get; set; }
        public float FieldOfView { get; set; }
        public sbyte LockedPositionOffsetBase { get; set; }
        public sbyte LockedPositionOffsetDirection { get; set; }
        public sbyte LockedTargetOffsetBase { get; set; }
        public sbyte LockedTargetOffsetDirection { get; set; }
    }
}
