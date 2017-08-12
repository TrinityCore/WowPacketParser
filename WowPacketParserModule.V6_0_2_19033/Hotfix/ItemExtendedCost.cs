using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.ItemExtendedCost)]
    public class ItemExtendedCost
    {
        public uint ID { get; set; }
        [HotfixVersion(ClientVersionBuild.V6_1_0_19678, true)]
        public uint RequiredHonorPoints { get; set; }
        [HotfixVersion(ClientVersionBuild.V6_1_0_19678, true)]
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
        public uint RequiredAchievement { get; set; }
        [HotfixVersion(ClientVersionBuild.V6_1_0_19678, false)]
        public int RequiredMoney { get; set; }
    }
}
