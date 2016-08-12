using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemArmorQuality, HasIndexInData = false)]
    public class ItemArmorQualityEntry
    {
        [HotfixArray(7)]
        public float[] QualityMod { get; set; }
        public ushort ItemLevel { get; set; }
    }
}