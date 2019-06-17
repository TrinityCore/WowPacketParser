using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Criteria, HasIndexInData = false)]
    public class CriteriaEntry
    {
        public short Type { get; set; }
        public int Asset { get; set; }
        public uint ModifierTreeId { get; set; }
        public byte StartEvent { get; set; }
        public int StartAsset { get; set; }
        public ushort StartTimer { get; set; }
        public byte FailEvent { get; set; }
        public int FailAsset { get; set; }
        public byte Flags { get; set; }
        public short EligibilityWorldStateID { get; set; }
        public sbyte EligibilityWorldStateValue { get; set; }
    }
}
