using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiPartyPose, HasIndexInData = false)]
    public class UiPartyPoseEntry
    {
        public int UiWidgetSetID { get; set; }
        public int VictoryUiModelSceneID { get; set; }
        public int DefeatUiModelSceneID { get; set; }
        public int VictorySoundKitID { get; set; }
        public int DefeatSoundKitID { get; set; }
        public int MapID { get; set; }
    }
}
