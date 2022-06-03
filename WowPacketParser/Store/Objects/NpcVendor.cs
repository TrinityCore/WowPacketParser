using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("npc_vendor")]
    public sealed record NpcVendor : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("slot")]
        public int? Slot;

        [DBFieldName("item", true)]
        public int? Item;

        [DBFieldName("maxcount")]
        public uint? MaxCount;

        [DBFieldName("ExtendedCost", true)]
        public uint? ExtendedCost;

        [DBFieldName("type", true)]
        public uint? Type;

        [DBFieldName("PlayerConditionID", TargetedDatabase.Cataclysm)]
        public uint? PlayerConditionID;

        [DBFieldName("IgnoreFiltering", TargetedDatabase.WarlordsOfDraenor)]
        public bool? IgnoreFiltering;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
