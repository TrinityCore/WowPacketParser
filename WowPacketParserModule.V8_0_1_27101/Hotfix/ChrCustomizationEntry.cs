using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChrCustomization, HasIndexInData = false)]
    public class ChrCustomizationEntry
    {
        public string Name { get; set; }
        public int Sex { get; set; }
        public int BaseSection { get; set; }
        public int UiCustomizationType { get; set; }
        public int Flags { get; set; }
        [HotfixArray(3)]
        public int[] ComponentSection { get; set; }
        public int RaceID { get; set; }
    }
}
