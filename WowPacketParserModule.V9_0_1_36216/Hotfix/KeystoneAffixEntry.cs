using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.KeystoneAffix, HasIndexInData = false)]
    public class KeystoneAffixEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int FiledataID { get; set; }
    }
}
