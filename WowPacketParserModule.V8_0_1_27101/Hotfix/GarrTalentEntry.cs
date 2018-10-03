using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrTalent)]
    public class GarrTalentEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public uint GarrTalentTreeID { get; set; }
        public sbyte Tier { get; set; }
        public sbyte UiOrder { get; set; }
        public int IconFileDataID { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint GarrAbilityID { get; set; }
        public uint PerkSpellID { get; set; }
        public uint PerkPlayerConditionID { get; set; }
        public int ResearchDurationSecs { get; set; }
        public int ResearchGoldCost { get; set; }
        public int ResearchCost { get; set; }
        public uint ResearchCostCurrencyTypesID { get; set; }
        public int RespecDurationSecs { get; set; }
        public int RespecGoldCost { get; set; }
        public int RespecCost { get; set; }
        public uint RespecCostCurrencyTypesID { get; set; }
        public sbyte Flags { get; set; }
    }
}
