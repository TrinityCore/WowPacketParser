using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_building")]
    public sealed record GarrBuildingHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("HordeName")]
        public string HordeName;

        [DBFieldName("AllianceName")]
        public string AllianceName;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Tooltip")]
        public string Tooltip;

        [DBFieldName("GarrTypeID")]
        public byte? GarrTypeID;

        [DBFieldName("BuildingType")]
        public sbyte? BuildingType;

        [DBFieldName("HordeGameObjectID")]
        public int? HordeGameObjectID;

        [DBFieldName("AllianceGameObjectID")]
        public int? AllianceGameObjectID;

        [DBFieldName("GarrSiteID")]
        public int? GarrSiteID;

        [DBFieldName("UpgradeLevel")]
        public byte? UpgradeLevel;

        [DBFieldName("BuildSeconds")]
        public int? BuildSeconds;

        [DBFieldName("CurrencyTypeID")]
        public ushort? CurrencyTypeID;

        [DBFieldName("CurrencyQty")]
        public int? CurrencyQty;

        [DBFieldName("HordeUiTextureKitID")]
        public ushort? HordeUiTextureKitID;

        [DBFieldName("AllianceUiTextureKitID")]
        public ushort? AllianceUiTextureKitID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("AllianceSceneScriptPackageID")]
        public ushort? AllianceSceneScriptPackageID;

        [DBFieldName("HordeSceneScriptPackageID")]
        public ushort? HordeSceneScriptPackageID;

        [DBFieldName("MaxAssignments")]
        public int? MaxAssignments;

        [DBFieldName("ShipmentCapacity")]
        public byte? ShipmentCapacity;

        [DBFieldName("GarrAbilityID")]
        public ushort? GarrAbilityID;

        [DBFieldName("BonusGarrAbilityID")]
        public ushort? BonusGarrAbilityID;

        [DBFieldName("GoldCost")]
        public ushort? GoldCost;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_building_locale")]
    public sealed record GarrBuildingLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("HordeName_lang")]
        public string HordeNameLang;

        [DBFieldName("AllianceName_lang")]
        public string AllianceNameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Tooltip_lang")]
        public string TooltipLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_building")]
    public sealed record GarrBuildingHotfix1007 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("HordeName")]
        public string HordeName;

        [DBFieldName("AllianceName")]
        public string AllianceName;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Tooltip")]
        public string Tooltip;

        [DBFieldName("GarrTypeID")]
        public sbyte? GarrTypeID;

        [DBFieldName("BuildingType")]
        public sbyte? BuildingType;

        [DBFieldName("HordeGameObjectID")]
        public int? HordeGameObjectID;

        [DBFieldName("AllianceGameObjectID")]
        public int? AllianceGameObjectID;

        [DBFieldName("GarrSiteID")]
        public int? GarrSiteID;

        [DBFieldName("UpgradeLevel")]
        public byte? UpgradeLevel;

        [DBFieldName("BuildSeconds")]
        public int? BuildSeconds;

        [DBFieldName("CurrencyTypeID")]
        public ushort? CurrencyTypeID;

        [DBFieldName("CurrencyQty")]
        public int? CurrencyQty;

        [DBFieldName("HordeUiTextureKitID")]
        public ushort? HordeUiTextureKitID;

        [DBFieldName("AllianceUiTextureKitID")]
        public ushort? AllianceUiTextureKitID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("AllianceSceneScriptPackageID")]
        public ushort? AllianceSceneScriptPackageID;

        [DBFieldName("HordeSceneScriptPackageID")]
        public ushort? HordeSceneScriptPackageID;

        [DBFieldName("MaxAssignments")]
        public int? MaxAssignments;

        [DBFieldName("ShipmentCapacity")]
        public byte? ShipmentCapacity;

        [DBFieldName("GarrAbilityID")]
        public ushort? GarrAbilityID;

        [DBFieldName("BonusGarrAbilityID")]
        public ushort? BonusGarrAbilityID;

        [DBFieldName("GoldCost")]
        public ushort? GoldCost;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_building_locale")]
    public sealed record GarrBuildingLocaleHotfix1007 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("HordeName_lang")]
        public string HordeNameLang;

        [DBFieldName("AllianceName_lang")]
        public string AllianceNameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Tooltip_lang")]
        public string TooltipLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_building")]
    public sealed record GarrBuildingHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("HordeName")]
        public string HordeName;

        [DBFieldName("AllianceName")]
        public string AllianceName;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Tooltip")]
        public string Tooltip;

        [DBFieldName("GarrTypeID")]
        public byte? GarrTypeID;

        [DBFieldName("BuildingType")]
        public byte? BuildingType;

        [DBFieldName("HordeGameObjectID")]
        public int? HordeGameObjectID;

        [DBFieldName("AllianceGameObjectID")]
        public int? AllianceGameObjectID;

        [DBFieldName("GarrSiteID")]
        public byte? GarrSiteID;

        [DBFieldName("UpgradeLevel")]
        public byte? UpgradeLevel;

        [DBFieldName("BuildSeconds")]
        public int? BuildSeconds;

        [DBFieldName("CurrencyTypeID")]
        public ushort? CurrencyTypeID;

        [DBFieldName("CurrencyQty")]
        public int? CurrencyQty;

        [DBFieldName("HordeUiTextureKitID")]
        public ushort? HordeUiTextureKitID;

        [DBFieldName("AllianceUiTextureKitID")]
        public ushort? AllianceUiTextureKitID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("AllianceSceneScriptPackageID")]
        public ushort? AllianceSceneScriptPackageID;

        [DBFieldName("HordeSceneScriptPackageID")]
        public ushort? HordeSceneScriptPackageID;

        [DBFieldName("MaxAssignments")]
        public int? MaxAssignments;

        [DBFieldName("ShipmentCapacity")]
        public byte? ShipmentCapacity;

        [DBFieldName("GarrAbilityID")]
        public ushort? GarrAbilityID;

        [DBFieldName("BonusGarrAbilityID")]
        public ushort? BonusGarrAbilityID;

        [DBFieldName("GoldCost")]
        public ushort? GoldCost;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_building_locale")]
    public sealed record GarrBuildingLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("HordeName_lang")]
        public string HordeNameLang;

        [DBFieldName("AllianceName_lang")]
        public string AllianceNameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Tooltip_lang")]
        public string TooltipLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
