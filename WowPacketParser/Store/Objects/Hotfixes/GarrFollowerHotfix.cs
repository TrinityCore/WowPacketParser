using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_follower")]
    public sealed record GarrFollowerHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("HordeSourceText")]
        public string HordeSourceText;

        [DBFieldName("AllianceSourceText")]
        public string AllianceSourceText;

        [DBFieldName("TitleName")]
        public string TitleName;

        [DBFieldName("GarrTypeID")]
        public byte? GarrTypeID;

        [DBFieldName("GarrFollowerTypeID")]
        public sbyte? GarrFollowerTypeID;

        [DBFieldName("HordeCreatureID")]
        public int? HordeCreatureID;

        [DBFieldName("AllianceCreatureID")]
        public int? AllianceCreatureID;

        [DBFieldName("HordeGarrFollRaceID")]
        public byte? HordeGarrFollRaceID;

        [DBFieldName("AllianceGarrFollRaceID")]
        public byte? AllianceGarrFollRaceID;

        [DBFieldName("HordeGarrClassSpecID")]
        public int? HordeGarrClassSpecID;

        [DBFieldName("AllianceGarrClassSpecID")]
        public int? AllianceGarrClassSpecID;

        [DBFieldName("Quality")]
        public sbyte? Quality;

        [DBFieldName("FollowerLevel")]
        public byte? FollowerLevel;

        [DBFieldName("ItemLevelWeapon")]
        public ushort? ItemLevelWeapon;

        [DBFieldName("ItemLevelArmor")]
        public ushort? ItemLevelArmor;

        [DBFieldName("HordeSourceTypeEnum")]
        public sbyte? HordeSourceTypeEnum;

        [DBFieldName("AllianceSourceTypeEnum")]
        public sbyte? AllianceSourceTypeEnum;

        [DBFieldName("HordeIconFileDataID")]
        public int? HordeIconFileDataID;

        [DBFieldName("AllianceIconFileDataID")]
        public int? AllianceIconFileDataID;

        [DBFieldName("HordeGarrFollItemSetID")]
        public ushort? HordeGarrFollItemSetID;

        [DBFieldName("AllianceGarrFollItemSetID")]
        public ushort? AllianceGarrFollItemSetID;

        [DBFieldName("HordeUITextureKitID")]
        public ushort? HordeUITextureKitID;

        [DBFieldName("AllianceUITextureKitID")]
        public ushort? AllianceUITextureKitID;

        [DBFieldName("Vitality")]
        public byte? Vitality;

        [DBFieldName("HordeFlavorGarrStringID")]
        public byte? HordeFlavorGarrStringID;

        [DBFieldName("AllianceFlavorGarrStringID")]
        public byte? AllianceFlavorGarrStringID;

        [DBFieldName("HordeSlottingBroadcastTextID")]
        public uint? HordeSlottingBroadcastTextID;

        [DBFieldName("AllySlottingBroadcastTextID")]
        public uint? AllySlottingBroadcastTextID;

        [DBFieldName("ChrClassID")]
        public byte? ChrClassID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Gender")]
        public byte? Gender;

        [DBFieldName("AutoCombatantID")]
        public int? AutoCombatantID;

        [DBFieldName("CovenantID")]
        public int? CovenantID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_follower_locale")]
    public sealed record GarrFollowerLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("HordeSourceText_lang")]
        public string HordeSourceTextLang;

        [DBFieldName("AllianceSourceText_lang")]
        public string AllianceSourceTextLang;

        [DBFieldName("TitleName_lang")]
        public string TitleNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_follower")]
    public sealed record GarrFollowerHotfix1007 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("HordeSourceText")]
        public string HordeSourceText;

        [DBFieldName("AllianceSourceText")]
        public string AllianceSourceText;

        [DBFieldName("TitleName")]
        public string TitleName;

        [DBFieldName("GarrTypeID")]
        public sbyte? GarrTypeID;

        [DBFieldName("GarrFollowerTypeID")]
        public sbyte? GarrFollowerTypeID;

        [DBFieldName("HordeCreatureID")]
        public int? HordeCreatureID;

        [DBFieldName("AllianceCreatureID")]
        public int? AllianceCreatureID;

        [DBFieldName("HordeGarrFollRaceID")]
        public byte? HordeGarrFollRaceID;

        [DBFieldName("AllianceGarrFollRaceID")]
        public byte? AllianceGarrFollRaceID;

        [DBFieldName("HordeGarrClassSpecID")]
        public int? HordeGarrClassSpecID;

        [DBFieldName("AllianceGarrClassSpecID")]
        public int? AllianceGarrClassSpecID;

        [DBFieldName("Quality")]
        public sbyte? Quality;

        [DBFieldName("FollowerLevel")]
        public byte? FollowerLevel;

        [DBFieldName("ItemLevelWeapon")]
        public ushort? ItemLevelWeapon;

        [DBFieldName("ItemLevelArmor")]
        public ushort? ItemLevelArmor;

        [DBFieldName("HordeSourceTypeEnum")]
        public sbyte? HordeSourceTypeEnum;

        [DBFieldName("AllianceSourceTypeEnum")]
        public sbyte? AllianceSourceTypeEnum;

        [DBFieldName("HordeIconFileDataID")]
        public int? HordeIconFileDataID;

        [DBFieldName("AllianceIconFileDataID")]
        public int? AllianceIconFileDataID;

        [DBFieldName("HordeGarrFollItemSetID")]
        public ushort? HordeGarrFollItemSetID;

        [DBFieldName("AllianceGarrFollItemSetID")]
        public ushort? AllianceGarrFollItemSetID;

        [DBFieldName("HordeUITextureKitID")]
        public ushort? HordeUITextureKitID;

        [DBFieldName("AllianceUITextureKitID")]
        public ushort? AllianceUITextureKitID;

        [DBFieldName("Vitality")]
        public byte? Vitality;

        [DBFieldName("HordeFlavorGarrStringID")]
        public byte? HordeFlavorGarrStringID;

        [DBFieldName("AllianceFlavorGarrStringID")]
        public byte? AllianceFlavorGarrStringID;

        [DBFieldName("HordeSlottingBroadcastTextID")]
        public uint? HordeSlottingBroadcastTextID;

        [DBFieldName("AllySlottingBroadcastTextID")]
        public uint? AllySlottingBroadcastTextID;

        [DBFieldName("ChrClassID")]
        public byte? ChrClassID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Gender")]
        public byte? Gender;

        [DBFieldName("AutoCombatantID")]
        public int? AutoCombatantID;

        [DBFieldName("CovenantID")]
        public int? CovenantID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_follower_locale")]
    public sealed record GarrFollowerLocaleHotfix1007 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("HordeSourceText_lang")]
        public string HordeSourceTextLang;

        [DBFieldName("AllianceSourceText_lang")]
        public string AllianceSourceTextLang;

        [DBFieldName("TitleName_lang")]
        public string TitleNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_follower")]
    public sealed record GarrFollowerHotfix340: IDataModel
    {
        [DBFieldName("HordeSourceText")]
        public string HordeSourceText;

        [DBFieldName("AllianceSourceText")]
        public string AllianceSourceText;

        [DBFieldName("TitleName")]
        public string TitleName;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GarrTypeID")]
        public byte? GarrTypeID;

        [DBFieldName("GarrFollowerTypeID")]
        public byte? GarrFollowerTypeID;

        [DBFieldName("HordeCreatureID")]
        public int? HordeCreatureID;

        [DBFieldName("AllianceCreatureID")]
        public int? AllianceCreatureID;

        [DBFieldName("HordeGarrFollRaceID")]
        public byte? HordeGarrFollRaceID;

        [DBFieldName("AllianceGarrFollRaceID")]
        public byte? AllianceGarrFollRaceID;

        [DBFieldName("HordeGarrClassSpecID")]
        public byte? HordeGarrClassSpecID;

        [DBFieldName("AllianceGarrClassSpecID")]
        public byte? AllianceGarrClassSpecID;

        [DBFieldName("Quality")]
        public byte? Quality;

        [DBFieldName("FollowerLevel")]
        public byte? FollowerLevel;

        [DBFieldName("ItemLevelWeapon")]
        public ushort? ItemLevelWeapon;

        [DBFieldName("ItemLevelArmor")]
        public ushort? ItemLevelArmor;

        [DBFieldName("HordeSourceTypeEnum")]
        public sbyte? HordeSourceTypeEnum;

        [DBFieldName("AllianceSourceTypeEnum")]
        public sbyte? AllianceSourceTypeEnum;

        [DBFieldName("HordeIconFileDataID")]
        public int? HordeIconFileDataID;

        [DBFieldName("AllianceIconFileDataID")]
        public int? AllianceIconFileDataID;

        [DBFieldName("HordeGarrFollItemSetID")]
        public ushort? HordeGarrFollItemSetID;

        [DBFieldName("AllianceGarrFollItemSetID")]
        public ushort? AllianceGarrFollItemSetID;

        [DBFieldName("HordeUITextureKitID")]
        public ushort? HordeUITextureKitID;

        [DBFieldName("AllianceUITextureKitID")]
        public ushort? AllianceUITextureKitID;

        [DBFieldName("Vitality")]
        public byte? Vitality;

        [DBFieldName("HordeFlavorGarrStringID")]
        public byte? HordeFlavorGarrStringID;

        [DBFieldName("AllianceFlavorGarrStringID")]
        public byte? AllianceFlavorGarrStringID;

        [DBFieldName("HordeSlottingBroadcastTextID")]
        public uint? HordeSlottingBroadcastTextID;

        [DBFieldName("AllySlottingBroadcastTextID")]
        public uint? AllySlottingBroadcastTextID;

        [DBFieldName("ChrClassID")]
        public byte? ChrClassID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Gender")]
        public byte? Gender;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_follower_locale")]
    public sealed record GarrFollowerLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("HordeSourceText_lang")]
        public string HordeSourceTextLang;

        [DBFieldName("AllianceSourceText_lang")]
        public string AllianceSourceTextLang;

        [DBFieldName("TitleName_lang")]
        public string TitleNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
