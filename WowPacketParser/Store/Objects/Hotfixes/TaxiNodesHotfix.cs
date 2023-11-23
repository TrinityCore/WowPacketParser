using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("taxi_nodes")]
    public sealed record TaxiNodesHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("MapOffsetX")]
        public float? MapOffsetX;

        [DBFieldName("MapOffsetY")]
        public float? MapOffsetY;

        [DBFieldName("FlightMapOffsetX")]
        public float? FlightMapOffsetX;

        [DBFieldName("FlightMapOffsetY")]
        public float? FlightMapOffsetY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("ConditionID")]
        public int? ConditionID;

        [DBFieldName("CharacterBitNumber")]
        public ushort? CharacterBitNumber;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("UiTextureKitID")]
        public int? UiTextureKitID;

        [DBFieldName("MinimapAtlasMemberID")]
        public int? MinimapAtlasMemberID;

        [DBFieldName("Facing")]
        public float? Facing;

        [DBFieldName("SpecialIconConditionID")]
        public uint? SpecialIconConditionID;

        [DBFieldName("VisibilityConditionID")]
        public uint? VisibilityConditionID;

        [DBFieldName("MountCreatureID", 2)]
        public int?[] MountCreatureID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("taxi_nodes_locale")]
    public sealed record TaxiNodesLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("taxi_nodes")]
    public sealed record TaxiNodesHotfix1017 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("MapOffsetX")]
        public float? MapOffsetX;

        [DBFieldName("MapOffsetY")]
        public float? MapOffsetY;

        [DBFieldName("FlightMapOffsetX")]
        public float? FlightMapOffsetX;

        [DBFieldName("FlightMapOffsetY")]
        public float? FlightMapOffsetY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContinentID")]
        public ushort? ContinentID;

        [DBFieldName("ConditionID")]
        public int? ConditionID;

        [DBFieldName("CharacterBitNumber")]
        public ushort? CharacterBitNumber;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UiTextureKitID")]
        public int? UiTextureKitID;

        [DBFieldName("MinimapAtlasMemberID")]
        public int? MinimapAtlasMemberID;

        [DBFieldName("Facing")]
        public float? Facing;

        [DBFieldName("SpecialIconConditionID")]
        public uint? SpecialIconConditionID;

        [DBFieldName("VisibilityConditionID")]
        public uint? VisibilityConditionID;

        [DBFieldName("MountCreatureID", 2)]
        public int?[] MountCreatureID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("taxi_nodes_locale")]
    public sealed record TaxiNodesLocaleHotfix1017 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("taxi_nodes")]
    public sealed record TaxiNodesHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("MapOffsetX")]
        public float? MapOffsetX;

        [DBFieldName("MapOffsetY")]
        public float? MapOffsetY;

        [DBFieldName("FlightMapOffsetX")]
        public float? FlightMapOffsetX;

        [DBFieldName("FlightMapOffsetY")]
        public float? FlightMapOffsetY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContinentID")]
        public uint? ContinentID;

        [DBFieldName("ConditionID")]
        public uint? ConditionID;

        [DBFieldName("CharacterBitNumber")]
        public ushort? CharacterBitNumber;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("UiTextureKitID")]
        public int? UiTextureKitID;

        [DBFieldName("Facing")]
        public float? Facing;

        [DBFieldName("SpecialIconConditionID")]
        public uint? SpecialIconConditionID;

        [DBFieldName("VisibilityConditionID")]
        public uint? VisibilityConditionID;

        [DBFieldName("MountCreatureID", 2)]
        public int?[] MountCreatureID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("taxi_nodes_locale")]
    public sealed record TaxiNodesLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("taxi_nodes")]
    public sealed record TaxiNodesHotfix343: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("MapOffsetX")]
        public float? MapOffsetX;

        [DBFieldName("MapOffsetY")]
        public float? MapOffsetY;

        [DBFieldName("FlightMapOffsetX")]
        public float? FlightMapOffsetX;

        [DBFieldName("FlightMapOffsetY")]
        public float? FlightMapOffsetY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContinentID")]
        public uint? ContinentID;

        [DBFieldName("ConditionID")]
        public uint? ConditionID;

        [DBFieldName("CharacterBitNumber")]
        public ushort? CharacterBitNumber;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UiTextureKitID")]
        public int? UiTextureKitID;

        [DBFieldName("Facing")]
        public float? Facing;

        [DBFieldName("SpecialIconConditionID")]
        public uint? SpecialIconConditionID;

        [DBFieldName("VisibilityConditionID")]
        public uint? VisibilityConditionID;

        [DBFieldName("MountCreatureID", 2)]
        public int?[] MountCreatureID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("taxi_nodes_locale")]
    public sealed record TaxiNodesLocaleHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
