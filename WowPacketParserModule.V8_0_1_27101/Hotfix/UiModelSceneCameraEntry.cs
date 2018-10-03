using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiModelSceneCamera)]
    public class UiModelSceneCameraEntry
    {
        public string ScriptTag { get; set; }
        [HotfixArray(3)]
        public float[] Target { get; set; }
        [HotfixArray(3)]
        public float[] ZoomedTargetOffset { get; set; }
        public int ID { get; set; }
        public byte Flags { get; set; }
        public byte CameraType { get; set; }
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public float Roll { get; set; }
        public float ZoomedYawOffset { get; set; }
        public float ZoomedPitchOffset { get; set; }
        public float ZoomedRollOffset { get; set; }
        public float ZoomDistance { get; set; }
        public float MinZoomDistance { get; set; }
        public float MaxZoomDistance { get; set; }
        public int UiModelSceneID { get; set; }
    }
}
