namespace WowPacketParser.Enums.Version.V2_5_1_38835
{
    // ReSharper disable InconsistentNaming
    // 2.5.1.38835
    public enum ObjectField
    {
        OBJECT_FIELD_GUID                                           = 0x000, // Size: 4, Flags: PUBLIC
        OBJECT_FIELD_ENTRY                                          = 0x004, // Size: 1, Flags: DYNAMIC
        OBJECT_DYNAMIC_FLAGS                                        = 0x005, // Size: 1, Flags: DYNAMIC, URGENT
        OBJECT_FIELD_SCALE_X                                        = 0x006, // Size: 1, Flags: PUBLIC
        OBJECT_END                                                  = 0x007,
    }

    public enum ObjectDynamicField
    {
        OBJECT_DYNAMIC_END                                          = 0x000,
    }

    public enum ItemField
    {
        ITEM_FIELD_OWNER                                            = ObjectField.OBJECT_END + 0x000, // Size: 4, Flags: PUBLIC
        ITEM_FIELD_CONTAINED                                        = ObjectField.OBJECT_END + 0x004, // Size: 4, Flags: PUBLIC
        ITEM_FIELD_CREATOR                                          = ObjectField.OBJECT_END + 0x008, // Size: 4, Flags: PUBLIC
        ITEM_FIELD_GIFTCREATOR                                      = ObjectField.OBJECT_END + 0x00C, // Size: 4, Flags: PUBLIC
        ITEM_FIELD_STACK_COUNT                                      = ObjectField.OBJECT_END + 0x010, // Size: 1, Flags: OWNER
        ITEM_FIELD_DURATION                                         = ObjectField.OBJECT_END + 0x011, // Size: 1, Flags: OWNER
        ITEM_FIELD_SPELL_CHARGES                                    = ObjectField.OBJECT_END + 0x012, // Size: 5, Flags: OWNER
        ITEM_FIELD_FLAGS                                            = ObjectField.OBJECT_END + 0x017, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_ENCHANTMENT                                      = ObjectField.OBJECT_END + 0x018, // Size: 39, Flags: PUBLIC
        ITEM_FIELD_PROPERTY_SEED                                    = ObjectField.OBJECT_END + 0x03F, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_RANDOM_PROPERTIES_ID                             = ObjectField.OBJECT_END + 0x040, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_DURABILITY                                       = ObjectField.OBJECT_END + 0x041, // Size: 1, Flags: OWNER
        ITEM_FIELD_MAXDURABILITY                                    = ObjectField.OBJECT_END + 0x042, // Size: 1, Flags: OWNER
        ITEM_FIELD_CREATE_PLAYED_TIME                               = ObjectField.OBJECT_END + 0x043, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_MODIFIERS_MASK                                   = ObjectField.OBJECT_END + 0x044, // Size: 1, Flags: OWNER
        ITEM_FIELD_CONTEXT                                          = ObjectField.OBJECT_END + 0x045, // Size: 1, Flags: PUBLIC
        ITEM_FIELD_ARTIFACT_XP                                      = ObjectField.OBJECT_END + 0x046, // Size: 2, Flags: OWNER
        ITEM_FIELD_APPEARANCE_MOD_ID                                = ObjectField.OBJECT_END + 0x048, // Size: 1, Flags: OWNER
        ITEM_END                                                    = ObjectField.OBJECT_END + 0x049,
    }

    public enum ItemDynamicField
    {
        ITEM_DYNAMIC_FIELD_MODIFIERS                                = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000, // Flags: OWNER
        ITEM_DYNAMIC_FIELD_BONUSLIST_IDS                            = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x001, // Flags: OWNER, 0x100
        ITEM_DYNAMIC_FIELD_ARTIFACT_POWERS                          = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x002, // Flags: OWNER
        ITEM_DYNAMIC_FIELD_GEMS                                     = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x003, // Flags: OWNER
        ITEM_DYNAMIC_END                                            = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x004,
    }

    public enum ContainerField
    {
        CONTAINER_FIELD_SLOT_1                                      = ItemField.ITEM_END + 0x000, // Size: 144, Flags: PUBLIC
        CONTAINER_FIELD_NUM_SLOTS                                   = ItemField.ITEM_END + 0x090, // Size: 1, Flags: PUBLIC
        CONTAINER_END                                               = ItemField.ITEM_END + 0x091,
    }

    public enum ContainerDynamicField
    {
        CONTAINER_DYNAMIC_END                                       = ItemDynamicField.ITEM_DYNAMIC_END + 0x000,
    }

