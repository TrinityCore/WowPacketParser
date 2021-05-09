using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AdventureJournal, HasIndexInData = false)]
    public class AdventureJournalEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public string RewardDescription { get; set; }
        public string ContinueDescription { get; set; }
        public byte Type { get; set; }
        public uint PlayerConditionID { get; set; }
        public byte Flags { get; set; }
        public byte ButtonActionType { get; set; }
        public int TextureFileDataID { get; set; }
        public ushort LfgDungeonID { get; set; }
        public int QuestID { get; set; }
        public ushort BattleMasterListID { get; set; }
        public byte PriorityMin { get; set; }
        public byte PriorityMax { get; set; }
        public int ItemID { get; set; }
        public uint ItemQuantity { get; set; }
        public ushort CurrencyType { get; set; }
        public uint CurrencyQuantity { get; set; }
        public ushort UiMapID { get; set; }
        [HotfixArray(2)]
        public uint[] BonusPlayerConditionID { get; set; }
        [HotfixArray(2)]
        public byte[] BonusValue { get; set; }
    }
}
