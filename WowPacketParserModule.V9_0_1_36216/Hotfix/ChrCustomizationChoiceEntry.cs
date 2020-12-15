using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChrCustomizationChoice)]
    public class ChrCustomizationChoiceEntry
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public int ChrCustomizationOptionID { get; set; }
        public int ChrCustomizationReqID { get; set; }
        public ushort SortOrder { get; set; }
        public int SwatchColor1 { get; set; }
        public int SwatchColor2 { get; set; }
        public ushort UiOrderIndex { get; set; }
        public int Flags { get; set; }
    }
}