    public enum UnitField
    {
        UNIT_FIELD_CHARM                                                  = ObjectField.OBJECT_END + 0x0000,      // Size: 4, Flags: (PUBLIC)
        UNIT_FIELD_SUMMON                                                 = ObjectField.OBJECT_END + 0x0004,      // Size: 4, Flags: (PUBLIC)
        UNIT_FIELD_CRITTER                                                = ObjectField.OBJECT_END + 0x0008,      // Size: 4, Flags: (PRIVATE)
        UNIT_FIELD_CHARMEDBY                                              = ObjectField.OBJECT_END + 0x000C,      // Size: 4, Flags: (PUBLIC)
        UNIT_FIELD_SUMMONEDBY                                             = ObjectField.OBJECT_END + 0x0010,      // Size: 4, Flags: (PUBLIC)
        UNIT_FIELD_CREATEDBY                                              = ObjectField.OBJECT_END + 0x0014,      // Size: 4, Flags: (PUBLIC)
        UNIT_FIELD_DEMON_CREATOR                                          = ObjectField.OBJECT_END + 0x0018,      // Size: 4, Flags: (PUBLIC)
        UNIT_FIELD_LOOK_AT_CONTROLLER_TARGET                              = ObjectField.OBJECT_END + 0x001C,      // Size: 4, Flags: (PUBLIC)
        UNIT_FIELD_TARGET                                                 = ObjectField.OBJECT_END + 0x0020,      // Size: 4, Flags: (PUBLIC)
        UNIT_FIELD_BATTLE_PET_COMPANION_GUID                              = ObjectField.OBJECT_END + 0x0024,      // Size: 4, Flags: (PUBLIC)
        UNIT_FIELD_BATTLE_PET_DB_ID                                       = ObjectField.OBJECT_END + 0x0028,      // Size: 2, Flags: (PUBLIC)
        UNIT_FIELD_CHANNEL_DATA                                           = ObjectField.OBJECT_END + 0x002A,      // Size: 2, Flags: (PUBLIC, URGENT)
        UNIT_FIELD_SUMMONED_BY_HOME_REALM                                 = ObjectField.OBJECT_END + 0x002C,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_BYTES_0                                                = ObjectField.OBJECT_END + 0x002D,      // Size: 1, Flags: (PUBLIC) Nested: (Race, ClassId, PlayerClassId, Sex)
        UNIT_FIELD_DISPLAY_POWER                                          = ObjectField.OBJECT_END + 0x002E,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_OVERRIDE_DISPLAY_POWER_ID                              = ObjectField.OBJECT_END + 0x002F,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_HEALTH                                                 = ObjectField.OBJECT_END + 0x0030,      // Size: 2, Flags: (DYNAMIC)
        UNIT_FIELD_POWER                                                  = ObjectField.OBJECT_END + 0x0032,      // Size: 6, Flags: (PUBLIC, URGENT_SELF_ONLY)
        UNIT_FIELD_MAXHEALTH                                              = ObjectField.OBJECT_END + 0x0038,      // Size: 2, Flags: (DYNAMIC)
        UNIT_FIELD_MAXPOWER                                               = ObjectField.OBJECT_END + 0x003A,      // Size: 6, Flags: (PUBLIC)
        UNIT_FIELD_MOD_POWER_REGEN                                        = ObjectField.OBJECT_END + 0x0040,      // Size: 6, Flags: (PRIVATE, OWNER, UnitAll)
        UNIT_FIELD_LEVEL                                                  = ObjectField.OBJECT_END + 0x0046,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_EFFECTIVE_LEVEL                                        = ObjectField.OBJECT_END + 0x0047,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_CONTENT_TUNING_ID                                      = ObjectField.OBJECT_END + 0x0048,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_SCALING_LEVEL_MIN                                      = ObjectField.OBJECT_END + 0x0049,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_SCALING_LEVEL_MAX                                      = ObjectField.OBJECT_END + 0x004A,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_SCALING_LEVEL_DELTA                                    = ObjectField.OBJECT_END + 0x004B,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_SCALING_FACTION_GROUP                                  = ObjectField.OBJECT_END + 0x004C,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_SCALING_HEALTH_ITEM_LEVEL_CURVE_ID                     = ObjectField.OBJECT_END + 0x004D,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_SCALING_DAMAGE_ITEM_LEVEL_CURVE_ID                     = ObjectField.OBJECT_END + 0x004E,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_FACTIONTEMPLATE                                        = ObjectField.OBJECT_END + 0x004F,      // Size: 1, Flags: (PUBLIC)
        UNIT_VIRTUAL_ITEM_SLOT_ID                                         = ObjectField.OBJECT_END + 0x0050,      // Size: 6, Flags: (PUBLIC)
        UNIT_FIELD_FLAGS                                                  = ObjectField.OBJECT_END + 0x0056,      // Size: 1, Flags: (PUBLIC, URGENT)
        UNIT_FIELD_FLAGS_2                                                = ObjectField.OBJECT_END + 0x0057,      // Size: 1, Flags: (PUBLIC, URGENT)
        UNIT_FIELD_FLAGS_3                                                = ObjectField.OBJECT_END + 0x0058,      // Size: 1, Flags: (PUBLIC, URGENT)
        UNIT_FIELD_AURASTATE                                              = ObjectField.OBJECT_END + 0x0059,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_BASEATTACKTIME                                         = ObjectField.OBJECT_END + 0x005A,      // Size: 2, Flags: (PUBLIC)
        UNIT_FIELD_RANGEDATTACKTIME                                       = ObjectField.OBJECT_END + 0x005C,      // Size: 1, Flags: (PRIVATE)
        UNIT_FIELD_BOUNDINGRADIUS                                         = ObjectField.OBJECT_END + 0x005D,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_COMBATREACH                                            = ObjectField.OBJECT_END + 0x005E,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_DISPLAYID                                              = ObjectField.OBJECT_END + 0x005F,      // Size: 1, Flags: (DYNAMIC, URGENT)
        UNIT_FIELD_DISPLAYSCALE                                           = ObjectField.OBJECT_END + 0x0060,      // Size: 1, Flags: (DYNAMIC, URGENT)
        UNIT_FIELD_NATIVEDISPLAYID                                        = ObjectField.OBJECT_END + 0x0061,      // Size: 1, Flags: (PUBLIC, URGENT)
        UNIT_FIELD_NATIVEXDISPLAYSCALE                                    = ObjectField.OBJECT_END + 0x0062,      // Size: 1, Flags: (PUBLIC, URGENT)
        UNIT_FIELD_MOUNTDISPLAYID                                         = ObjectField.OBJECT_END + 0x0063,      // Size: 1, Flags: (PUBLIC, URGENT)
        UNIT_FIELD_MINDAMAGE                                              = ObjectField.OBJECT_END + 0x0064,      // Size: 1, Flags: (PRIVATE, OWNER, SPECIAL_INFO)
        UNIT_FIELD_MAXDAMAGE                                              = ObjectField.OBJECT_END + 0x0065,      // Size: 1, Flags: (PRIVATE, OWNER, SPECIAL_INFO)
        UNIT_FIELD_MINOFFHANDDAMAGE                                       = ObjectField.OBJECT_END + 0x0066,      // Size: 1, Flags: (PRIVATE, OWNER, SPECIAL_INFO)
        UNIT_FIELD_MAXOFFHANDDAMAGE                                       = ObjectField.OBJECT_END + 0x0067,      // Size: 1, Flags: (PRIVATE, OWNER, SPECIAL_INFO)
        UNIT_FIELD_BYTES_1                                                = ObjectField.OBJECT_END + 0x0068,      // Size: 1, Flags: (PUBLIC) Nested: (StandState, PetLoyaltyIndex, VisFlags, AnimTier)
        UNIT_FIELD_PETNUMBER                                              = ObjectField.OBJECT_END + 0x0069,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_PET_NAME_TIMESTAMP                                     = ObjectField.OBJECT_END + 0x006A,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_PETEXPERIENCE                                          = ObjectField.OBJECT_END + 0x006B,      // Size: 1, Flags: (OWNER)
        UNIT_FIELD_PETNEXTLEVELEXPERIENCE                                 = ObjectField.OBJECT_END + 0x006C,      // Size: 1, Flags: (OWNER)
        UNIT_MOD_CAST_SPEED                                               = ObjectField.OBJECT_END + 0x006D,      // Size: 1, Flags: (PUBLIC)
        UNIT_MOD_CAST_HASTE                                               = ObjectField.OBJECT_END + 0x006E,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_MOD_HASTE                                              = ObjectField.OBJECT_END + 0x006F,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_MOD_RANGED_HASTE                                       = ObjectField.OBJECT_END + 0x0070,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_MOD_HASTE_REGEN                                        = ObjectField.OBJECT_END + 0x0071,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_MOD_TIME_RATE                                          = ObjectField.OBJECT_END + 0x0072,      // Size: 1, Flags: (PUBLIC)
        UNIT_CREATED_BY_SPELL                                             = ObjectField.OBJECT_END + 0x0073,      // Size: 1, Flags: (PUBLIC)
        UNIT_NPC_FLAGS                                                    = ObjectField.OBJECT_END + 0x0074,      // Size: 2, Flags: (PUBLIC, DYNAMIC)
        UNIT_NPC_EMOTESTATE                                               = ObjectField.OBJECT_END + 0x0076,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_TRAINING_POINTS_TOTAL                                  = ObjectField.OBJECT_END + 0x0077,      // Size: 1, Flags: (OWNER) Nested: (TrainingPointsUsed, TrainingPointsTotal)
        UNIT_FIELD_STAT                                                   = ObjectField.OBJECT_END + 0x0078,      // Size: 5, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_POSSTAT                                                = ObjectField.OBJECT_END + 0x007D,      // Size: 5, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_NEGSTAT                                                = ObjectField.OBJECT_END + 0x0082,      // Size: 5, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_RESISTANCES                                            = ObjectField.OBJECT_END + 0x0087,      // Size: 7, Flags: (PRIVATE, OWNER, SPECIAL_INFO)
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE                             = ObjectField.OBJECT_END + 0x008E,      // Size: 7, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE                             = ObjectField.OBJECT_END + 0x0095,      // Size: 7, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_BASE_MANA                                              = ObjectField.OBJECT_END + 0x009C,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_BASE_HEALTH                                            = ObjectField.OBJECT_END + 0x009D,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_BYTES_2                                                = ObjectField.OBJECT_END + 0x009E,      // Size: 1, Flags: (PUBLIC) Nested: (SheatheState, PvpFlags, PetFlags, ShapeshiftForm)
        UNIT_FIELD_ATTACK_POWER                                           = ObjectField.OBJECT_END + 0x009F,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_ATTACK_POWER_MOD_POS                                   = ObjectField.OBJECT_END + 0x00A0,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_ATTACK_POWER_MOD_NEG                                   = ObjectField.OBJECT_END + 0x00A1,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER                                = ObjectField.OBJECT_END + 0x00A2,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_RANGED_ATTACK_POWER                                    = ObjectField.OBJECT_END + 0x00A3,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_POS                            = ObjectField.OBJECT_END + 0x00A4,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_NEG                            = ObjectField.OBJECT_END + 0x00A5,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER                         = ObjectField.OBJECT_END + 0x00A6,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_ATTACK_SPEED_AURA                                      = ObjectField.OBJECT_END + 0x00A7,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_LIFESTEAL                                              = ObjectField.OBJECT_END + 0x00A8,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_MINRANGEDDAMAGE                                        = ObjectField.OBJECT_END + 0x00A9,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_MAXRANGEDDAMAGE                                        = ObjectField.OBJECT_END + 0x00AA,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_POWER_COST_MODIFIER                                    = ObjectField.OBJECT_END + 0x00AB,      // Size: 7, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_POWER_COST_MULTIPLIER                                  = ObjectField.OBJECT_END + 0x00B2,      // Size: 7, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_MAXHEALTHMODIFIER                                      = ObjectField.OBJECT_END + 0x00B9,      // Size: 1, Flags: (PRIVATE, OWNER)
        UNIT_FIELD_HOVERHEIGHT                                            = ObjectField.OBJECT_END + 0x00BA,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_MIN_ITEM_LEVEL_CUTOFF                                  = ObjectField.OBJECT_END + 0x00BB,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_MIN_ITEM_LEVEL                                         = ObjectField.OBJECT_END + 0x00BC,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_MAXITEMLEVEL                                           = ObjectField.OBJECT_END + 0x00BD,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_WILD_BATTLE_PET_LEVEL                                  = ObjectField.OBJECT_END + 0x00BE,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_BATTLEPET_COMPANION_NAME_TIMESTAMP                     = ObjectField.OBJECT_END + 0x00BF,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_INTERACT_SPELL_ID                                      = ObjectField.OBJECT_END + 0x00C0,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_STATE_SPELL_VISUAL_ID                                  = ObjectField.OBJECT_END + 0x00C1,      // Size: 1, Flags: (DYNAMIC, URGENT)
        UNIT_FIELD_STATE_ANIM_ID                                          = ObjectField.OBJECT_END + 0x00C2,      // Size: 1, Flags: (DYNAMIC, URGENT)
        UNIT_FIELD_STATE_ANIM_KIT_ID                                      = ObjectField.OBJECT_END + 0x00C3,      // Size: 1, Flags: (DYNAMIC, URGENT)
        UNIT_FIELD_STATE_WORLD_EFFECT_ID                                  = ObjectField.OBJECT_END + 0x00C4,      // Size: 4, Flags: (DYNAMIC, URGENT)
        UNIT_FIELD_SCALE_DURATION                                         = ObjectField.OBJECT_END + 0x00C8,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_LOOKS_LIKE_MOUNT_ID                                    = ObjectField.OBJECT_END + 0x00C9,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_LOOKS_LIKE_CREATURE_ID                                 = ObjectField.OBJECT_END + 0x00CA,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_LOOK_AT_CONTROLLER_ID                                  = ObjectField.OBJECT_END + 0x00CB,      // Size: 1, Flags: (PUBLIC)
        UNIT_FIELD_GUILD_GUID                                             = ObjectField.OBJECT_END + 0x00CC,      // Size: 4, Flags: (PUBLIC)
        UNIT_END                                                          = ObjectField.OBJECT_END + 0x00D0,
    }

