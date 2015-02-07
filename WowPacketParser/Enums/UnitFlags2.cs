using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UnitFlags2 // 4.x
    {
        None                             = 0x0,
        AppearDead                       = 0x1,
        Unk1                             = 0x2,
        IgnoreReputation                 = 0x4, // ?
        ComprehendLanguages              = 0x8,
        MirrorImage                      = 0x10,
        DoNotFadeIn                      = 0x20,
        ForceMovement                    = 0x40,
        OffHandDisarmed                  = 0x80,
        DisablePredictedStats            = 0x100,
        RangedDisarm                     = 0x400,
        RegeneratePower                  = 0x800,
        RestrictInteractionToPartyOrRaid = 0x1000,
        PreventSpellClick                = 0x2000,
        CanInteractEvenIfHostile         = 0x4000,
        CannotTurn                       = 0x8000,
        Unk2                             = 0x10000,
        PlayDeathAnimationOnDeath        = 0x20000,
        AllowCastingCheatSpells          = 0x40000,

        NoActions                        = 0x800000,
        SwimPrevent                      = 0x1000000,
        HideInCombatLog                  = 0x2000000,
        CannotSwitchTargets              = 0x4000000,
        IgnoreSpellMinRangeRestrictions  = 0x8000000,
        Unk3                             = 0x10000000
    }
}
