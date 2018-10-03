using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.JournalTierXInstance, HasIndexInData = false)]
    public class JournalTierXInstanceEntry
    {
        public ushort JournalTierID { get; set; }
        public ushort JournalInstanceID { get; set; }
    }
}
