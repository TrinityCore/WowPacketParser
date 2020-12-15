using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChrCustomizationOption)]
    public class ChrCustomizationOptionEntry
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public ushort SecondaryID { get; set; }
        public int Flags { get; set; }
        public int ChrModelID { get; set; }
        public int SortIndex { get; set; }
        public int ChrCustomizationCategoryID { get; set; }
        public int OptionType { get; set; }
        public float BarberShopCostModifier { get; set; }
        public int ChrCustomizationID { get; set; }
        public int ChrCustomizationReqID { get; set; }
        public int UiOrderIndex { get; set; }
    }
}
