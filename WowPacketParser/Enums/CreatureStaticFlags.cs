using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum CreatureStaticFlags : uint
    {
        Mountable                                         = 0x00000001,
        NoXp                                              = 0x00000002, // CREATURE_FLAG_EXTRA_NO_XP
        NoLoot                                            = 0x00000004,
        Unkillable                                        = 0x00000008,
        Tameable                                          = 0x00000010, // CREATURE_TYPE_FLAG_TAMEABLE
        ImmuneToPc                                        = 0x00000020, // UNIT_FLAG_IMMUNE_TO_PC
        ImmuneToNpc                                       = 0x00000040, // UNIT_FLAG_IMMUNE_TO_NPC
        CanWieldLoot                                      = 0x00000080,
        Sessile                                           = 0x00000100, // Rooted movementflag, creature is permanently rooted in place
        Uninteractible                                    = 0x00000200, // UNIT_FLAG_UNINTERACTIBLE
        NoAutomaticRegen                                  = 0x00000400, // Creatures with that flag uses no UNIT_FLAG2_REGENERATE_POWER
        DespawnInstantly                                  = 0x00000800, // Creature instantly disappear when killed
        CorpseRaid                                        = 0x00001000,
        CreatorLoot                                       = 0x00002000, // Lootable only by creator(engineering dummies)
        NoDefense                                         = 0x00004000,
        NoSpellDefense                                    = 0x00008000,
        BossMob                                           = 0x00010000, // CREATURE_TYPE_FLAG_BOSS_MOB, original description: Raid Boss Mob
        CombatPing                                        = 0x00020000,
        Aquatic                                           = 0x00040000, // aka Water Only, creature_template_movement.Ground = 0
        Amphibious                                        = 0x00080000, // Creatures will be able to enter and leave water but can only move on the ocean floor when CREATURE_STATIC_FLAG_CAN_SWIM is not present
        NoMeleeFlee                                       = 0x00100000, // "No Melee (Flee)" Prevents melee (moves as-if feared, does not make creature passive)
        VisibleToGhosts                                   = 0x00200000, // CREATURE_TYPE_FLAG_VISIBLE_TO_GHOSTS
        PvpEnabling                                       = 0x00400000, // Old UNIT_FLAG_PVP_ENABLING, now UNIT_BYTES_2_OFFSET_PVP_FLAG from UNIT_FIELD_BYTES_2
        DoNotPlayWoundAnim                                = 0x00800000, // CREATURE_TYPE_FLAG_DO_NOT_PLAY_WOUND_ANIM
        NoFactionTooltip                                  = 0x01000000, // CREATURE_TYPE_FLAG_NO_FACTION_TOOLTIP
        IgnoreCombat                                      = 0x02000000, // Actually only changes react state to passive
        OnlyAttackPvpEnabling                             = 0x04000000, // "Only attack targets that are PvP enabling"
        CallsGuards                                       = 0x08000000, // Creature will summon a guard if player is within its aggro range (even if creature doesn't attack per se)
        CanSwim                                           = 0x10000000, // UnitFlags 0x8000 UNIT_FLAG_CAN_SWIM
        Floating                                          = 0x20000000, // sets DisableGravity movementflag on spawn/reset
        MoreAudible                                       = 0x40000000, // CREATURE_TYPE_FLAG_MORE_AUDIBLE
        LargeAoi                                          = 0x80000000  // UnitFlags2 0x200000
    }

    [Flags]
    public enum CreatureStaticFlags2 : uint
    {
        NoPetScaling = 0x00000001,
        ForcePartyMembersIntoCombat                       = 0x00000002, // Original description: Force Raid Combat
        LockTappersToRaidOnDeath                          = 0x00000004, // "Lock Tappers To Raid On Death", toggleable by 'Set "RAID_LOCK_ON_DEATH" flag for unit(s)' action, CREATURE_FLAG_EXTRA_INSTANCE_BIND
        SpellAttackable                                   = 0x00000008, // CREATURE_TYPE_FLAG_SPELL_ATTACKABLE, original description(not valid anymore?): No Harmful Vertex Coloring
        NoCrushingBlows                                   = 0x00000010, // CREATURE_FLAG_EXTRA_NO_CRUSHING_BLOWS
        NoOwnerThreat                                     = 0x00000020,
        NoWoundedSlowdown                                 = 0x00000040,
        UseCreatorBonuses                                 = 0x00000080,
        IgnoreFeignDeath                                  = 0x00000100, // CREATURE_FLAG_EXTRA_IGNORE_FEIGN_DEATH
        IgnoreSanctuary                                   = 0x00000200,
        ActionTriggersWhileCharmed                        = 0x00000400,
        InteractWhileDead                                 = 0x00000800, // CREATURE_TYPE_FLAG_INTERACT_WHILE_DEAD
        NoInterruptSchoolCooldown                         = 0x00001000,
        ReturnSoulShardToMasterOfPet                      = 0x00002000,
        SkinWithHerbalism                                 = 0x00004000, // CREATURE_TYPE_FLAG_SKIN_WITH_HERBALISM
        SkinWithMining                                    = 0x00008000, // CREATURE_TYPE_FLAG_SKIN_WITH_MINING
        AlertContentTeamOnDeath                           = 0x00010000,
        AlertContentTeamAt90PctHp                         = 0x00020000,
        AllowMountedCombat                                = 0x00040000, // CREATURE_TYPE_FLAG_ALLOW_MOUNTED_COMBAT
        PvpEnablingOoc                                    = 0x00080000,
        NoDeathMessage                                    = 0x00100000, // CREATURE_TYPE_FLAG_NO_DEATH_MESSAGE
        IgnorePathingFailure                              = 0x00200000,
        FullSpellList                                     = 0x00400000,
        DoesNotReduceReputationForRaids                   = 0x00800000,
        IgnoreMisdirection                                = 0x01000000,
        HideBody                                          = 0x02000000, // UNIT_FLAG2_HIDE_BODY
        SpawnDefensive                                    = 0x04000000,
        ServerOnly                                        = 0x08000000,
        CanSafeFall                                       = 0x10000000, // Original description: No Collision
        CanAssist                                         = 0x20000000, // CREATURE_TYPE_FLAG_CAN_ASSIST, original description: Player Can Heal/Buff
        NoSkillGains                                      = 0x40000000, // CREATURE_FLAG_EXTRA_NO_SKILL_GAINS
        NoPetBar                                          = 0x80000000  // CREATURE_TYPE_FLAG_NO_PET_BAR
    }

    [Flags]
    public enum CreatureStaticFlags3 : uint
    {
        NoDamageHistory                                   = 0x00000001,
        DontPvpEnableOwner                                = 0x00000002,
        DoNotFadeIn                                       = 0x00000004, // UNIT_FLAG2_DO_NOT_FADE_IN
        MaskUid                                           = 0x00000008, // CREATURE_TYPE_FLAG_MASK_UID, original description: Non-Unique In Combat Log
        SkinWithEngineering                               = 0x00000010, // CREATURE_TYPE_FLAG_SKIN_WITH_ENGINEERING
        NoAggroOnLeash                                    = 0x00000020,
        NoFriendlyAreaAuras                               = 0x00000040,
        ExtendedCorpseDuration                            = 0x00000080,
        CannotSwim                                        = 0x00000100, // UNIT_FLAG_CANNOT_SWIM
        TameableExotic                                    = 0x00000200, // CREATURE_TYPE_FLAG_TAMEABLE_EXOTIC
        GiganticAoi                                       = 0x00000400, // Since MoP, creatures with that flag have UnitFlags2 0x400000
        InfiniteAoi                                       = 0x00000800, // Since MoP, creatures with that flag have UnitFlags2 0x40000000
        CannotPenetrateWater                              = 0x00001000, // Waterwalking
        NoNamePlate                                       = 0x00002000, // CREATURE_TYPE_FLAG_NO_NAME_PLATE
        ChecksLiquids                                     = 0x00004000,
        NoThreatFeedback                                  = 0x00008000,
        UseModelCollisionSize                             = 0x00010000, // CREATURE_TYPE_FLAG_USE_MODEL_COLLISION_SIZE
        AttackerIgnoresFacing                             = 0x00020000, // In 3.3.5 used only by Rocket Propelled Warhead
        AllowInteractionWhileInCombat                     = 0x00040000, // CREATURE_TYPE_FLAG_ALLOW_INTERACTION_WHILE_IN_COMBAT
        SpellClickForPartyOnly                            = 0x00080000,
        FactionLeader                                     = 0x00100000,
        ImmuneToPlayerBuffs                               = 0x00200000,
        CollideWithMissiles                               = 0x00400000, // CREATURE_TYPE_FLAG_COLLIDE_WITH_MISSILES
        CanBeMultitapped                                  = 0x00800000, // Original description: Do Not Tap (Credit to threat list)
        DoNotPlayMountedAnimations                        = 0x01000000, // CREATURE_TYPE_FLAG_DO_NOT_PLAY_MOUNTED_ANIMATIONS, original description: Disable Dodge, Parry and Block Animations
        CannotTurn                                        = 0x02000000, // UNIT_FLAG2_CANNOT_TURN
        EnemyCheckIgnoresLos                              = 0x04000000,
        ForeverCorpseDuration                             = 0x08000000, // 7 days
        PetsAttackWith3DPathing                           = 0x10000000, // "Pets attack with 3d pathing (Kologarn)"
        LinkAll                                           = 0x20000000, // CREATURE_TYPE_FLAG_LINK_ALL
        AiCanAutoTakeoffInCombat                          = 0x40000000,
        AiCanAutoLandInCombat                             = 0x80000000
    }

    [Flags]
    public enum CreatureStaticFlags4 : uint
    {
        NoBirthAnim                                       = 0x00000001, // SMSG_UPDATE_OBJECT's "NoBirthAnim"
        TreatAsPlayerForDiminishingReturns                = 0x00000002, // Primarily used by ToC champions
        TreatAsPlayerForPvpDebuffDuration                 = 0x00000004, // Primarily used by ToC champions
        InteractOnlyWithCreator                           = 0x00000008, // CREATURE_TYPE_FLAG_INTERACT_ONLY_WITH_CREATOR, original description: Only Display Gossip for Summoner
        DoNotPlayUnitEventSounds                          = 0x00000010, // CREATURE_TYPE_FLAG_DO_NOT_PLAY_UNIT_EVENT_SOUNDS, original description: No Death Scream
        HasNoShadowBlob                                   = 0x00000020, // CREATURE_TYPE_FLAG_HAS_NO_SHADOW_BLOB, original description(wrongly linked type flag or behavior was changed?): Can be Healed by Enemies
        DealsTripleDamageToPcControlledPets               = 0x00000040,
        NoNpcDamageBelow85Ptc                             = 0x00000080,
        ObeysTauntDiminishingReturns                      = 0x00000100, // CREATURE_FLAG_EXTRA_OBEYS_TAUNT_DIMINISHING_RETURNS
        NoMeleeApproach                                   = 0x00000200, // "No Melee (Approach)" Prevents melee (chases into melee range, does not make creature passive)
        UpdateCreatureRecordWhenInstanceChangesDifficulty = 0x00000400, // Used only by Snobold Vassal
        CannotDaze                                        = 0x00000800, // "Cannot Daze (Combat Stun)"
        FlatHonorAward                                    = 0x00001000,
        IgnoreLosWhenCastingOnMe                          = 0x00002000, // "Other objects can ignore line of sight requirements when casting spells on me", used only by Ice Tomb in 3.3.5
        GiveQuestKillCreditWhileOffline                   = 0x00004000,
        TreatAsRaidUnitForHelpfulSpells                   = 0x00008000, // CREATURE_TYPE_FLAG_TREAT_AS_RAID_UNIT, "Treat as Raid Unit For Helpful Spells (Instances ONLY)", used by Valithria Dreamwalker
        DontRepositionIfMeleeTargetIsTooClose             = 0x00010000, // "Don't reposition because melee target is too close"
        PetOrGuardianAiDontGoBehindTarget                 = 0x00020000,
        _5MinuteLootRollTimer                             = 0x00040000, // Used by Lich King
        ForceGossip                                       = 0x00080000, // CREATURE_TYPE_FLAG_FORCE_GOSSIP
        DontRepositionWithFriendsInCombat                 = 0x00100000,
        DoNotSheathe                                      = 0x00200000, // CREATURE_TYPE_FLAG_DO_NOT_SHEATHE, original description: Manual Sheathing control
        IgnoreSpellMinRangeRestrictions                   = 0x00400000, // UnitFlags2 0x8000000, original description: Attacker Ignores Minimum Ranges
        SuppressInstanceWideReleaseInCombat               = 0x00800000,
        PreventSwim                                       = 0x01000000, // UnitFlags2 0x1000000, original description: AI will only swim if target swims
        HideInCombatLog                                   = 0x02000000, // UnitFlags2 0x2000000, original description: Don't generate combat log when engaged with NPC's
        AllowNpcCombatWhileUninteractible                 = 0x04000000,
        PreferNpcsWhenSearchingForEnemies                 = 0x08000000,
        OnlyGenerateInitialThreat                         = 0x10000000,
        DoNotTargetOnInteraction                          = 0x20000000, // CREATURE_TYPE_FLAG_DO_NOT_TARGET_ON_INTERACTION, original description: Doesn't change target on right click
        DoNotRenderObjectName                             = 0x40000000, // CREATURE_TYPE_FLAG_DO_NOT_RENDER_OBJECT_NAME, original description: Hide name in world frame
        QuestBoss                                         = 0x80000000  // CREATURE_TYPE_FLAG_QUEST_BOSS
    }

    [Flags]
    public enum CreatureStaticFlags5 : uint
    {
        UntargetableByClient                              = 0x00000001, // UnitFlags2 0x4000000 UNIT_FLAG2_UNTARGETABLE_BY_CLIENT
        ForceSelfMounting                                 = 0x00000002,
        UninteractibleIfHostile                           = 0x00000004, // UnitFlags2 0x10000000
        DisablesXpAward                                   = 0x00000008,
        DisableAiPrediction                               = 0x00000010,
        NoLeavecombatStateRestore                         = 0x00000020,
        BypassInteractInterrupts                          = 0x00000040,
        _240DegreeBackArc                                 = 0x00000080,
        InteractWhileHostile                              = 0x00000100, // UnitFlags2 0x4000 UNIT_FLAG2_INTERACT_WHILE_HOSTILE
        DontDismissOnFlyingMount                          = 0x00000200,
        PredictivePowerRegen                              = 0x00000400, // CREATURE_TYPEFLAGS_2_UNK1
        HideLevelInfoInTooltip                            = 0x00000800, // CREATURE_TYPEFLAGS_2_UNK2
        HideHealthBarUnderTooltip                         = 0x00001000, // CREATURE_TYPEFLAGS_2_UNK3
        SuppressHighlightWhenTargetedOrMousedOver         = 0x00002000, // UnitFlags2 0x80000
        AiPreferPathableTargets                           = 0x00004000,
        FrequentAreaTriggerChecks                         = 0x00008000,
        AssignKillCreditToEncounterList                   = 0x00010000,
        NeverEvade                                        = 0x00020000,
        AiCantPathOnSteepSlopes                           = 0x00040000,
        AiIgnoreLosToMeleeTarget                          = 0x00080000,
        NoTextInChatBubble                                = 0x00100000, // "Never display emote or chat text in a chat bubble", CREATURE_TYPEFLAGS_2_UNK4
        CloseInOnUnpathableTarget                         = 0x00200000, // "AI Pets close in on unpathable target"
        DontGoBehindMe                                    = 0x00400000, // "Pet/Guardian AI Don't Go Behind Me (use on target)"
        NoDeathThud                                       = 0x00800000, // CREATURE_TYPEFLAGS_2_UNK5
        ClientLocalCreature                               = 0x01000000,
        CanDropLootWhileInAChallengeModeInstance          = 0x02000000,
        HasSafeLocation                                   = 0x04000000,
        NoHealthRegen                                     = 0x08000000,
        NoPowerRegen                                      = 0x10000000,
        NoPetUnitFrame                                    = 0x20000000,
        NoInteractOnLeftClick                             = 0x40000000, // CREATURE_TYPEFLAGS_2_UNK6
        GiveCriteriaKillCreditWhenCharmed                 = 0x80000000
    }

    [Flags]
    public enum CreatureStaticFlags6 : uint
    {
        DoNotAutoResummon                                 = 0x00000001, // "Do not auto-resummon this companion creature"
        ReplaceVisibleUnitIfAvailable                     = 0x00000002, // "Smooth Phasing: Replace visible unit if available"
        IgnoreRealmCoalescingHidingCode                   = 0x00000004, // "Ignore the realm coalescing hiding code (always show)"
        TapsToFaction                                     = 0x00000008,
        OnlyQuestgiverForSummoner                         = 0x00000010,
        AiCombatReturnPrecise                             = 0x00000020,
        HomeRealmOnlyLoot                                 = 0x00000040,
        NoInteractResponse                                = 0x00000080, // TFLAG2_UNK7
        NoInitialPower                                    = 0x00000100,
        DontCancelChannelOnMasterMounting                 = 0x00000200,
        CanToggleBetweenDeathAndPersonalLoot              = 0x00000400,
        AlwaysStandOnTopOfTarget                          = 0x00000800, // "Always, ALWAYS tries to stand right on top of his move to target. ALWAYS!!", toggleable by 'Set "Always Stand on Target" flag for unit(s)' or not same?
        UnconsciousOnDeath                                = 0x00001000,
        DontReportToLocalDefenseChannelOnDeath            = 0x00002000,
        PreferUnengagedMonsters                           = 0x00004000, // "Prefer unengaged monsters when picking a target"
        UsePvpPowerAndResilience                          = 0x00008000, // "Use PVP power and resilience when players attack this creature"
        DontClearDebuffsOnLeaveCombat                     = 0x00010000,
        PersonalLootHasFullSecurity                       = 0x00020000, // "Personal loot has full security (guaranteed push/mail delivery)"
        TripleSpellVisuals                                = 0x00040000,
        UseGarrisonOwnerLevel                             = 0x00080000,
        ImmediateAoiUpdateOnSpawn                         = 0x00100000,
        UiCanGetPosition                                  = 0x00200000,
        SeamlessTransferProhibited                        = 0x00400000,
        AlwaysUseGroupLootMethod                          = 0x00800000,
        NoBossKillBanner                                  = 0x01000000,
        ForceTriggeringPlayerLootOnly                     = 0x02000000,
        ShowBossFrameWhileUninteractable                  = 0x04000000,
        ScalesToPlayerLevel                               = 0x08000000,
        AiDontLeaveMeleeForRangedWhenTargetGetsRooted     = 0x10000000,
        DontUseCombatReachForChaining                     = 0x20000000,
        DoNotPlayProceduralWoundAnim                      = 0x40000000,
        ApplyProceduralWoundAnimToBase                    = 0x80000000  // TFLAG2_UNK14
    }

    [Flags]
    public enum CreatureStaticFlags7 : uint
    {
        ImportantNpc                                      = 0x00000001,
        ImportantQuestNpc                                 = 0x00000002,
        LargeNameplate                                    = 0x00000004,
        TrivialPet                                        = 0x00000008,
        AiEnemiesDontBackupWhenIGetRooted                 = 0x00000010,
        NoAutomaticCombatAnchor                           = 0x00000020,
        OnlyTargetableByCreator                           = 0x00000040,
        TreatAsPlayerForIsplayercontrolled                = 0x00000080,
        GenerateNoThreatOrDamage                          = 0x00000100,
        InteractOnlyOnQuest                               = 0x00000200,
        DisableKillCreditForOfflinePlayers                = 0x00000400,
        AiAdditionalPathing                               = 0x00080000,
    }

    [Flags]
    public enum CreatureStaticFlags8 : uint
    {
        ForceCloseInOnPathFailBehavior                    = 0x00000002,
        Use2DChasingCalculation                           = 0x00000020,
        UseFastClassicHeartbeat                           = 0x00000040,
    }
}