    public enum UnitDynamicField
    {
        UNIT_DYNAMIC_FIELD_PASSIVE_SPELLS                                 = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x0000,      // Flags: (PUBLIC, URGENT)
        UNIT_DYNAMIC_FIELD_WORLD_EFFECTS                                  = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x0001,      // Flags: (PUBLIC, URGENT)
        UNIT_DYNAMIC_FIELD_CHANNEL_OBJECTS                                = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x0002,      // Flags: (PUBLIC, URGENT)
        UNIT_DYNAMIC_END                                                  = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x0003,
    }

    public enum PlayerField
    {
        PLAYER_DUEL_ARBITER                                               = UnitField.UNIT_END + 0x0000,      // Size: 4, Flags: (PUBLIC)
        PLAYER_WOW_ACCOUNT                                                = UnitField.UNIT_END + 0x0004,      // Size: 4, Flags: (PUBLIC)
        PLAYER_LOOT_TARGET_GUID                                           = UnitField.UNIT_END + 0x0008,      // Size: 4, Flags: (PUBLIC)
        PLAYER_FLAGS                                                      = UnitField.UNIT_END + 0x000C,      // Size: 1, Flags: (PUBLIC)
        PLAYER_FLAGS_EX                                                   = UnitField.UNIT_END + 0x000D,      // Size: 1, Flags: (PUBLIC)
        PLAYER_GUILDRANK                                                  = UnitField.UNIT_END + 0x000E,      // Size: 1, Flags: (PUBLIC)
        PLAYER_GUILDDELETE_DATE                                           = UnitField.UNIT_END + 0x000F,      // Size: 1, Flags: (PUBLIC)
        PLAYER_GUILDLEVEL                                                 = UnitField.UNIT_END + 0x0010,      // Size: 1, Flags: (PUBLIC)
        PLAYER_BYTES                                                      = UnitField.UNIT_END + 0x0011,      // Size: 1, Flags: (PUBLIC) Nested: (PartyType, NumBankSlots, NativeSex, Inebriation)
        PLAYER_BYTES_2                                                    = UnitField.UNIT_END + 0x0012,      // Size: 1, Flags: (PUBLIC) Nested: (PvpTitle, ArenaFaction, PvpRank)
        PLAYER_DUEL_TEAM                                                  = UnitField.UNIT_END + 0x0013,      // Size: 1, Flags: (PUBLIC)
        PLAYER_GUILD_TIMESTAMP                                            = UnitField.UNIT_END + 0x0014,      // Size: 1, Flags: (PUBLIC)
        PLAYER_QUEST_LOG                                                  = UnitField.UNIT_END + 0x0015,      // Size: 320, Flags: (GROUP_ONLY)
        PLAYER_VISIBLE_ITEM                                               = UnitField.UNIT_END + 0x01A5,      // Size: 38, Flags: (PUBLIC)
        PLAYER_CHOSEN_TITLE                                               = UnitField.UNIT_END + 0x01CB,      // Size: 1, Flags: (PUBLIC)
        PLAYER_FAKE_INEBRIATION                                           = UnitField.UNIT_END + 0x01CC,      // Size: 1, Flags: (PUBLIC)
        PLAYER_FIELD_VIRTUAL_PLAYER_REALM                                 = UnitField.UNIT_END + 0x01CD,      // Size: 1, Flags: (PUBLIC)
        PLAYER_FIELD_CURRENT_SPEC_ID                                      = UnitField.UNIT_END + 0x01CE,      // Size: 1, Flags: (PUBLIC)
        PLAYER_FIELD_TAXI_MOUNT_ANIM_KIT_ID                               = UnitField.UNIT_END + 0x01CF,      // Size: 1, Flags: (PUBLIC)
        PLAYER_FIELD_AVG_ITEM_LEVEL                                       = UnitField.UNIT_END + 0x01D0,      // Size: 4, Flags: (PUBLIC)
        PLAYER_FIELD_CURRENT_BATTLE_PET_BREED_QUALITY                     = UnitField.UNIT_END + 0x01D4,      // Size: 1, Flags: (PUBLIC)
        PLAYER_FIELD_HONOR_LEVEL                                          = UnitField.UNIT_END + 0x01D5,      // Size: 1, Flags: (PUBLIC)
        PLAYER_FIELD_CUSTOMIZATION_CHOICES                                = UnitField.UNIT_END + 0x01D6,      // Size: 72, Flags: (PUBLIC)
        PLAYER_END                                                        = UnitField.UNIT_END + 0x021E,
    }

