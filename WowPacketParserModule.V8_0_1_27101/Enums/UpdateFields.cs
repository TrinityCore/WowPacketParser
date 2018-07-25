namespace WowPacketParserModule.V8_0_1_27101.Enums
{
    // ReSharper disable InconsistentNaming
    // 8_0_1_27101
    public enum ObjectField
    {
        OBJECT_FIELD_GUID                                      = 0x000, // Size: 4, Flags: PUBLIC
        OBJECT_FIELD_ENTRY                                     = 0x004, // Size: 1, Flags: DYNAMIC
        OBJECT_DYNAMIC_FLAGS                                   = 0x005, // Size: 1, Flags: DYNAMIC, URGENT
        OBJECT_FIELD_SCALE_X                                   = 0x006, // Size: 1, Flags: PUBLIC
        OBJECT_END                                             = 0x007,
    }

    public enum ObjectDynamicField
    {
        OBJECT_DYNAMIC_END                                     = 0x000,
    }

    public enum ItemField
    {
        ITEM_FIELD_OWNER                                       = ObjectField.OBJECT_END + 0x000, // Size: 4, Flags: PUBLIC
        ITEM_FIELD_CONTAINED                                   = ObjectField.OBJECT_END + 0x004, // Size: 4, Flags: PUBLIC
        ITEM_FIELD_CREATOR                                     = ObjectField.OBJECT_END + 0x008, // Size: 4, Flags: PUBLIC
        ITEM_FIELD_GIFTCREATOR                                 = ObjectField.OBJECT_END + 0x00C, // Size: 4, Flags: PUBLIC
        ITEM_FIELD_STACK_COUNT                                 = ObjectField.OBJECT_END + 0x010, // Size: 1, Flags: OWNER
        ITEM_FIELD_DURATION                                    = ObjectField.OBJECT_END + 0x011, // Size: 1, Flags: OWNER
        ITEM_FIELD_SPELL_CHARGES                               = ObjectField.OBJECT_END + 0x012, // Size: 5, Flags: OWNER
        ITEM_FIELD_FLAGS                                       = ObjectField.OBJECT_END + 0x017, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_ENCHANTMENT                                 = ObjectField.OBJECT_END + 0x018, // Size: 39, Flags: PUBLIC
        ITEM_FIELD_PROPERTY_SEED                               = ObjectField.OBJECT_END + 0x03F, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_RANDOM_PROPERTIES_ID                        = ObjectField.OBJECT_END + 0x040, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_DURABILITY                                  = ObjectField.OBJECT_END + 0x041, // Size: 1, Flags: OWNER
        ITEM_FIELD_MAXDURABILITY                               = ObjectField.OBJECT_END + 0x042, // Size: 1, Flags: OWNER
        ITEM_FIELD_CREATE_PLAYED_TIME                          = ObjectField.OBJECT_END + 0x043, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_MODIFIERS_MASK                              = ObjectField.OBJECT_END + 0x044, // Size: 1, Flags: OWNER
        ITEM_FIELD_CONTEXT                                     = ObjectField.OBJECT_END + 0x045, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_ARTIFACT_XP                                 = ObjectField.OBJECT_END + 0x046, // Size: 2, Flags: OWNER
        ITEM_FIELD_APPEARANCE_MOD_ID                           = ObjectField.OBJECT_END + 0x048, // Size: 1, Flags: OWNER
        ITEM_END                                               = ObjectField.OBJECT_END + 0x049,
    }

    public enum ItemDynamicField
    {
        ITEM_DYNAMIC_FIELD_MODIFIERS                           = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000, // Flags: OWNER
        ITEM_DYNAMIC_FIELD_BONUSLIST_IDS                       = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x001, // Flags: OWNER, 0x100
        ITEM_DYNAMIC_FIELD_ARTIFACT_POWERS                     = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x002, // Flags: OWNER
        ITEM_DYNAMIC_FIELD_GEMS                                = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x003, // Flags: OWNER
        ITEM_DYNAMIC_END                                       = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x004,
    }

    public enum ContainerField
    {
        CONTAINER_FIELD_SLOT_1                                 = ItemField.ITEM_END + 0x000, // Size: 144, Flags: PUBLIC
        CONTAINER_FIELD_NUM_SLOTS                              = ItemField.ITEM_END + 0x090, // Size: 1, Flags: PUBLIC
        CONTAINER_END                                          = ItemField.ITEM_END + 0x091,
    }

    public enum ContainerDynamicField
    {
        CONTAINER_DYNAMIC_END                                  = ItemDynamicField.ITEM_DYNAMIC_END + 0x000,
    }

    public enum AzeriteEmpoweredItemField
    {
        AZERITE_EMPOWERED_ITEM_FIELD_SELECTIONS                = ItemField.ITEM_END + 0x000, // Size: 4, Flags: PUBLIC
        AZERITE_EMPOWERED_ITEM_END                             = ItemField.ITEM_END + 0x004,
    }

    public enum AzeriteEmpoweredItemDynamicField
    {
        AZERITE_EMPOWERED_ITEM_DYNAMIC_END                     = ItemDynamicField.ITEM_DYNAMIC_END + 0x000,
    }

    public enum AzeriteItemField
    {
        AZERITE_ITEM_FIELD_XP                                  = ItemField.ITEM_END + 0x000, // Size: 2, Flags: PUBLIC
        AZERITE_ITEM_FIELD_LEVEL                               = ItemField.ITEM_END + 0x002, // Size: 1, Flags: PUBLIC
        AZERITE_ITEM_FIELD_AURA_LEVEL                          = ItemField.ITEM_END + 0x003, // Size: 1, Flags: PUBLIC
        AZERITE_ITEM_FIELD_KNOWLEDGE_LEVEL                     = ItemField.ITEM_END + 0x004, // Size: 1, Flags: OWNER
        AZERITE_ITEM_FIELD_DEBUG_KNOWLEDGE_WEEK                = ItemField.ITEM_END + 0x005, // Size: 1, Flags: OWNER
        AZERITE_ITEM_END                                       = ItemField.ITEM_END + 0x006,
    }

    public enum AzeriteItemDynamicField
    {
        AZERITE_ITEM_DYNAMIC_END                               = ItemDynamicField.ITEM_DYNAMIC_END + 0x000,
    }

    public enum UnitField
    {
        UNIT_FIELD_CHARM                                       = ObjectField.OBJECT_END + 0x000, // Size: 4, Flags: PUBLIC
        UNIT_FIELD_SUMMON                                      = ObjectField.OBJECT_END + 0x004, // Size: 4, Flags: PUBLIC
        UNIT_FIELD_CRITTER                                     = ObjectField.OBJECT_END + 0x008, // Size: 4, Flags: PRIVATE
        UNIT_FIELD_CHARMEDBY                                   = ObjectField.OBJECT_END + 0x00C, // Size: 4, Flags: PUBLIC
        UNIT_FIELD_SUMMONEDBY                                  = ObjectField.OBJECT_END + 0x010, // Size: 4, Flags: PUBLIC
        UNIT_FIELD_CREATEDBY                                   = ObjectField.OBJECT_END + 0x014, // Size: 4, Flags: PUBLIC
        UNIT_FIELD_DEMON_CREATOR                               = ObjectField.OBJECT_END + 0x018, // Size: 4, Flags: PUBLIC
        UNIT_FIELD_LOOK_AT_CONTROLLER_TARGET                   = ObjectField.OBJECT_END + 0x01C, // Size: 4, Flags: PUBLIC
        UNIT_FIELD_TARGET                                      = ObjectField.OBJECT_END + 0x020, // Size: 4, Flags: PUBLIC
        UNIT_FIELD_BATTLE_PET_COMPANION_GUID                   = ObjectField.OBJECT_END + 0x024, // Size: 4, Flags: PUBLIC
        UNIT_FIELD_BATTLE_PET_DB_ID                            = ObjectField.OBJECT_END + 0x028, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_CHANNEL_DATA                                = ObjectField.OBJECT_END + 0x02A, // Size: 2, Flags: PUBLIC, URGENT
        UNIT_FIELD_SUMMONED_BY_HOME_REALM                      = ObjectField.OBJECT_END + 0x02C, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_BYTES_0                                     = ObjectField.OBJECT_END + 0x02D, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_DISPLAY_POWER                               = ObjectField.OBJECT_END + 0x02E, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_OVERRIDE_DISPLAY_POWER_ID                   = ObjectField.OBJECT_END + 0x02F, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_HEALTH                                      = ObjectField.OBJECT_END + 0x030, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_POWER                                       = ObjectField.OBJECT_END + 0x032, // Size: 6, Flags: PUBLIC, URGENT_SELF_ONLY
        UNIT_FIELD_MAXHEALTH                                   = ObjectField.OBJECT_END + 0x038, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_MAXPOWER                                    = ObjectField.OBJECT_END + 0x03A, // Size: 6, Flags: PUBLIC
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER                   = ObjectField.OBJECT_END + 0x040, // Size: 6, Flags: PRIVATE, OWNER, UNIT_ALL
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER       = ObjectField.OBJECT_END + 0x046, // Size: 6, Flags: PRIVATE, OWNER, UNIT_ALL
        UNIT_FIELD_LEVEL                                       = ObjectField.OBJECT_END + 0x04C, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_EFFECTIVE_LEVEL                             = ObjectField.OBJECT_END + 0x04D, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_CONTENT_TUNING_ID                           = ObjectField.OBJECT_END + 0x04E, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_SCALING_LEVEL_MIN                           = ObjectField.OBJECT_END + 0x04F, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_SCALING_LEVEL_MAX                           = ObjectField.OBJECT_END + 0x050, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_SCALING_LEVEL_DELTA                         = ObjectField.OBJECT_END + 0x051, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_SCALING_FACTION_GROUP                       = ObjectField.OBJECT_END + 0x052, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_SCALING_HEALTH_ITEM_LEVEL_CURVE_ID          = ObjectField.OBJECT_END + 0x053, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_SCALING_DAMAGE_ITEM_LEVEL_CURVE_ID          = ObjectField.OBJECT_END + 0x054, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_FACTIONTEMPLATE                             = ObjectField.OBJECT_END + 0x055, // Size: 1, Flags: PUBLIC
        UNIT_VIRTUAL_ITEM_SLOT_ID                              = ObjectField.OBJECT_END + 0x056, // Size: 6, Flags: PUBLIC
        UNIT_FIELD_FLAGS                                       = ObjectField.OBJECT_END + 0x05C, // Size: 1, Flags: PUBLIC, URGENT
        UNIT_FIELD_FLAGS_2                                     = ObjectField.OBJECT_END + 0x05D, // Size: 1, Flags: PUBLIC, URGENT
        UNIT_FIELD_FLAGS_3                                     = ObjectField.OBJECT_END + 0x05E, // Size: 1, Flags: PUBLIC, URGENT
        UNIT_FIELD_AURASTATE                                   = ObjectField.OBJECT_END + 0x05F, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_BASEATTACKTIME                              = ObjectField.OBJECT_END + 0x060, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_RANGEDATTACKTIME                            = ObjectField.OBJECT_END + 0x062, // Size: 1, Flags: PRIVATE
        UNIT_FIELD_BOUNDINGRADIUS                              = ObjectField.OBJECT_END + 0x063, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_COMBATREACH                                 = ObjectField.OBJECT_END + 0x064, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_DISPLAYID                                   = ObjectField.OBJECT_END + 0x065, // Size: 1, Flags: DYNAMIC, URGENT
        UNIT_FIELD_DISPLAY_SCALE                               = ObjectField.OBJECT_END + 0x066, // Size: 1, Flags: DYNAMIC, URGENT
        UNIT_FIELD_NATIVEDISPLAYID                             = ObjectField.OBJECT_END + 0x067, // Size: 1, Flags: PUBLIC, URGENT
        UNIT_FIELD_NATIVE_X_DISPLAY_SCALE                      = ObjectField.OBJECT_END + 0x068, // Size: 1, Flags: PUBLIC, URGENT
        UNIT_FIELD_MOUNTDISPLAYID                              = ObjectField.OBJECT_END + 0x069, // Size: 1, Flags: PUBLIC, URGENT
        UNIT_FIELD_MINDAMAGE                                   = ObjectField.OBJECT_END + 0x06A, // Size: 1, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_MAXDAMAGE                                   = ObjectField.OBJECT_END + 0x06B, // Size: 1, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_MINOFFHANDDAMAGE                            = ObjectField.OBJECT_END + 0x06C, // Size: 1, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_MAXOFFHANDDAMAGE                            = ObjectField.OBJECT_END + 0x06D, // Size: 1, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_BYTES_1                                     = ObjectField.OBJECT_END + 0x06E, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_PETNUMBER                                   = ObjectField.OBJECT_END + 0x06F, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_PET_NAME_TIMESTAMP                          = ObjectField.OBJECT_END + 0x070, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_PETEXPERIENCE                               = ObjectField.OBJECT_END + 0x071, // Size: 1, Flags: OWNER
        UNIT_FIELD_PETNEXTLEVELEXP                             = ObjectField.OBJECT_END + 0x072, // Size: 1, Flags: OWNER
        UNIT_MOD_CAST_SPEED                                    = ObjectField.OBJECT_END + 0x073, // Size: 1, Flags: PUBLIC
        UNIT_MOD_CAST_HASTE                                    = ObjectField.OBJECT_END + 0x074, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MOD_HASTE                                   = ObjectField.OBJECT_END + 0x075, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MOD_RANGED_HASTE                            = ObjectField.OBJECT_END + 0x076, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MOD_HASTE_REGEN                             = ObjectField.OBJECT_END + 0x077, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MOD_TIME_RATE                               = ObjectField.OBJECT_END + 0x078, // Size: 1, Flags: PUBLIC
        UNIT_CREATED_BY_SPELL                                  = ObjectField.OBJECT_END + 0x079, // Size: 1, Flags: PUBLIC
        UNIT_NPC_FLAGS                                         = ObjectField.OBJECT_END + 0x07A, // Size: 2, Flags: PUBLIC, DYNAMIC
        UNIT_NPC_EMOTESTATE                                    = ObjectField.OBJECT_END + 0x07C, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_STAT                                        = ObjectField.OBJECT_END + 0x07D, // Size: 4, Flags: PRIVATE, OWNER
        UNIT_FIELD_POSSTAT                                     = ObjectField.OBJECT_END + 0x081, // Size: 4, Flags: PRIVATE, OWNER
        UNIT_FIELD_NEGSTAT                                     = ObjectField.OBJECT_END + 0x085, // Size: 4, Flags: PRIVATE, OWNER
        UNIT_FIELD_RESISTANCES                                 = ObjectField.OBJECT_END + 0x089, // Size: 7, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_BONUS_RESISTANCE_MODS                       = ObjectField.OBJECT_END + 0x090, // Size: 7, Flags: PRIVATE, OWNER
        UNIT_FIELD_BASE_MANA                                   = ObjectField.OBJECT_END + 0x097, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_BASE_HEALTH                                 = ObjectField.OBJECT_END + 0x098, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_BYTES_2                                     = ObjectField.OBJECT_END + 0x099, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_ATTACK_POWER                                = ObjectField.OBJECT_END + 0x09A, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_ATTACK_POWER_MOD_POS                        = ObjectField.OBJECT_END + 0x09B, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_ATTACK_POWER_MOD_NEG                        = ObjectField.OBJECT_END + 0x09C, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER                     = ObjectField.OBJECT_END + 0x09D, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER                         = ObjectField.OBJECT_END + 0x09E, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_POS                 = ObjectField.OBJECT_END + 0x09F, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_NEG                 = ObjectField.OBJECT_END + 0x0A0, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER              = ObjectField.OBJECT_END + 0x0A1, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_MAIN_HAND_WEAPON_ATTACK_POWER               = ObjectField.OBJECT_END + 0x0A2, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_OFF_HAND_WEAPON_ATTACK_POWER                = ObjectField.OBJECT_END + 0x0A3, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_RANGED_HAND_WEAPON_ATTACK_POWER             = ObjectField.OBJECT_END + 0x0A4, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_ATTACK_SPEED_AURA                           = ObjectField.OBJECT_END + 0x0A5, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_LIFESTEAL                                   = ObjectField.OBJECT_END + 0x0A6, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_MINRANGEDDAMAGE                             = ObjectField.OBJECT_END + 0x0A7, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_MAXRANGEDDAMAGE                             = ObjectField.OBJECT_END + 0x0A8, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_POWER_COST_MODIFIER                         = ObjectField.OBJECT_END + 0x0A9, // Size: 7, Flags: PRIVATE, OWNER
        UNIT_FIELD_POWER_COST_MULTIPLIER                       = ObjectField.OBJECT_END + 0x0B0, // Size: 7, Flags: PRIVATE, OWNER
        UNIT_FIELD_MAXHEALTHMODIFIER                           = ObjectField.OBJECT_END + 0x0B7, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_HOVERHEIGHT                                 = ObjectField.OBJECT_END + 0x0B8, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MIN_ITEM_LEVEL_CUTOFF                       = ObjectField.OBJECT_END + 0x0B9, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MIN_ITEM_LEVEL                              = ObjectField.OBJECT_END + 0x0BA, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MAXITEMLEVEL                                = ObjectField.OBJECT_END + 0x0BB, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_WILD_BATTLEPET_LEVEL                        = ObjectField.OBJECT_END + 0x0BC, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_BATTLEPET_COMPANION_NAME_TIMESTAMP          = ObjectField.OBJECT_END + 0x0BD, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_INTERACT_SPELLID                            = ObjectField.OBJECT_END + 0x0BE, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_STATE_SPELL_VISUAL_ID                       = ObjectField.OBJECT_END + 0x0BF, // Size: 1, Flags: DYNAMIC, URGENT
        UNIT_FIELD_STATE_ANIM_ID                               = ObjectField.OBJECT_END + 0x0C0, // Size: 1, Flags: DYNAMIC, URGENT
        UNIT_FIELD_STATE_ANIM_KIT_ID                           = ObjectField.OBJECT_END + 0x0C1, // Size: 1, Flags: DYNAMIC, URGENT
        UNIT_FIELD_STATE_WORLD_EFFECT_ID                       = ObjectField.OBJECT_END + 0x0C2, // Size: 4, Flags: DYNAMIC, URGENT
        UNIT_FIELD_SCALE_DURATION                              = ObjectField.OBJECT_END + 0x0C6, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_LOOKS_LIKE_MOUNT_ID                         = ObjectField.OBJECT_END + 0x0C7, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_LOOKS_LIKE_CREATURE_ID                      = ObjectField.OBJECT_END + 0x0C8, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_LOOK_AT_CONTROLLER_ID                       = ObjectField.OBJECT_END + 0x0C9, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_GUILD_GUID                                  = ObjectField.OBJECT_END + 0x0CA, // Size: 4, Flags: PUBLIC
        UNIT_END                                               = ObjectField.OBJECT_END + 0x0CE,
    }

    public enum UnitDynamicField
    {
        UNIT_DYNAMIC_FIELD_PASSIVE_SPELLS                      = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000, // Flags: PUBLIC, URGENT
        UNIT_DYNAMIC_FIELD_WORLD_EFFECTS                       = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x001, // Flags: PUBLIC, URGENT
        UNIT_DYNAMIC_FIELD_CHANNEL_OBJECTS                     = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x002, // Flags: PUBLIC, URGENT
        UNIT_DYNAMIC_END                                       = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x003,
    }

    public enum PlayerField
    {
        PLAYER_DUEL_ARBITER                                    = UnitField.UNIT_END + 0x000, // Size: 4, Flags: PUBLIC
        PLAYER_WOW_ACCOUNT                                     = UnitField.UNIT_END + 0x004, // Size: 4, Flags: PUBLIC
        PLAYER_LOOT_TARGET_GUID                                = UnitField.UNIT_END + 0x008, // Size: 4, Flags: PUBLIC
        PLAYER_FLAGS                                           = UnitField.UNIT_END + 0x00C, // Size: 1, Flags: PUBLIC
        PLAYER_FLAGS_EX                                        = UnitField.UNIT_END + 0x00D, // Size: 1, Flags: PUBLIC
        PLAYER_GUILDRANK                                       = UnitField.UNIT_END + 0x00E, // Size: 1, Flags: PUBLIC
        PLAYER_GUILDDELETE_DATE                                = UnitField.UNIT_END + 0x00F, // Size: 1, Flags: PUBLIC
        PLAYER_GUILDLEVEL                                      = UnitField.UNIT_END + 0x010, // Size: 1, Flags: PUBLIC
        PLAYER_BYTES                                           = UnitField.UNIT_END + 0x011, // Size: 1, Flags: PUBLIC
        PLAYER_BYTES_2                                         = UnitField.UNIT_END + 0x012, // Size: 1, Flags: PUBLIC
        PLAYER_BYTES_3                                         = UnitField.UNIT_END + 0x013, // Size: 1, Flags: PUBLIC
        PLAYER_BYTES_4                                         = UnitField.UNIT_END + 0x014, // Size: 1, Flags: PUBLIC
        PLAYER_DUEL_TEAM                                       = UnitField.UNIT_END + 0x015, // Size: 1, Flags: PUBLIC
        PLAYER_GUILD_TIMESTAMP                                 = UnitField.UNIT_END + 0x016, // Size: 1, Flags: PUBLIC
        PLAYER_QUEST_LOG                                       = UnitField.UNIT_END + 0x017, // Size: 1600, Flags: PARTY_MEMBER
        PLAYER_VISIBLE_ITEM                                    = UnitField.UNIT_END + 0x657, // Size: 38, Flags: PUBLIC
        PLAYER_CHOSEN_TITLE                                    = UnitField.UNIT_END + 0x67D, // Size: 1, Flags: PUBLIC
        PLAYER_FAKE_INEBRIATION                                = UnitField.UNIT_END + 0x67E, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_VIRTUAL_PLAYER_REALM                      = UnitField.UNIT_END + 0x67F, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_CURRENT_SPEC_ID                           = UnitField.UNIT_END + 0x680, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_TAXI_MOUNT_ANIM_KIT_ID                    = UnitField.UNIT_END + 0x681, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_AVG_ITEM_LEVEL                            = UnitField.UNIT_END + 0x682, // Size: 4, Flags: PUBLIC
        PLAYER_FIELD_CURRENT_BATTLE_PET_BREED_QUALITY          = UnitField.UNIT_END + 0x686, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_HONOR_LEVEL                               = UnitField.UNIT_END + 0x687, // Size: 1, Flags: PUBLIC
        PLAYER_END                                             = UnitField.UNIT_END + 0x688,
    }

    public enum PlayerDynamicField
    {
        PLAYER_DYNAMIC_FIELD_ARENA_COOLDOWNS                   = UnitDynamicField.UNIT_DYNAMIC_END + 0x000, // Flags: PUBLIC
        PLAYER_DYNAMIC_END                                     = UnitDynamicField.UNIT_DYNAMIC_END + 0x001,
    }

    public enum ActivePlayerField
    {
        ACTIVE_PLAYER_FIELD_INV_SLOT_HEAD                      = PlayerField.PLAYER_END + 0x000, // Size: 780, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_FARSIGHT                           = PlayerField.PLAYER_END + 0x30C, // Size: 4, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_SUMMONED_BATTLE_PET_ID             = PlayerField.PLAYER_END + 0x310, // Size: 4, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_KNOWN_TITLES                       = PlayerField.PLAYER_END + 0x314, // Size: 12, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_COINAGE                            = PlayerField.PLAYER_END + 0x320, // Size: 2, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_XP                                 = PlayerField.PLAYER_END + 0x322, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_NEXT_LEVEL_XP                      = PlayerField.PLAYER_END + 0x323, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_TRIAL_XP                           = PlayerField.PLAYER_END + 0x324, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_SKILL_LINEID                       = PlayerField.PLAYER_END + 0x325, // Size: 896, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_CHARACTER_POINTS                   = PlayerField.PLAYER_END + 0x6A5, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MAX_TALENT_TIERS                   = PlayerField.PLAYER_END + 0x6A6, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_TRACK_CREATURES                    = PlayerField.PLAYER_END + 0x6A7, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_TRACK_RESOURCES                    = PlayerField.PLAYER_END + 0x6A8, // Size: 2, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_EXPERTISE                          = PlayerField.PLAYER_END + 0x6AA, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_OFFHAND_EXPERTISE                  = PlayerField.PLAYER_END + 0x6AB, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_RANGED_EXPERTISE                   = PlayerField.PLAYER_END + 0x6AC, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_COMBAT_RATING_EXPERTISE            = PlayerField.PLAYER_END + 0x6AD, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_BLOCK_PERCENTAGE                   = PlayerField.PLAYER_END + 0x6AE, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_DODGE_PERCENTAGE                   = PlayerField.PLAYER_END + 0x6AF, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_DODGE_PERCENTAGE_FROM_ATTRIBUTE    = PlayerField.PLAYER_END + 0x6B0, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_PARRY_PERCENTAGE                   = PlayerField.PLAYER_END + 0x6B1, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_PARRY_PERCENTAGE_FROM_ATTRIBUTE    = PlayerField.PLAYER_END + 0x6B2, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_CRIT_PERCENTAGE                    = PlayerField.PLAYER_END + 0x6B3, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_RANGED_CRIT_PERCENTAGE             = PlayerField.PLAYER_END + 0x6B4, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_OFFHAND_CRIT_PERCENTAGE            = PlayerField.PLAYER_END + 0x6B5, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_SPELL_CRIT_PERCENTAGE1             = PlayerField.PLAYER_END + 0x6B6, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_SHIELD_BLOCK                       = PlayerField.PLAYER_END + 0x6B7, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_SHIELD_BLOCK_CRIT_PERCENTAGE       = PlayerField.PLAYER_END + 0x6B8, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MASTERY                            = PlayerField.PLAYER_END + 0x6B9, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_SPEED                              = PlayerField.PLAYER_END + 0x6BA, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_AVOIDANCE                          = PlayerField.PLAYER_END + 0x6BB, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_STURDINESS                         = PlayerField.PLAYER_END + 0x6BC, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_VERSATILITY                        = PlayerField.PLAYER_END + 0x6BD, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_VERSATILITY_BONUS                  = PlayerField.PLAYER_END + 0x6BE, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_PVP_POWER_DAMAGE                   = PlayerField.PLAYER_END + 0x6BF, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_PVP_POWER_HEALING                  = PlayerField.PLAYER_END + 0x6C0, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_EXPLORED_ZONES                     = PlayerField.PLAYER_END + 0x6C1, // Size: 320, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_REST_INFO                          = PlayerField.PLAYER_END + 0x801, // Size: 4, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_DAMAGE_DONE_POS                = PlayerField.PLAYER_END + 0x805, // Size: 7, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_DAMAGE_DONE_NEG                = PlayerField.PLAYER_END + 0x80C, // Size: 7, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_DAMAGE_DONE_PCT                = PlayerField.PLAYER_END + 0x813, // Size: 7, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_HEALING_DONE_POS               = PlayerField.PLAYER_END + 0x81A, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_HEALING_PCT                    = PlayerField.PLAYER_END + 0x81B, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_HEALING_DONE_PCT               = PlayerField.PLAYER_END + 0x81C, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_PERIODIC_HEALING_DONE_PERCENT  = PlayerField.PLAYER_END + 0x81D, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_WEAPON_DMG_MULTIPLIERS             = PlayerField.PLAYER_END + 0x81E, // Size: 3, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_WEAPON_ATK_SPEED_MULTIPLIERS       = PlayerField.PLAYER_END + 0x821, // Size: 3, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_SPELL_POWER_PCT                = PlayerField.PLAYER_END + 0x824, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_RESILIENCE_PERCENT             = PlayerField.PLAYER_END + 0x825, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_OVERRIDE_SPELL_POWER_BY_AP_PCT     = PlayerField.PLAYER_END + 0x826, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_OVERRIDE_AP_BY_SPELL_POWER_PERCENT = PlayerField.PLAYER_END + 0x827, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_TARGET_RESISTANCE              = PlayerField.PLAYER_END + 0x828, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE     = PlayerField.PLAYER_END + 0x829, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_LOCAL_FLAGS                        = PlayerField.PLAYER_END + 0x82A, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_BYTES                              = PlayerField.PLAYER_END + 0x82B, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_PVP_MEDALS                         = PlayerField.PLAYER_END + 0x82C, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_BUYBACK_PRICE                      = PlayerField.PLAYER_END + 0x82D, // Size: 12, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_BUYBACK_TIMESTAMP                  = PlayerField.PLAYER_END + 0x839, // Size: 12, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_KILLS                              = PlayerField.PLAYER_END + 0x845, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_LIFETIME_HONORABLE_KILLS           = PlayerField.PLAYER_END + 0x846, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_WATCHED_FACTION_INDEX              = PlayerField.PLAYER_END + 0x847, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_COMBAT_RATING                      = PlayerField.PLAYER_END + 0x848, // Size: 32, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_ARENA_TEAM_INFO                    = PlayerField.PLAYER_END + 0x868, // Size: 54, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MAX_LEVEL                          = PlayerField.PLAYER_END + 0x89E, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_SCALING_PLAYER_LEVEL_DELTA         = PlayerField.PLAYER_END + 0x89F, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MAX_CREATURE_SCALING_LEVEL         = PlayerField.PLAYER_END + 0x8A0, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_NO_REAGENT_COST                    = PlayerField.PLAYER_END + 0x8A1, // Size: 4, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_PET_SPELL_POWER                    = PlayerField.PLAYER_END + 0x8A5, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_PROFESSION_SKILL_LINE              = PlayerField.PLAYER_END + 0x8A6, // Size: 2, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_UI_HIT_MODIFIER                    = PlayerField.PLAYER_END + 0x8A8, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_UI_SPELL_HIT_MODIFIER              = PlayerField.PLAYER_END + 0x8A9, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_HOME_REALM_TIME_OFFSET             = PlayerField.PLAYER_END + 0x8AA, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_MOD_PET_HASTE                      = PlayerField.PLAYER_END + 0x8AB, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_BYTES2                             = PlayerField.PLAYER_END + 0x8AC, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_BYTES3                             = PlayerField.PLAYER_END + 0x8AD, // Size: 1, Flags: PUBLIC, URGENT_SELF_ONLY
        ACTIVE_PLAYER_FIELD_LFG_BONUS_FACTION_ID               = PlayerField.PLAYER_END + 0x8AE, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_LOOT_SPEC_ID                       = PlayerField.PLAYER_END + 0x8AF, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_OVERRIDE_ZONE_PVP_TYPE             = PlayerField.PLAYER_END + 0x8B0, // Size: 1, Flags: PUBLIC, URGENT_SELF_ONLY
        ACTIVE_PLAYER_FIELD_BAG_SLOT_FLAGS                     = PlayerField.PLAYER_END + 0x8B1, // Size: 4, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_BANK_BAG_SLOT_FLAGS                = PlayerField.PLAYER_END + 0x8B5, // Size: 7, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_INSERT_ITEMS_LEFT_TO_RIGHT         = PlayerField.PLAYER_END + 0x8BC, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_QUEST_COMPLETED                    = PlayerField.PLAYER_END + 0x8BD, // Size: 1750, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_HONOR                              = PlayerField.PLAYER_END + 0xF93, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_HONOR_NEXT_LEVEL                   = PlayerField.PLAYER_END + 0xF94, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_PVP_TIER_MAX_FROM_WINS             = PlayerField.PLAYER_END + 0xF95, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_FIELD_PVP_LAST_WEEKS_TIER_MAX_FROM_WINS  = PlayerField.PLAYER_END + 0xF96, // Size: 1, Flags: PUBLIC
        ACTIVE_PLAYER_END                                      = PlayerField.PLAYER_END + 0xF97,
    }

    public enum ActivePlayerDynamicField
    {
        ACTIVE_PLAYER_DYNAMIC_FIELD_RESERACH_SITE              = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x000, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_RESEARCH_SITE_PROGRESS     = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x001, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_DAILY_QUESTS               = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x002, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_AVAILABLE_QUEST_LINE_X_QUEST_ID = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x003, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_HEIRLOOMS                  = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x005, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_HEIRLOOM_FLAGS             = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x006, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_TOYS                       = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x007, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_TRANSMOG                   = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x008, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_CONDITIONAL_TRANSMOG       = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x009, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_SELF_RES_SPELLS            = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x00A, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_CHARACTER_RESTRICTIONS     = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x00B, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_SPELL_PCT_MOD_BY_LABEL     = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x00C, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_SPELL_FLAT_MOD_BY_LABEL    = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x00D, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_FIELD_RESERACH                   = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x00E, // Flags: PUBLIC
        ACTIVE_PLAYER_DYNAMIC_END                              = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x00F,
    }

    public enum GameObjectField
    {
        GAMEOBJECT_FIELD_CREATED_BY                            = ObjectField.OBJECT_END + 0x000, // Size: 4, Flags: PUBLIC
        GAMEOBJECT_FIELD_GUILD_GUID                            = ObjectField.OBJECT_END + 0x004, // Size: 4, Flags: PUBLIC
        GAMEOBJECT_DISPLAYID                                   = ObjectField.OBJECT_END + 0x008, // Size: 1, Flags: DYNAMIC, URGENT
        GAMEOBJECT_FLAGS                                       = ObjectField.OBJECT_END + 0x009, // Size: 1, Flags: PUBLIC, URGENT
        GAMEOBJECT_PARENTROTATION                              = ObjectField.OBJECT_END + 0x00A, // Size: 4, Flags: PUBLIC
        GAMEOBJECT_FACTION                                     = ObjectField.OBJECT_END + 0x00E, // Size: 1, Flags: PUBLIC
        GAMEOBJECT_LEVEL                                       = ObjectField.OBJECT_END + 0x00F, // Size: 1, Flags: PUBLIC
        GAMEOBJECT_BYTES_1                                     = ObjectField.OBJECT_END + 0x010, // Size: 1, Flags: PUBLIC, URGENT
        GAMEOBJECT_SPELL_VISUAL_ID                             = ObjectField.OBJECT_END + 0x011, // Size: 1, Flags: PUBLIC, DYNAMIC, URGENT
        GAMEOBJECT_STATE_SPELL_VISUAL_ID                       = ObjectField.OBJECT_END + 0x012, // Size: 1, Flags: DYNAMIC, URGENT
        GAMEOBJECT_STATE_ANIM_ID                               = ObjectField.OBJECT_END + 0x013, // Size: 1, Flags: DYNAMIC, URGENT
        GAMEOBJECT_STATE_ANIM_KIT_ID                           = ObjectField.OBJECT_END + 0x014, // Size: 1, Flags: DYNAMIC, URGENT
        GAMEOBJECT_STATE_WORLD_EFFECT_ID                       = ObjectField.OBJECT_END + 0x015, // Size: 4, Flags: DYNAMIC, URGENT
        GAMEOBJECT_FIELD_CUSTOM_PARAM                          = ObjectField.OBJECT_END + 0x019, // Size: 1, Flags: PUBLIC, URGENT
        GAMEOBJECT_END                                         = ObjectField.OBJECT_END + 0x01A,
    }

    public enum GameObjectDynamicField
    {
        GAMEOBJECT_DYNAMIC_ENABLE_DOODAD_SETS                  = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000, // Flags: PUBLIC
        GAMEOBJECT_DYNAMIC_END                                 = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x001,
    }

    public enum DynamicObjectField
    {
        DYNAMICOBJECT_CASTER                                   = ObjectField.OBJECT_END + 0x000, // Size: 4, Flags: PUBLIC
        DYNAMICOBJECT_TYPE                                     = ObjectField.OBJECT_END + 0x004, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_SPELL_X_SPELL_VISUAL_ID                  = ObjectField.OBJECT_END + 0x005, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_SPELLID                                  = ObjectField.OBJECT_END + 0x006, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_RADIUS                                   = ObjectField.OBJECT_END + 0x007, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_CASTTIME                                 = ObjectField.OBJECT_END + 0x008, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_END                                      = ObjectField.OBJECT_END + 0x009,
    }

    public enum DynamicObjectDynamicField
    {
        DYNAMICOBJECT_DYNAMIC_END                              = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000,
    }

    public enum CorpseField
    {
        CORPSE_FIELD_OWNER                                     = ObjectField.OBJECT_END + 0x000, // Size: 4, Flags: PUBLIC
        CORPSE_FIELD_PARTY                                     = ObjectField.OBJECT_END + 0x004, // Size: 4, Flags: PUBLIC
        CORPSE_FIELD_GUILD_GUID                                = ObjectField.OBJECT_END + 0x008, // Size: 4, Flags: PUBLIC
        CORPSE_FIELD_DISPLAY_ID                                = ObjectField.OBJECT_END + 0x00C, // Size: 1, Flags: PUBLIC
        CORPSE_FIELD_ITEM                                      = ObjectField.OBJECT_END + 0x00D, // Size: 19, Flags: PUBLIC
        CORPSE_FIELD_BYTES_1                                   = ObjectField.OBJECT_END + 0x020, // Size: 1, Flags: PUBLIC
        CORPSE_FIELD_BYTES_2                                   = ObjectField.OBJECT_END + 0x021, // Size: 1, Flags: PUBLIC
        CORPSE_FIELD_FLAGS                                     = ObjectField.OBJECT_END + 0x022, // Size: 1, Flags: PUBLIC
        CORPSE_FIELD_DYNAMIC_FLAGS                             = ObjectField.OBJECT_END + 0x023, // Size: 1, Flags: DYNAMIC
        CORPSE_FIELD_FACTIONTEMPLATE                           = ObjectField.OBJECT_END + 0x024, // Size: 1, Flags: PUBLIC
        CORPSE_FIELD_CUSTOM_DISPLAY_OPTION                     = ObjectField.OBJECT_END + 0x025, // Size: 1, Flags: PUBLIC
        CORPSE_END                                             = ObjectField.OBJECT_END + 0x026,
    }

    public enum CorpseDynamicField
    {
        CORPSE_DYNAMIC_END                                     = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000,
    }

    public enum AreaTriggerField
    {
        AREATRIGGER_OVERRIDE_SCALE_CURVE                       = ObjectField.OBJECT_END + 0x000, // Size: 7, Flags: PUBLIC, URGENT
        AREATRIGGER_EXTRA_SCALE_CURVE                          = ObjectField.OBJECT_END + 0x007, // Size: 7, Flags: PUBLIC, URGENT
        AREATRIGGER_CASTER                                     = ObjectField.OBJECT_END + 0x00E, // Size: 4, Flags: PUBLIC
        AREATRIGGER_DURATION                                   = ObjectField.OBJECT_END + 0x012, // Size: 1, Flags: PUBLIC
        AREATRIGGER_TIME_TO_TARGET                             = ObjectField.OBJECT_END + 0x013, // Size: 1, Flags: PUBLIC, URGENT
        AREATRIGGER_TIME_TO_TARGET_SCALE                       = ObjectField.OBJECT_END + 0x014, // Size: 1, Flags: PUBLIC, URGENT
        AREATRIGGER_TIME_TO_TARGET_EXTRA_SCALE                 = ObjectField.OBJECT_END + 0x015, // Size: 1, Flags: PUBLIC, URGENT
        AREATRIGGER_SPELLID                                    = ObjectField.OBJECT_END + 0x016, // Size: 1, Flags: PUBLIC
        AREATRIGGER_SPELL_FOR_VISUALS                          = ObjectField.OBJECT_END + 0x017, // Size: 1, Flags: PUBLIC
        AREATRIGGER_SPELL_X_SPELL_VISUAL_ID                    = ObjectField.OBJECT_END + 0x018, // Size: 1, Flags: PUBLIC
        AREATRIGGER_BOUNDS_RADIUS_2D                           = ObjectField.OBJECT_END + 0x019, // Size: 1, Flags: DYNAMIC, URGENT
        AREATRIGGER_DECAL_PROPERTIES_ID                        = ObjectField.OBJECT_END + 0x01A, // Size: 1, Flags: PUBLIC
        AREATRIGGER_CREATING_EFFECT_GUID                       = ObjectField.OBJECT_END + 0x01B, // Size: 4, Flags: PUBLIC
        AREATRIGGER_END                                        = ObjectField.OBJECT_END + 0x01F,
    }

    public enum AreaTriggerDynamicField
    {
        AREATRIGGER_DYNAMIC_END                                = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000,
    }

    public enum SceneObjectField
    {
        SCENEOBJECT_FIELD_SCRIPT_PACKAGE_ID                    = ObjectField.OBJECT_END + 0x000, // Size: 1, Flags: PUBLIC
        SCENEOBJECT_FIELD_RND_SEED_VAL                         = ObjectField.OBJECT_END + 0x001, // Size: 1, Flags: PUBLIC
        SCENEOBJECT_FIELD_CREATEDBY                            = ObjectField.OBJECT_END + 0x002, // Size: 4, Flags: PUBLIC
        SCENEOBJECT_FIELD_SCENE_TYPE                           = ObjectField.OBJECT_END + 0x006, // Size: 1, Flags: PUBLIC
        SCENEOBJECT_END                                        = ObjectField.OBJECT_END + 0x007,
    }

    public enum SceneObjectDynamicField
    {
        SCENEOBJECT_DYNAMIC_END                                = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000,
    }

    public enum ConversationField
    {
        CONVERSATION_LAST_LINE_END_TIME                        = ObjectField.OBJECT_END + 0x000, // Size: 1, Flags: DYNAMIC
        CONVERSATION_END                                       = ObjectField.OBJECT_END + 0x001,
    }

    public enum ConversationDynamicField
    {
        CONVERSATION_DYNAMIC_FIELD_ACTORS                      = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000, // Flags: PUBLIC
        CONVERSATION_DYNAMIC_FIELD_LINES                       = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x001, // Flags: 0x100
        CONVERSATION_DYNAMIC_END                               = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x002,
    }

    // ReSharper restore InconsistentNaming
}
