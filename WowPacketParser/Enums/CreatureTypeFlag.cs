using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum CreatureTypeFlag : uint
    {
        None = 0x00000000,
        TameablePet = 0x00000001, // 4.x name
        GhostVisible = 0x00000002,
        BossMob = 0x00000004, // 4.x name
        DoNotPlayWoundParryAnimation = 0x00000008, // 4.x
        HideFactionTooltip = 0x00000010, // 4.x
        Unknown4 = 0x00000020, // sound related
        SpellAttackable = 0x00000040,
        CanInteractWhileDead = 0x00000080, // 4.x name
        HerbSkinningSkill = 0x00000100, // 4.x name
        MiningSkinningSkill = 0x00000200, // 4.x name
        DoNotLogDeath = 0x00000400, // 4.x
        MountedCombatAllowed = 0x000800, // 4.x name
        CanAssist = 0x00001000,
        IsPetBarUsed = 0x00002000, // 4.x name
        MaskUID = 0x00004000, // 4.x
        EngineeringSkinningSkill = 0x00008000, // 4.x name
        ExoticPet = 0x00010000, // 4.x name
        UseDefaultCollisionBox = 0x00020000, // 4.x
        IsSiegeWeapon = 0x00040000,
        DoesNotCollideWithMissiles = 0x00080000, // 4.x
        HideNamePlate = 0x00100000, // 4.x
        DoNotPlayMountedAnimations = 0x00200000, // 4.x
        IsLinkAll = 0x00400000, // 4.x
        InteractOnlyWithCreator = 0x00800000, // 4.x
        DoNotPlayUnitEventSounds = 0x01000000, // 4.x
        HasNoShadowBlob = 0x02000000, // 4.x
        TreatAsRaidUnit = 0x04000000, // 4.x
        ForceGossip = 0x08000000, // 4.x
        DoNotSheathe = 0x10000000, // 4.x
        DoNotTargetOnInteraction = 0x20000000, // 4.x
        DoNotRenderObjectName = 0x40000000, // 4.x
        Unknown = 0x80000000,
    }
}
