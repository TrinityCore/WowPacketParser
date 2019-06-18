using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemArmorQuality, HasIndexInData = false)]
    public class ItemArmorQualityEntry
    {
        [HotfixArray(7)]
        public float[] Qualitymod { get; set; }
    }
}
