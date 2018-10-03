using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BonusRoll, HasIndexInData = false)]
    public class BonusRollEntry
    {
        public uint CurrencyTypesID { get; set; }
        public uint CurrencyCost { get; set; }
        public uint JournalEncounterID { get; set; }
        public uint JournalInstanceID { get; set; }
    }
}
