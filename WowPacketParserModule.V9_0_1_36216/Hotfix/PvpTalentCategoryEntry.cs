using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.PvpTalentCategory, HasIndexInData = false)]
    public class PvpTalentCategoryEntry
    {
        public byte TalentSlotMask { get; set; }
    }
}