    public enum PlayerDynamicField
    {
        PLAYER_DYNAMIC_FIELD_ARENA_COOLDOWNS                              = UnitDynamicField.UNIT_DYNAMIC_END + 0x0000,      // Flags: (PUBLIC)
        PLAYER_DYNAMIC_END                                                = UnitDynamicField.UNIT_DYNAMIC_END + 0x0001,
    }

    public enum ActivePlayerField
    {
        ACTIVE_PLAYER_FIELD_INV_SLOT_HEAD                                 = PlayerField.PLAYER_END + 0x0000,      // Size: 516, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_FARSIGHT                                      = PlayerField.PLAYER_END + 0x0204,      // Size: 4, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_COMBO_TARGET                                  = PlayerField.PLAYER_END + 0x0208,      // Size: 4, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_SUMMONED_BATTLE_PET_ID                        = PlayerField.PLAYER_END + 0x020C,      // Size: 4, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_KNOWN_TITLES                                  = PlayerField.PLAYER_END + 0x0210,      // Size: 12, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_COINAGE                                       = PlayerField.PLAYER_END + 0x021C,      // Size: 2, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_XP                                            = PlayerField.PLAYER_END + 0x021E,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_NEXT_LEVEL_XP                                 = PlayerField.PLAYER_END + 0x021F,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_TRIAL_XP                                      = PlayerField.PLAYER_END + 0x0220,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_SKILL_LINEID                                  = PlayerField.PLAYER_END + 0x0221,      // Size: 896, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_CHARACTER_POINTS                              = PlayerField.PLAYER_END + 0x05A1,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MAX_TALENT_TIERS                              = PlayerField.PLAYER_END + 0x05A2,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_TRACK_CREATURES                               = PlayerField.PLAYER_END + 0x05A3,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_TRACK_RESOURCES                               = PlayerField.PLAYER_END + 0x05A4,      // Size: 2, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_EXPERTISE                                     = PlayerField.PLAYER_END + 0x05A6,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_OFFHAND_EXPERTISE                             = PlayerField.PLAYER_END + 0x05A7,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_RANGED_EXPERTISE                              = PlayerField.PLAYER_END + 0x05A8,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_COMBAT_RATING_EXPERTISE                       = PlayerField.PLAYER_END + 0x05A9,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_BLOCK_PERCENTAGE                              = PlayerField.PLAYER_END + 0x05AA,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_DODGE_PERCENTAGE                              = PlayerField.PLAYER_END + 0x05AB,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_DODGE_PERCENTAGE_FROM_ATTRIBUTE               = PlayerField.PLAYER_END + 0x05AC,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PARRY_PERCENTAGE                              = PlayerField.PLAYER_END + 0x05AD,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PARRY_PERCENTAGE_FROM_ATTRIBUTE               = PlayerField.PLAYER_END + 0x05AE,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_CRIT_PERCENTAGE                               = PlayerField.PLAYER_END + 0x05AF,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_RANGED_CRIT_PERCENTAGE                        = PlayerField.PLAYER_END + 0x05B0,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_OFFHAND_CRIT_PERCENTAGE                       = PlayerField.PLAYER_END + 0x05B1,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_SPELL_CRIT_PERCENTAGE                         = PlayerField.PLAYER_END + 0x05B2,      // Size: 7, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_SHIELD_BLOCK                                  = PlayerField.PLAYER_END + 0x05B9,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MASTERY                                       = PlayerField.PLAYER_END + 0x05BA,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_SPEED                                         = PlayerField.PLAYER_END + 0x05BB,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_AVOIDANCE                                     = PlayerField.PLAYER_END + 0x05BC,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_STURDINESS                                    = PlayerField.PLAYER_END + 0x05BD,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_VERSATILITY                                   = PlayerField.PLAYER_END + 0x05BE,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_VERSATILITY_BONUS                             = PlayerField.PLAYER_END + 0x05BF,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PVP_POWER_DAMAGE                              = PlayerField.PLAYER_END + 0x05C0,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PVP_POWER_HEALING                             = PlayerField.PLAYER_END + 0x05C1,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_EXPLORED_ZONES                                = PlayerField.PLAYER_END + 0x05C2,      // Size: 384, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_REST_INFO                                     = PlayerField.PLAYER_END + 0x0742,      // Size: 4, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_DAMAGE_DONE_POS                           = PlayerField.PLAYER_END + 0x0746,      // Size: 7, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_DAMAGE_DONE_NEG                           = PlayerField.PLAYER_END + 0x074D,      // Size: 7, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_DAMAGE_DONE_PCT                           = PlayerField.PLAYER_END + 0x0754,      // Size: 7, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_HEALING_DONE_POS                          = PlayerField.PLAYER_END + 0x075B,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_HEALING_PCT                               = PlayerField.PLAYER_END + 0x075C,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_HEALING_DONE_PCT                          = PlayerField.PLAYER_END + 0x075D,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_PERIODIC_HEALING_DONE_PERCENT             = PlayerField.PLAYER_END + 0x075E,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_WEAPON_DMG_MULTIPLIERS                        = PlayerField.PLAYER_END + 0x075F,      // Size: 3, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_WEAPON_ATK_SPEED_MULTIPLIERS                  = PlayerField.PLAYER_END + 0x0762,      // Size: 3, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_SPELL_POWER_PCT                           = PlayerField.PLAYER_END + 0x0765,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_RESILIENCE_PERCENT                        = PlayerField.PLAYER_END + 0x0766,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_OVERRIDE_SPELL_POWER_BY_AP_PCT                = PlayerField.PLAYER_END + 0x0767,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_OVERRIDE_AP_BY_SPELL_POWER_PERCENT            = PlayerField.PLAYER_END + 0x0768,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_TARGET_RESISTANCE                         = PlayerField.PLAYER_END + 0x0769,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE                = PlayerField.PLAYER_END + 0x076A,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_LOCAL_FLAGS                                   = PlayerField.PLAYER_END + 0x076B,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_BYTES                                         = PlayerField.PLAYER_END + 0x076C,      // Size: 1, Flags: (PUBLIC) Nested: (GrantableLevels, MultiActionBars, LifetimeMaxRank, NumRespecs)
        ACTIVE_PLAYER_FIELD_AMMO_ID                                       = PlayerField.PLAYER_END + 0x076D,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PVP_MEDALS                                    = PlayerField.PLAYER_END + 0x076E,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_BUYBACK_PRICE                                 = PlayerField.PLAYER_END + 0x076F,      // Size: 12, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_BUYBACK_TIMESTAMP                             = PlayerField.PLAYER_END + 0x077B,      // Size: 12, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_BYTES_2                                       = PlayerField.PLAYER_END + 0x0787,      // Size: 1, Flags: (PUBLIC) Nested: (TodayHonorableKills, YesterdayHonorableKills)
        ACTIVE_PLAYER_FIELD_BYTES_3                                       = PlayerField.PLAYER_END + 0x0788,      // Size: 1, Flags: (PUBLIC) Nested: (LastWeekHonorableKills, ThisWeekHonorableKills)
        ACTIVE_PLAYER_FIELD_THIS_WEEK_CONTRIBUTION                        = PlayerField.PLAYER_END + 0x0789,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_LIFETIME_HONORABLE_KILLS                      = PlayerField.PLAYER_END + 0x078A,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_YESTERDAY_CONTRIBUTION                        = PlayerField.PLAYER_END + 0x078B,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_LAST_WEEK_CONTRIBUTION                        = PlayerField.PLAYER_END + 0x078C,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_LAST_WEEK_RANK                                = PlayerField.PLAYER_END + 0x078D,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_WATCHED_FACTION_INDEX                         = PlayerField.PLAYER_END + 0x078E,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_COMBAT_RATINGS                                = PlayerField.PLAYER_END + 0x078F,      // Size: 32, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PVP_INFO                                      = PlayerField.PLAYER_END + 0x07AF,      // Size: 72, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MAX_LEVEL                                     = PlayerField.PLAYER_END + 0x07F7,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_SCALING_PLAYER_LEVEL_DELTA                    = PlayerField.PLAYER_END + 0x07F8,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MAX_CREATURE_SCALING_LEVEL                    = PlayerField.PLAYER_END + 0x07F9,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_NO_REAGENT_COST_MASK                          = PlayerField.PLAYER_END + 0x07FA,      // Size: 4, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PET_SPELL_POWER                               = PlayerField.PLAYER_END + 0x07FE,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PROFESSION_SKILL_LINE                         = PlayerField.PLAYER_END + 0x07FF,      // Size: 2, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_UI_HIT_MODIFIER                               = PlayerField.PLAYER_END + 0x0801,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_UI_SPELL_HIT_MODIFIER                         = PlayerField.PLAYER_END + 0x0802,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_HOME_REALM_TIME_OFFSET                        = PlayerField.PLAYER_END + 0x0803,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_MOD_PET_HASTE                                 = PlayerField.PLAYER_END + 0x0804,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_BYTES_4                                       = PlayerField.PLAYER_END + 0x0805,      // Size: 1, Flags: (PUBLIC) Nested: (LocalRegenFlags, AuraVision, NumBackpackSlots)
        ACTIVE_PLAYER_FIELD_OVERRIDE_SPELLS_ID                            = PlayerField.PLAYER_END + 0x0806,      // Size: 1, Flags: (PUBLIC, URGENT_SELF_ONLY)
        ACTIVE_PLAYER_FIELD_LFG_BONUS_FACTION_ID                          = PlayerField.PLAYER_END + 0x0807,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_LOOT_SPEC_ID                                  = PlayerField.PLAYER_END + 0x0808,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_OVERRIDE_ZONE_PVP_TYPE                        = PlayerField.PLAYER_END + 0x0809,      // Size: 1, Flags: (PUBLIC, URGENT_SELF_ONLY)
        ACTIVE_PLAYER_FIELD_BAG_SLOT_FLAGS                                = PlayerField.PLAYER_END + 0x080A,      // Size: 4, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_BANK_BAG_SLOT_FLAGS                           = PlayerField.PLAYER_END + 0x080E,      // Size: 7, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_QUEST_COMPLETED                               = PlayerField.PLAYER_END + 0x0815,      // Size: 1750, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_HONOR                                         = PlayerField.PLAYER_END + 0x0EEB,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_HONOR_NEXT_LEVEL                              = PlayerField.PLAYER_END + 0x0EEC,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PVP_TIER_MAX_FROM_WINS                        = PlayerField.PLAYER_END + 0x0EED,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_PVP_LAST_WEEKS_TIER_MAX_FROM_WINS             = PlayerField.PLAYER_END + 0x0EEE,      // Size: 1, Flags: (PUBLIC)
        ACTIVE_PLAYER_FIELD_BYTES_5                                       = PlayerField.PLAYER_END + 0x0EEF,      // Size: 1, Flags: (PUBLIC) Nested: (InsertItemsLeftToRight, PvpRankProgress)
        ACTIVE_PLAYER_END                                                 = PlayerField.PLAYER_END + 0x0EF0,
    }

