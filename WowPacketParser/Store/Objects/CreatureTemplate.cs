using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public sealed class CreatureTemplate : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("difficulty_entry_", 3)]
        public uint?[] DifficultyEntries = { 0, 0, 0 };

        [DBFieldName("KillCredit", 2)]
        public uint?[] KillCredits;

        [DBFieldName("modelid", 4)]
        public uint?[] ModelIDs;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("femaleName", TargetedDatabase.Cataclysm)]
        public string FemaleName = "";

        [DBFieldName("subname")]
        public string SubName;

        [DBFieldName("IconName")]
        public string IconName;

        [DBFieldName("gossip_menu_id")]
        public uint? GossipMenuID = 0;

        [DBFieldName("minlevel")]
        public int? MinLevel;

        [DBFieldName("maxlevel")]
        public int? MaxLevel;

        [DBFieldName("exp")]
        public ClientType? Expansion;

        [DBFieldName("HealthScalingExpansion", TargetedDatabase.WarlordsOfDraenor)]
        public ClientType? HealthScalingExpansion;

        [DBFieldName("RequiredExpansion", TargetedDatabase.Cataclysm)]
        public ClientType? RequiredExpansion;

        [DBFieldName("VignetteID", TargetedDatabase.Legion)]
        public uint? VignetteID;

        [DBFieldName("faction")]
        public uint? Faction;

        [DBFieldName("npcflag")]
        public NPCFlags? NpcFlag;

        [DBFieldName("speed_walk")]
        public float? SpeedWalk;

        [DBFieldName("speed_run")]
        public float? SpeedRun;

        [DBFieldName("scale")]
        public float? Scale = 1;

        [DBFieldName("rank")]
        public CreatureRank? Rank;

        [DBFieldName("dmgschool")]
        public uint? DmgSchool = 0;

        [DBFieldName("BaseAttackTime")]
        public uint? BaseAttackTime;

        [DBFieldName("RangeAttackTime")]
        public uint? RangeAttackTime;

        [DBFieldName("BaseVariance")]
        public float? BaseVariance = 1;

        [DBFieldName("RangeVariance")]
        public float? RangeVariance = 1;

        [DBFieldName("unit_class")]
        public uint? UnitClass;

        [DBFieldName("unit_flags")]
        public UnitFlags? UnitFlags;

        [DBFieldName("unit_flags2")]
        public UnitFlags2? UnitFlags2;

        [DBFieldName("dynamicflags", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public UnitDynamicFlags? DynamicFlags;

        [DBFieldName("dynamicflags", TargetedDatabase.WarlordsOfDraenor)]
        public UnitDynamicFlagsWOD? DynamicFlagsWod;

        [DBFieldName("family")]
        public CreatureFamily? Family;

        [DBFieldName("trainer_type")]
        public TrainerType? TrainerType = Enums.TrainerType.Class;

        [DBFieldName("trainer_spell", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? TrainerSpell = 0;

        [DBFieldName("trainer_class")]
        public Class? TrainerClass;

        [DBFieldName("trainer_race")]
        public Race? TrainerRace = 0;

        [DBFieldName("type")]
        public CreatureType? Type;

        [DBFieldName("type_flags")]
        public CreatureTypeFlag? TypeFlags;

        [DBFieldName("type_flags2", TargetedDatabase.Cataclysm)]
        public uint? TypeFlags2;

        [DBFieldName("lootid")]
        public uint? LootID = 0;

        [DBFieldName("pickpocketloot")]
        public uint? PickPocketLoot = 0;

        [DBFieldName("skinloot")]
        public uint? SkinLoot = 0;

        [DBFieldName("resistance", 6)]
        public short?[] Resistances = { 0, 0, 0, 0, 0, 0 };

        [DBFieldName("spell", 8)]
        public uint?[] Spells = { 0, 0, 0, 0, 0, 0, 0, 0 };

        [DBFieldName("PetSpellDataId", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? PetSpellDataID;

        [DBFieldName("VehicleId")]
        public uint? VehicleID;

        [DBFieldName("mingold")]
        public uint? MinGold;

        [DBFieldName("maxgold")]
        public uint? MaxGold;

        [DBFieldName("AIName")]
        public string AIName = "";

        [DBFieldName("MovementType")]
        public uint? MovementType;

        [DBFieldName("InhabitType")]
        public InhabitType? InhabitType;

        [DBFieldName("HoverHeight")]
        public float? HoverHeight;

        [DBFieldName("HealthModifier")]
        public float? HealthModifier;

        [DBFieldName("HealthModifierExtra", TargetedDatabase.Cataclysm)]
        public float? HealthModifierExtra = 1;

        [DBFieldName("ManaModifier")]
        public float? ManaModifier;

        [DBFieldName("ManaModifierExtra", TargetedDatabase.Cataclysm)]
        public float? ManaModifierExtra = 1;

        [DBFieldName("ArmorModifier")]
        public float? ArmorModifier = 1;

        [DBFieldName("DamageModifier")]
        public float? DamageModifier;

        [DBFieldName("ExperienceModifier")]
        public float? ExperienceModifier = 1;

        [DBFieldName("RacialLeader")]
        public bool? RacialLeader;

        [DBFieldName("movementId")]
        public uint? MovementID;

        [DBFieldName("RegenHealth")]
        public uint? RegenHealth = 1;

        [DBFieldName("mechanic_immune_mask")]
        public uint? MechanicImmuneMask = 1;

        [DBFieldName("flags_extra")]
        public uint? FlagsExtra = 1;

        [DBFieldName("ScriptName")]
        public string ScriptName = "";

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
