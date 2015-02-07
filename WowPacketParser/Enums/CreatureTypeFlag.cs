using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum CreatureTypeFlag : uint
    {
        None                         = 0x00000000,
        TameablePet                  = 0x00000001,
        GhostVisible                 = 0x00000002,
        BossMob                      = 0x00000004,
        DoNotPlayWoundParryAnimation = 0x00000008,
        HideFactionTooltip           = 0x00000010,
        Unknown4                     = 0x00000020, // sound related
        SpellAttackable              = 0x00000040,
        CanInteractWhileDead         = 0x00000080,
        HerbSkinningSkill            = 0x00000100,
        MiningSkinningSkill          = 0x00000200,
        DoNotLogDeath                = 0x00000400,
        MountedCombatAllowed         = 0x00000800,
        CanAssist                    = 0x00001000,
        IsPetBarUsed                 = 0x00002000,
        MaskUID                      = 0x00004000,
        EngineeringSkinningSkill     = 0x00008000,
        ExoticPet                    = 0x00010000,
        UseDefaultCollisionBox       = 0x00020000,
        IsSiegeWeapon                = 0x00040000,
        DoesNotCollideWithMissiles   = 0x00080000,
        HideNamePlate                = 0x00100000,
        DoNotPlayMountedAnimations   = 0x00200000,
        IsLinkAll                    = 0x00400000,
        InteractOnlyWithCreator      = 0x00800000,
        DoNotPlayUnitEventSounds     = 0x01000000,
        HasNoShadowBlob              = 0x02000000,
        TreatAsRaidUnit              = 0x04000000,
        ForceGossip                  = 0x08000000,
        DoNotSheathe                 = 0x10000000,
        DoNotTargetOnInteraction     = 0x20000000,
        DoNotRenderObjectName        = 0x40000000,
        UnitIsQuestBoss              = 0x80000000 // not verified
    }
}
