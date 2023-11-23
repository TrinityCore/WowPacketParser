using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_mission")]
    public sealed record GarrMissionHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Location")]
        public string Location;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("MapPosX")]
        public float? MapPosX;

        [DBFieldName("MapPosY")]
        public float? MapPosY;

        [DBFieldName("WorldPosX")]
        public float? WorldPosX;

        [DBFieldName("WorldPosY")]
        public float? WorldPosY;

        [DBFieldName("GarrTypeID")]
        public byte? GarrTypeID;

        [DBFieldName("GarrMissionTypeID")]
        public byte? GarrMissionTypeID;

        [DBFieldName("GarrFollowerTypeID")]
        public sbyte? GarrFollowerTypeID;

        [DBFieldName("MaxFollowers")]
        public byte? MaxFollowers;

        [DBFieldName("MissionCost")]
        public uint? MissionCost;

        [DBFieldName("MissionCostCurrencyTypesID")]
        public ushort? MissionCostCurrencyTypesID;

        [DBFieldName("OfferedGarrMissionTextureID")]
        public byte? OfferedGarrMissionTextureID;

        [DBFieldName("UiTextureKitID")]
        public ushort? UiTextureKitID;

        [DBFieldName("EnvGarrMechanicID")]
        public uint? EnvGarrMechanicID;

        [DBFieldName("EnvGarrMechanicTypeID")]
        public int? EnvGarrMechanicTypeID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("GarrMissionSetID")]
        public int? GarrMissionSetID;

        [DBFieldName("TargetLevel")]
        public sbyte? TargetLevel;

        [DBFieldName("TargetItemLevel")]
        public ushort? TargetItemLevel;

        [DBFieldName("MissionDuration")]
        public int? MissionDuration;

        [DBFieldName("TravelDuration")]
        public int? TravelDuration;

        [DBFieldName("OfferDuration")]
        public uint? OfferDuration;

        [DBFieldName("BaseCompletionChance")]
        public byte? BaseCompletionChance;

        [DBFieldName("BaseFollowerXP")]
        public uint? BaseFollowerXP;

        [DBFieldName("OvermaxRewardPackID")]
        public uint? OvermaxRewardPackID;

        [DBFieldName("FollowerDeathChance")]
        public byte? FollowerDeathChance;

        [DBFieldName("AreaID")]
        public uint? AreaID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AutoMissionScalar")]
        public float? AutoMissionScalar;

        [DBFieldName("AutoMissionScalarCurveID")]
        public int? AutoMissionScalarCurveID;

        [DBFieldName("AutoCombatantEnvCasterID")]
        public int? AutoCombatantEnvCasterID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_mission_locale")]
    public sealed record GarrMissionLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Location_lang")]
        public string LocationLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_mission")]
    public sealed record GarrMissionHotfix1007 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Location")]
        public string Location;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("MapPosX")]
        public float? MapPosX;

        [DBFieldName("MapPosY")]
        public float? MapPosY;

        [DBFieldName("WorldPosX")]
        public float? WorldPosX;

        [DBFieldName("WorldPosY")]
        public float? WorldPosY;

        [DBFieldName("GarrTypeID")]
        public sbyte? GarrTypeID;

        [DBFieldName("GarrMissionTypeID")]
        public byte? GarrMissionTypeID;

        [DBFieldName("GarrFollowerTypeID")]
        public byte? GarrFollowerTypeID;

        [DBFieldName("MaxFollowers")]
        public byte? MaxFollowers;

        [DBFieldName("MissionCost")]
        public uint? MissionCost;

        [DBFieldName("MissionCostCurrencyTypesID")]
        public ushort? MissionCostCurrencyTypesID;

        [DBFieldName("OfferedGarrMissionTextureID")]
        public byte? OfferedGarrMissionTextureID;

        [DBFieldName("UiTextureKitID")]
        public ushort? UiTextureKitID;

        [DBFieldName("EnvGarrMechanicID")]
        public uint? EnvGarrMechanicID;

        [DBFieldName("EnvGarrMechanicTypeID")]
        public int? EnvGarrMechanicTypeID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("GarrMissionSetID")]
        public int? GarrMissionSetID;

        [DBFieldName("TargetLevel")]
        public sbyte? TargetLevel;

        [DBFieldName("TargetItemLevel")]
        public ushort? TargetItemLevel;

        [DBFieldName("MissionDuration")]
        public int? MissionDuration;

        [DBFieldName("TravelDuration")]
        public int? TravelDuration;

        [DBFieldName("OfferDuration")]
        public uint? OfferDuration;

        [DBFieldName("BaseCompletionChance")]
        public byte? BaseCompletionChance;

        [DBFieldName("BaseFollowerXP")]
        public uint? BaseFollowerXP;

        [DBFieldName("OvermaxRewardPackID")]
        public uint? OvermaxRewardPackID;

        [DBFieldName("FollowerDeathChance")]
        public byte? FollowerDeathChance;

        [DBFieldName("AreaID")]
        public uint? AreaID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AutoMissionScalar")]
        public float? AutoMissionScalar;

        [DBFieldName("AutoMissionScalarCurveID")]
        public int? AutoMissionScalarCurveID;

        [DBFieldName("AutoCombatantEnvCasterID")]
        public int? AutoCombatantEnvCasterID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_mission_locale")]
    public sealed record GarrMissionLocaleHotfix1007 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Location_lang")]
        public string LocationLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_mission")]
    public sealed record GarrMissionHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Location")]
        public string Location;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("MapPosX")]
        public float? MapPosX;

        [DBFieldName("MapPosY")]
        public float? MapPosY;

        [DBFieldName("WorldPosX")]
        public float? WorldPosX;

        [DBFieldName("WorldPosY")]
        public float? WorldPosY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GarrTypeID")]
        public byte? GarrTypeID;

        [DBFieldName("GarrMissionTypeID")]
        public byte? GarrMissionTypeID;

        [DBFieldName("GarrFollowerTypeID")]
        public byte? GarrFollowerTypeID;

        [DBFieldName("MaxFollowers")]
        public byte? MaxFollowers;

        [DBFieldName("MissionCost")]
        public uint? MissionCost;

        [DBFieldName("MissionCostCurrencyTypesID")]
        public ushort? MissionCostCurrencyTypesID;

        [DBFieldName("OfferedGarrMissionTextureID")]
        public byte? OfferedGarrMissionTextureID;

        [DBFieldName("UiTextureKitID")]
        public ushort? UiTextureKitID;

        [DBFieldName("EnvGarrMechanicID")]
        public uint? EnvGarrMechanicID;

        [DBFieldName("EnvGarrMechanicTypeID")]
        public byte? EnvGarrMechanicTypeID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("TargetLevel")]
        public sbyte? TargetLevel;

        [DBFieldName("TargetItemLevel")]
        public ushort? TargetItemLevel;

        [DBFieldName("MissionDuration")]
        public int? MissionDuration;

        [DBFieldName("TravelDuration")]
        public int? TravelDuration;

        [DBFieldName("OfferDuration")]
        public uint? OfferDuration;

        [DBFieldName("BaseCompletionChance")]
        public byte? BaseCompletionChance;

        [DBFieldName("BaseFollowerXP")]
        public uint? BaseFollowerXP;

        [DBFieldName("OvermaxRewardPackID")]
        public uint? OvermaxRewardPackID;

        [DBFieldName("FollowerDeathChance")]
        public byte? FollowerDeathChance;

        [DBFieldName("AreaID")]
        public uint? AreaID;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("GarrMissionSetID")]
        public int? GarrMissionSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_mission_locale")]
    public sealed record GarrMissionLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Location_lang")]
        public string LocationLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
