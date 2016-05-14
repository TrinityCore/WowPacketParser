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
        public uint?[] DifficultyEntries;

        [DBFieldName("KillCredit", 2)]
        public uint?[] KillCredits;

        [DBFieldName("modelid", 4)]
        public uint?[] ModelIDs;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("femaleName", TargetedDatabase.Cataclysm)]
        public string FemaleName;

        [DBFieldName("subname")]
        public string SubName;

        [DBFieldName("IconName")]
        public string IconName;

        [DBFieldName("gossip_menu_id")]
        public uint? GossipMenuID;

        [DBFieldName("minlevel")]
        public int? MinLevel;

        [DBFieldName("maxlevel")]
        public int? MaxLevel;

        [DBFieldName("exp")]
        public ClientType? Expansion;

        [DBFieldName("exp_unk", TargetedDatabase.Cataclysm)]
        public ClientType? ExpUnk;

        [DBFieldName("faction")]
        public uint? Faction;

        [DBFieldName("npcflag")]
        public NPCFlags? NpcFlag;

        [DBFieldName("speed_walk")]
        public float? SpeedWalk;

        [DBFieldName("speed_run")]
        public float? SpeedRun;

        [DBFieldName("scale")]
        public float? Scale;

        [DBFieldName("rank")]
        public CreatureRank? Rank;

        [DBFieldName("dmgschool")]
        public uint? DmgSchool;

        [DBFieldName("BaseAttackTime")]
        public uint? BaseAttackTime;

        [DBFieldName("RangeAttackTime")]
        public uint? RangeAttackTime;

        [DBFieldName("BaseVariance")]
        public float? BaseVariance;

        [DBFieldName("RangeVariance")]
        public float? RangeVariance;

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
        public TrainerType? TrainerType;

        [DBFieldName("trainer_spell", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? TrainerSpell;

        [DBFieldName("trainer_class")]
        public Class? TrainerClass;

        [DBFieldName("trainer_race")]
        public Race? TrainerRace;

        [DBFieldName("type")]
        public CreatureType? Type;

        [DBFieldName("type_flags")]
        public CreatureTypeFlag? TypeFlags;

        [DBFieldName("type_flags2", TargetedDatabase.Cataclysm)]
        public uint? TypeFlags2;

        [DBFieldName("lootid")]
        public uint? LootID;

        [DBFieldName("pickpocketloot")]
        public uint? PickPocketLoot;

        [DBFieldName("skinloot")]
        public uint? SkinLoot;

        [DBFieldName("resistance", 6)]
        public uint?[] Resistances;

        [DBFieldName("spell", 8)]
        public uint?[] Spells;

        [DBFieldName("PetSpellDataId", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? PetSpellDataID;

        [DBFieldName("VehicleId")]
        public uint? VehicleID;

        [DBFieldName("mingold")]
        public uint? MinGold;

        [DBFieldName("maxgold")]
        public uint? MaxGold;

        [DBFieldName("AIName")]
        public string AIName;

        [DBFieldName("MovementType")]
        public uint? MovementType;

        [DBFieldName("InhabitType")]
        public InhabitType? InhabitType;

        [DBFieldName("HoverHeight")]
        public float? HoverHeight;

        [DBFieldName("HealthModifier")]
        public float? HealthModifier;

        [DBFieldName("HealthModifierExtra", TargetedDatabase.Cataclysm)]
        public float? HealthModifierExtra;

        [DBFieldName("ManaModifier")]
        public float? ManaModifier;

        [DBFieldName("ManaModifierExtra", TargetedDatabase.Cataclysm)]
        public float? ManaModifierExtra;

        [DBFieldName("ArmorModifier")]
        public float? ArmorModifier;

        [DBFieldName("DamageModifier")]
        public float? DamageModifier;

        [DBFieldName("ExperienceModifier")]
        public float? ExperienceModifier;

        [DBFieldName("RacialLeader")]
        public bool? RacialLeader;

        [DBFieldName("movementId")]
        public uint? MovementID;

        [DBFieldName("RegenHealth")]
        public uint? RegenHealth;

        [DBFieldName("mechanic_immune_mask")]
        public uint? MechanicImmuneMask;

        [DBFieldName("flags_extra")]
        public uint? FlagsExtra;

        [DBFieldName("ScriptName")]
        public string ScriptName;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
