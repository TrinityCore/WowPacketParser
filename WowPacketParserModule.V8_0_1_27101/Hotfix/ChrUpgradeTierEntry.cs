using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChrUpgradeTier)]
    public class ChrUpgradeTierEntry
    {
        public string DisplayName { get; set; }
        public int ID { get; set; }
        public byte OrderIndex { get; set; }
        public byte NumTalents { get; set; }
    }
}
