using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_extended_cost")]
    public sealed class ItemExtendedCost : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RequiredArenaSlot")]
        public uint? RequiredArenaSlot;

        [DBFieldName("RequiredItem", 5)]
        public uint?[] RequiredItem;

        [DBFieldName("RequiredItemCount", 5)]
        public uint?[] RequiredItemCount;

        [DBFieldName("RequiredPersonalArenaRating")]
        public uint? RequiredPersonalArenaRating;

        [DBFieldName("ItemPurchaseGroup")]
        public uint? ItemPurchaseGroup;

        [DBFieldName("RequiredCurrency", 5)]
        public uint?[] RequiredCurrency;

        [DBFieldName("RequiredCurrencyCount", 5)]
        public uint?[] RequiredCurrencyCount;

        [DBFieldName("RequiredFactionId")]
        public uint? RequiredFactionId;

        [DBFieldName("RequiredFactionStanding")]
        public uint? RequiredFactionStanding;

        [DBFieldName("RequirementFlags")]
        public uint? RequirementFlags;

        [DBFieldName("RequiredAchievement")]
        public uint? RequiredAchievement;

        [DBFieldName("RequiredMoney", TargetedDatabase.WarlordsOfDraenor)]
        public uint? RequiredMoney;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
