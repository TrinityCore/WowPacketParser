using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemExtendedCost, HasIndexInData = false)]
    public class ItemExtendedCostEntry
    {
        [HotfixArray(5)]
        public uint[] RequiredItem { get; set; }
        [HotfixArray(5)]
        public uint[] RequiredCurrencyCount { get; set; }
        [HotfixArray(5)]
        public ushort[] RequiredItemCount { get; set; }
        public ushort RequiredPersonalArenaRating { get; set; }
        [HotfixArray(5)]
        public ushort[] RequiredCurrency { get; set; }
        public byte RequiredArenaSlot { get; set; }
        public byte RequiredFactionId { get; set; }
        public byte RequiredFactionStanding { get; set; }
        public byte RequirementFlags { get; set; }
        public byte RequiredAchievement { get; set; }
    }
}