    public enum ActivePlayerDynamicField
    {
        ACTIVE_PLAYER_DYNAMIC_FIELD_RESEARCH                              = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0000,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_RESEARCH_SITES                        = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0001,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_RESEARCH_SITE_PROGRESS                = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0002,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_DAILY_QUESTS_COMPLETED                = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0003,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_AVAILABLE_QUEST_LINE_X_QUEST_IDS      = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0004,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_HEIRLOOMS                             = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0005,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_HEIRLOOM_FLAGS                        = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0006,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_TOYS                                  = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0007,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_TRANSMOG                              = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0008,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_CONDITIONAL_TRANSMOG                  = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x0009,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_SELF_RES_SPELLS                       = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x000A,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_CHARACTER_RESTRICTIONS                = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x000B,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_SPELL_FLAT_MOD_BY_LABEL               = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x000C,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_FIELD_SPELL_PCT_MOD_BY_LABEL                = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x000D,      // Flags: (PUBLIC)
        ACTIVE_PLAYER_DYNAMIC_END                                         = PlayerDynamicField.PLAYER_DYNAMIC_END + 0x000E,
    }

    public enum GameObjectField
    {
        GAMEOBJECT_FIELD_CREATED_BY                                 = ObjectField.OBJECT_END + 0x000, // Size: 4, Flags: PUBLIC
        GAMEOBJECT_GUILD                                            = ObjectField.OBJECT_END + 0x004, // Size: 4, Flags: PUBLIC
        GAMEOBJECT_DISPLAYID                                        = ObjectField.OBJECT_END + 0x008, // Size: 1, Flags: DYNAMIC, URGENT
        GAMEOBJECT_FLAGS                                            = ObjectField.OBJECT_END + 0x009, // Size: 1, Flags: PUBLIC, URGENT
        GAMEOBJECT_PARENTROTATION                                   = ObjectField.OBJECT_END + 0x00A, // Size: 4, Flags: PUBLIC
        GAMEOBJECT_FACTION                                          = ObjectField.OBJECT_END + 0x00E, // Size: 1, Flags: PUBLIC
        GAMEOBJECT_LEVEL                                            = ObjectField.OBJECT_END + 0x00F, // Size: 1, Flags: PUBLIC
        GAMEOBJECT_BYTES_1                                          = ObjectField.OBJECT_END + 0x010, // Size: 1, Flags: PUBLIC, URGENT
        GAMEOBJECT_SPELL_VISUAL_ID                                  = ObjectField.OBJECT_END + 0x011, // Size: 1, Flags: PUBLIC, DYNAMIC, URGENT
        GAMEOBJECT_STATE_SPELL_VISUAL_ID                            = ObjectField.OBJECT_END + 0x012, // Size: 1, Flags: DYNAMIC, URGENT
        GAMEOBJECT_STATE_ANIM_ID                                    = ObjectField.OBJECT_END + 0x013, // Size: 1, Flags: DYNAMIC, URGENT
        GAMEOBJECT_STATE_ANIM_KIT_ID                                = ObjectField.OBJECT_END + 0x014, // Size: 1, Flags: DYNAMIC, URGENT
        GAMEOBJECT_STATE_WORLD_EFFECT_ID                            = ObjectField.OBJECT_END + 0x015, // Size: 4, Flags: DYNAMIC, URGENT
        GAMEOBJECT_CUSTOM_PARAM                                     = ObjectField.OBJECT_END + 0x019, // Size: 1, Flags: PUBLIC, URGENT
        GAMEOBJECT_END                                              = ObjectField.OBJECT_END + 0x01A,
    }

