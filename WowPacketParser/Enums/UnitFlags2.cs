using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UnitFlags2 : uint // 4.x
    {
        None                                      = 0x00000000,
        FeignDeath                                = 0x00000001,
        HideBody                                  = 0x00000002,
        IgnoreReputation                          = 0x00000004,
        ComprehendLang                            = 0x00000008,
        MirrorImage                               = 0x00000010,
        InstantlyDontFadeIn                       = 0x00000020,
        ForceMovement                             = 0x00000040,
        DisarmOffhand                             = 0x00000080,
        DisablePredStats                          = 0x00000100,
        AllowChangingTalents                      = 0x00000200,
        DisarmRanged                              = 0x00000400,
        RegeneratePower                           = 0x00000800,
        RestrictPartyInteraction                  = 0x00001000,
        PreventSpellClick                         = 0x00002000,
        InteractWhileHostile                      = 0x00004000,
        CannotTurn                                = 0x00008000,
        Unk2                                      = 0x00010000,
        PlayDeathAnim                             = 0x00020000,
        AllowCheatSpells                          = 0x00040000,
        SuppressHighlightWhenTargetedOrMousedOver = 0x00080000,
        TreatAsRaidUnitForHelpfulSpells           = 0x00100000,
        LargeAOI                                  = 0x00200000,
        GiganticAOI                               = 0x00400000,
        NoActions                                 = 0x00800000,
        AiWillOnlySwimIfTargetSwims               = 0x01000000,
        DontGenerateCombatLogWhenEngagedWithNpcs  = 0x02000000,
        UntargetableByClient                      = 0x04000000,
        AttackerIgnoresMinimumRanges              = 0x08000000,
        UninteractibleIfHostile                   = 0x10000000,
        Unused11                                  = 0x20000000,
        InfiniteAOI                               = 0x40000000,
        Unused13                                  = 0x80000000,

        Disallowed                                = (FeignDeath | IgnoreReputation | ComprehendLang |
                                                     MirrorImage | ForceMovement | DisarmOffhand |
                                                     DisablePredStats | AllowChangingTalents | DisarmRanged |
                                                     /* RegeneratePower | */ RestrictPartyInteraction | CannotTurn |
                                                     PreventSpellClick | /* InteractWhileHostile | */ /* Unk2 | */
                                                     /* PlayDeathAnim | */ AllowCheatSpells | SuppressHighlightWhenTargetedOrMousedOver |
                                                     TreatAsRaidUnitForHelpfulSpells | LargeAOI | GiganticAOI | NoActions |
                                                     AiWillOnlySwimIfTargetSwims | DontGenerateCombatLogWhenEngagedWithNpcs | AttackerIgnoresMinimumRanges |
                                                     UninteractibleIfHostile | Unused11 | InfiniteAOI | Unused13)
    }
}
