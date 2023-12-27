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

        [DBFieldName("PlayerConditionID", TargetedDatabaseFlag.SinceCataclysm | TargetedDatabaseFlag.WotlkClassic)]
        public uint? PlayerConditionID;

        [DBFieldName("IgnoreFiltering", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic)]
        public bool? IgnoreFiltering;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
