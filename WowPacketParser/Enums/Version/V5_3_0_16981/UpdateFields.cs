namespace WowPacketParser.Enums.Version.V5_3_0_16981
{
    // ReSharper disable InconsistentNaming
    // 5.3.0.16981
    public enum ObjectField
    {
        OBJECT_FIELD_guid                              = 0x0000, // Size =   2, Type: Flags PUBLIC
        OBJECT_FIELD_data                              = 0x0002, // Size =   2, Type: Flags PUBLIC
        OBJECT_FIELD_type                              = 0x0004, // Size =   1, Type: Flags PUBLIC
        OBJECT_FIELD_entryID                           = 0x0005, // Size =   1, Type: Flags UNUSED2
        OBJECT_FIELD_dynamicFlags                      = 0x0006, // Size =   1, Type: Flags UNUSED2 | DYNAMIC
        OBJECT_FIELD_scale                             = 0x0007, // Size =   1, Type: Flags PUBLIC
        OBJECT_END                                     = 0x0008,
    };

    public enum ItemField
    {
        ITEM_FIELD_owner                               = ObjectField.OBJECT_END + 0x0000, // Size =   2, Type: Flags PUBLIC
        ITEM_FIELD_containedIn                         = ObjectField.OBJECT_END + 0x0002, // Size =   2, Type: Flags PUBLIC
        ITEM_FIELD_creator                             = ObjectField.OBJECT_END + 0x0004, // Size =   2, Type: Flags PUBLIC
        ITEM_FIELD_giftCreator                         = ObjectField.OBJECT_END + 0x0006, // Size =   2, Type: Flags PUBLIC
        ITEM_FIELD_stackCount                          = ObjectField.OBJECT_END + 0x0008, // Size =   1, Type: Flags OWNER
        ITEM_FIELD_expiration                          = ObjectField.OBJECT_END + 0x0009, // Size =   1, Type: Flags OWNER
        ITEM_FIELD_spellCharges                        = ObjectField.OBJECT_END + 0x000A, // Size =   5, Type: Flags OWNER
        ITEM_FIELD_dynamicFlags                        = ObjectField.OBJECT_END + 0x000F, // Size =   1, Type: Flags PUBLIC
        ITEM_FIELD_enchantment                         = ObjectField.OBJECT_END + 0x0010, // Size =  39, Type: Flags PUBLIC
        ITEM_FIELD_propertySeed                        = ObjectField.OBJECT_END + 0x0037, // Size =   1, Type: Flags PUBLIC
        ITEM_FIELD_randomPropertiesID                  = ObjectField.OBJECT_END + 0x0038, // Size =   1, Type: Flags PUBLIC
        ITEM_FIELD_durability                          = ObjectField.OBJECT_END + 0x0039, // Size =   1, Type: Flags OWNER
        ITEM_FIELD_maxDurability                       = ObjectField.OBJECT_END + 0x003A, // Size =   1, Type: Flags OWNER
        ITEM_FIELD_createPlayedTime                    = ObjectField.OBJECT_END + 0x003B, // Size =   1, Type: Flags PUBLIC
        ITEM_FIELD_modifiersMask                       = ObjectField.OBJECT_END + 0x003C, // Size =   1, Type: Flags OWNER
        ITEM_END                                       = ObjectField.OBJECT_END + 0x003D,
    };


    public enum ItemDynamicField
    {
        ITEM_DYNAMIC_FIELD_MODIFIERS                        = ItemField.ITEM_END + 0x0,
        ITEM_DYNAMIC_END                                    = ItemField.ITEM_END + 0x48
    }

    public enum ContainerField
    {
        CONTAINER_FIELD_slots                          = ItemField.ITEM_END + 0x0000, // Size =  72, Type: Flags PUBLIC
        CONTAINER_FIELD_numSlots                       = ItemField.ITEM_END + 0x0048, // Size =   1, Type: Flags PUBLIC
        CONTAINER_END                                  = ItemField.ITEM_END + 0x0049,
    };


    public enum UnitField
    {
        UNIT_FIELD_charm                                 = ObjectField.OBJECT_END + 0x0000, // Size =   2, Type: Flags PUBLIC
        UNIT_FIELD_summon                                = ObjectField.OBJECT_END + 0x0002, // Size =   2, Type: Flags PUBLIC
        UNIT_FIELD_critter                               = ObjectField.OBJECT_END + 0x0004, // Size =   2, Type: Flags PRIVATE
        UNIT_FIELD_charmedBy                             = ObjectField.OBJECT_END + 0x0006, // Size =   2, Type: Flags PUBLIC
        UNIT_FIELD_summonedBy                            = ObjectField.OBJECT_END + 0x0008, // Size =   2, Type: Flags PUBLIC
        UNIT_FIELD_createdBy                             = ObjectField.OBJECT_END + 0x000A, // Size =   2, Type: Flags PUBLIC
        UNIT_FIELD_target                                = ObjectField.OBJECT_END + 0x000C, // Size =   2, Type: Flags PUBLIC
        UNIT_FIELD_battlePetCompanionGUID                = ObjectField.OBJECT_END + 0x000E, // Size =   2, Type: Flags PUBLIC
        UNIT_FIELD_channelObject                         = ObjectField.OBJECT_END + 0x0010, // Size =   2, Type: Flags PUBLIC | DYNAMIC
        UNIT_FIELD_channelSpell                          = ObjectField.OBJECT_END + 0x0012, // Size =   1, Type: Flags PUBLIC | DYNAMIC
        UNIT_FIELD_summonedByHomeRealm                   = ObjectField.OBJECT_END + 0x0013, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_displayPower                          = ObjectField.OBJECT_END + 0x0014, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_overrideDisplayPowerID                = ObjectField.OBJECT_END + 0x0015, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_health                                = ObjectField.OBJECT_END + 0x0016, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_power                                 = ObjectField.OBJECT_END + 0x0017, // Size =   5, Type: Flags PUBLIC
        UNIT_FIELD_maxHealth                             = ObjectField.OBJECT_END + 0x001C, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_maxPower                              = ObjectField.OBJECT_END + 0x001D, // Size =   5, Type: Flags PUBLIC
        UNIT_FIELD_powerRegenFlatModifier                = ObjectField.OBJECT_END + 0x0022, // Size =   5, Type: Flags PRIVATE | OWNER | PARTY_MEMBER
        UNIT_FIELD_powerRegenInterruptedFlatModifier     = ObjectField.OBJECT_END + 0x0027, // Size =   5, Type: Flags PRIVATE | OWNER | PARTY_MEMBER
        UNIT_FIELD_level                                 = ObjectField.OBJECT_END + 0x002C, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_effectiveLevel                        = ObjectField.OBJECT_END + 0x002D, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_factionTemplate                       = ObjectField.OBJECT_END + 0x002E, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_virtualItemID                         = ObjectField.OBJECT_END + 0x002F, // Size =   3, Type: Flags PUBLIC
        UNIT_FIELD_flags                                 = ObjectField.OBJECT_END + 0x0032, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_flags2                                = ObjectField.OBJECT_END + 0x0033, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_auraState                             = ObjectField.OBJECT_END + 0x0034, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_attackRoundBaseTime                   = ObjectField.OBJECT_END + 0x0035, // Size =   2, Type: Flags PUBLIC
        UNIT_FIELD_rangedAttackRoundBaseTime             = ObjectField.OBJECT_END + 0x0037, // Size =   1, Type: Flags PRIVATE
        UNIT_FIELD_boundingRadius                        = ObjectField.OBJECT_END + 0x0038, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_combatReach                           = ObjectField.OBJECT_END + 0x0039, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_displayID                             = ObjectField.OBJECT_END + 0x003A, // Size =   1, Type: Flags UNUSED2 | DYNAMIC
        UNIT_FIELD_nativeDisplayID                       = ObjectField.OBJECT_END + 0x003B, // Size =   1, Type: Flags PUBLIC | DYNAMIC
        UNIT_FIELD_mountDisplayID                        = ObjectField.OBJECT_END + 0x003C, // Size =   1, Type: Flags PUBLIC | DYNAMIC
        UNIT_FIELD_minDamage                             = ObjectField.OBJECT_END + 0x003D, // Size =   1, Type: Flags PRIVATE | OWNER | ITEM_OWNER
        UNIT_FIELD_maxDamage                             = ObjectField.OBJECT_END + 0x003E, // Size =   1, Type: Flags PRIVATE | OWNER | ITEM_OWNER
        UNIT_FIELD_minOffHandDamage                      = ObjectField.OBJECT_END + 0x003F, // Size =   1, Type: Flags PRIVATE | OWNER | ITEM_OWNER
        UNIT_FIELD_maxOffHandDamage                      = ObjectField.OBJECT_END + 0x0040, // Size =   1, Type: Flags PRIVATE | OWNER | ITEM_OWNER
        UNIT_FIELD_animTier                              = ObjectField.OBJECT_END + 0x0041, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_petNumber                             = ObjectField.OBJECT_END + 0x0042, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_petNameTimestamp                      = ObjectField.OBJECT_END + 0x0043, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_petExperience                         = ObjectField.OBJECT_END + 0x0044, // Size =   1, Type: Flags OWNER
        UNIT_FIELD_petNextLevelExperience                = ObjectField.OBJECT_END + 0x0045, // Size =   1, Type: Flags OWNER
        UNIT_FIELD_modCastingSpeed                       = ObjectField.OBJECT_END + 0x0046, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_modSpellHaste                         = ObjectField.OBJECT_END + 0x0047, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_modHaste                              = ObjectField.OBJECT_END + 0x0048, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_modHasteRegen                         = ObjectField.OBJECT_END + 0x0049, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_createdBySpell                        = ObjectField.OBJECT_END + 0x004A, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_npcFlagsunk                           = ObjectField.OBJECT_END + 0x004B, // Size =   1, Type: Flags PUBLIC | UNUSED2
        UNIT_FIELD_npcFlags                              = ObjectField.OBJECT_END + 0x004C, // Size =   2, Type: Flags PUBLIC
        UNIT_FIELD_stats                                 = ObjectField.OBJECT_END + 0x004E, // Size =   5, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_statPosBuff                           = ObjectField.OBJECT_END + 0x0053, // Size =   5, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_statNegBuff                           = ObjectField.OBJECT_END + 0x0058, // Size =   5, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_resistances                           = ObjectField.OBJECT_END + 0x005D, // Size =   7, Type: Flags PRIVATE | OWNER | ITEM_OWNER
        UNIT_FIELD_resistanceBuffModsPositive            = ObjectField.OBJECT_END + 0x0064, // Size =   7, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_resistanceBuffModsNegative            = ObjectField.OBJECT_END + 0x006B, // Size =   7, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_baseMana                              = ObjectField.OBJECT_END + 0x0072, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_baseHealth                            = ObjectField.OBJECT_END + 0x0073, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_shapeshiftForm                        = ObjectField.OBJECT_END + 0x0074, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_attackPower                           = ObjectField.OBJECT_END + 0x0075, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_attackPowerModPos                     = ObjectField.OBJECT_END + 0x0076, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_attackPowerModNeg                     = ObjectField.OBJECT_END + 0x0077, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_attackPowerMultiplier                 = ObjectField.OBJECT_END + 0x0078, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_rangedAttackPower                     = ObjectField.OBJECT_END + 0x0079, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_rangedAttackPowerModPos               = ObjectField.OBJECT_END + 0x007A, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_rangedAttackPowerModNeg               = ObjectField.OBJECT_END + 0x007B, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_rangedAttackPowerMultiplier           = ObjectField.OBJECT_END + 0x007C, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_minRangedDamage                       = ObjectField.OBJECT_END + 0x007D, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_maxRangedDamage                       = ObjectField.OBJECT_END + 0x007E, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_powerCostModifier                     = ObjectField.OBJECT_END + 0x007F, // Size =   7, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_powerCostMultiplier                   = ObjectField.OBJECT_END + 0x0086, // Size =   7, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_maxHealthModifier                     = ObjectField.OBJECT_END + 0x008D, // Size =   1, Type: Flags PRIVATE | OWNER
        UNIT_FIELD_hoverHeight                           = ObjectField.OBJECT_END + 0x008E, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_minItemLevel                          = ObjectField.OBJECT_END + 0x008F, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_maxItemLevel                          = ObjectField.OBJECT_END + 0x0090, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_wildBattlePetLevel                    = ObjectField.OBJECT_END + 0x0091, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_battlePetCompanionNameTimestamp       = ObjectField.OBJECT_END + 0x0092, // Size =   1, Type: Flags PUBLIC
        UNIT_FIELD_END                                   = ObjectField.OBJECT_END + 0x0093,
    };

    public enum UnitDynamicField
    {
        UNIT_DYNAMIC_FIELD_PASSIVE_SPELLS                   = ObjectField.OBJECT_END + 0x0,
        UNIT_DYNAMIC_END                                    = ObjectField.OBJECT_END + 0x101
    }

    public enum PlayerField
    {
        PLAYER_FIELD_duelArbiter                         = UnitField.UNIT_FIELD_END + 0x0000, // Size =   2, Type: Flags PUBLIC
        PLAYER_FIELD_playerFlags                         = UnitField.UNIT_FIELD_END + 0x0002, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_guildRankID                         = UnitField.UNIT_FIELD_END + 0x0003, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_guildDeleteDate                     = UnitField.UNIT_FIELD_END + 0x0004, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_guildLevel                          = UnitField.UNIT_FIELD_END + 0x0005, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_hairColorID                         = UnitField.UNIT_FIELD_END + 0x0006, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_restState                           = UnitField.UNIT_FIELD_END + 0x0007, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_arenaFaction                        = UnitField.UNIT_FIELD_END + 0x0008, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_duelTeam                            = UnitField.UNIT_FIELD_END + 0x0009, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_guildTimeStamp                      = UnitField.UNIT_FIELD_END + 0x000A, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_questLog                            = UnitField.UNIT_FIELD_END + 0x000B, // Size = 750, Type: Flags SPECIAL_INFO
        PLAYER_FIELD_visibleItems                        = UnitField.UNIT_FIELD_END + 0x02F9, // Size =  38, Type: Flags PUBLIC
        PLAYER_FIELD_playerTitle                         = UnitField.UNIT_FIELD_END + 0x031F, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_fakeInebriation                     = UnitField.UNIT_FIELD_END + 0x0320, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_virtualPlayerRealm                  = UnitField.UNIT_FIELD_END + 0x0321, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_currentSpecID                       = UnitField.UNIT_FIELD_END + 0x0322, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_taxiMountAnimKitID                  = UnitField.UNIT_FIELD_END + 0x0323, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_currentBattlePetBreedQuality        = UnitField.UNIT_FIELD_END + 0x0324, // Size =   1, Type: Flags PUBLIC
        PLAYER_FIELD_invSlots                      = UnitField.UNIT_FIELD_END + 0x0325, // Size = 172, Type: Flags PRIVATE
        PLAYER_FIELD_farsightObject                = UnitField.UNIT_FIELD_END + 0x03D1, // Size =   2, Type: Flags PRIVATE
        PLAYER_FIELD_knownTitles                   = UnitField.UNIT_FIELD_END + 0x03D3, // Size =   8, Type: Flags PRIVATE
        PLAYER_FIELD_coinage                       = UnitField.UNIT_FIELD_END + 0x03DB, // Size =   2, Type: Flags PRIVATE
        PLAYER_FIELD_XP                            = UnitField.UNIT_FIELD_END + 0x03DD, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_nextLevelXP                   = UnitField.UNIT_FIELD_END + 0x03DE, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_skill                         = UnitField.UNIT_FIELD_END + 0x03DF, // Size = 448, Type: Flags PRIVATE
        PLAYER_FIELD_characterPoints               = UnitField.UNIT_FIELD_END + 0x059F, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_maxTalentTiers                = UnitField.UNIT_FIELD_END + 0x05A0, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_trackCreatureMask             = UnitField.UNIT_FIELD_END + 0x05A1, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_trackResourceMask             = UnitField.UNIT_FIELD_END + 0x05A2, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_mainhandExpertise             = UnitField.UNIT_FIELD_END + 0x05A3, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_offhandExpertise              = UnitField.UNIT_FIELD_END + 0x05A4, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_rangedExpertise               = UnitField.UNIT_FIELD_END + 0x05A5, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_combatRatingExpertise         = UnitField.UNIT_FIELD_END + 0x05A6, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_blockPercentage               = UnitField.UNIT_FIELD_END + 0x05A7, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_dodgePercentage               = UnitField.UNIT_FIELD_END + 0x05A8, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_parryPercentage               = UnitField.UNIT_FIELD_END + 0x05A9, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_critPercentage                = UnitField.UNIT_FIELD_END + 0x05AA, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_rangedCritPercentage          = UnitField.UNIT_FIELD_END + 0x05AB, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_offhandCritPercentage         = UnitField.UNIT_FIELD_END + 0x05AC, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_spellCritPercentage           = UnitField.UNIT_FIELD_END + 0x05AD, // Size =   7, Type: Flags PRIVATE
        PLAYER_FIELD_shieldBlock                   = UnitField.UNIT_FIELD_END + 0x05B4, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_shieldBlockCritPercentage     = UnitField.UNIT_FIELD_END + 0x05B5, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_mastery                       = UnitField.UNIT_FIELD_END + 0x05B6, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_pvpPowerDamage                = UnitField.UNIT_FIELD_END + 0x05B7, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_pvpPowerHealing               = UnitField.UNIT_FIELD_END + 0x05B8, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_exploredZones                 = UnitField.UNIT_FIELD_END + 0x05B9, // Size = 200, Type: Flags PRIVATE
        PLAYER_FIELD_restStateBonusPool            = UnitField.UNIT_FIELD_END + 0x0681, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_modDamageDonePos              = UnitField.UNIT_FIELD_END + 0x0682, // Size =   7, Type: Flags PRIVATE
        PLAYER_FIELD_modDamageDoneNeg              = UnitField.UNIT_FIELD_END + 0x0689, // Size =   7, Type: Flags PRIVATE
        PLAYER_FIELD_modDamageDonePercent          = UnitField.UNIT_FIELD_END + 0x0690, // Size =   7, Type: Flags PRIVATE
        PLAYER_FIELD_modHealingDonePos             = UnitField.UNIT_FIELD_END + 0x0697, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_modHealingPercent             = UnitField.UNIT_FIELD_END + 0x0698, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_modHealingDonePercent         = UnitField.UNIT_FIELD_END + 0x0699, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_modPeriodicHealingDonePercent = UnitField.UNIT_FIELD_END + 0x069A, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_weaponDmgMultipliers          = UnitField.UNIT_FIELD_END + 0x069B, // Size =   3, Type: Flags PRIVATE
        PLAYER_FIELD_modSpellPowerPercent          = UnitField.UNIT_FIELD_END + 0x069E, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_modResiliencePercent          = UnitField.UNIT_FIELD_END + 0x069F, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_overrideSpellPowerByAPPercent = UnitField.UNIT_FIELD_END + 0x06A0, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_overrideAPBySpellPowerPercent = UnitField.UNIT_FIELD_END + 0x06A1, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_modTargetResistance           = UnitField.UNIT_FIELD_END + 0x06A2, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_modTargetPhysicalResistance   = UnitField.UNIT_FIELD_END + 0x06A3, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_lifetimeMaxRank               = UnitField.UNIT_FIELD_END + 0x06A4, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_selfResSpell                  = UnitField.UNIT_FIELD_END + 0x06A5, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_pvpMedals                     = UnitField.UNIT_FIELD_END + 0x06A6, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_buybackPrice                  = UnitField.UNIT_FIELD_END + 0x06A7, // Size =  12, Type: Flags PRIVATE
        PLAYER_FIELD_buybackTimestamp              = UnitField.UNIT_FIELD_END + 0x06B3, // Size =  12, Type: Flags PRIVATE
        PLAYER_FIELD_yesterdayHonorableKills       = UnitField.UNIT_FIELD_END + 0x06BF, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_lifetimeHonorableKills        = UnitField.UNIT_FIELD_END + 0x06C0, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_watchedFactionIndex           = UnitField.UNIT_FIELD_END + 0x06C1, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_combatRatings                 = UnitField.UNIT_FIELD_END + 0x06C2, // Size =  27, Type: Flags PRIVATE
        PLAYER_FIELD_arenaTeams                    = UnitField.UNIT_FIELD_END + 0x06DD, // Size =  21, Type: Flags PRIVATE
        PLAYER_FIELD_battlegroundRating            = UnitField.UNIT_FIELD_END + 0x06F2, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_maxLevel                      = UnitField.UNIT_FIELD_END + 0x06F3, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_runeRegen                     = UnitField.UNIT_FIELD_END + 0x06F4, // Size =   4, Type: Flags PRIVATE
        PLAYER_FIELD_noReagentCostMask             = UnitField.UNIT_FIELD_END + 0x06F8, // Size =   4, Type: Flags PRIVATE
        PLAYER_FIELD_glyphSlots                    = UnitField.UNIT_FIELD_END + 0x06FC, // Size =   6, Type: Flags PRIVATE
        PLAYER_FIELD_glyphs                        = UnitField.UNIT_FIELD_END + 0x0702, // Size =   6, Type: Flags PRIVATE
        PLAYER_FIELD_glyphSlotsEnabled             = UnitField.UNIT_FIELD_END + 0x0708, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_petSpellPower                 = UnitField.UNIT_FIELD_END + 0x0709, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_researching                   = UnitField.UNIT_FIELD_END + 0x070A, // Size =   8, Type: Flags PRIVATE
        PLAYER_FIELD_professionSkillLine           = UnitField.UNIT_FIELD_END + 0x0712, // Size =   2, Type: Flags PRIVATE
        PLAYER_FIELD_uiHitModifier                 = UnitField.UNIT_FIELD_END + 0x0714, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_uiSpellHitModifier            = UnitField.UNIT_FIELD_END + 0x0715, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_homeRealmTimeOffset           = UnitField.UNIT_FIELD_END + 0x0716, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_modRangedHaste                = UnitField.UNIT_FIELD_END + 0x0717, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_modPetHaste                   = UnitField.UNIT_FIELD_END + 0x0718, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_summonedBattlePetGUID         = UnitField.UNIT_FIELD_END + 0x0719, // Size =   2, Type: Flags PRIVATE
        PLAYER_FIELD_overrideSpellsID              = UnitField.UNIT_FIELD_END + 0x071B, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_lfgBonusFactionID             = UnitField.UNIT_FIELD_END + 0x071C, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_lootSpecID                    = UnitField.UNIT_FIELD_END + 0x071D, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_overrideZonePVPType           = UnitField.UNIT_FIELD_END + 0x071E, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_itemLevelDelta                = UnitField.UNIT_FIELD_END + 0x071F, // Size =   1, Type: Flags PRIVATE
        PLAYER_FIELD_END                           = UnitField.UNIT_FIELD_END + 0x0720,
    };

    public enum PlayerDynamicField
    {
        PLAYER_DYNAMIC_FIELD_RESEARCH_SITES                 = PlayerField.PLAYER_FIELD_END + 0x0,
        PLAYER_DYNAMIC_FIELD_DAILY_QUESTS_COMPLETED         = PlayerField.PLAYER_FIELD_END + 0x2,
        PLAYER_DYNAMIC_END                                  = PlayerField.PLAYER_FIELD_END + 0x4
    }

    public enum GameObjectField
    {
        GAMEOBJECT_createdBy                     = ObjectField.OBJECT_END + 0x0000, // Size =   2, Type: Flags PUBLIC
        GAMEOBJECT_displayID                     = ObjectField.OBJECT_END + 0x0002, // Size =   1, Type: Flags PUBLIC
        GAMEOBJECT_flags                         = ObjectField.OBJECT_END + 0x0003, // Size =   1, Type: Flags PUBLIC | DYNAMIC
        GAMEOBJECT_parentRotation                = ObjectField.OBJECT_END + 0x0004, // Size =   4, Type: Flags PUBLIC
        GAMEOBJECT_factionTemplate               = ObjectField.OBJECT_END + 0x0008, // Size =   1, Type: Flags PUBLIC
        GAMEOBJECT_level                         = ObjectField.OBJECT_END + 0x0009, // Size =   1, Type: Flags PUBLIC
        GAMEOBJECT_percentHealth                 = ObjectField.OBJECT_END + 0x000A, // Size =   1, Type: Flags PUBLIC | DYNAMIC
        GAMEOBJECT_stateSpellVisualID            = ObjectField.OBJECT_END + 0x000B, // Size =   1, Type: Flags PUBLIC | DYNAMIC
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
