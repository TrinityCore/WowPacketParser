using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.WorldEffect, HasIndexInData = false)]
    public class WorldEffectEntry
    {
        public uint QuestFeedbackEffectID { get; set; }
        public byte WhenToDisplay { get; set; }
        public sbyte TargetType { get; set; }
        public int TargetAsset { get; set; }
        public uint PlayerConditionID { get; set; }
        public ushort CombatConditionID { get; set; }
    }
}
