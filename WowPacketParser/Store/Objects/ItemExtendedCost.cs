using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_extended_cost")]
    public sealed class ItemExtendedCost
    {
        [DBFieldName("RequiredHonorPoints", ClientVersionBuild.Zero, ClientVersionBuild.V6_1_0_19678)]
        public uint RequiredHonorPoints;

        [DBFieldName("RequiredArenaPoints", ClientVersionBuild.Zero, ClientVersionBuild.V6_1_0_19678)]
        public uint RequiredArenaPoints;

        [DBFieldName("RequiredArenaSlot")]
        public uint RequiredArenaSlot;

        [DBFieldName("RequiredItem", 5)]
        public uint[] RequiredItem;

        [DBFieldName("RequiredItemCount", 5)]
        public uint[] RequiredItemCount;

        [DBFieldName("RequiredPersonalArenaRating")]
        public uint RequiredPersonalArenaRating;

        [DBFieldName("ItemPurchaseGroup")]
        public uint ItemPurchaseGroup;

        [DBFieldName("RequiredCurrency", 5)]
        public uint[] RequiredCurrency;

        [DBFieldName("RequiredCurrencyCount", 5)]
        public uint[] RequiredCurrencyCount;

        [DBFieldName("RequiredFactionId")]
        public uint RequiredFactionId;

        [DBFieldName("RequiredFactionStanding")]
        public uint RequiredFactionStanding;

        [DBFieldName("RequirementFlags")]
        public uint RequirementFlags;

        [DBFieldName("RequiredAchievement")]
        public uint RequiredAchievement;

        [DBFieldName("RequiredMoney", ClientVersionBuild.V6_1_0_19678)]
        public uint RequiredMoney;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
