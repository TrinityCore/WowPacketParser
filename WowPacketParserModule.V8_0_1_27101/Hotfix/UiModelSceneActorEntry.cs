using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiModelSceneActor)]
    public class UiModelSceneActorEntry
    {
        public string ScriptTag { get; set; }
        [HotfixArray(3)]
        public float[] Position { get; set; }
        public int ID { get; set; }
        public byte Flags { get; set; }
        public int UiModelSceneActorDisplayID { get; set; }
        public float OrientationYaw { get; set; }
        public float OrientationPitch { get; set; }
        public float OrientationRoll { get; set; }
        public float NormalizedScale { get; set; }
        public int UiModelSceneID { get; set; }
    }
}
