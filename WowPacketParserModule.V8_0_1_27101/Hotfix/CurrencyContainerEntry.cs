using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
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
        public int CurrencyTypeID { get; set; }
    }
}
