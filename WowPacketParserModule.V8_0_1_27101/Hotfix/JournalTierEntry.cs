using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.JournalTier, HasIndexInData = false)]
    public class JournalTierEntry
    {
        public string Name { get; set; }
    }
}
