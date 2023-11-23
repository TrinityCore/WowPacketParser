using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("armor_location")]
    public sealed record ArmorLocationHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Clothmodifier")]
        public float? Clothmodifier;

        [DBFieldName("Leathermodifier")]
        public float? Leathermodifier;

        [DBFieldName("Chainmodifier")]
        public float? Chainmodifier;

        [DBFieldName("Platemodifier")]
        public float? Platemodifier;

        [DBFieldName("Modifier")]
        public float? Modifier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("armor_location")]
    public sealed record ArmorLocationHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Clothmodifier")]
        public float? Clothmodifier;

        [DBFieldName("Leathermodifier")]
        public float? Leathermodifier;

        [DBFieldName("Chainmodifier")]
        public float? Chainmodifier;

        [DBFieldName("Platemodifier")]
        public float? Platemodifier;

        [DBFieldName("Modifier")]
        public float? Modifier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
