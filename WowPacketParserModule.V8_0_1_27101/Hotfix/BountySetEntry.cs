using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BountySet, HasIndexInData = false)]
    public class BountySetEntry
    {
        public uint VisiblePlayerConditionID { get; set; }
        public ushort LockedQuestID { get; set; }
    }
}