    public enum GameObjectDynamicField
    {
        GAMEOBJECT_DYNAMIC_ENABLE_DOODAD_SETS                       = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000, // Flags: PUBLIC
        GAMEOBJECT_DYNAMIC_END                                      = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x001,
    }

    public enum DynamicObjectField
    {
        DYNAMICOBJECT_CASTER                                        = ObjectField.OBJECT_END + 0x000, // Size: 4, Flags: PUBLIC
        DYNAMICOBJECT_TYPE                                          = ObjectField.OBJECT_END + 0x004, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_SPELL_X_SPELL_VISUAL_ID                       = ObjectField.OBJECT_END + 0x005, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_SPELLID                                       = ObjectField.OBJECT_END + 0x006, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_RADIUS                                        = ObjectField.OBJECT_END + 0x007, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_CASTTIME                                      = ObjectField.OBJECT_END + 0x008, // Size: 1, Flags: PUBLIC
        DYNAMICOBJECT_END                                           = ObjectField.OBJECT_END + 0x009,
    }

    public enum DynamicObjectDynamicField
    {
        DYNAMICOBJECT_DYNAMIC_END                                   = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000,
    }

    public enum CorpseField
    {
        CORPSE_FIELD_OWNER                                          = ObjectField.OBJECT_END + 0x0000,      // Size: 4, Flags: (PUBLIC)
        CORPSE_FIELD_PARTY_GUID                                     = ObjectField.OBJECT_END + 0x0004,      // Size: 4, Flags: (PUBLIC)
        CORPSE_FIELD_GUILD_GUID                                     = ObjectField.OBJECT_END + 0x0008,      // Size: 4, Flags: (PUBLIC)
        CORPSE_FIELD_DISPLAY_ID                                     = ObjectField.OBJECT_END + 0x000C,      // Size: 1, Flags: (PUBLIC)
        CORPSE_FIELD_ITEMS                                          = ObjectField.OBJECT_END + 0x000D,      // Size: 19, Flags: (PUBLIC)
        CORPSE_FIELD_BYTES_1                                        = ObjectField.OBJECT_END + 0x0020,      // Size: 1, Flags: (PUBLIC) Nested: (RaceID, Sex, ClassID, Padding)
        CORPSE_FIELD_FLAGS                                          = ObjectField.OBJECT_END + 0x0021,      // Size: 1, Flags: (PUBLIC)
        CORPSE_FIELD_DYNAMIC_FLAGS                                  = ObjectField.OBJECT_END + 0x0022,      // Size: 1, Flags: (DYNAMIC)
        CORPSE_FIELD_FACTION_TEMPLATE                               = ObjectField.OBJECT_END + 0x0023,      // Size: 1, Flags: (PUBLIC)
        CORPSE_FIELD_CUSTOMIZATION_CHOICES                          = ObjectField.OBJECT_END + 0x0024,      // Size: 72, Flags: (PUBLIC)
        CORPSE_END                                                  = ObjectField.OBJECT_END + 0x006C,
    }

