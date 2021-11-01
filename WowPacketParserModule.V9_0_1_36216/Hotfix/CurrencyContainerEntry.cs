using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.CurrencyContainer, HasIndexInData = false)]
    public class CurrencyContainerEntry
    {
        public string ContainerName { get; set; }
        public string ContainerDescription { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
        public int ContainerIconID { get; set; }
        public int ContainerQuality { get; set; }
        public int OnLootSpellVisualKitID { get; set; }
        public uint CurrencyTypeID { get; set; }
    }
}
