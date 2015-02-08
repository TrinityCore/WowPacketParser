namespace WowPacketParser.Enums.Version.V5_4_0_17359
{
    // ReSharper disable InconsistentNaming
    // 5.4.0.17359
    public enum ObjectField
    {
        OBJECT_FIELD_GUID                                   = 0x0000, // Size = 2
        OBJECT_FIELD_DATA                                   = 0x0002, // Size = 2
        OBJECT_FIELD_TYPE                                   = 0x0004, // Size = 1
        OBJECT_FIELD_ENTRY                                  = 0x0005, // Size = 1
        OBJECT_FIELD_DYNAMIC_FLAGS                          = 0x0006, // Size = 1
        OBJECT_FIELD_SCALE_X                                = 0x0007, // Size = 1
        OBJECT_END                                          = 0x0008
    };

    public enum ItemField
    {
        ITEM_FIELD_OWNER                                    = ObjectField.OBJECT_END + 0x0000, // Size = 2
        ITEM_FIELD_CONTAINED_IN                             = ObjectField.OBJECT_END + 0x0002, // Size = 2
        ITEM_FIELD_CREATOR                                  = ObjectField.OBJECT_END + 0x0004, // Size = 2
        ITEM_FIELD_GIFT_CREATOR                             = ObjectField.OBJECT_END + 0x0006, // Size = 2
        ITEM_FIELD_STACK_COUNT                              = ObjectField.OBJECT_END + 0x0008, // Size = 1
        ITEM_FIELD_EXPIRATION                               = ObjectField.OBJECT_END + 0x0009, // Size = 1
        ITEM_FIELD_SPELL_CHARGES                            = ObjectField.OBJECT_END + 0x000A, // Size = 5
        ITEM_FIELD_DYNAMIC_FLAGS                            = ObjectField.OBJECT_END + 0x000F, // Size = 1
        ITEM_FIELD_ENCHANTMENT                              = ObjectField.OBJECT_END + 0x0010, // Size = 39
        ITEM_FIELD_PROPERTY_SEED                            = ObjectField.OBJECT_END + 0x0037, // Size = 1
        ITEM_FIELD_RANDOM_PROPERTIES_ID                     = ObjectField.OBJECT_END + 0x0038, // Size = 1
        ITEM_FIELD_DURABILITY                               = ObjectField.OBJECT_END + 0x0039, // Size = 1
        ITEM_FIELD_MAX_DURABILITY                           = ObjectField.OBJECT_END + 0x003A, // Size = 1
        ITEM_FIELD_CREATE_PLAYED_TIME                       = ObjectField.OBJECT_END + 0x003B, // Size = 1
        ITEM_FIELD_MODIFIERS_MASK                           = ObjectField.OBJECT_END + 0x003C, // Size = 1
        ITEM_END                                            = ObjectField.OBJECT_END + 0x003D
    };

    public enum ItemDynamicField
    {
        ITEM_DYNAMIC_FIELD_MODIFIERS                        = ItemField.ITEM_END + 0x0,
        ITEM_DYNAMIC_END                                    = ItemField.ITEM_END + 0x48
    }

    public enum ContainerField
    {
        CONTAINER_FIELD_SLOTS                               = ItemField.ITEM_END + 0x0000, // Size =  72
        CONTAINER_FIELD_NUM_SLOTS                           = ItemField.ITEM_END + 0x0048, // Size =   1
        CONTAINER_END                                       = ItemField.ITEM_END + 0x0049
    };

    public enum UnitField
    {
        UNIT_FIELD_CHARM                                    = ObjectField.OBJECT_END + 0x0000, // Size = 2
        UNIT_FIELD_SUMMON                                   = ObjectField.OBJECT_END + 0x0002, // Size = 2
        UNIT_FIELD_CRITTER                                  = ObjectField.OBJECT_END + 0x0004, // Size = 2
        UNIT_FIELD_CHARMEDBY                                = ObjectField.OBJECT_END + 0x0006, // Size = 2
        UNIT_FIELD_SUMMONEDBY                               = ObjectField.OBJECT_END + 0x0008, // Size = 2
        UNIT_FIELD_CREATEDBY                                = ObjectField.OBJECT_END + 0x000A, // Size = 2
        UNIT_FIELD_DEMON_CREATOR                            = ObjectField.OBJECT_END + 0x000C, // Size = 2
        UNIT_FIELD_TARGET                                   = ObjectField.OBJECT_END + 0x000E, // Size = 2
        UNIT_FIELD_BATTLE_PET_COMPANION_GUID                = ObjectField.OBJECT_END + 0x0010, // Size = 2
        UNIT_FIELD_CHANNEL_OBJECT                           = ObjectField.OBJECT_END + 0x0012, // Size = 2
        UNIT_FIELD_CHANNEL_SPELL                            = ObjectField.OBJECT_END + 0x0014, // Size = 1
        UNIT_FIELD_SUMMONED_BY_HOME_REALM                   = ObjectField.OBJECT_END + 0x0015, // Size = 1
        UNIT_FIELD_BYTES_0                                  = ObjectField.OBJECT_END + 0x0016, // Size = 1
        UNIT_FIELD_DISPLAY_POWER                            = ObjectField.OBJECT_END + 0x0017, // Size = 1
        UNIT_FIELD_OVERRIDE_DISPLAY_POWER_ID                = ObjectField.OBJECT_END + 0x0018, // Size = 1
        UNIT_FIELD_HEALTH                                   = ObjectField.OBJECT_END + 0x0019, // Size = 1
        UNIT_FIELD_POWER                                    = ObjectField.OBJECT_END + 0x001A, // Size = 5
        UNIT_FIELD_MAXHEALTH                                = ObjectField.OBJECT_END + 0x001F, // Size = 1
        UNIT_FIELD_MAXPOWER                                 = ObjectField.OBJECT_END + 0x0020, // Size = 5
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER                = ObjectField.OBJECT_END + 0x0025, // Size = 5
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER    = ObjectField.OBJECT_END + 0x002A, // Size = 5
        UNIT_FIELD_LEVEL                                    = ObjectField.OBJECT_END + 0x002F, // Size = 1
        UNIT_FIELD_EFFECTIVE_LEVEL                          = ObjectField.OBJECT_END + 0x0030, // Size = 1
        UNIT_FIELD_FACTIONTEMPLATE                          = ObjectField.OBJECT_END + 0x0031, // Size = 1
        UNIT_VIRTUAL_ITEM_SLOT_ID1                          = ObjectField.OBJECT_END + 0x0032, // Size = 1
        UNIT_VIRTUAL_ITEM_SLOT_ID2                          = ObjectField.OBJECT_END + 0x0033, // Size = 1
        UNIT_VIRTUAL_ITEM_SLOT_ID3                          = ObjectField.OBJECT_END + 0x0034, // Size = 1
        UNIT_FIELD_FLAGS                                    = ObjectField.OBJECT_END + 0x0035, // Size = 1
        UNIT_FIELD_FLAGS_2                                  = ObjectField.OBJECT_END + 0x0036, // Size = 1
        UNIT_FIELD_AURASTATE                                = ObjectField.OBJECT_END + 0x0037, // Size = 1
        UNIT_FIELD_BASEATTACKTIME                           = ObjectField.OBJECT_END + 0x0038, // Size = 2
        UNIT_FIELD_RANGEDATTACKTIME                         = ObjectField.OBJECT_END + 0x003A, // Size = 1
        UNIT_FIELD_BOUNDINGRADIUS                           = ObjectField.OBJECT_END + 0x003B, // Size = 1
        UNIT_FIELD_COMBATREACH                              = ObjectField.OBJECT_END + 0x003C, // Size = 1
        UNIT_FIELD_DISPLAYID                                = ObjectField.OBJECT_END + 0x003D, // Size = 1
        UNIT_FIELD_NATIVEDISPLAYID                          = ObjectField.OBJECT_END + 0x003E, // Size = 1
        UNIT_FIELD_MOUNTDISPLAYID                           = ObjectField.OBJECT_END + 0x003F, // Size = 1
        UNIT_FIELD_MIN_DAMAGE                               = ObjectField.OBJECT_END + 0x0040, // Size = 1
        UNIT_FIELD_MAX_DAMAGE                               = ObjectField.OBJECT_END + 0x0041, // Size = 1
        UNIT_FIELD_MIN_OFFHAND_DAMAGE                       = ObjectField.OBJECT_END + 0x0042, // Size = 1
        UNIT_FIELD_MAX_OFFHAND_DAMAGE                       = ObjectField.OBJECT_END + 0x0043, // Size = 1
        UNIT_FIELD_BYTES_1                                  = ObjectField.OBJECT_END + 0x0044, // Size = 1
        UNIT_FIELD_PET_NUMBER                               = ObjectField.OBJECT_END + 0x0045, // Size = 1
        UNIT_FIELD_PET_NAME_TIMESTAMP                       = ObjectField.OBJECT_END + 0x0046, // Size = 1
        UNIT_FIELD_PET_EXPERIENCE                           = ObjectField.OBJECT_END + 0x0047, // Size = 1
        UNIT_FIELD_PET_NEXTLEVEL_EXPERIENCE                 = ObjectField.OBJECT_END + 0x0048, // Size = 1
        UNIT_FIELD_MOD_CASTING_SPEED                        = ObjectField.OBJECT_END + 0x0049, // Size = 1
        UNIT_FIELD_MOD_SPELL_HASTE                          = ObjectField.OBJECT_END + 0x004A, // Size = 1
        UNIT_FIELD_MOD_HASTE                                = ObjectField.OBJECT_END + 0x004B, // Size = 1
        UNIT_FIELD_MOD_RANGED_HASTE                         = ObjectField.OBJECT_END + 0x004C, // Size = 1
        UNIT_FIELD_MOD_HASTE_REGEN                          = ObjectField.OBJECT_END + 0x004D, // Size = 1
        UNIT_CREATED_BY_SPELL                               = ObjectField.OBJECT_END + 0x004E, // Size = 1
        UNIT_NPC_FLAGS                                      = ObjectField.OBJECT_END + 0x004F, // Size = 1
        UNIT_NPC_EMOTESTATE                                 = ObjectField.OBJECT_END + 0x0050, // Size = 2
        UNIT_FIELD_STATS                                    = ObjectField.OBJECT_END + 0x0052, // Size = 5
        UNIT_FIELD_STAT_POSBUFF                             = ObjectField.OBJECT_END + 0x0057, // Size = 5
        UNIT_FIELD_STAT_NEGBUFF                             = ObjectField.OBJECT_END + 0x005C, // Size = 5
        UNIT_FIELD_RESISTANCES                              = ObjectField.OBJECT_END + 0x0061, // Size = 7
        UNIT_FIELD_RESISTANCE_BUFF_MODS_POSITIVE            = ObjectField.OBJECT_END + 0x0068, // Size = 7
        UNIT_FIELD_RESISTANCE_BUFF_MODS_NEGATIVE            = ObjectField.OBJECT_END + 0x006F, // Size = 7
        UNIT_FIELD_BASE_MANA                                = ObjectField.OBJECT_END + 0x0076, // Size = 1
        UNIT_FIELD_BASE_HEALTH                              = ObjectField.OBJECT_END + 0x0077, // Size = 1
        UNIT_FIELD_BYTES_2                                  = ObjectField.OBJECT_END + 0x0078, // Size = 1
        UNIT_FIELD_ATTACK_POWER                             = ObjectField.OBJECT_END + 0x0079, // Size = 1
        UNIT_FIELD_ATTACK_POWER_MOD_POS                     = ObjectField.OBJECT_END + 0x007A, // Size = 1
        UNIT_FIELD_ATTACK_POWER_MOD_NEG                     = ObjectField.OBJECT_END + 0x007B, // Size = 1
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER                  = ObjectField.OBJECT_END + 0x007C, // Size = 1
        UNIT_FIELD_RANGED_ATTACK_POWER                      = ObjectField.OBJECT_END + 0x007D, // Size = 1
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_POS              = ObjectField.OBJECT_END + 0x007E, // Size = 1
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_NEG              = ObjectField.OBJECT_END + 0x007F, // Size = 1
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER           = ObjectField.OBJECT_END + 0x0080, // Size = 1
        UNIT_FIELD_MIN_RANGED_DAMAGE                        = ObjectField.OBJECT_END + 0x0081, // Size = 1
        UNIT_FIELD_MAX_RANGED_DAMAGE                        = ObjectField.OBJECT_END + 0x0082, // Size = 1
        UNIT_FIELD_POWER_COST_MODIFIER                      = ObjectField.OBJECT_END + 0x0083, // Size = 7
        UNIT_FIELD_POWER_COST_MULTIPLIER                    = ObjectField.OBJECT_END + 0x008A, // Size = 7
        UNIT_FIELD_MAX_HEALTH_MODIFIER                      = ObjectField.OBJECT_END + 0x0091, // Size = 1
        UNIT_FIELD_HOVERHEIGHT                              = ObjectField.OBJECT_END + 0x0092, // Size = 1
        UNIT_FIELD_MIN_ITEM_LEVEL                           = ObjectField.OBJECT_END + 0x0093, // Size = 1
        UNIT_FIELD_MAX_ITEM_LEVEL                           = ObjectField.OBJECT_END + 0x0094, // Size = 1
        UNIT_FIELD_WILD_BATTLEPET_LEVEL                     = ObjectField.OBJECT_END + 0x0095, // Size = 1
        UNIT_FIELD_BATTLEPET_COMPANION_NAME_TIMESTAMP       = ObjectField.OBJECT_END + 0x0096, // Size = 1
        UNIT_FIELD_INTERACT_SPELLID                         = ObjectField.OBJECT_END + 0x0097, // Size = 1
        UNIT_FIELD_END                                      = ObjectField.OBJECT_END + 0x0098
    };

    public enum UnitDynamicField
    {
        UNIT_DYNAMIC_FIELD_PASSIVE_SPELLS                   = UnitField.UNIT_FIELD_END + 0x0,
        UNIT_DYNAMIC_END                                    = UnitField.UNIT_FIELD_END + 0x101
    }

    public enum PlayerField
    {
        PLAYER_FIELD_DUEL_ARBITER                           = UnitField.UNIT_FIELD_END + 0x0000, // Size = 2
        PLAYER_FIELD_FLAGS                                  = UnitField.UNIT_FIELD_END + 0x0002, // Size = 1
        PLAYER_FIELD_GUILD_RANK_ID                          = UnitField.UNIT_FIELD_END + 0x0003, // Size = 1
        PLAYER_FIELD_GUILD_DELETE_DATE                      = UnitField.UNIT_FIELD_END + 0x0004, // Size = 1
        PLAYER_FIELD_GUILD_LEVEL                            = UnitField.UNIT_FIELD_END + 0x0005, // Size = 1
        PLAYER_FIELD_HAIR_COLOR_ID                          = UnitField.UNIT_FIELD_END + 0x0006, // Size = 1
        PLAYER_FIELD_REST_STATE                             = UnitField.UNIT_FIELD_END + 0x0007, // Size = 1
        PLAYER_FIELD_ARENA_FACTION                          = UnitField.UNIT_FIELD_END + 0x0008, // Size = 1
        PLAYER_FIELD_DUEL_TEAM                              = UnitField.UNIT_FIELD_END + 0x0009, // Size = 1
        PLAYER_FIELD_GUILD_TIMESTAMP                        = UnitField.UNIT_FIELD_END + 0x000A, // Size = 1
        PLAYER_FIELD_QUESTLOG                               = UnitField.UNIT_FIELD_END + 0x000B, // Size = 750
        PLAYER_FIELD_VISIBLE_ITEMS                          = UnitField.UNIT_FIELD_END + 0x02F9, // Size = 38
        PLAYER_FIELD_PLAYER_TITLE                           = UnitField.UNIT_FIELD_END + 0x031F, // Size = 1
        PLAYER_FIELD_FAKE_INEBRIATION                       = UnitField.UNIT_FIELD_END + 0x0320, // Size = 1
        PLAYER_FIELD_HOME_PLAYER_REALM                      = UnitField.UNIT_FIELD_END + 0x0321, // Size = 1
        PLAYER_FIELD_CURRENT_SPEC_ID                        = UnitField.UNIT_FIELD_END + 0x0322, // Size = 1
        PLAYER_FIELD_TAXI_MOUNT_ANIM_KIT_ID                 = UnitField.UNIT_FIELD_END + 0x0323, // Size = 1
        PLAYER_FIELD_CURRENT_BATTLE_PET_BREED_QUALITY       = UnitField.UNIT_FIELD_END + 0x0324, // Size = 1
        PLAYER_FIELD_INV_SLOTS                              = UnitField.UNIT_FIELD_END + 0x0325, // Size = 172
        PLAYER_FIELD_FARSIGHT_OBJECT                        = UnitField.UNIT_FIELD_END + 0x03D1, // Size = 2
        PLAYER_FIELD_KNOWN_TITLES                           = UnitField.UNIT_FIELD_END + 0x03D3, // Size =  10
        PLAYER_FIELD_COINAGE                                = UnitField.UNIT_FIELD_END + 0x03DD, // Size = 2
        PLAYER_FIELD_XP                                     = UnitField.UNIT_FIELD_END + 0x03DF, // Size = 1
        PLAYER_FIELD_NEXT_LEVEL_XP                          = UnitField.UNIT_FIELD_END + 0x03E0, // Size = 1
        PLAYER_FIELD_SKILL                                  = UnitField.UNIT_FIELD_END + 0x03E1, // Size = 448
        PLAYER_FIELD_CHARACTER_POINTS                       = UnitField.UNIT_FIELD_END + 0x05A1, // Size = 1
        PLAYER_FIELD_MAX_TALENT_TIERS                       = UnitField.UNIT_FIELD_END + 0x05A2, // Size = 1
        PLAYER_FIELD_TRACK_CREATURE_MASK                    = UnitField.UNIT_FIELD_END + 0x05A3, // Size = 1
        PLAYER_FIELD_TRACK_RESOURCE_MASK                    = UnitField.UNIT_FIELD_END + 0x05A4, // Size = 1
        PLAYER_FIELD_EXPERTISE                              = UnitField.UNIT_FIELD_END + 0x05A5, // Size = 1
        PLAYER_FIELD_OFFHAND_EXPERTISE                      = UnitField.UNIT_FIELD_END + 0x05A6, // Size = 1
        PLAYER_FIELD_RANGED_EXPERTISE                       = UnitField.UNIT_FIELD_END + 0x05A7, // Size = 1
        PLAYER_FIELD_COMBAT_RATING_EXPERTISE                = UnitField.UNIT_FIELD_END + 0x05A8, // Size = 1
        PLAYER_FIELD_BLOCK_PERCENTAGE                       = UnitField.UNIT_FIELD_END + 0x05A9, // Size = 1
        PLAYER_FIELD_DODGE_PERCENTAGE                       = UnitField.UNIT_FIELD_END + 0x05AA, // Size = 1
        PLAYER_FIELD_PARRY_PERCENTAGE                       = UnitField.UNIT_FIELD_END + 0x05AB, // Size = 1
        PLAYER_FIELD_CRIT_PERCENTAGE                        = UnitField.UNIT_FIELD_END + 0x05AC, // Size = 1
        PLAYER_FIELD_RANGED_CRIT_PERCENTAGE                 = UnitField.UNIT_FIELD_END + 0x05AD, // Size = 1
        PLAYER_FIELD_OFFHAND_CRIT_PERCENTAGE                = UnitField.UNIT_FIELD_END + 0x05AE, // Size = 1
        PLAYER_FIELD_SPELL_CRIT_PERCENTAGE                  = UnitField.UNIT_FIELD_END + 0x05AF, // Size = 7
        PLAYER_FIELD_SHIELD_BLOCK                           = UnitField.UNIT_FIELD_END + 0x05B6, // Size = 1
        PLAYER_FIELD_SHIELD_BLOCK_CRIT_PERCENTAGE           = UnitField.UNIT_FIELD_END + 0x05B7, // Size = 1
        PLAYER_FIELD_MASTERY                                = UnitField.UNIT_FIELD_END + 0x05B8, // Size = 1
        PLAYER_FIELD_PVP_POWER_DAMAGE                       = UnitField.UNIT_FIELD_END + 0x05B9, // Size = 1
        PLAYER_FIELD_PVP_POWER_HEALING                      = UnitField.UNIT_FIELD_END + 0x05BA, // Size = 1
        PLAYER_FIELD_EXPLORED_ZONES                         = UnitField.UNIT_FIELD_END + 0x05BB, // Size = 200
        PLAYER_FIELD_REST_STATE_BONUS_POOL                  = UnitField.UNIT_FIELD_END + 0x0683, // Size = 1
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS                    = UnitField.UNIT_FIELD_END + 0x0684, // Size = 7
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG                    = UnitField.UNIT_FIELD_END + 0x068B, // Size = 7
        PLAYER_FIELD_MOD_DAMAGE_DONE_PERCENT                = UnitField.UNIT_FIELD_END + 0x0692, // Size = 7
        PLAYER_FIELD_MOD_HEALING_DONE_POS                   = UnitField.UNIT_FIELD_END + 0x0699, // Size = 1
        PLAYER_FIELD_MOD_HEALING_PERCENT                    = UnitField.UNIT_FIELD_END + 0x069A, // Size = 1
        PLAYER_FIELD_MOD_HEALING_DONE_PERCENT               = UnitField.UNIT_FIELD_END + 0x069B, // Size = 1
        PLAYER_FIELD_MOD_PERIODIC_HEALING_DONE_PERCENT      = UnitField.UNIT_FIELD_END + 0x069C, // Size = 1
        PLAYER_FIELD_WEAPON_DMG_MULTIPLIERS                 = UnitField.UNIT_FIELD_END + 0x069D, // Size = 3
        PLAYER_FIELD_MOD_SPELL_POWER_PERCENT                = UnitField.UNIT_FIELD_END + 0x06A0, // Size = 1
        PLAYER_FIELD_MOD_RESILIENCE_PERCENT                 = UnitField.UNIT_FIELD_END + 0x06A1, // Size = 1
        PLAYER_FIELD_OVERRIDE_SPELL_POWER_BY_AP_PERCENT     = UnitField.UNIT_FIELD_END + 0x06A2, // Size = 1
        PLAYER_FIELD_OVERRIDE_AP_BY_SPELL_POWER_PERCENT     = UnitField.UNIT_FIELD_END + 0x06A3, // Size = 1
        PLAYER_FIELD_MOD_TARGET_RESISTANCE                  = UnitField.UNIT_FIELD_END + 0x06A4, // Size = 1
        PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE         = UnitField.UNIT_FIELD_END + 0x06A5, // Size = 1
        PLAYER_FIELD_LIFETIME_MAX_RANK                      = UnitField.UNIT_FIELD_END + 0x06A6, // Size = 1
        PLAYER_FIELD_SELF_RES_SPELL                         = UnitField.UNIT_FIELD_END + 0x06A7, // Size = 1
        PLAYER_FIELD_PVP_MEDALS                             = UnitField.UNIT_FIELD_END + 0x06A8, // Size = 1
        PLAYER_FIELD_BUYBACK_PRICE                          = UnitField.UNIT_FIELD_END + 0x06A9, // Size = 12
        PLAYER_FIELD_BUYBACK_TIMESTAMP                      = UnitField.UNIT_FIELD_END + 0x06B5, // Size = 12
        PLAYER_FIELD_YESTERDAY_HONORABLE_KILLS              = UnitField.UNIT_FIELD_END + 0x06C1, // Size = 1
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS               = UnitField.UNIT_FIELD_END + 0x06C2, // Size = 1
        PLAYER_FIELD_WATCHED_FACTION_INDEX                  = UnitField.UNIT_FIELD_END + 0x06C3, // Size = 1
        PLAYER_FIELD_COMBAT_RATINGS                         = UnitField.UNIT_FIELD_END + 0x06C4, // Size = 27
        PLAYER_FIELD_PVP_INFO                               = UnitField.UNIT_FIELD_END + 0x06DF, // Size = 24
        PLAYER_FIELD_MAXLEVEL                               = UnitField.UNIT_FIELD_END + 0x06F7, // Size = 1
        PLAYER_FIELD_RUNEREGEN                              = UnitField.UNIT_FIELD_END + 0x06F8, // Size = 4
        PLAYER_FIELD_NO_REAGENT_COST_MASK                   = UnitField.UNIT_FIELD_END + 0x06FC, // Size = 4
        PLAYER_FIELD_GLYPH_SLOTS                            = UnitField.UNIT_FIELD_END + 0x0700, // Size = 6
        PLAYER_FIELD_GLYPHS                                 = UnitField.UNIT_FIELD_END + 0x0706, // Size = 6
        PLAYER_FIELD_GLYPH_SLOTS_ENABLED                    = UnitField.UNIT_FIELD_END + 0x070C, // Size = 1
        PLAYER_FIELD_PET_SPELL_POWER                        = UnitField.UNIT_FIELD_END + 0x070D, // Size = 1
        PLAYER_FIELD_RESEARCHING                            = UnitField.UNIT_FIELD_END + 0x070E, // Size = 8
        PLAYER_FIELD_PROFESSION_SKILL_LINE                  = UnitField.UNIT_FIELD_END + 0x0716, // Size = 2
        PLAYER_FIELD_UI_HIT_MODIFIER                        = UnitField.UNIT_FIELD_END + 0x0718, // Size = 1
        PLAYER_FIELD_UI_SPELL_HIT_MODIFIER                  = UnitField.UNIT_FIELD_END + 0x0719, // Size = 1
        PLAYER_FIELD_HOME_REALM_TIME_OFFSET                 = UnitField.UNIT_FIELD_END + 0x071A, // Size = 1
        PLAYER_FIELD_MOD_PET_HASTE                          = UnitField.UNIT_FIELD_END + 0x071B, // Size = 1
        PLAYER_FIELD_SUMMONED_BATTLE_PET_ID                 = UnitField.UNIT_FIELD_END + 0x071C, // Size = 2
        PLAYER_FIELD_OVERRIDE_SPELLS_ID                     = UnitField.UNIT_FIELD_END + 0x071E, // Size = 1
        PLAYER_FIELD_LFG_BONUS_FACTION_ID                   = UnitField.UNIT_FIELD_END + 0x071F, // Size = 1
        PLAYER_FIELD_LOOT_SPEC_ID                           = UnitField.UNIT_FIELD_END + 0x0720, // Size = 1
        PLAYER_FIELD_OVERRIDE_ZONE_PVP_TYPE                 = UnitField.UNIT_FIELD_END + 0x0721, // Size = 1
        PLAYER_FIELD_ITEM_LEVEL_DELTA                       = UnitField.UNIT_FIELD_END + 0x0722, // Size = 1
        PLAYER_FIELD_END                                    = UnitField.UNIT_FIELD_END + 0x0723
    };

    public enum PlayerDynamicField
    {
        PLAYER_DYNAMIC_FIELD_RESEARCH_SITES                 = PlayerField.PLAYER_FIELD_END + 0x0,
        PLAYER_DYNAMIC_FIELD_DAILY_QUESTS_COMPLETED         = PlayerField.PLAYER_FIELD_END + 0x2,
        PLAYER_DYNAMIC_END                                  = PlayerField.PLAYER_FIELD_END + 0x4
    }

    public enum GameObjectField
    {
        GAMEOBJECT_FIELD_CREATED_BY                         = ObjectField.OBJECT_END + 0x0000, // Size = 2
        GAMEOBJECT_DISPLAYID                                = ObjectField.OBJECT_END + 0x0002, // Size = 1
        GAMEOBJECT_FLAGS                                    = ObjectField.OBJECT_END + 0x0003, // Size = 1
        GAMEOBJECT_PARENTROTATION                           = ObjectField.OBJECT_END + 0x0004, // Size = 4
        GAMEOBJECT_FACTION                                  = ObjectField.OBJECT_END + 0x0008, // Size = 1
        GAMEOBJECT_LEVEL                                    = ObjectField.OBJECT_END + 0x0009, // Size = 1
        GAMEOBJECT_BYTES_1                                  = ObjectField.OBJECT_END + 0x000A, // Size = 1
        GAMEOBJECT_STATE_SPELL_VISUAL_ID                    = ObjectField.OBJECT_END + 0x000B, // Size = 1
        GAMEOBJECT_END                                      = ObjectField.OBJECT_END + 0x000C
    };

    public enum DynamicObjectField
    {
        DYNAMICOBJECT_FIELD_CASTER                          = ObjectField.OBJECT_END + 0x0000, // Size = 2
        DYNAMICOBJECT_FIELD_TYPE_AND_VISUAL_ID              = ObjectField.OBJECT_END + 0x0002, // Size = 1
        DYNAMICOBJECT_FIELD_SPELLID                         = ObjectField.OBJECT_END + 0x0003, // Size = 1
        DYNAMICOBJECT_FIELD_RADIUS                          = ObjectField.OBJECT_END + 0x0004, // Size = 1
        DYNAMICOBJECT_FIELD_CASTTIME                        = ObjectField.OBJECT_END + 0x0005, // Size = 1
        DYNAMICOBJECT_END                                   = ObjectField.OBJECT_END + 0x0006
    };

    public enum CorpseField
    {
        CORPSE_FIELD_OWNER                                  = ObjectField.OBJECT_END + 0x0000, // Size = 2
        CORPSE_FIELD_PARTY_GUID                             = ObjectField.OBJECT_END + 0x0002, // Size = 2
        CORPSE_FIELD_DISPLAYID                              = ObjectField.OBJECT_END + 0x0004, // Size = 1
        CORPSE_FIELD_ITEMS                                  = ObjectField.OBJECT_END + 0x0005, // Size = 19
        CORPSE_FIELD_SKINID                                 = ObjectField.OBJECT_END + 0x0018, // Size = 1
        CORPSE_FIELD_FACIAL_HAIR_STYLE_ID                   = ObjectField.OBJECT_END + 0x0019, // Size = 1
        CORPSE_FIELD_FLAGS                                  = ObjectField.OBJECT_END + 0x001A, // Size = 1
        CORPSE_FIELD_DYNAMIC_FLAGS                          = ObjectField.OBJECT_END + 0x001B, // Size = 1
        CORPSE_FIELD_END                                    = ObjectField.OBJECT_END + 0x001C
    };

    public enum AreaTriggerField
    {
        AREATRIGGER_FIELD_CASTER                            = ObjectField.OBJECT_END + 0x0000, // Size = 2
        AREATRIGGER_FIELD_DURATION                          = ObjectField.OBJECT_END + 0x0002, // Size = 1
        AREATRIGGER_FIELD_SPELLID                           = ObjectField.OBJECT_END + 0x0003, // Size = 1
        AREATRIGGER_FIELD_SPELL_VISUAL_ID                   = ObjectField.OBJECT_END + 0x0004, // Size = 1
        AREATRIGGER_FIELD_EXPLICIT_SCALE                    = ObjectField.OBJECT_END + 0x0005, // Size = 1
        AREATRIGGER_FIELD_END                               = ObjectField.OBJECT_END + 0x0006
    };

    public enum SceneObjectField
    {
        SCENEOBJECT_FIELD_SCRIPT_PACKAGE_ID                 = ObjectField.OBJECT_END + 0x0000, // Size = 1
        SCENEOBJECT_FIELD_RND_SEED_VAL                      = ObjectField.OBJECT_END + 0x0001, // Size = 1
        SCENEOBJECT_FIELD_CREATEDBY                         = ObjectField.OBJECT_END + 0x0002, // Size = 2
        SCENEOBJECT_FIELD_SCENE_TYPE                        = ObjectField.OBJECT_END + 0x0004, // Size = 1
        SCENEOBJECT_END                                     = ObjectField.OBJECT_END + 0x0005
    };
    // ReSharper restore InconsistentNaming
}