    public enum CorpseDynamicField
    {
        CORPSE_DYNAMIC_END                                          = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000,
    }

    public enum AreaTriggerField
    {
        AREATRIGGER_OVERRIDE_SCALE_CURVE                            = ObjectField.OBJECT_END + 0x000, // Size: 7, Flags: PUBLIC, URGENT
        AREATRIGGER_EXTRA_SCALE_CURVE                               = ObjectField.OBJECT_END + 0x007, // Size: 7, Flags: PUBLIC, URGENT
        AREATRIGGER_CASTER                                          = ObjectField.OBJECT_END + 0x00E, // Size: 4, Flags: PUBLIC
        AREATRIGGER_DURATION                                        = ObjectField.OBJECT_END + 0x012, // Size: 1, Flags: PUBLIC
        AREATRIGGER_TIME_TO_TARGET                                  = ObjectField.OBJECT_END + 0x013, // Size: 1, Flags: PUBLIC, URGENT
        AREATRIGGER_TIME_TO_TARGET_SCALE                            = ObjectField.OBJECT_END + 0x014, // Size: 1, Flags: PUBLIC, URGENT
        AREATRIGGER_TIME_TO_TARGET_EXTRA_SCALE                      = ObjectField.OBJECT_END + 0x015, // Size: 1, Flags: PUBLIC, URGENT
        AREATRIGGER_SPELLID                                         = ObjectField.OBJECT_END + 0x016, // Size: 1, Flags: PUBLIC
        AREATRIGGER_SPELL_FOR_VISUALS                               = ObjectField.OBJECT_END + 0x017, // Size: 1, Flags: PUBLIC
        AREATRIGGER_SPELL_X_SPELL_VISUAL_ID                         = ObjectField.OBJECT_END + 0x018, // Size: 1, Flags: PUBLIC
        AREATRIGGER_BOUNDS_RADIUS_2D                                = ObjectField.OBJECT_END + 0x019, // Size: 1, Flags: DYNAMIC, URGENT
        AREATRIGGER_DECAL_PROPERTIES_ID                             = ObjectField.OBJECT_END + 0x01A, // Size: 1, Flags: PUBLIC
        AREATRIGGER_CREATING_EFFECT_GUID                            = ObjectField.OBJECT_END + 0x01B, // Size: 4, Flags: PUBLIC
        AREATRIGGER_END                                             = ObjectField.OBJECT_END + 0x01F,
    }

