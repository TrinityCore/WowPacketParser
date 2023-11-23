using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("summon_properties")]
    public sealed record SummonPropertiesHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Control")]
        public int? Control;

        [DBFieldName("Faction")]
        public int? Faction;

        [DBFieldName("Title")]
        public int? Title;

        [DBFieldName("Slot")]
        public int? Slot;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("summon_properties")]
    public sealed record SummonPropertiesHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Control")]
        public int? Control;

        [DBFieldName("Faction")]
        public int? Faction;

        [DBFieldName("Title")]
        public int? Title;

        [DBFieldName("Slot")]
        public int? Slot;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
