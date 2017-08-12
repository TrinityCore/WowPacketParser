using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Criteria, HasIndexInData = false)]
    public class CriteriaEntry
    {
        public uint Asset { get; set; }
        public uint StartAsset { get; set; }
        public uint FailAsset { get; set; }
        public ushort StartTimer { get; set; }
        public ushort ModifierTreeId { get; set; }
        public ushort EligibilityWorldStateID { get; set; }
        public byte Type { get; set; }
        public byte StartEvent { get; set; }
        public byte FailEvent { get; set; }
        public byte Flags { get; set; }
        public byte EligibilityWorldStateValue { get; set; }
    }
}
