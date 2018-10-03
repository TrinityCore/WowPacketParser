using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellActionBarPref, HasIndexInData = false)]
    public class SpellActionBarPrefEntry
    {
        public int SpellID { get; set; }
        public ushort PreferredActionBarMask { get; set; }
    }
}
