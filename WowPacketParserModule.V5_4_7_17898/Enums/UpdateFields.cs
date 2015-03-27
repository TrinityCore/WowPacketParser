namespace WowPacketParserModule.V5_4_7_17898.Enums
{
    // ReSharper disable InconsistentNaming
    // 5.4.7.18019
    public enum ObjectField
    {
        OBJECT_FIELD_GUID                                   = 0x000, // Size: 2, Flags: PUBLIC
        OBJECT_FIELD_DATA                                   = 0x002, // Size: 2, Flags: PUBLIC
        OBJECT_FIELD_TYPE                                   = 0x004, // Size: 1, Flags: PUBLIC
        OBJECT_FIELD_ENTRY                                  = 0x005, // Size: 1, Flags: DYNAMIC
        OBJECT_DYNAMIC_FLAGS                                = 0x006, // Size: 1, Flags: DYNAMIC, 0x100
        OBJECT_FIELD_SCALE_X                                = 0x007, // Size: 1, Flags: PUBLIC
        OBJECT_END                                          = 0x008
    }

    public enum ItemField
    {
        ITEM_FIELD_OWNER                                    = ObjectField.OBJECT_END + 0x000, // Size: 2, Flags: PUBLIC
        ITEM_FIELD_CONTAINED                                = ObjectField.OBJECT_END + 0x002, // Size: 2, Flags: PUBLIC
        ITEM_FIELD_CREATOR                                  = ObjectField.OBJECT_END + 0x004, // Size: 2, Flags: PUBLIC
        ITEM_FIELD_GIFTCREATOR                              = ObjectField.OBJECT_END + 0x006, // Size: 2, Flags: PUBLIC
        ITEM_FIELD_STACK_COUNT                              = ObjectField.OBJECT_END + 0x008, // Size: 1, Flags: OWNER
        ITEM_FIELD_DURATION                                 = ObjectField.OBJECT_END + 0x009, // Size: 1, Flags: OWNER
        ITEM_FIELD_SPELL_CHARGES                            = ObjectField.OBJECT_END + 0x00A, // Size: 5, Flags: OWNER
        ITEM_FIELD_FLAGS                                    = ObjectField.OBJECT_END + 0x00F, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_ENCHANTMENT                              = ObjectField.OBJECT_END + 0x010, // Size: 39, Flags: PUBLIC
        ITEM_FIELD_PROPERTY_SEED                            = ObjectField.OBJECT_END + 0x037, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_RANDOM_PROPERTIES_ID                     = ObjectField.OBJECT_END + 0x038, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_DURABILITY                               = ObjectField.OBJECT_END + 0x039, // Size: 1, Flags: OWNER
        ITEM_FIELD_MAXDURABILITY                            = ObjectField.OBJECT_END + 0x03A, // Size: 1, Flags: OWNER
        ITEM_FIELD_CREATE_PLAYED_TIME                       = ObjectField.OBJECT_END + 0x03B, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_MODIFIERS_MASK                           = ObjectField.OBJECT_END + 0x03C, // Size: 1, Flags: OWNER
        ITEM_END                                            = ObjectField.OBJECT_END + 0x03D
    }

    public enum ItemDynamicField
    {
        ITEM_DYNAMIC_FIELD_MODIFIERS                        = 0x000, //  Flags: OWNER
        ITEM_DYNAMIC_END                                    = 0x001
    }

    public enum ContainerField
    {
        CONTAINER_FIELD_SLOT_1                              = ItemField.ITEM_END + 0x000, // Size: 72, Flags: PUBLIC
        CONTAINER_FIELD_NUM_SLOTS                           = ItemField.ITEM_END + 0x048, // Size: 1, Flags: PUBLIC
        CONTAINER_END                                       = ItemField.ITEM_END + 0x049
    }

    public enum UnitField
    {
        UNIT_FIELD_CHARM                                    = ObjectField.OBJECT_END + 0x000, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_SUMMON                                   = ObjectField.OBJECT_END + 0x002, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_CRITTER                                  = ObjectField.OBJECT_END + 0x004, // Size: 2, Flags: PRIVATE
        UNIT_FIELD_CHARMEDBY                                = ObjectField.OBJECT_END + 0x006, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_SUMMONEDBY                               = ObjectField.OBJECT_END + 0x008, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_CREATEDBY                                = ObjectField.OBJECT_END + 0x00A, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_DEMON_CREATOR                            = ObjectField.OBJECT_END + 0x00C, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_TARGET                                   = ObjectField.OBJECT_END + 0x00E, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_BATTLE_PET_COMPANION_GUID                = ObjectField.OBJECT_END + 0x010, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_CHANNEL_OBJECT                           = ObjectField.OBJECT_END + 0x012, // Size: 2, Flags: PUBLIC, 0x100
        UNIT_CHANNEL_SPELL                                  = ObjectField.OBJECT_END + 0x014, // Size: 1, Flags: PUBLIC, 0x100
        UNIT_FIELD_SUMMONED_BY_HOME_REALM                   = ObjectField.OBJECT_END + 0x015, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_BYTES_0                                  = ObjectField.OBJECT_END + 0x016, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_DISPLAY_POWER                            = ObjectField.OBJECT_END + 0x017, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_OVERRIDE_DISPLAY_POWER_ID                = ObjectField.OBJECT_END + 0x018, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_HEALTH                                   = ObjectField.OBJECT_END + 0x019, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_POWER                                    = ObjectField.OBJECT_END + 0x01A, // Size: 5, Flags: PUBLIC
        UNIT_FIELD_MAXHEALTH                                = ObjectField.OBJECT_END + 0x01F, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MAXPOWER                                 = ObjectField.OBJECT_END + 0x020, // Size: 5, Flags: PUBLIC
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER                = ObjectField.OBJECT_END + 0x025, // Size: 5, Flags: PRIVATE, OWNER, UNUSED2
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER    = ObjectField.OBJECT_END + 0x02A, // Size: 5, Flags: PRIVATE, OWNER, UNUSED2
        UNIT_FIELD_LEVEL                                    = ObjectField.OBJECT_END + 0x02F, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_EFFECTIVE_LEVEL                          = ObjectField.OBJECT_END + 0x030, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_FACTIONTEMPLATE                          = ObjectField.OBJECT_END + 0x031, // Size: 1, Flags: PUBLIC
        UNIT_VIRTUAL_ITEM_SLOT_ID1                          = ObjectField.OBJECT_END + 0x032, // Size: 1, Flags: PUBLIC
        UNIT_VIRTUAL_ITEM_SLOT_ID2                          = ObjectField.OBJECT_END + 0x033, // Size: 1, Flags: PUBLIC
        UNIT_VIRTUAL_ITEM_SLOT_ID3                          = ObjectField.OBJECT_END + 0x034, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_FLAGS                                    = ObjectField.OBJECT_END + 0x035, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_FLAGS_2                                  = ObjectField.OBJECT_END + 0x036, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_AURASTATE                                = ObjectField.OBJECT_END + 0x037, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_BASEATTACKTIME                           = ObjectField.OBJECT_END + 0x038, // Size: 2, Flags: PUBLIC
        UNIT_FIELD_RANGEDATTACKTIME                         = ObjectField.OBJECT_END + 0x03A, // Size: 1, Flags: PRIVATE
        UNIT_FIELD_BOUNDINGRADIUS                           = ObjectField.OBJECT_END + 0x03B, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_COMBATREACH                              = ObjectField.OBJECT_END + 0x03C, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_DISPLAYID                                = ObjectField.OBJECT_END + 0x03D, // Size: 1, Flags: DYNAMIC, 0x100
        UNIT_FIELD_NATIVEDISPLAYID                          = ObjectField.OBJECT_END + 0x03E, // Size: 1, Flags: PUBLIC, 0x100
        UNIT_FIELD_MOUNTDISPLAYID                           = ObjectField.OBJECT_END + 0x03F, // Size: 1, Flags: PUBLIC, 0x100
        UNIT_FIELD_MINDAMAGE                                = ObjectField.OBJECT_END + 0x040, // Size: 1, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_MAXDAMAGE                                = ObjectField.OBJECT_END + 0x041, // Size: 1, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_MINOFFHANDDAMAGE                         = ObjectField.OBJECT_END + 0x042, // Size: 1, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_MAXOFFHANDDAMAGE                         = ObjectField.OBJECT_END + 0x043, // Size: 1, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_BYTES_1                                  = ObjectField.OBJECT_END + 0x044, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_PETNUMBER                                = ObjectField.OBJECT_END + 0x045, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_PET_NAME_TIMESTAMP                       = ObjectField.OBJECT_END + 0x046, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_PETEXPERIENCE                            = ObjectField.OBJECT_END + 0x047, // Size: 1, Flags: OWNER
        UNIT_FIELD_PETNEXTLEVELEXP                          = ObjectField.OBJECT_END + 0x048, // Size: 1, Flags: OWNER
        UNIT_MOD_CAST_SPEED                                 = ObjectField.OBJECT_END + 0x049, // Size: 1, Flags: PUBLIC
        UNIT_MOD_CAST_HASTE                                 = ObjectField.OBJECT_END + 0x04A, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MOD_HASTE                                = ObjectField.OBJECT_END + 0x04B, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MOD_RANGED_HASTE                         = ObjectField.OBJECT_END + 0x04C, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MOD_HASTE_REGEN                          = ObjectField.OBJECT_END + 0x04D, // Size: 1, Flags: PUBLIC
        UNIT_CREATED_BY_SPELL                               = ObjectField.OBJECT_END + 0x04E, // Size: 1, Flags: PUBLIC
        UNIT_NPC_FLAGS                                      = ObjectField.OBJECT_END + 0x04F, // Size: 2, Flags: PUBLIC
        UNIT_NPC_EMOTESTATE                                 = ObjectField.OBJECT_END + 0x051, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_STAT                                     = ObjectField.OBJECT_END + 0x052, // Size: 5, Flags: PRIVATE, OWNER
        UNIT_FIELD_POSSTAT                                  = ObjectField.OBJECT_END + 0x057, // Size: 5, Flags: PRIVATE, OWNER
        UNIT_FIELD_NEGSTAT                                  = ObjectField.OBJECT_END + 0x05C, // Size: 5, Flags: PRIVATE, OWNER
        UNIT_FIELD_RESISTANCES                              = ObjectField.OBJECT_END + 0x061, // Size: 7, Flags: PRIVATE, OWNER, SPECIAL_INFO
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE               = ObjectField.OBJECT_END + 0x068, // Size: 7, Flags: PRIVATE, OWNER
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE               = ObjectField.OBJECT_END + 0x06F, // Size: 7, Flags: PRIVATE, OWNER
        UNIT_FIELD_BASE_MANA                                = ObjectField.OBJECT_END + 0x076, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_BASE_HEALTH                              = ObjectField.OBJECT_END + 0x077, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_BYTES_2                                  = ObjectField.OBJECT_END + 0x078, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_ATTACK_POWER                             = ObjectField.OBJECT_END + 0x079, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_ATTACK_POWER_MOD_POS                     = ObjectField.OBJECT_END + 0x07A, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_ATTACK_POWER_MOD_NEG                     = ObjectField.OBJECT_END + 0x07B, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER                  = ObjectField.OBJECT_END + 0x07C, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER                      = ObjectField.OBJECT_END + 0x07D, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_POS              = ObjectField.OBJECT_END + 0x07E, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_NEG              = ObjectField.OBJECT_END + 0x07F, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER           = ObjectField.OBJECT_END + 0x080, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_MINRANGEDDAMAGE                          = ObjectField.OBJECT_END + 0x081, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_MAXRANGEDDAMAGE                          = ObjectField.OBJECT_END + 0x082, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_POWER_COST_MODIFIER                      = ObjectField.OBJECT_END + 0x083, // Size: 7, Flags: PRIVATE, OWNER
        UNIT_FIELD_POWER_COST_MULTIPLIER                    = ObjectField.OBJECT_END + 0x08A, // Size: 7, Flags: PRIVATE, OWNER
        UNIT_FIELD_MAXHEALTHMODIFIER                        = ObjectField.OBJECT_END + 0x091, // Size: 1, Flags: PRIVATE, OWNER
        UNIT_FIELD_HOVERHEIGHT                              = ObjectField.OBJECT_END + 0x092, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MIN_ITEM_LEVEL                           = ObjectField.OBJECT_END + 0x093, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_MAXITEMLEVEL                             = ObjectField.OBJECT_END + 0x094, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_WILD_BATTLEPET_LEVEL                     = ObjectField.OBJECT_END + 0x095, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_BATTLEPET_COMPANION_NAME_TIMESTAMP       = ObjectField.OBJECT_END + 0x096, // Size: 1, Flags: PUBLIC
        UNIT_FIELD_INTERACT_SPELLID                         = ObjectField.OBJECT_END + 0x097, // Size: 1, Flags: PUBLIC
        UNIT_END                                            = ObjectField.OBJECT_END + 0x098
    }

    public enum UnitDynamicField
    {
        UNIT_DYNAMIC_FIELD_PASSIVE_SPELLS                   = 0x000, //  Flags: PUBLIC, 0x100
        UNIT_DYNAMIC_FIELD_WORLD_EFFECTS                    = 0x001, //  Flags: PUBLIC, 0x100
        UNIT_DYNAMIC_END                                    = 0x002
    }

    public enum PlayerField
    {
        PLAYER_DUEL_ARBITER                                 = UnitField.UNIT_END + 0x000, // Size: 2, Flags: PUBLIC
        PLAYER_FLAGS                                        = UnitField.UNIT_END + 0x002, // Size: 1, Flags: PUBLIC
        PLAYER_GUILDRANK                                    = UnitField.UNIT_END + 0x003, // Size: 1, Flags: PUBLIC
        PLAYER_GUILDDELETE_DATE                             = UnitField.UNIT_END + 0x004, // Size: 1, Flags: PUBLIC
        PLAYER_GUILDLEVEL                                   = UnitField.UNIT_END + 0x005, // Size: 1, Flags: PUBLIC
        PLAYER_BYTES                                        = UnitField.UNIT_END + 0x006, // Size: 1, Flags: PUBLIC
        PLAYER_BYTES_2                                      = UnitField.UNIT_END + 0x007, // Size: 1, Flags: PUBLIC
        PLAYER_BYTES_3                                      = UnitField.UNIT_END + 0x008, // Size: 1, Flags: PUBLIC
        PLAYER_DUEL_TEAM                                    = UnitField.UNIT_END + 0x009, // Size: 1, Flags: PUBLIC
        PLAYER_GUILD_TIMESTAMP                              = UnitField.UNIT_END + 0x00A, // Size: 1, Flags: PUBLIC
        PLAYER_QUEST_LOG                                    = UnitField.UNIT_END + 0x00B, // Size: 750, Flags: PARTY_MEMBER
        PLAYER_VISIBLE_ITEM                                 = UnitField.UNIT_END + 0x2F9, // Size: 38, Flags: PUBLIC
        PLAYER_CHOSEN_TITLE                                 = UnitField.UNIT_END + 0x31F, // Size: 1, Flags: PUBLIC
        PLAYER_FAKE_INEBRIATION                             = UnitField.UNIT_END + 0x320, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_VIRTUAL_PLAYER_REALM                   = UnitField.UNIT_END + 0x321, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_CURRENT_SPEC_ID                        = UnitField.UNIT_END + 0x322, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_TAXI_MOUNT_ANIM_KIT_ID                 = UnitField.UNIT_END + 0x323, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_CURRENT_BATTLE_PET_BREED_QUALITY       = UnitField.UNIT_END + 0x324, // Size: 1, Flags: PUBLIC
        PLAYER_FIELD_INV_SLOT_HEAD                          = UnitField.UNIT_END + 0x325, // Size: 172, Flags: PRIVATE
        PLAYER_FARSIGHT                                     = UnitField.UNIT_END + 0x3D1, // Size: 2, Flags: PRIVATE
        PLAYER__FIELD_KNOWN_TITLES                          = UnitField.UNIT_END + 0x3D3, // Size: 10, Flags: PRIVATE
        PLAYER_FIELD_COINAGE                                = UnitField.UNIT_END + 0x3DD, // Size: 2, Flags: PRIVATE
        PLAYER_XP                                           = UnitField.UNIT_END + 0x3DF, // Size: 1, Flags: PRIVATE
        PLAYER_NEXT_LEVEL_XP                                = UnitField.UNIT_END + 0x3E0, // Size: 1, Flags: PRIVATE
        PLAYER_SKILL_LINEID                                 = UnitField.UNIT_END + 0x3E1, // Size: 448, Flags: PRIVATE
        PLAYER_CHARACTER_POINTS                             = UnitField.UNIT_END + 0x5A1, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_MAX_TALENT_TIERS                       = UnitField.UNIT_END + 0x5A2, // Size: 1, Flags: PRIVATE
        PLAYER_TRACK_CREATURES                              = UnitField.UNIT_END + 0x5A3, // Size: 1, Flags: PRIVATE
        PLAYER_TRACK_RESOURCES                              = UnitField.UNIT_END + 0x5A4, // Size: 1, Flags: PRIVATE
        PLAYER_EXPERTISE                                    = UnitField.UNIT_END + 0x5A5, // Size: 1, Flags: PRIVATE
        PLAYER_OFFHAND_EXPERTISE                            = UnitField.UNIT_END + 0x5A6, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_RANGED_EXPERTISE                       = UnitField.UNIT_END + 0x5A7, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_COMBAT_RATING_EXPERTISE                = UnitField.UNIT_END + 0x5A8, // Size: 1, Flags: PRIVATE
        PLAYER_BLOCK_PERCENTAGE                             = UnitField.UNIT_END + 0x5A9, // Size: 1, Flags: PRIVATE
        PLAYER_DODGE_PERCENTAGE                             = UnitField.UNIT_END + 0x5AA, // Size: 1, Flags: PRIVATE
        PLAYER_PARRY_PERCENTAGE                             = UnitField.UNIT_END + 0x5AB, // Size: 1, Flags: PRIVATE
        PLAYER_CRIT_PERCENTAGE                              = UnitField.UNIT_END + 0x5AC, // Size: 1, Flags: PRIVATE
        PLAYER_RANGED_CRIT_PERCENTAGE                       = UnitField.UNIT_END + 0x5AD, // Size: 1, Flags: PRIVATE
        PLAYER_OFFHAND_CRIT_PERCENTAGE                      = UnitField.UNIT_END + 0x5AE, // Size: 1, Flags: PRIVATE
        PLAYER_SPELL_CRIT_PERCENTAGE1                       = UnitField.UNIT_END + 0x5AF, // Size: 7, Flags: PRIVATE
        PLAYER_SHIELD_BLOCK                                 = UnitField.UNIT_END + 0x5B6, // Size: 1, Flags: PRIVATE
        PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE                 = UnitField.UNIT_END + 0x5B7, // Size: 1, Flags: PRIVATE
        PLAYER_MASTERY                                      = UnitField.UNIT_END + 0x5B8, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_PVP_POWER_DAMAGE                       = UnitField.UNIT_END + 0x5B9, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_PVP_POWER_HEALING                      = UnitField.UNIT_END + 0x5BA, // Size: 1, Flags: PRIVATE
        PLAYER_EXPLORED_ZONES_1                             = UnitField.UNIT_END + 0x5BB, // Size: 200, Flags: PRIVATE
        PLAYER_REST_STATE_EXPERIENCE                        = UnitField.UNIT_END + 0x683, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS                    = UnitField.UNIT_END + 0x684, // Size: 7, Flags: PRIVATE
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG                    = UnitField.UNIT_END + 0x68B, // Size: 7, Flags: PRIVATE
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT                    = UnitField.UNIT_END + 0x692, // Size: 7, Flags: PRIVATE
        PLAYER_FIELD_MOD_HEALING_DONE_POS                   = UnitField.UNIT_END + 0x699, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_MOD_HEALING_PCT                        = UnitField.UNIT_END + 0x69A, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_MOD_HEALING_DONE_PCT                   = UnitField.UNIT_END + 0x69B, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_MOD_PERIODIC_HEALING_DONE_PERCENT      = UnitField.UNIT_END + 0x69C, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_WEAPON_DMG_MULTIPLIERS                 = UnitField.UNIT_END + 0x69D, // Size: 3, Flags: PRIVATE
        PLAYER_FIELD_MOD_SPELL_POWER_PCT                    = UnitField.UNIT_END + 0x6A0, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_MOD_RESILIENCE_PERCENT                 = UnitField.UNIT_END + 0x6A1, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_OVERRIDE_SPELL_POWER_BY_AP_PCT         = UnitField.UNIT_END + 0x6A2, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_OVERRIDE_AP_BY_SPELL_POWER_PERCENT     = UnitField.UNIT_END + 0x6A3, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_MOD_TARGET_RESISTANCE                  = UnitField.UNIT_END + 0x6A4, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE         = UnitField.UNIT_END + 0x6A5, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_BYTES                                  = UnitField.UNIT_END + 0x6A6, // Size: 1, Flags: PRIVATE
        PLAYER_SELF_RES_SPELL                               = UnitField.UNIT_END + 0x6A7, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_PVP_MEDALS                             = UnitField.UNIT_END + 0x6A8, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_BUYBACK_PRICE_1                        = UnitField.UNIT_END + 0x6A9, // Size: 12, Flags: PRIVATE
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1                    = UnitField.UNIT_END + 0x6B5, // Size: 12, Flags: PRIVATE
        PLAYER_FIELD_KILLS                                  = UnitField.UNIT_END + 0x6C1, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS               = UnitField.UNIT_END + 0x6C2, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_WATCHED_FACTION_INDEX                  = UnitField.UNIT_END + 0x6C3, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_COMBAT_RATING_1                        = UnitField.UNIT_END + 0x6C4, // Size: 27, Flags: PRIVATE
        PLAYER_FIELD_ARENA_TEAM_INFO_1_1                    = UnitField.UNIT_END + 0x6DF, // Size: 24, Flags: PRIVATE
        PLAYER_FIELD_MAX_LEVEL                              = UnitField.UNIT_END + 0x6F7, // Size: 1, Flags: PRIVATE
        PLAYER_RUNE_REGEN_1                                 = UnitField.UNIT_END + 0x6F8, // Size: 4, Flags: PRIVATE
        PLAYER_NO_REAGENT_COST_1                            = UnitField.UNIT_END + 0x6FC, // Size: 4, Flags: PRIVATE
        PLAYER_FIELD_GLYPH_SLOTS_1                          = UnitField.UNIT_END + 0x700, // Size: 6, Flags: PRIVATE
        PLAYER_FIELD_GLYPHS_1                               = UnitField.UNIT_END + 0x706, // Size: 6, Flags: PRIVATE
        PLAYER_GLYPHS_ENABLED                               = UnitField.UNIT_END + 0x70C, // Size: 1, Flags: PRIVATE
        PLAYER_PET_SPELL_POWER                              = UnitField.UNIT_END + 0x70D, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_RESEARCHING_1                          = UnitField.UNIT_END + 0x70E, // Size: 8, Flags: PRIVATE
        PLAYER_PROFESSION_SKILL_LINE_1                      = UnitField.UNIT_END + 0x716, // Size: 2, Flags: PRIVATE
        PLAYER_FIELD_UI_HIT_MODIFIER                        = UnitField.UNIT_END + 0x718, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_UI_SPELL_HIT_MODIFIER                  = UnitField.UNIT_END + 0x719, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_HOME_REALM_TIME_OFFSET                 = UnitField.UNIT_END + 0x71A, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_MOD_PET_HASTE                          = UnitField.UNIT_END + 0x71B, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_SUMMONED_BATTLE_PET_ID                 = UnitField.UNIT_END + 0x71C, // Size: 2, Flags: PRIVATE
        PLAYER_FIELD_BYTES2                                 = UnitField.UNIT_END + 0x71E, // Size: 1, Flags: PRIVATE, OVERRIDE
        PLAYER_FIELD_LFG_BONUS_FACTION_ID                   = UnitField.UNIT_END + 0x71F, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_LOOT_SPEC_ID                           = UnitField.UNIT_END + 0x720, // Size: 1, Flags: PRIVATE
        PLAYER_FIELD_OVERRIDE_ZONE_PVP_TYPE                 = UnitField.UNIT_END + 0x721, // Size: 1, Flags: PRIVATE, OVERRIDE
        PLAYER_FIELD_ITEM_LEVEL_DELTA                       = UnitField.UNIT_END + 0x722, // Size: 1, Flags: PRIVATE
        PLAYER_END                                          = UnitField.UNIT_END + 0x723
    }

    public enum PlayerDynamicField
    {
        PLAYER_DYNAMIC_FIELD_RESERACH_SITE_1                = 0x000, //  Flags: PRIVATE
        PLAYER_DYNAMIC_FIELD_RESEARCH_SITE_PROGRESS_1       = 0x001, //  Flags: PRIVATE
        PLAYER_DYNAMIC_FIELD_DAILY_QUESTS_1                 = 0x002, //  Flags: PRIVATE
        PLAYER_DYNAMIC_END                                  = 0x003
    }

    public enum GameObjectField
    {
        GAMEOBJECT_FIELD_CREATED_BY                         = ObjectField.OBJECT_END + 0x000, // Size: 2, Flags: PUBLIC
        GAMEOBJECT_DISPLAYID                                = ObjectField.OBJECT_END + 0x002, // Size: 1, Flags: PUBLIC
        GAMEOBJECT_FLAGS                                    = ObjectField.OBJECT_END + 0x003, // Size: 1, Flags: PUBLIC, 0x100
        GAMEOBJECT_PARENTROTATION                           = ObjectField.OBJECT_END + 0x004, // Size: 4, Flags: PUBLIC
        GAMEOBJECT_FACTION                                  = ObjectField.OBJECT_END + 0x008, // Size: 1, Flags: PUBLIC
        GAMEOBJECT_LEVEL                                    = ObjectField.OBJECT_END + 0x009, // Size: 1, Flags: PUBLIC
        GAMEOBJECT_BYTES_1                                  = ObjectField.OBJECT_END + 0x00A, // Size: 1, Flags: PUBLIC, 0x100
        GAMEOBJECT_STATE_SPELL_VISUAL_ID                    = ObjectField.OBJECT_END + 0x00B, // Size: 1, Flags: PUBLIC, 0x100
        GAMEOBJECT_END                                      = ObjectField.OBJECT_END + 0x00C
    }

    public enum DynamicObjectField
    {
        DYNAMICOBJECT_CASTER                                = ObjectField.OBJECT_END + 0x000, // Size: 2, Flags: PUBLIC
        DYNAMICOBJECT_BYTES                                 = ObjectField.OBJECT_END + 0x002, // Size: 1, Flags: DYNAMIC
        DYNAMICOBJECT_SPELLID                               = ObjectField.OBJECT_END + 0x003, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_RADIUS                                = ObjectField.OBJECT_END + 0x004, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_CASTTIME                              = ObjectField.OBJECT_END + 0x005, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_END                                   = ObjectField.OBJECT_END + 0x006
    }

    public enum CorpseField
    {
        CORPSE_FIELD_OWNER                                  = ObjectField.OBJECT_END + 0x000, // Size: 2, Flags: PUBLIC
        CORPSE_FIELD_PARTY                                  = ObjectField.OBJECT_END + 0x002, // Size: 2, Flags: PUBLIC
        CORPSE_FIELD_DISPLAY_ID                             = ObjectField.OBJECT_END + 0x004, // Size: 1, Flags: PUBLIC
        CORPSE_FIELD_ITEM                                   = ObjectField.OBJECT_END + 0x005, // Size: 19, Flags: PUBLIC
        CORPSE_FIELD_BYTES_1                                = ObjectField.OBJECT_END + 0x018, // Size: 1, Flags: PUBLIC
        CORPSE_FIELD_BYTES_2                                = ObjectField.OBJECT_END + 0x019, // Size: 1, Flags: PUBLIC
        CORPSE_FIELD_FLAGS                                  = ObjectField.OBJECT_END + 0x01A, // Size: 1, Flags: PUBLIC
        CORPSE_FIELD_DYNAMIC_FLAGS                          = ObjectField.OBJECT_END + 0x01B, // Size: 1, Flags: DYNAMIC
        CORPSE_END                                          = ObjectField.OBJECT_END + 0x01C
    }

    public enum AreaTriggerField
    {
        AREATRIGGER_CASTER                                  = ObjectField.OBJECT_END + 0x000, // Size: 2, Flags: PUBLIC
        AREATRIGGER_DURATION                                = ObjectField.OBJECT_END + 0x002, // Size: 1, Flags: PUBLIC
        AREATRIGGER_SPELLID                                 = ObjectField.OBJECT_END + 0x003, // Size: 1, Flags: PUBLIC
        AREATRIGGER_SPELLVISUALID                           = ObjectField.OBJECT_END + 0x004, // Size: 1, Flags: DYNAMIC
        AREATRIGGER_EXPLICIT_SCALE                          = ObjectField.OBJECT_END + 0x005, // Size: 1, Flags: PUBLIC, 0x100
        AREATRIGGER_END                                     = ObjectField.OBJECT_END + 0x006
    }

    public enum SceneObjectField
    {
        SCENEOBJECT_FIELD_SCRIPT_PACKAGE_ID                 = ObjectField.OBJECT_END + 0x000, // Size: 1, Flags: PUBLIC
        SCENEOBJECT_FIELD_RND_SEED_VAL                      = ObjectField.OBJECT_END + 0x001, // Size: 1, Flags: PUBLIC
        SCENEOBJECT_FIELD_CREATEDBY                         = ObjectField.OBJECT_END + 0x002, // Size: 2, Flags: PUBLIC
        SCENEOBJECT_FIELD_SCENE_TYPE                        = ObjectField.OBJECT_END + 0x004, // Size: 1, Flags: PUBLIC
        SCENEOBJECT_END                                     = ObjectField.OBJECT_END + 0x005
    }

    // ReSharper restore InconsistentNaming
}
