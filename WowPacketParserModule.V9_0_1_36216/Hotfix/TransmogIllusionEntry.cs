using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogIllusion)]
    public class TransmogIllusionEntry
    {
        public uint ID { get; set; }
        public int UnlockConditionID { get; set; }
        public int TransmogCost { get; set; }
        public int SpellItemEnchantmentID { get; set; }
        public int Flags { get; set; }
    }
}
