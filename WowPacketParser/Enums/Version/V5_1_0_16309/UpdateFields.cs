namespace WowPacketParser.Enums.Version.V5_1_0_16309
{
    // ReSharper disable InconsistentNaming
    // 5.1.0.16309
    public enum ObjectField
    {
        OBJECT_FIELD_GUID    = 0x0000,
        OBJECT_FIELD_DATA    = 0x0002,
        OBJECT_FIELD_TYPE    = 0x0004,
        OBJECT_FIELD_ENTRY   = 0x0005,
        OBJECT_FIELD_SCALE_X = 0x0006,
        OBJECT_END           = 0x0007
    };

    public enum ItemField
    {
        ITEM_FIELD_OWNER                                    = ObjectField.OBJECT_END + 0x0000,
        ITEM_FIELD_CONTAINED_IN                             = ObjectField.OBJECT_END + 0x0002,
        ITEM_FIELD_CREATOR                                  = ObjectField.OBJECT_END + 0x0004,
        ITEM_FIELD_GIFT_CREATOR                             = ObjectField.OBJECT_END + 0x0006,
        ITEM_FIELD_STACK_COUNT                              = ObjectField.OBJECT_END + 0x0008,
        ITEM_FIELD_EXPIRATION                               = ObjectField.OBJECT_END + 0x0009,
        ITEM_FIELD_SPELL_CHARGES                            = ObjectField.OBJECT_END + 0x000A,
        ITEM_FIELD_DYNAMIC_FLAGS                            = ObjectField.OBJECT_END + 0x000F,
        ITEM_FIELD_ENCHANTMENT                              = ObjectField.OBJECT_END + 0x0010,
        ITEM_FIELD_PROPERTY_SEED                            = ObjectField.OBJECT_END + 0x0037,
        ITEM_FIELD_RANDOM_PROPERTIES_ID                     = ObjectField.OBJECT_END + 0x0038,
        ITEM_FIELD_DURABILITY                               = ObjectField.OBJECT_END + 0x0039,
        ITEM_FIELD_MAX_DURABILITY                           = ObjectField.OBJECT_END + 0x003A,
        ITEM_FIELD_CREATE_PLAYED_TIME                       = ObjectField.OBJECT_END + 0x003B,
        ITEM_FIELD_MODIFIERS_MASK                           = ObjectField.OBJECT_END + 0x003C,
        ITEM_END                                            = ObjectField.OBJECT_END + 0x003D
    };

    public enum ItemDynamicField
    {
        ITEM_DYNAMIC_FIELD_MODIFIERS                        = ItemField.ITEM_END + 0x0,
        ITEM_DYNAMIC_END                                    = ItemField.ITEM_END + 0x48
    }

    public enum ContainerField
    {
        CONTAINER_FIELD_SLOTS                               = ItemField.ITEM_END + 0x0000,
        CONTAINER_FIELD_NUM_SLOTS                           = ItemField.ITEM_END + 0x0048,
        CONTAINER_END                                       = ItemField.ITEM_END + 0x0049
    };

    public enum UnitField
    {
        UNIT_FIELD_CHARM                                    = ObjectField.OBJECT_END + 0x0000,
        UNIT_FIELD_SUMMON                                   = ObjectField.OBJECT_END + 0x0002,
        UNIT_FIELD_CRITTER                                  = ObjectField.OBJECT_END + 0x0004,
        UNIT_FIELD_CHARMEDBY                                = ObjectField.OBJECT_END + 0x0006,
        UNIT_FIELD_SUMMONEDBY                               = ObjectField.OBJECT_END + 0x0008,
        UNIT_FIELD_CREATEDBY                                = ObjectField.OBJECT_END + 0x000A,
        UNIT_FIELD_TARGET                                   = ObjectField.OBJECT_END + 0x000C,
        UNIT_FIELD_CHANNEL_OBJECT                           = ObjectField.OBJECT_END + 0x000E,
        UNIT_FIELD_SUMMONED_BY_HOME_REALM                   = ObjectField.OBJECT_END + 0x0010,
        UNIT_FIELD_CHANNEL_SPELL                            = ObjectField.OBJECT_END + 0x0011,
        UNIT_FIELD_DISPLAY_POWER                            = ObjectField.OBJECT_END + 0x0012,
        UNIT_FIELD_OVERRIDE_DISPLAY_POWER_ID                = ObjectField.OBJECT_END + 0x0013,
        UNIT_FIELD_HEALTH                                   = ObjectField.OBJECT_END + 0x0014,
        UNIT_FIELD_POWER                                    = ObjectField.OBJECT_END + 0x0015,
        UNIT_FIELD_MAXHEALTH                                = ObjectField.OBJECT_END + 0x001A,
        UNIT_FIELD_MAXPOWER                                 = ObjectField.OBJECT_END + 0x001B,
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER                = ObjectField.OBJECT_END + 0x0020,
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER    = ObjectField.OBJECT_END + 0x0025,
        UNIT_FIELD_LEVEL                                    = ObjectField.OBJECT_END + 0x002A,
        UNIT_FIELD_FACTIONTEMPLATE                          = ObjectField.OBJECT_END + 0x002B,
        UNIT_FIELD_VIRTUAL_ITEM_ID1                         = ObjectField.OBJECT_END + 0x002C,
        UNIT_FIELD_VIRTUAL_ITEM_ID2                         = ObjectField.OBJECT_END + 0x002D,
        UNIT_FIELD_VIRTUAL_ITEM_ID3                         = ObjectField.OBJECT_END + 0x002E,
        UNIT_FIELD_FLAGS                                    = ObjectField.OBJECT_END + 0x002F,
        UNIT_FIELD_FLAGS_2                                  = ObjectField.OBJECT_END + 0x0030,
        UNIT_FIELD_AURASTATE                                = ObjectField.OBJECT_END + 0x0031,
        UNIT_FIELD_BASEATTACKTIME                           = ObjectField.OBJECT_END + 0x0032,
        UNIT_FIELD_RANGEDATTACKTIME                         = ObjectField.OBJECT_END + 0x0034,
        UNIT_FIELD_BOUNDINGRADIUS                           = ObjectField.OBJECT_END + 0x0035,
        UNIT_FIELD_COMBATREACH                              = ObjectField.OBJECT_END + 0x0036,
        UNIT_FIELD_DISPLAYID                                = ObjectField.OBJECT_END + 0x0037,
        UNIT_FIELD_NATIVEDISPLAYID                          = ObjectField.OBJECT_END + 0x0038,
        UNIT_FIELD_MOUNTDISPLAYID                           = ObjectField.OBJECT_END + 0x0039,
        UNIT_FIELD_MIN_DAMAGE                               = ObjectField.OBJECT_END + 0x003A,
        UNIT_FIELD_MAX_DAMAGE                               = ObjectField.OBJECT_END + 0x003B,
        UNIT_FIELD_MIN_OFFHAND_DAMAGE                       = ObjectField.OBJECT_END + 0x003C,
        UNIT_FIELD_MAX_OFFHAND_DAMAGE                       = ObjectField.OBJECT_END + 0x003D,
        UNIT_FIELD_ANIMTIER                                 = ObjectField.OBJECT_END + 0x003E,
        UNIT_FIELD_PET_NUMBER                               = ObjectField.OBJECT_END + 0x003F,
        UNIT_FIELD_PET_NAME_TIMESTAMP                       = ObjectField.OBJECT_END + 0x0040,
        UNIT_FIELD_PET_EXPERIENCE                           = ObjectField.OBJECT_END + 0x0041,
        UNIT_FIELD_PET_NEXTLEVEL_EXPERIENCE                 = ObjectField.OBJECT_END + 0x0042,
        UNIT_DYNAMIC_FLAGS                                  = ObjectField.OBJECT_END + 0x0043,
        UNIT_FIELD_MOD_CASTING_SPEED                        = ObjectField.OBJECT_END + 0x0044,
        UNIT_FIELD_MOD_SPELL_HASTE                          = ObjectField.OBJECT_END + 0x0045,
        UNIT_FIELD_MOD_HASTE                                = ObjectField.OBJECT_END + 0x0046,
        UNIT_FIELD_MOD_HASTE_REGEN                          = ObjectField.OBJECT_END + 0x0047,
        UNIT_CREATED_BY_SPELL                               = ObjectField.OBJECT_END + 0x0048,
        UNIT_NPC_FLAGS                                      = ObjectField.OBJECT_END + 0x0049,
        UNIT_NPC_EMOTESTATE                                 = ObjectField.OBJECT_END + 0x004B,
        UNIT_FIELD_STATS                                    = ObjectField.OBJECT_END + 0x004C,
        UNIT_FIELD_STAT_POSBUFF                             = ObjectField.OBJECT_END + 0x0051,
        UNIT_FIELD_STAT_NEGBUFF                             = ObjectField.OBJECT_END + 0x0056,
        UNIT_FIELD_RESISTANCES                              = ObjectField.OBJECT_END + 0x005B,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_POSITIVE            = ObjectField.OBJECT_END + 0x0062,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_NEGATIVE            = ObjectField.OBJECT_END + 0x0069,
        UNIT_FIELD_BASE_MANA                                = ObjectField.OBJECT_END + 0x0070,
        UNIT_FIELD_BASE_HEALTH                              = ObjectField.OBJECT_END + 0x0071,
        UNIT_FIELD_SHAPESHIFT_FORM                          = ObjectField.OBJECT_END + 0x0072,
        UNIT_FIELD_ATTACK_POWER                             = ObjectField.OBJECT_END + 0x0073,
        UNIT_FIELD_ATTACK_POWER_MOD_POS                     = ObjectField.OBJECT_END + 0x0074,
        UNIT_FIELD_ATTACK_POWER_MOD_NEG                     = ObjectField.OBJECT_END + 0x0075,
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER                  = ObjectField.OBJECT_END + 0x0076,
        UNIT_FIELD_RANGED_ATTACK_POWER                      = ObjectField.OBJECT_END + 0x0077,
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_POS              = ObjectField.OBJECT_END + 0x0078,
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_NEG              = ObjectField.OBJECT_END + 0x0079,
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER           = ObjectField.OBJECT_END + 0x007A,
        UNIT_FIELD_MIN_RANGED_DAMAGE                        = ObjectField.OBJECT_END + 0x007B,
        UNIT_FIELD_MAX_RANGED_DAMAGE                        = ObjectField.OBJECT_END + 0x007C,
        UNIT_FIELD_POWER_COST_MODIFIER                      = ObjectField.OBJECT_END + 0x007D,
        UNIT_FIELD_POWER_COST_MULTIPLIER                    = ObjectField.OBJECT_END + 0x0084,
        UNIT_FIELD_MAX_HEALTH_MODIFIER                      = ObjectField.OBJECT_END + 0x008B,
        UNIT_FIELD_HOVERHEIGHT                              = ObjectField.OBJECT_END + 0x008C,
        UNIT_FIELD_MIN_ITEM_LEVEL                           = ObjectField.OBJECT_END + 0x008D,
        UNIT_FIELD_MAX_ITEM_LEVEL                           = ObjectField.OBJECT_END + 0x008E,
        UNIT_FIELD_WILD_BATTLEPET_LEVEL                     = ObjectField.OBJECT_END + 0x008F,
        UNIT_FIELD_BATTLEPET_COMPANION_GUID                 = ObjectField.OBJECT_END + 0x0090,
        UNIT_FIELD_BATTLEPET_COMPANION_NAME_TIMESTAMP       = ObjectField.OBJECT_END + 0x0092,
        UNIT_FIELD_END                                      = ObjectField.OBJECT_END + 0x0093
    };

    public enum UnitDynamicField
    {
        UNIT_DYNAMIC_FIELD_PASSIVE_SPELLS                   = ObjectField.OBJECT_END + 0x0,
        UNIT_DYNAMIC_END                                    = ObjectField.OBJECT_END + 0x101
    }

    public enum PlayerField
    {
        PLAYER_FIELD_DUEL_ARBITER                           = UnitField.UNIT_FIELD_END + 0x0,
        PLAYER_FIELD_FLAGS                                  = UnitField.UNIT_FIELD_END + 0x2,
        PLAYER_FIELD_GUILD_RANK_ID                          = UnitField.UNIT_FIELD_END + 0x3,
        PLAYER_FIELD_GUILD_DELETE_DATE                      = UnitField.UNIT_FIELD_END + 0x4,
        PLAYER_FIELD_GUILD_LEVEL                            = UnitField.UNIT_FIELD_END + 0x5,
        PLAYER_FIELD_HAIR_COLOR_ID                          = UnitField.UNIT_FIELD_END + 0x6,
        PLAYER_FIELD_REST_STATE                             = UnitField.UNIT_FIELD_END + 0x7,
        PLAYER_FIELD_ARENA_FACTION                          = UnitField.UNIT_FIELD_END + 0x8,
        PLAYER_FIELD_DUEL_TEAM                              = UnitField.UNIT_FIELD_END + 0x9,
        PLAYER_FIELD_GUILD_TIMESTAMP                        = UnitField.UNIT_FIELD_END + 0xA,
        PLAYER_FIELD_QUESTLOG                               = UnitField.UNIT_FIELD_END + 0xB,
        PLAYER_FIELD_VISIBLE_ITEMS                          = UnitField.UNIT_FIELD_END + 0x02F9,
        PLAYER_FIELD_PLAYER_TITLE                           = UnitField.UNIT_FIELD_END + 0x031F,
        PLAYER_FIELD_FAKE_INEBRIATION                       = UnitField.UNIT_FIELD_END + 0x0320,
        PLAYER_FIELD_HOME_PLAYER_REALM                      = UnitField.UNIT_FIELD_END + 0x0321,
        PLAYER_FIELD_CURRENT_SPEC_ID                        = UnitField.UNIT_FIELD_END + 0x0322,
        PLAYER_FIELD_TAXI_MOUNT_ANIM_KIT_ID                 = UnitField.UNIT_FIELD_END + 0x0323,
        PLAYER_FIELD_CURRENT_BATTLE_PET_BREED_QUALITY       = UnitField.UNIT_FIELD_END + 0x0324,
        PLAYER_FIELD_INV_SLOTS                              = UnitField.UNIT_FIELD_END + 0x0325,
        PLAYER_FIELD_FARSIGHT_OBJECT                        = UnitField.UNIT_FIELD_END + 0x03D1,
        PLAYER_FIELD_KNOWN_TITLES                           = UnitField.UNIT_FIELD_END + 0x03D3,
        PLAYER_FIELD_COINAGE                                = UnitField.UNIT_FIELD_END + 0x03DB,
        PLAYER_FIELD_XP                                     = UnitField.UNIT_FIELD_END + 0x03DD,
        PLAYER_FIELD_NEXT_LEVEL_XP                          = UnitField.UNIT_FIELD_END + 0x03DE,
        PLAYER_FIELD_SKILL                                  = UnitField.UNIT_FIELD_END + 0x03DF,
        PLAYER_FIELD_CHARACTER_POINTS                       = UnitField.UNIT_FIELD_END + 0x059F,
        PLAYER_FIELD_MAX_TALENT_TIERS                       = UnitField.UNIT_FIELD_END + 0x05A0,
        PLAYER_FIELD_TRACK_CREATURE_MASK                    = UnitField.UNIT_FIELD_END + 0x05A1,
        PLAYER_FIELD_TRACK_RESOURCE_MASK                    = UnitField.UNIT_FIELD_END + 0x05A2,
        PLAYER_FIELD_EXPERTISE                              = UnitField.UNIT_FIELD_END + 0x05A3,
        PLAYER_FIELD_OFFHAND_EXPERTISE                      = UnitField.UNIT_FIELD_END + 0x05A4,
        PLAYER_FIELD_RANGED_EXPERTISE                       = UnitField.UNIT_FIELD_END + 0x05A5,
        PLAYER_FIELD_BLOCK_PERCENTAGE                       = UnitField.UNIT_FIELD_END + 0x05A6,
        PLAYER_FIELD_DODGE_PERCENTAGE                       = UnitField.UNIT_FIELD_END + 0x05A7,
        PLAYER_FIELD_PARRY_PERCENTAGE                       = UnitField.UNIT_FIELD_END + 0x05A8,
        PLAYER_FIELD_CRIT_PERCENTAGE                        = UnitField.UNIT_FIELD_END + 0x05A9,
        PLAYER_FIELD_RANGED_CRIT_PERCENTAGE                 = UnitField.UNIT_FIELD_END + 0x05AA,
        PLAYER_FIELD_OFFHAND_CRIT_PERCENTAGE                = UnitField.UNIT_FIELD_END + 0x05AB,
        PLAYER_FIELD_SPELL_CRIT_PERCENTAGE                  = UnitField.UNIT_FIELD_END + 0x05AC,
        PLAYER_FIELD_SHIELD_BLOCK                           = UnitField.UNIT_FIELD_END + 0x05B3,
        PLAYER_FIELD_SHIELD_BLOCK_CRIT_PERCENTAGE           = UnitField.UNIT_FIELD_END + 0x05B4,
        PLAYER_FIELD_MASTERY                                = UnitField.UNIT_FIELD_END + 0x05B5,
        PLAYER_FIELD_PVP_POWER_DAMAGE                       = UnitField.UNIT_FIELD_END + 0x05B6,
        PLAYER_FIELD_PVP_POWER_HEALING                      = UnitField.UNIT_FIELD_END + 0x05B7,
        PLAYER_FIELD_EXPLORED_ZONES                         = UnitField.UNIT_FIELD_END + 0x05B8,
        PLAYER_FIELD_REST_STATE_BONUS_POOL                  = UnitField.UNIT_FIELD_END + 0x0680,
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS                    = UnitField.UNIT_FIELD_END + 0x0681,
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG                    = UnitField.UNIT_FIELD_END + 0x0688,
        PLAYER_FIELD_MOD_DAMAGE_DONE_PERCENT                = UnitField.UNIT_FIELD_END + 0x068F,
        PLAYER_FIELD_MOD_HEALING_DONE_POS                   = UnitField.UNIT_FIELD_END + 0x0696,
        PLAYER_FIELD_MOD_HEALING_PERCENT                    = UnitField.UNIT_FIELD_END + 0x0697,
        PLAYER_FIELD_MOD_HEALING_DONE_PERCENT               = UnitField.UNIT_FIELD_END + 0x0698,
        PLAYER_FIELD_MOD_PERIODIC_HEALING_DONE_PERCENT      = UnitField.UNIT_FIELD_END + 0x0699,
        PLAYER_FIELD_WEAPON_DMG_MULTIPLIERS                 = UnitField.UNIT_FIELD_END + 0x069A,
        PLAYER_FIELD_MOD_SPELL_POWER_PERCENT                = UnitField.UNIT_FIELD_END + 0x069D,
        PLAYER_FIELD_MOD_RESILIENCE_PERCENT                 = UnitField.UNIT_FIELD_END + 0x069E,
        PLAYER_FIELD_OVERRIDE_SPELL_POWER_BY_AP_PERCENT     = UnitField.UNIT_FIELD_END + 0x069F,
        PLAYER_FIELD_OVERRIDE_AP_BY_SPELL_POWER_PERCENT     = UnitField.UNIT_FIELD_END + 0x06A0,
        PLAYER_FIELD_MOD_TARGET_RESISTANCE                  = UnitField.UNIT_FIELD_END + 0x06A1,
        PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE         = UnitField.UNIT_FIELD_END + 0x06A2,
        PLAYER_FIELD_LIFETIME_MAX_RANK                      = UnitField.UNIT_FIELD_END + 0x06A3,
        PLAYER_FIELD_SELF_RES_SPELL                         = UnitField.UNIT_FIELD_END + 0x06A4,
        PLAYER_FIELD_PVP_MEDALS                             = UnitField.UNIT_FIELD_END + 0x06A5,
        PLAYER_FIELD_BUYBACK_PRICE                          = UnitField.UNIT_FIELD_END + 0x06A6,
        PLAYER_FIELD_BUYBACK_TIMESTAMP                      = UnitField.UNIT_FIELD_END + 0x06B2,
        PLAYER_FIELD_YESTERDAY_HONORABLE_KILLS              = UnitField.UNIT_FIELD_END + 0x06BE,
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS               = UnitField.UNIT_FIELD_END + 0x06BF,
        PLAYER_FIELD_WATCHED_FACTION_INDEX                  = UnitField.UNIT_FIELD_END + 0x06C0,
        PLAYER_FIELD_COMBAT_RATINGS                         = UnitField.UNIT_FIELD_END + 0x06C1,
        PLAYER_FIELD_ARENA_TEAMS                            = UnitField.UNIT_FIELD_END + 0x06DC,
        PLAYER_FIELD_BATTLEGROUND_RATING                    = UnitField.UNIT_FIELD_END + 0x06F1,
        PLAYER_FIELD_MAXLEVEL                               = UnitField.UNIT_FIELD_END + 0x06F2,
        PLAYER_FIELD_RUNEREGEN                              = UnitField.UNIT_FIELD_END + 0x06F3,
        PLAYER_FIELD_NO_REAGENT_COST_MASK                   = UnitField.UNIT_FIELD_END + 0x06F7,
        PLAYER_FIELD_GLYPH_SLOTS                            = UnitField.UNIT_FIELD_END + 0x06FB,
        PLAYER_FIELD_GLYPHS                                 = UnitField.UNIT_FIELD_END + 0x0701,
        PLAYER_FIELD_GLYPH_SLOTS_ENABLED                    = UnitField.UNIT_FIELD_END + 0x0707,
        PLAYER_FIELD_PET_SPELL_POWER                        = UnitField.UNIT_FIELD_END + 0x0708,
        PLAYER_FIELD_RESEARCHING                            = UnitField.UNIT_FIELD_END + 0x0709,
        PLAYER_FIELD_PROFESSION_SKILL_LINE                  = UnitField.UNIT_FIELD_END + 0x0711,
        PLAYER_FIELD_UI_HIT_MODIFIER                        = UnitField.UNIT_FIELD_END + 0x0713,
        PLAYER_FIELD_UI_SPELL_HIT_MODIFIER                  = UnitField.UNIT_FIELD_END + 0x0714,
        PLAYER_FIELD_HOME_REALM_TIME_OFFSET                 = UnitField.UNIT_FIELD_END + 0x0715,
        PLAYER_FIELD_MOD_RANGED_HASTE                       = UnitField.UNIT_FIELD_END + 0x0716,
        PLAYER_FIELD_MOD_PET_HASTE                          = UnitField.UNIT_FIELD_END + 0x0717,
        PLAYER_FIELD_SUMMONED_BATTLE_PET_ID                 = UnitField.UNIT_FIELD_END + 0x0718,
        PLAYER_FIELD_OVERRIDE_SPELLS_ID                     = UnitField.UNIT_FIELD_END + 0x071A,
        PLAYER_FIELD_END                                    = UnitField.UNIT_FIELD_END + 0x071B
    };

    public enum PlayerDynamicField
    {
        PLAYER_DYNAMIC_FIELD_RESEARCH_SITES                 = PlayerField.PLAYER_FIELD_END + 0x0,
        PLAYER_DYNAMIC_FIELD_DAILY_QUESTS_COMPLETED         = PlayerField.PLAYER_FIELD_END + 0x2,
        PLAYER_DYNAMIC_END                                  = PlayerField.PLAYER_FIELD_END + 0x4
    }

    public enum GameObjectField
    {
        GAMEOBJECT_FIELD_CREATED_BY                         = ObjectField.OBJECT_END + 0x0,
        GAMEOBJECT_DISPLAYID                                = ObjectField.OBJECT_END + 0x2,
        GAMEOBJECT_FLAGS                                    = ObjectField.OBJECT_END + 0x3,
        GAMEOBJECT_PARENTROTATION                           = ObjectField.OBJECT_END + 0x4,
        GAMEOBJECT_FIELD_ANIM_PROGRESS                      = ObjectField.OBJECT_END + 0x8,
        GAMEOBJECT_FACTION                                  = ObjectField.OBJECT_END + 0x9,
        GAMEOBJECT_LEVEL                                    = ObjectField.OBJECT_END + 0xA,
        GAMEOBJECT_BYTES_1                                  = ObjectField.OBJECT_END + 0xB,
        GAMEOBJECT_FIELD_END                                = ObjectField.OBJECT_END + 0xC
    };

    public enum DynamicObjectField
    {
        DYNAMICOBJECT_FIELD_CASTER                          = ObjectField.OBJECT_END + 0x0,
        DYNAMICOBJECT_FIELD_TYPE_AND_VISUAL_ID              = ObjectField.OBJECT_END + 0x2,
        DYNAMICOBJECT_FIELD_SPELLID                         = ObjectField.OBJECT_END + 0x3,
        DYNAMICOBJECT_FIELD_RADIUS                          = ObjectField.OBJECT_END + 0x4,
        DYNAMICOBJECT_FIELD_CASTTIME                        = ObjectField.OBJECT_END + 0x5,
        DYNAMICOBJECT_FIELD_END                             = ObjectField.OBJECT_END + 0x6
    };

    public enum CorpseField
    {
        CORPSE_FIELD_OWNER                                  = ObjectField.OBJECT_END + 0x0,
        CORPSE_FIELD_PARTY_GUID                             = ObjectField.OBJECT_END + 0x2,
        CORPSE_FIELD_DISPLAYID                              = ObjectField.OBJECT_END + 0x4,
        CORPSE_FIELD_ITEMS                                  = ObjectField.OBJECT_END + 0x5,
        CORPSE_FIELD_SKINID                                 = ObjectField.OBJECT_END + 0x18,
        CORPSE_FIELD_FACIAL_HAIR_STYLE_ID                   = ObjectField.OBJECT_END + 0x19,
        CORPSE_FIELD_FLAGS                                  = ObjectField.OBJECT_END + 0x1A,
        CORPSE_FIELD_DYNAMIC_FLAGS                          = ObjectField.OBJECT_END + 0x1B,
        CORPSE_END                                          = ObjectField.OBJECT_END + 0x1C
    };

    public enum AreaTriggerField
    {
        AREATRIGGER_FIELD_CASTER                            = ObjectField.OBJECT_END + 0x0,
        AREATRIGGER_FIELD_SPELLID                           = ObjectField.OBJECT_END + 0x2,
        AREATRIGGER_FIELD_SPELL_VISUAL_ID                   = ObjectField.OBJECT_END + 0x3,
        AREATRIGGER_FIELD_DURATION                          = ObjectField.OBJECT_END + 0x4,
        AREATRIGGER_END                                     = ObjectField.OBJECT_END + 0x5
    };

    public enum SceneObjectField
    {
        SCENEOBJECT_FIELD_SCRIPT_PACKAGE_ID                 = ObjectField.OBJECT_END + 0x0,
        SCENEOBJECT_FIELD_RND_SEED_VAL                      = ObjectField.OBJECT_END + 0x1,
        SCENEOBJECT_FIELD_CREATEDBY                         = ObjectField.OBJECT_END + 0x2,
        SCENEOBJECT_FIELD_END                               = ObjectField.OBJECT_END + 0x4
    };
    // ReSharper restore InconsistentNaming
}
