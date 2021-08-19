using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_4_0_17359.Hotfix
{
    [HotfixStructure(DB2Hash.ItemExtendedCost)]
    public class ItemExtendedCost
    {
        public uint ID { get; set; }
        public uint RequiredHonorPoints { get; set; }
        public uint RequiredArenaPoints { get; set; }
        public uint RequiredArenaSlot { get; set; }
        [HotfixArray(5)]
        public uint[] RequiredItems { get; set; }
        [HotfixArray(5)]
        public uint[] RequiredItemCount { get; set; }
        public uint RequiredPersonalArenaRating { get; set; }
        public uint ItemPurchaseGroup { get; set; }
        [HotfixArray(5)]
        public uint[] RequiredCurrency { get; set; }
        [HotfixArray(5)]
        public uint[] RequiredCurrencyCount { get; set; }
        public uint RequiredFactionId { get; set; }
        public uint RequiredFactionStanding { get; set; }
        public uint RequirementFlags { get; set; }
        public uint RequiredGuildLevel { get; set; }
        public uint RequiredAchievement { get; set; }
    }
}
