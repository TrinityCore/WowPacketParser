using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CombatCondition, HasIndexInData = false)]
    public class CombatConditionEntry
    {
        public ushort WorldStateExpressionID { get; set; }
        public ushort SelfConditionID { get; set; }
        public ushort TargetConditionID { get; set; }
        public byte FriendConditionLogic { get; set; }
        public byte EnemyConditionLogic { get; set; }
        [HotfixArray(2)]
        public ushort[] FriendConditionID { get; set; }
        [HotfixArray(2)]
        public byte[] FriendConditionOp { get; set; }
        [HotfixArray(2)]
        public byte[] FriendConditionCount { get; set; }
        [HotfixArray(2)]
        public ushort[] EnemyConditionID { get; set; }
        [HotfixArray(2)]
        public byte[] EnemyConditionOp { get; set; }
        [HotfixArray(2)]
        public byte[] EnemyConditionCount { get; set; }
    }
}
