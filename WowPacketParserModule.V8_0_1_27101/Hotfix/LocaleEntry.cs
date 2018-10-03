using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Locale, HasIndexInData = false)]
    public class LocaleEntry
    {
        public byte WowLocale { get; set; }
        public int FontFileDataID { get; set; }
        public byte ClientDisplayExpansion { get; set; }
        public byte Secondary { get; set; }
    }
}
