using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MarketingPromotionsXLocale, HasIndexInData = false)]
    public class MarketingPromotionsXLocaleEntry
    {
        public string AcceptURL { get; set; }
        public byte PromotionID { get; set; }
        public sbyte LocaleID { get; set; }
        public int AdTexture { get; set; }
        public int LogoTexture { get; set; }
        public int AcceptButtonTexture { get; set; }
        public int DeclineButtonTexture { get; set; }
    }
}
