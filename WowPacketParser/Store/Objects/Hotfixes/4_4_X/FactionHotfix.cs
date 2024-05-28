using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("faction")]
    public sealed record FactionHotfix440: IDataModel
    {
        [DBFieldName("ReputationRaceMask", 4)]
        public long?[] ReputationRaceMask;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ReputationIndex")]
        public short? ReputationIndex;

        [DBFieldName("ParentFactionID")]
        public ushort? ParentFactionID;

        [DBFieldName("Expansion")]
        public byte? Expansion;

        [DBFieldName("FriendshipRepID")]
        public byte? FriendshipRepID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ParagonFactionID")]
        public ushort? ParagonFactionID;

        [DBFieldName("RenownFactionID")]
        public int? RenownFactionID;

        [DBFieldName("RenownCurrencyID")]
        public int? RenownCurrencyID;

        [DBFieldName("ReputationClassMask", 4)]
        public short?[] ReputationClassMask;

        [DBFieldName("ReputationFlags", 4)]
        public ushort?[] ReputationFlags;

        [DBFieldName("ReputationBase", 4)]
        public int?[] ReputationBase;

        [DBFieldName("ReputationMax", 4)]
        public int?[] ReputationMax;

        [DBFieldName("ParentFactionMod", 2)]
        public float?[] ParentFactionMod;

        [DBFieldName("ParentFactionCap", 2)]
        public byte?[] ParentFactionCap;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("faction_locale")]
    public sealed record FactionLocaleHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
