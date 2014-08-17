namespace WowPacketParserModule.V5_4_8_18414.Enums
{
    public enum ObjectField
    {
        OBJECT_FIELD_GUID                                        = 0x0, // Size: 2, Flags: UF_FLAG_PUBLIC
        OBJECT_FIELD_GUID2,
        OBJECT_FIELD_DATA                                        = 0x2, // Size: 2, Flags: UF_FLAG_PUBLIC
        OBJECT_FIELD_DATA2,
        OBJECT_FIELD_TYPE                                        = 0x4, // Size: 1, Flags: UF_FLAG_PUBLIC
        OBJECT_FIELD_ENTRY_ID                                    = 0x5, // Size: 1, Flags: UF_FLAG_VIEWER_DEPENDENT
        OBJECT_FIELD_DYNAMIC_FLAGS                               = 0x6, // Size: 1, Flags: UF_FLAG_VIEWER_DEPENDENT, UF_FLAG_URGENT
        OBJECT_FIELD_SCALE                                       = 0x7, // Size: 1, Flags: UF_FLAG_PUBLIC
        OBJECT_END                                               = 0x8,
    }

    public enum ItemField
    {
        ITEM_FIELD_OWNER                                         = ObjectField.OBJECT_END + 0x00, // Size: 2, Flags: UF_FLAG_PUBLIC
        ITEM_FIELD_OWNER2,
        ITEM_FIELD_CONTAINED_IN                                  = ObjectField.OBJECT_END + 0x02, // Size: 2, Flags: UF_FLAG_PUBLIC
        ITEM_FIELD_CONTAINED_IN2,
        ITEM_FIELD_CREATOR                                       = ObjectField.OBJECT_END + 0x04, // Size: 2, Flags: UF_FLAG_PUBLIC
        ITEM_FIELD_CREATOR2,
        ITEM_FIELD_GIFT_CREATOR                                  = ObjectField.OBJECT_END + 0x06, // Size: 2, Flags: UF_FLAG_PUBLIC
        ITEM_FIELD_GIFT_CREATOR2,
        ITEM_FIELD_STACK_COUNT                                   = ObjectField.OBJECT_END + 0x08, // Size: 1, Flags: UF_FLAG_OWNER
        ITEM_FIELD_EXPIRATION                                    = ObjectField.OBJECT_END + 0x09, // Size: 1, Flags: UF_FLAG_OWNER
        ITEM_FIELD_SPELL_CHARGES                                 = ObjectField.OBJECT_END + 0x0A, // Size: 5, Flags: UF_FLAG_OWNER
        ITEM_FIELD_SPELL_CHARGES2,
        ITEM_FIELD_SPELL_CHARGES3,
        ITEM_FIELD_SPELL_CHARGES4,
        ITEM_FIELD_SPELL_CHARGES5,
        ITEM_FIELD_DYNAMIC_FLAGS                                 = ObjectField.OBJECT_END + 0x0F, // Size: 1, Flags: UF_FLAG_PUBLIC
        ITEM_FIELD_ENCHANTMENT                                   = ObjectField.OBJECT_END + 0x10, // Size: 39, Flags: UF_FLAG_PUBLIC
        ITEM_FIELD_ENCHANTMENT1_2,
        ITEM_FIELD_ENCHANTMENT1_3,
        ITEM_FIELD_ENCHANTMENT2_1,
        ITEM_FIELD_ENCHANTMENT2_2,
        ITEM_FIELD_ENCHANTMENT2_3,
        ITEM_FIELD_ENCHANTMENT3_1,
        ITEM_FIELD_ENCHANTMENT3_2,
        ITEM_FIELD_ENCHANTMENT3_3,
        ITEM_FIELD_ENCHANTMENT4_1,
        ITEM_FIELD_ENCHANTMENT4_2,
        ITEM_FIELD_ENCHANTMENT4_3,
        ITEM_FIELD_ENCHANTMENT5_1,
        ITEM_FIELD_ENCHANTMENT5_2,
        ITEM_FIELD_ENCHANTMENT5_3,
        ITEM_FIELD_ENCHANTMENT6_1,
        ITEM_FIELD_ENCHANTMENT6_2,
        ITEM_FIELD_ENCHANTMENT6_3,
        ITEM_FIELD_ENCHANTMENT7_1,
        ITEM_FIELD_ENCHANTMENT7_2,
        ITEM_FIELD_ENCHANTMENT7_3,
        ITEM_FIELD_ENCHANTMENT8_1,
        ITEM_FIELD_ENCHANTMENT8_2,
        ITEM_FIELD_ENCHANTMENT8_3,
        ITEM_FIELD_ENCHANTMENT9_1,
        ITEM_FIELD_ENCHANTMENT9_2,
        ITEM_FIELD_ENCHANTMENT9_3,
        ITEM_FIELD_ENCHANTMENT10_1,
        ITEM_FIELD_ENCHANTMENT10_2,
        ITEM_FIELD_ENCHANTMENT10_3,
        ITEM_FIELD_ENCHANTMENT11_1,
        ITEM_FIELD_ENCHANTMENT11_2,
        ITEM_FIELD_ENCHANTMENT11_3,
        ITEM_FIELD_ENCHANTMENT12_1,
        ITEM_FIELD_ENCHANTMENT12_2,
        ITEM_FIELD_ENCHANTMENT12_3,
        ITEM_FIELD_ENCHANTMENT13_1,
        ITEM_FIELD_ENCHANTMENT13_2,
        ITEM_FIELD_ENCHANTMENT13_3,
        ITEM_FIELD_PROPERTY_SEED                                 = ObjectField.OBJECT_END + 0x37, // Size: 1, Flags: UF_FLAG_PUBLIC
        ITEM_FIELD_RANDOM_PROPERTIES_ID                          = ObjectField.OBJECT_END + 0x38, // Size: 1, Flags: UF_FLAG_PUBLIC
        ITEM_FIELD_DURABILITY                                    = ObjectField.OBJECT_END + 0x39, // Size: 1, Flags: UF_FLAG_OWNER
        ITEM_FIELD_MAX_DURABILITY                                = ObjectField.OBJECT_END + 0x3A, // Size: 1, Flags: UF_FLAG_OWNER
        ITEM_FIELD_CREATE_PLAYED_TIME                            = ObjectField.OBJECT_END + 0x3B, // Size: 1, Flags: UF_FLAG_PUBLIC
        ITEM_FIELD_MODIFIERS_MASK                                = ObjectField.OBJECT_END + 0x3C, // Size: 1, Flags: UF_FLAG_OWNER
        ITEM_END                                                 = ObjectField.OBJECT_END + 0x3D,
    }

    public enum ContainerField
    {
        CONTAINER_FIELD_SLOT                                    = ItemField.ITEM_END + 0x00, // Size: 72, Flags: UF_FLAG_PUBLIC
        CONTAINER_FIELD_SLOTh,
        CONTAINER_FIELD_SLOT2,
        CONTAINER_FIELD_SLOT2h,
        CONTAINER_FIELD_SLOT3,
        CONTAINER_FIELD_SLOT3h,
        CONTAINER_FIELD_SLOT4,
        CONTAINER_FIELD_SLOT4h,
        CONTAINER_FIELD_SLOT5,
        CONTAINER_FIELD_SLOT5h,
        CONTAINER_FIELD_SLOT6,
        CONTAINER_FIELD_SLOT6h,
        CONTAINER_FIELD_SLOT7,
        CONTAINER_FIELD_SLOT7h,
        CONTAINER_FIELD_SLOT8,
        CONTAINER_FIELD_SLOT8h,
        CONTAINER_FIELD_SLOT9,
        CONTAINER_FIELD_SLOT9h,
        CONTAINER_FIELD_SLOT10,
        CONTAINER_FIELD_SLOT10h,
        CONTAINER_FIELD_SLOT11,
        CONTAINER_FIELD_SLOT11h,
        CONTAINER_FIELD_SLOT12,
        CONTAINER_FIELD_SLOT12h,
        CONTAINER_FIELD_SLOT13,
        CONTAINER_FIELD_SLOT13h,
        CONTAINER_FIELD_SLOT14,
        CONTAINER_FIELD_SLOT14h,
        CONTAINER_FIELD_SLOT15,
        CONTAINER_FIELD_SLOT15h,
        CONTAINER_FIELD_SLOT16,
        CONTAINER_FIELD_SLOT16h,
        CONTAINER_FIELD_SLOT17,
        CONTAINER_FIELD_SLOT17h,
        CONTAINER_FIELD_SLOT18,
        CONTAINER_FIELD_SLOT18h,
        CONTAINER_FIELD_SLOT19,
        CONTAINER_FIELD_SLOT19h,
        CONTAINER_FIELD_SLOT20,
        CONTAINER_FIELD_SLOT20h,
        CONTAINER_FIELD_SLOT21,
        CONTAINER_FIELD_SLOT21h,
        CONTAINER_FIELD_SLOT22,
        CONTAINER_FIELD_SLOT22h,
        CONTAINER_FIELD_SLOT23,
        CONTAINER_FIELD_SLOT23h,
        CONTAINER_FIELD_SLOT24,
        CONTAINER_FIELD_SLOT24h,
        CONTAINER_FIELD_SLOT25,
        CONTAINER_FIELD_SLOT25h,
        CONTAINER_FIELD_SLOT26,
        CONTAINER_FIELD_SLOT26h,
        CONTAINER_FIELD_SLOT27,
        CONTAINER_FIELD_SLOT27h,
        CONTAINER_FIELD_SLOT28,
        CONTAINER_FIELD_SLOT28h,
        CONTAINER_FIELD_SLOT29,
        CONTAINER_FIELD_SLOT29h,
        CONTAINER_FIELD_SLOT30,
        CONTAINER_FIELD_SLOT30h,
        CONTAINER_FIELD_SLOT31,
        CONTAINER_FIELD_SLOT31h,
        CONTAINER_FIELD_SLOT32,
        CONTAINER_FIELD_SLOT32h,
        CONTAINER_FIELD_SLOT33,
        CONTAINER_FIELD_SLOT33h,
        CONTAINER_FIELD_SLOT34,
        CONTAINER_FIELD_SLOT34h,
        CONTAINER_FIELD_SLOT35,
        CONTAINER_FIELD_SLOT35h,
        CONTAINER_FIELD_SLOT36,
        CONTAINER_FIELD_SLOT36h,
        CONTAINER_FIELD_NUM_SLOTS                                = ItemField.ITEM_END + 0x48, // Size: 1, Flags: UF_FLAG_PUBLIC
        CONTAINER_END                                            = ItemField.ITEM_END + 0x49
    }

    public enum UnitField
    {
        UNIT_FIELD_CHARM                                         = ObjectField.OBJECT_END + 0x00, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_CHARM2,
        UNIT_FIELD_SUMMON                                        = ObjectField.OBJECT_END + 0x02, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_SUMMON2,
        UNIT_FIELD_CRITTER                                       = ObjectField.OBJECT_END + 0x04, // Size: 2, Flags: UF_FLAG_PRIVATE
        UNIT_FIELD_CRITTER2,
        UNIT_FIELD_CHARMED_BY                                    = ObjectField.OBJECT_END + 0x06, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_CHARMED_BY2,
        UNIT_FIELD_SUMMONED_BY                                   = ObjectField.OBJECT_END + 0x08, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_SUMMONED_BY2,
        UNIT_FIELD_CREATED_BY                                    = ObjectField.OBJECT_END + 0x0A, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_CREATED_BY2,
        UNIT_FIELD_DEMON_CREATOR                                 = ObjectField.OBJECT_END + 0x0C, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_DEMON_CREATOR2,
        UNIT_FIELD_TARGET                                        = ObjectField.OBJECT_END + 0x0E, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_TARGET2,
        UNIT_FIELD_BATTLE_PET_COMPANION_GUID                     = ObjectField.OBJECT_END + 0x10, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_BATTLE_PET_COMPANION_GUID2,
        UNIT_FIELD_CHANNEL_OBJECT                                = ObjectField.OBJECT_END + 0x12, // Size: 2, Flags: UF_FLAG_PUBLIC, UF_FLAG_URGENT
        UNIT_FIELD_CHANNEL_OBJECT2,
        UNIT_FIELD_CHANNEL_SPELL                                 = ObjectField.OBJECT_END + 0x14, // Size: 1, Flags: UF_FLAG_PUBLIC, UF_FLAG_URGENT
        UNIT_FIELD_SUMMONED_BY_HOME_REALM                        = ObjectField.OBJECT_END + 0x15, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_SEX                                           = ObjectField.OBJECT_END + 0x16, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_DISPLAY_POWER                                 = ObjectField.OBJECT_END + 0x17, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_OVERRIDE_DISPLAY_POWER_ID                     = ObjectField.OBJECT_END + 0x18, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_HEALTH                                        = ObjectField.OBJECT_END + 0x19, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_POWER                                         = ObjectField.OBJECT_END + 0x1A, // Size: 5, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_POWER2,
        UNIT_FIELD_POWER3,
        UNIT_FIELD_POWER4,
        UNIT_FIELD_POWER5,
        UNIT_FIELD_MAX_HEALTH                                    = ObjectField.OBJECT_END + 0x1F, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_MAX_POWER                                     = ObjectField.OBJECT_END + 0x20, // Size: 5, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_MAX_POWER2,
        UNIT_FIELD_MAX_POWER3,
        UNIT_FIELD_MAX_POWER4,
        UNIT_FIELD_MAX_POWER5,
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER                     = ObjectField.OBJECT_END + 0x25, // Size: 5, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER, UF_FLAG_UNIT_ALL
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER2,
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER3,
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER4,
        UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER5,
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER         = ObjectField.OBJECT_END + 0x2A, // Size: 5, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER, UF_FLAG_UNIT_ALL
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER2,
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER3,
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER4,
        UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER5,
        UNIT_FIELD_LEVEL                                         = ObjectField.OBJECT_END + 0x2F, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_EFFECTIVE_LEVEL                               = ObjectField.OBJECT_END + 0x30, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_FACTION_TEMPLATE                              = ObjectField.OBJECT_END + 0x31, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_VIRTUAL_ITEM_ID                               = ObjectField.OBJECT_END + 0x32, // Size: 3, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_VIRTUAL_ITEM_ID2,
        UNIT_FIELD_VIRTUAL_ITEM_ID3,
        UNIT_FIELD_FLAGS                                         = ObjectField.OBJECT_END + 0x35, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_FLAGS2                                        = ObjectField.OBJECT_END + 0x36, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_AURA_STATE                                    = ObjectField.OBJECT_END + 0x37, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_ATTACK_ROUND_BASE_TIME                        = ObjectField.OBJECT_END + 0x38, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_ATTACK_ROUND_BASE_TIME2,
        UNIT_FIELD_RANGED_ATTACK_ROUND_BASE_TIME                 = ObjectField.OBJECT_END + 0x3A, // Size: 1, Flags: UF_FLAG_PRIVATE
        UNIT_FIELD_BOUNDING_RADIUS                               = ObjectField.OBJECT_END + 0x3B, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_COMBAT_REACH                                  = ObjectField.OBJECT_END + 0x3C, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_DISPLAY_ID                                    = ObjectField.OBJECT_END + 0x3D, // Size: 1, Flags: UF_FLAG_VIEWER_DEPENDENT, UF_FLAG_URGENT
        UNIT_FIELD_NATIVE_DISPLAY_ID                             = ObjectField.OBJECT_END + 0x3E, // Size: 1, Flags: UF_FLAG_PUBLIC, UF_FLAG_URGENT
        UNIT_FIELD_MOUNT_DISPLAY_ID                              = ObjectField.OBJECT_END + 0x3F, // Size: 1, Flags: UF_FLAG_PUBLIC, UF_FLAG_URGENT
        UNIT_FIELD_MIN_DAMAGE                                    = ObjectField.OBJECT_END + 0x40, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER, UF_FLAG_EMPATH
        UNIT_FIELD_MAX_DAMAGE                                    = ObjectField.OBJECT_END + 0x41, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER, UF_FLAG_EMPATH
        UNIT_FIELD_MIN_OFF_HAND_DAMAGE                           = ObjectField.OBJECT_END + 0x42, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER, UF_FLAG_EMPATH
        UNIT_FIELD_MAX_OFF_HAND_DAMAGE                           = ObjectField.OBJECT_END + 0x43, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER, UF_FLAG_EMPATH
        UNIT_FIELD_ANIM_TIER                                     = ObjectField.OBJECT_END + 0x44, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_PET_NUMBER                                    = ObjectField.OBJECT_END + 0x45, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_PET_NAME_TIMESTAMP                            = ObjectField.OBJECT_END + 0x46, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_PET_EXPERIENCE                                = ObjectField.OBJECT_END + 0x47, // Size: 1, Flags: UF_FLAG_OWNER
        UNIT_FIELD_PET_NEXT_LEVEL_EXPERIENCE                     = ObjectField.OBJECT_END + 0x48, // Size: 1, Flags: UF_FLAG_OWNER
        UNIT_FIELD_MOD_CASTING_SPEED                             = ObjectField.OBJECT_END + 0x49, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_MOD_SPELL_HASTE                               = ObjectField.OBJECT_END + 0x4A, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_MOD_HASTE                                     = ObjectField.OBJECT_END + 0x4B, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_MOD_RANGED_HASTE                              = ObjectField.OBJECT_END + 0x4C, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_MOD_HASTE_REGEN                               = ObjectField.OBJECT_END + 0x4D, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_CREATED_BY_SPELL                              = ObjectField.OBJECT_END + 0x4E, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_NPC_FLAGS                                     = ObjectField.OBJECT_END + 0x4F, // Size: 1, Flags: UF_FLAG_PUBLIC, UF_FLAG_VIEWER_DEPENDENT
        UNIT_FIELD_NPC_EMOTESTATE                                = ObjectField.OBJECT_END + 0x50, // Size: 2, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_NPC_EMOTESTATE2,
        UNIT_FIELD_STATS                                         = ObjectField.OBJECT_END + 0x52, // Size: 5, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_STATS2,
        UNIT_FIELD_STATS3,
        UNIT_FIELD_STATS4,
        UNIT_FIELD_STATS5,
        UNIT_FIELD_STAT_POS_BUFF                                 = ObjectField.OBJECT_END + 0x57, // Size: 5, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_STAT_POS_BUFF2,
        UNIT_FIELD_STAT_POS_BUFF3,
        UNIT_FIELD_STAT_POS_BUFF4,
        UNIT_FIELD_STAT_POS_BUFF5,
        UNIT_FIELD_STAT_NEG_BUFF                                 = ObjectField.OBJECT_END + 0x5C, // Size: 5, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_STAT_NEG_BUFF2,
        UNIT_FIELD_STAT_NEG_BUFF3,
        UNIT_FIELD_STAT_NEG_BUFF4,
        UNIT_FIELD_STAT_NEG_BUFF5,
        UNIT_FIELD_RESISTANCES                                   = ObjectField.OBJECT_END + 0x61, // Size: 7, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER, UF_FLAG_EMPATH
        UNIT_FIELD_RESISTANCES2,
        UNIT_FIELD_RESISTANCES3,
        UNIT_FIELD_RESISTANCES4,
        UNIT_FIELD_RESISTANCES5,
        UNIT_FIELD_RESISTANCES6,
        UNIT_FIELD_RESISTANCES7,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_POSITIVE                 = ObjectField.OBJECT_END + 0x68, // Size: 7, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_RESISTANCE_BUFF_MODS_POSITIVE2,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_POSITIVE3,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_POSITIVE4,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_POSITIVE5,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_POSITIVE6,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_POSITIVE7,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_NEGATIVE                 = ObjectField.OBJECT_END + 0x6F, // Size: 7, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_RESISTANCE_BUFF_MODS_NEGATIVE2,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_NEGATIVE3,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_NEGATIVE4,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_NEGATIVE5,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_NEGATIVE6,
        UNIT_FIELD_RESISTANCE_BUFF_MODS_NEGATIVE7,
        UNIT_FIELD_BASE_MANA                                     = ObjectField.OBJECT_END + 0x76, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_BASE_HEALTH                                   = ObjectField.OBJECT_END + 0x77, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_SHAPESHIFT_FORM                               = ObjectField.OBJECT_END + 0x78, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_ATTACK_POWER                                  = ObjectField.OBJECT_END + 0x79, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_ATTACK_POWER_MOD_POS                          = ObjectField.OBJECT_END + 0x7A, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_ATTACK_POWER_MOD_NEG                          = ObjectField.OBJECT_END + 0x7B, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER                       = ObjectField.OBJECT_END + 0x7C, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER                           = ObjectField.OBJECT_END + 0x7D, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_POS                   = ObjectField.OBJECT_END + 0x7E, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER_MOD_NEG                   = ObjectField.OBJECT_END + 0x7F, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER                = ObjectField.OBJECT_END + 0x80, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_MIN_RANGED_DAMAGE                             = ObjectField.OBJECT_END + 0x81, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_MAX_RANGED_DAMAGE                             = ObjectField.OBJECT_END + 0x82, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_POWER_COST_MODIFIER                           = ObjectField.OBJECT_END + 0x83, // Size: 7, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_POWER_COST_MODIFIER2,
        UNIT_FIELD_POWER_COST_MODIFIER3,
        UNIT_FIELD_POWER_COST_MODIFIER4,
        UNIT_FIELD_POWER_COST_MODIFIER5,
        UNIT_FIELD_POWER_COST_MODIFIER6,
        UNIT_FIELD_POWER_COST_MODIFIER7,
        UNIT_FIELD_POWER_COST_MULTIPLIER                         = ObjectField.OBJECT_END + 0x8A, // Size: 7, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_POWER_COST_MULTIPLIER2,
        UNIT_FIELD_POWER_COST_MULTIPLIER3,
        UNIT_FIELD_POWER_COST_MULTIPLIER4,
        UNIT_FIELD_POWER_COST_MULTIPLIER5,
        UNIT_FIELD_POWER_COST_MULTIPLIER6,
        UNIT_FIELD_POWER_COST_MULTIPLIER7,
        UNIT_FIELD_MAX_HEALTH_MODIFIER                           = ObjectField.OBJECT_END + 0x91, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_OWNER
        UNIT_FIELD_HOVER_HEIGHT                                  = ObjectField.OBJECT_END + 0x92, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_MIN_ITEM_LEVEL                                = ObjectField.OBJECT_END + 0x93, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_MAX_ITEM_LEVEL                                = ObjectField.OBJECT_END + 0x94, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_WILD_BATTLE_PET_LEVEL                         = ObjectField.OBJECT_END + 0x95, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_BATTLE_PET_COMPANION_NAME_TIMESTAMP           = ObjectField.OBJECT_END + 0x96, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_FIELD_INTERACT_SPELL_ID                             = ObjectField.OBJECT_END + 0x97, // Size: 1, Flags: UF_FLAG_PUBLIC
        UNIT_END                                                 = ObjectField.OBJECT_END + 0x98
    }

    public enum PlayerField
    {
        PLAYER_FIELD_DUEL_ARBITER                                = UnitField.UNIT_END + 0x000, // Size: 2, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_DUEL_ARBITER2,
        PLAYER_FIELD_PLAYER_FLAGS                                = UnitField.UNIT_END + 0x002, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_GUILD_RANK_ID                               = UnitField.UNIT_END + 0x003, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_GUILD_DELETE_DATE                           = UnitField.UNIT_END + 0x004, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_GUILD_LEVEL                                 = UnitField.UNIT_END + 0x005, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_HAIR_COLOR_ID                               = UnitField.UNIT_END + 0x006, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_REST_STATE                                  = UnitField.UNIT_END + 0x007, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_ARENA_FACTION                               = UnitField.UNIT_END + 0x008, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_DUEL_TEAM                                   = UnitField.UNIT_END + 0x009, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_GUILD_TIME_STAMP                            = UnitField.UNIT_END + 0x00A, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_QUEST_LOG                                   = UnitField.UNIT_END + 0x00B, // Size: 750, Flags: UF_FLAG_PARTY
        PLAYER_FIELD_VISIBLE_ITEMS                               = UnitField.UNIT_END + 0x2F9, // Size: 19, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_VISIBLE_ITEM_NCHANTMENTS                    = UnitField.UNIT_END + 0x30D, // Size: 19, Flags: UF_FLAG_PARTY
        PLAYER_FIELD_PLAYER_TITLE                                = UnitField.UNIT_END + 0x31F, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_FAKE_INEBRIATION                            = UnitField.UNIT_END + 0x320, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_VIRTUAL_PLAYER_REALM                        = UnitField.UNIT_END + 0x321, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_CURRENT_SPEC_ID                             = UnitField.UNIT_END + 0x322, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_TAXI_MOUNT_ANIM_KIT_ID                      = UnitField.UNIT_END + 0x323, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_CURRENT_BATTLE_PET_BREED_QUALITY            = UnitField.UNIT_END + 0x324, // Size: 1, Flags: UF_FLAG_PUBLIC
        PLAYER_FIELD_INV_SLOTS                                   = UnitField.UNIT_END + 0x325, // Size: 46, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_PACK_SLOTS                                  = UnitField.UNIT_END + 0x353, // Size: 32, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_BANK_SLOTS                                  = UnitField.UNIT_END + 0x373, // Size: 56, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_BANKBAG_SLOTS                               = UnitField.UNIT_END + 0x3AB, // Size: 14  Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_VENDORBUYBACK_SLOTS                         = UnitField.UNIT_END + 0x3B9, // Size: 24, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_FARSIGHT_OBJECT                             = UnitField.UNIT_END + 0x3D1, // Size: 2, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_FARSIGHT_OBJECT2,
        PLAYER_FIELD_KNOWN_TITLES                                = UnitField.UNIT_END + 0x3D3, // Size: 10, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_COINAGE                                     = UnitField.UNIT_END + 0x3DD, // Size: 2, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_COINAGE2,
        PLAYER_FIELD_XP                                          = UnitField.UNIT_END + 0x3DF, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_NEXT_LEVEL_XP                               = UnitField.UNIT_END + 0x3E0, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SKILL_LINEIDS                               = UnitField.UNIT_END + 0x03E1, // Size: 64, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SKILL_STEPS                                 = UnitField.UNIT_END + 0x0421, // Size: 64, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SKILL_RANKS                                 = UnitField.UNIT_END + 0x0461, // Size: 64, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SKILL_STARTING_RANKS                        = UnitField.UNIT_END + 0x04A1, // Size: 64, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SKILL_MAX_RANKS                             = UnitField.UNIT_END + 0x04E1, // Size: 64, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SKILL_MODIFIERS                             = UnitField.UNIT_END + 0x0521, // Size: 64, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SKILL_TALENTS                               = UnitField.UNIT_END + 0x056F, // Size: 64, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_CHARACTER_POINTS                            = UnitField.UNIT_END + 0x5A1, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MAX_TALENT_TIERS                            = UnitField.UNIT_END + 0x5A2, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_TRACK_CREATURE_MASK                         = UnitField.UNIT_END + 0x5A3, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_TRACK_RESOURCE_MASK                         = UnitField.UNIT_END + 0x5A4, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MAINHAND_EXPERTISE                          = UnitField.UNIT_END + 0x5A5, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_OFFHAND_EXPERTISE                           = UnitField.UNIT_END + 0x5A6, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_RANGED_EXPERTISE                            = UnitField.UNIT_END + 0x5A7, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_COMBAT_RATING_EXPERTISE                     = UnitField.UNIT_END + 0x5A8, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_BLOCK_PERCENTAGE                            = UnitField.UNIT_END + 0x5A9, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_DODGE_PERCENTAGE                            = UnitField.UNIT_END + 0x5AA, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_PARRY_PERCENTAGE                            = UnitField.UNIT_END + 0x5AB, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_CRIT_PERCENTAGE                             = UnitField.UNIT_END + 0x5AC, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_RANGED_CRIT_PERCENTAGE                      = UnitField.UNIT_END + 0x5AD, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_OFFHAND_CRIT_PERCENTAGE                     = UnitField.UNIT_END + 0x5AE, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SPELL_CRIT_PERCENTAGE                       = UnitField.UNIT_END + 0x5AF, // Size: 7, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SHIELD_BLOCK                                = UnitField.UNIT_END + 0x5B6, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SHIELD_BLOCK_CRIT_PERCENTAGE                = UnitField.UNIT_END + 0x5B7, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MASTERY                                     = UnitField.UNIT_END + 0x5B8, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_PVP_POWER_DAMAGE                            = UnitField.UNIT_END + 0x5B9, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_PVP_POWER_HEALING                           = UnitField.UNIT_END + 0x5BA, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_EXPLORED_ZONES                              = UnitField.UNIT_END + 0x5BB, // Size: 200, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_REST_STATE_BONUS_POOL                       = UnitField.UNIT_END + 0x683, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS                         = UnitField.UNIT_END + 0x684, // Size: 7, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG                         = UnitField.UNIT_END + 0x68B, // Size: 7, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_DAMAGE_DONE_PERCENT                     = UnitField.UNIT_END + 0x692, // Size: 7, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_HEALING_DONE_POS                        = UnitField.UNIT_END + 0x699, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_HEALING_PERCENT                         = UnitField.UNIT_END + 0x69A, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_HEALING_DONE_PERCENT                    = UnitField.UNIT_END + 0x69B, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_PERIODIC_HEALING_DONE_PERCENT           = UnitField.UNIT_END + 0x69C, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_WEAPON_DMG_MULTIPLIERS                      = UnitField.UNIT_END + 0x69D, // Size: 3, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_SPELL_POWER_PERCENT                     = UnitField.UNIT_END + 0x6A0, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_RESILIENCE_PERCENT                      = UnitField.UNIT_END + 0x6A1, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_OVERRIDE_SPELL_POWER_BY_APPERCENT           = UnitField.UNIT_END + 0x6A2, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_OVERRIDE_APBY_SPELL_POWER_PERCENT           = UnitField.UNIT_END + 0x6A3, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_TARGET_RESISTANCE                       = UnitField.UNIT_END + 0x6A4, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE              = UnitField.UNIT_END + 0x6A5, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_LIFETIME_MAX_RANK                           = UnitField.UNIT_END + 0x6A6, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SELF_RES_SPELL                              = UnitField.UNIT_END + 0x6A7, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_PVP_MEDALS                                  = UnitField.UNIT_END + 0x6A8, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_BUYBACK_PRICE                               = UnitField.UNIT_END + 0x6A9, // Size: 12, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_BUYBACK_TIMESTAMP                           = UnitField.UNIT_END + 0x6B5, // Size: 12, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_YESTERDAY_HONORABLE_KILLS                   = UnitField.UNIT_END + 0x6C1, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS                    = UnitField.UNIT_END + 0x6C2, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_WATCHED_FACTION_INDEX                       = UnitField.UNIT_END + 0x6C3, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_COMBAT_RATINGS                              = UnitField.UNIT_END + 0x6C4, // Size: 27, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_PVP_INFO                                    = UnitField.UNIT_END + 0x6DF, // Size: 24, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MAX_LEVEL                                   = UnitField.UNIT_END + 0x6F7, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_RUNE_REGEN                                  = UnitField.UNIT_END + 0x6F8, // Size: 4, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_NO_REAGENT_COST_MASK                        = UnitField.UNIT_END + 0x6FC, // Size: 4, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_GLYPH_SLOTS                                 = UnitField.UNIT_END + 0x700, // Size: 6, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_GLYPHS                                      = UnitField.UNIT_END + 0x706, // Size: 6, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_GLYPH_SLOTS_ENABLED                         = UnitField.UNIT_END + 0x70C, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_PET_SPELL_POWER                             = UnitField.UNIT_END + 0x70D, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_RESEARCHING                                 = UnitField.UNIT_END + 0x70E, // Size: 8, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_PROFESSION_SKILL_LINE                       = UnitField.UNIT_END + 0x716, // Size: 2, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_PROFESSION_SKILL_LINE2,
        PLAYER_FIELD_UI_HIT_MODIFIER                             = UnitField.UNIT_END + 0x718, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_UI_SPELL_HIT_MODIFIER                       = UnitField.UNIT_END + 0x719, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_HOME_REALM_TIME_OFFSET                      = UnitField.UNIT_END + 0x71A, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_MOD_PET_HASTE                               = UnitField.UNIT_END + 0x71B, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SUMMONED_BATTLE_PET_GUID                    = UnitField.UNIT_END + 0x71C, // Size: 2, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_SUMMONED_BATTLE_PET_GUID2,
        PLAYER_FIELD_OVERRIDE_SPELLS_ID                          = UnitField.UNIT_END + 0x71E, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_URGENT_SELF_ONLY
        PLAYER_FIELD_LFG_BONUS_FACTION_ID                        = UnitField.UNIT_END + 0x71F, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_LOOT_SPEC_ID                                = UnitField.UNIT_END + 0x720, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_FIELD_OVERRIDE_ZONE_PVPTYPE                       = UnitField.UNIT_END + 0x721, // Size: 1, Flags: UF_FLAG_PRIVATE, UF_FLAG_URGENT_SELF_ONLY
        PLAYER_FIELD_ITEM_LEVEL_DELTA                            = UnitField.UNIT_END + 0x722, // Size: 1, Flags: UF_FLAG_PRIVATE
        PLAYER_END                                               = UnitField.UNIT_END + 0x723
    }

    public enum GameObjectField
    {
        GAMEOBJECT_FIELD_CREATED_BY                              = ObjectField.OBJECT_END + 0x0, // Size: 2, Flags: UF_FLAG_PUBLIC
        GAMEOBJECT_FIELD_CREATED_BY2,
        GAMEOBJECT_FIELD_DISPLAY_ID                              = ObjectField.OBJECT_END + 0x2, // Size: 1, Flags: UF_FLAG_PUBLIC
        GAMEOBJECT_FIELD_FLAGS                                   = ObjectField.OBJECT_END + 0x3, // Size: 1, Flags: UF_FLAG_PUBLIC, UF_FLAG_URGENT
        GAMEOBJECT_FIELD_PARENT_ROTATION                         = ObjectField.OBJECT_END + 0x4, // Size: 4, Flags: UF_FLAG_PUBLIC
        GAMEOBJECT_FIELD_PARENT_ROTATION2,
        GAMEOBJECT_FIELD_PARENT_ROTATION3,
        GAMEOBJECT_FIELD_PARENT_ROTATION4,
        GAMEOBJECT_FIELD_FACTION_TEMPLATE                        = ObjectField.OBJECT_END + 0x8, // Size: 1, Flags: UF_FLAG_PUBLIC
        GAMEOBJECT_FIELD_LEVEL                                   = ObjectField.OBJECT_END + 0x9, // Size: 1, Flags: UF_FLAG_PUBLIC
        GAMEOBJECT_FIELD_PERCENT_HEALTH                          = ObjectField.OBJECT_END + 0xA, // Size: 1, Flags: UF_FLAG_PUBLIC, UF_FLAG_URGENT
        GAMEOBJECT_FIELD_STATE_SPELL_VISUAL_ID                   = ObjectField.OBJECT_END + 0xB, // Size: 1, Flags: UF_FLAG_PUBLIC, UF_FLAG_URGENT
        GAMEOBJECT_END                                           = ObjectField.OBJECT_END + 0xC
    }

    public enum DynamicObjectField
    {
        DYNAMICOBJECT_FIELD_CASTER                               = ObjectField.OBJECT_END + 0x0, // Size: 2, Flags: UF_FLAGý_PUBLIC
        DYNAMICOBJECT_FIELD_CASTER2,
        DYNAMICOBJECT_FIELD_TYPE_AND_VISUAL_ID                   = ObjectField.OBJECT_END + 0x2, // Size: 1, Flags: UF_FLAG_VIEWER_DEPENDENT
        DYNAMICOBJECT_FIELD_SPELL_ID                             = ObjectField.OBJECT_END + 0x3, // Size: 1, Flags: UF_FLAG_PUBLIC
        DYNAMICOBJECT_FIELD_RADIUS                               = ObjectField.OBJECT_END + 0x4, // Size: 1, Flags: UF_FLAG_PUBLIC
        DYNAMICOBJECT_FIELD_CAST_TIME                            = ObjectField.OBJECT_END + 0x5, // Size: 1, Flags: UF_FLAG_PUBLIC
        DYNAMICOBJECT_END                                        = ObjectField.OBJECT_END + 0x6
    }

    public enum CorpseField
    {
        CORPSE_FIELD_OWNER                                       = ObjectField.OBJECT_END + 0x00, // Size: 2, Flags: UF_FLAG_PUBLIC
        CORPSE_FIELD_OWNER2,
        CORPSE_FIELD_PARTY_GUID                                  = ObjectField.OBJECT_END + 0x02, // Size: 2, Flags: UF_FLAG_PUBLIC
        CORPSE_FIELD_PARTY_GUID2,
        CORPSE_FIELD_DISPLAY_ID                                  = ObjectField.OBJECT_END + 0x04, // Size: 1, Flags: UF_FLAG_PUBLIC
        CORPSE_FIELD_ITEM                                        = ObjectField.OBJECT_END + 0x05, // Size: 19, Flags: UF_FLAG_PUBLIC
        CORPSE_FIELD_ITEM2,
        CORPSE_FIELD_ITEM3,
        CORPSE_FIELD_ITEM4,
        CORPSE_FIELD_ITEM5,
        CORPSE_FIELD_ITEM6,
        CORPSE_FIELD_ITEM7,
        CORPSE_FIELD_ITEM8,
        CORPSE_FIELD_ITEM9,
        CORPSE_FIELD_ITEM10,
        CORPSE_FIELD_ITEM11,
        CORPSE_FIELD_ITEM12,
        CORPSE_FIELD_ITEM13,
        CORPSE_FIELD_ITEM14,
        CORPSE_FIELD_ITEM15,
        CORPSE_FIELD_ITEM16,
        CORPSE_FIELD_ITEM17,
        CORPSE_FIELD_ITEM18,
        CORPSE_FIELD_ITEM19,
        CORPSE_FIELD_SKIN_ID                                     = ObjectField.OBJECT_END + 0x18, // Size: 1, Flags: UF_FLAG_PUBLIC
        CORPSE_FIELD_FACIAL_HAIR_STYLE_ID                        = ObjectField.OBJECT_END + 0x19, // Size: 1, Flags: UF_FLAG_PUBLIC
        CORPSE_FIELD_FLAGS                                       = ObjectField.OBJECT_END + 0x1A, // Size: 1, Flags: UF_FLAG_PUBLIC
        CORPSE_FIELD_DYNAMIC_FLAGS                               = ObjectField.OBJECT_END + 0x1B, // Size: 1, Flags: UF_FLAG_VIEWER_DEPENDENT
        CORPSE_END                                               = ObjectField.OBJECT_END + 0x1C
    }

    public enum AreaTriggerField
    {
        AREATRIGGER_FIELD_CASTER                                 = ObjectField.OBJECT_END + 0x0, // Size: 2, Flags: UF_FLAG_PUBLIC
        AREATRIGGER_FIELD_CASTER2,
        AREATRIGGER_FIELD_DURATION                               = ObjectField.OBJECT_END + 0x2, // Size: 1, Flags: UF_FLAG_PUBLIC
        AREATRIGGER_FIELD_SPELL_ID                               = ObjectField.OBJECT_END + 0x3, // Size: 1, Flags: UF_FLAG_PUBLIC
        AREATRIGGER_FIELD_SPELL_VISUAL_ID                        = ObjectField.OBJECT_END + 0x4, // Size: 1, Flags: UF_FLAG_VIEWER_DEPENDENT
        AREATRIGGER_FIELD_EXPLICIT_SCALE                         = ObjectField.OBJECT_END + 0x5, // Size: 1, Flags: UF_FLAG_PUBLIC, UF_FLAG_URGENT
        AREATRIGGER_END                                          = ObjectField.OBJECT_END + 0x6
    }

    public enum SceneObjectField
    {
        SCENEOBJECT_FIELD_SCRIPT_PACKAGE_ID                      = ObjectField.OBJECT_END + 0x0, // Size: 1, Flags: UF_FLAG_PUBLIC
        SCENEOBJECT_FIELD_RND_SEED_VAL                           = ObjectField.OBJECT_END + 0x1, // Size: 1, Flags: UF_FLAG_PUBLIC
        SCENEOBJECT_FIELD_CREATED_BY                             = ObjectField.OBJECT_END + 0x2, // Size: 2, Flags: UF_FLAG_PUBLIC
        SCENEOBJECT_FIELD_CREATED_BY2,
        SCENEOBJECT_FIELD_SCENE_TYPE                             = ObjectField.OBJECT_END + 0x4, // Size: 1, Flags: UF_FLAG_PUBLIC
        SCENEOBJECT_FIELD_END                                    = ObjectField.OBJECT_END + 0x5
    };
}
