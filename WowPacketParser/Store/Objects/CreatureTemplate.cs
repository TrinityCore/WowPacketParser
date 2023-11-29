using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public sealed record CreatureTemplate : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("Civilian", TargetedDatabaseFlag.AnyClassic)]
        public bool? Civilian;

        [DBFieldName("KillCredit", 2)]
        public uint?[] KillCredits;

        [DBFieldName("modelid", TargetedDatabaseFlag.TillWarlordsOfDraenor, 4)]
        public uint?[] ModelIDs;

        [DBFieldName("name", LocaleConstant.enUS, nullable: true)]
        public string Name;

        [DBFieldName("femaleName", TargetedDatabaseFlag.SinceCataclysm | TargetedDatabaseFlag.AnyClassic, LocaleConstant.enUS, nullable: true)]
        public string FemaleName;

        [DBFieldName("subname", LocaleConstant.enUS, nullable: true)]
        public string SubName;

        [DBFieldName("TitleAlt", TargetedDatabaseFlag.SinceWarlordsOfDraenor /*Mists of Pandaria*/ | TargetedDatabaseFlag.AnyClassic, LocaleConstant.enUS, nullable: true)]
        public string TitleAlt;

        [DBFieldName("IconName", nullable: true)]
        public string IconName;

        [DBFieldName("HealthScalingExpansion", TargetedDatabaseFlag.SinceWarlordsOfDraenorTillShadowLands | TargetedDatabaseFlag.Classic)]
        public ClientType? HealthScalingExpansion;

        [DBFieldName("RequiredExpansion", TargetedDatabaseFlag.SinceCataclysm | TargetedDatabaseFlag.AnyClassic)]
        public ClientType? RequiredExpansion;

        [DBFieldName("VignetteID", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.AnyClassic)]
        public uint? VignetteID;

        [DBFieldName("unit_class", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.AnyClassic)]
        public uint? UnitClass;

        [DBFieldName("FadeRegionRadius", TargetedDatabaseFlag.BattleForAzeroth)]
        public float? FadeRegionRadius;

        [DBFieldName("WidgetSetID", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.AnyClassic)]
        public int? WidgetSetID;

        [DBFieldName("WidgetSetUnitConditionID", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.AnyClassic)]
        public int? WidgetSetUnitConditionID;

        [DBFieldName("rank")]
        public CreatureRank? Rank;

        [DBFieldName("family")]
        public CreatureFamily? Family;

        [DBFieldName("type")]
        public CreatureType? Type;

        [DBFieldName("type_flags", TargetedDatabaseFlag.TillShadowlands)]
        public CreatureTypeFlag? TypeFlags;

        [DBFieldName("type_flags2", TargetedDatabaseFlag.Cataclysm | TargetedDatabaseFlag.SinceWarlordsOfDraenorTillShadowLands)]
        public uint? TypeFlags2;

        [DBFieldName("PetSpellDataId", TargetedDatabaseFlag.TillWrathOfTheLichKing | TargetedDatabaseFlag.AnyClassic)]
        public uint? PetSpellDataID;

        [DBFieldName("HealthModifier", TargetedDatabaseFlag.TillShadowlands)]
        public float? HealthModifier;

        [DBFieldName("ManaModifier", TargetedDatabaseFlag.TillShadowlands)]
        public float? ManaModifier;

        [DBFieldName("RacialLeader")]
        public bool? RacialLeader;

        [DBFieldName("movementId")]
        public uint? MovementID;

        [DBFieldName("CreatureDifficultyID", TargetedDatabaseFlag.Shadowlands)]
        public int? CreatureDifficultyID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_template")]
    public sealed record CreatureTemplateNonWDB : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("gossip_menu_id", TargetedDatabaseFlag.TillShadowlands)]
        public uint? GossipMenuId;

        [DBFieldName("minlevel", TargetedDatabaseFlag.TillShadowlands)]
        public int? MinLevel;

        [DBFieldName("maxlevel", TargetedDatabaseFlag.TillShadowlands)]
        public int? MaxLevel;

        [DBFieldName("faction")]
        public uint? Faction;

        [DBFieldName("npcflag")]
        public NPCFlags? NpcFlag;

        [DBFieldName("speed_walk")]
        public float? SpeedWalk;

        [DBFieldName("speed_run")]
        public float? SpeedRun;

        [DBFieldName("BaseAttackTime")]
        public uint? BaseAttackTime;

        [DBFieldName("RangeAttackTime")]
        public uint? RangedAttackTime;

        [DBFieldName("unit_class", TargetedDatabaseFlag.TillWarlordsOfDraenor)]
        public uint? UnitClass;

        [DBFieldName("unit_flags")]
        public UnitFlags? UnitFlags;

        [DBFieldName("unit_flags2")]
        public UnitFlags2? UnitFlags2;

        [DBFieldName("unit_flags3", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.AnyClassic)]
        public UnitFlags3? UnitFlags3;

        [DBFieldName("dynamicflags", TargetedDatabaseFlag.TillCataclysm)]
        public UnitDynamicFlags? DynamicFlags;

        [DBFieldName("dynamicflags", TargetedDatabaseFlag.SinceWarlordsOfDraenorTillShadowLands | TargetedDatabaseFlag.Classic)]
        public UnitDynamicFlagsWOD? DynamicFlagsWod;

        [DBFieldName("VehicleId")]
        public uint? VehicleID;

        [DBFieldName("HoverHeight", TargetedDatabaseFlag.TillShadowlands)]
        public float? HoverHeight;

    }

    [DBTableName("creature_questitem")]
    public sealed record CreatureTemplateQuestItem : IDataModel
    {
        [DBFieldName("CreatureEntry", true)]
        public uint? CreatureEntry;

        [DBFieldName("DifficultyID", TargetedDatabaseFlag.SinceDragonflight, true)]
        public uint? DifficultyID;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("ItemId")]
        public uint? ItemId;

        [DBFieldName("VerifiedBuild", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_template")]
    public sealed record CreatureTemplateClassic : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("KillCredit", 2)]
        public uint?[] KillCredits;

        [DBFieldName("ModelId", 4)]
        public uint?[] DisplayId;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("femaleName")]
        public string FemaleName;

        [DBFieldName("subname", nullable: true)]
        public string SubName;

        [DBFieldName("TitleAlt", nullable: true)]
        public string TitleAlt;

        [DBFieldName("IconName", nullable: true)]
        public string IconName;

        [DBFieldName("HealthScalingExpansion")]
        public ClientType? HealthScalingExpansion;

        [DBFieldName("RequiredExpansion")]
        public ClientType? RequiredExpansion;

        [DBFieldName("VignetteID")]
        public uint? VignetteID;

        [DBFieldName("unit_class")]
        public uint? UnitClass;

        [DBFieldName("rank")]
        public CreatureRank? Rank;

        [DBFieldName("family")]
        public CreatureFamily? Family;

        [DBFieldName("type")]
        public CreatureType? Type;

        [DBFieldName("type_flags")]
        public CreatureTypeFlag? TypeFlags;

        [DBFieldName("type_flags2")]
        public uint? TypeFlags2;

        [DBFieldName("PetSpellDataId")]
        public uint? PetSpellDataID;

        [DBFieldName("HealthModifier")]
        public float? HealthModifier;

        [DBFieldName("ManaModifier")]
        public float? ManaModifier;

        [DBFieldName("Civilian")]
        public bool? Civilian;

        [DBFieldName("RacialLeader")]
        public bool? RacialLeader;

        [DBFieldName("movementId")]
        public uint? MovementID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