    public enum AreaTriggerDynamicField
    {
        AREATRIGGER_DYNAMIC_END                                     = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000,
    }

    public enum SceneObjectField
    {
        SCENEOBJECT_FIELD_SCRIPT_PACKAGE_ID                         = ObjectField.OBJECT_END + 0x000, // Size: 1, Flags: PUBLIC
        SCENEOBJECT_FIELD_RND_SEED_VAL                              = ObjectField.OBJECT_END + 0x001, // Size: 1, Flags: PUBLIC
        SCENEOBJECT_FIELD_CREATEDBY                                 = ObjectField.OBJECT_END + 0x002, // Size: 4, Flags: PUBLIC
        SCENEOBJECT_FIELD_SCENE_TYPE                                = ObjectField.OBJECT_END + 0x006, // Size: 1, Flags: PUBLIC
        SCENEOBJECT_END                                             = ObjectField.OBJECT_END + 0x007,
    }

    public enum SceneObjectDynamicField
    {
        SCENEOBJECT_DYNAMIC_END                                     = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000,
    }

    public enum ConversationField
    {
        CONVERSATION_LAST_LINE_END_TIME                             = ObjectField.OBJECT_END + 0x000, // Size: 1, Flags: DYNAMIC
        CONVERSATION_END                                            = ObjectField.OBJECT_END + 0x001,
    }

    public enum ConversationDynamicField
    {
        CONVERSATION_DYNAMIC_FIELD_ACTORS                           = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x000, // Flags: PUBLIC
        CONVERSATION_DYNAMIC_FIELD_LINES                            = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x001, // Flags: 0x100
        CONVERSATION_DYNAMIC_END                                    = ObjectDynamicField.OBJECT_DYNAMIC_END + 0x002,
    }

    // ReSharper restore InconsistentNaming
}
