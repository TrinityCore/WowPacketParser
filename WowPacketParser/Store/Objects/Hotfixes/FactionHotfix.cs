using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("faction")]
    public sealed record FactionHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ReputationRaceMask", 4)]
        public long?[] ReputationRaceMask;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ReputationIndex")]
        public short? ReputationIndex;

        [DBFieldName("ParentFactionID")]
        public ushort? ParentFactionID;

        [DBFieldName("Expansion")]
        public byte? Expansion;

        [DBFieldName("FriendshipRepID")]
        public uint? FriendshipRepID;

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
    public sealed record FactionLocaleHotfix1000: IDataModel
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

    [Hotfix]
    [DBTableName("faction")]
    public sealed record FactionHotfix340: IDataModel
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
        public byte? Flags;

        [DBFieldName("ParagonFactionID")]
        public ushort? ParagonFactionID;

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
    public sealed record FactionLocaleHotfix340: IDataModel
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
    [Hotfix]
    [DBTableName("faction")]
    public sealed record FactionHotfix341: IDataModel
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
    public sealed record FactionLocaleHotfix341: IDataModel
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
