using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PvpTalentCategory, HasIndexInData = false)]
    public class PvpTalentCategoryEntry
    {
        public byte TalentSlotMask { get; set; }
    }
}
