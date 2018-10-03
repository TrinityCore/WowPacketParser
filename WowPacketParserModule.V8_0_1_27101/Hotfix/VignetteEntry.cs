using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Vignette, HasIndexInData = false)]
    public class VignetteEntry
    {
        public string Name { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint VisibleTrackingQuestID { get; set; }
        public uint QuestFeedbackEffectID { get; set; }
        public uint Flags { get; set; }
        public float MaxHeight { get; set; }
        public float MinHeight { get; set; }
        public sbyte VignetteType { get; set; }
        public int RewardQuestID { get; set; }
    }
}
