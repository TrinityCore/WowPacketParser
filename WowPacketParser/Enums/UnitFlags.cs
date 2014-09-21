using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UnitFlags : uint
    {
        None                             = 0,
        NotClientControlled              = 0x1,
        PlayerCannotAttack               = 0x2,
        RemoveClientControl              = 0x4,
        PlayerControlled                 = 0x8,
        Unk4                             = 0x10,
        Preparation                      = 0x20,
        Unk6                             = 0x40,
        NoAttack                         = 0x80,
        NotAttackbleByPlayerControlled   = 0x100,
        OnlyAttackableByPlayerControlled = 0x200,
        Looting                          = 0x400,
        PetIsAttackingTarget             = 0x800,
        PVP                              = 0x1000,
        Silenced                         = 0x2000,
        CannotSwim                       = 0x4000,
        OnlySwim                         = 0x8000,
        NoAttack2                        = 0x10000,
        Pacified                         = 0x20000,
        Stunned                          = 0x40000,
        AffectingCombat                  = 0x80000,
        OnTaxi                           = 0x100000,
        MainHandDisarmed                 = 0x200000,
        Confused                         = 0x400000,
        Feared                           = 0x800000,
        PossessedByPlayer                = 0x1000000,
        NotSelectable                    = 0x2000000,
        Skinnable                        = 0x4000000,
        Mount                            = 0x8000000,
        PreventKneelingWhenLooting       = 0x10000000,
        PreventEmotes                    = 0x20000000,
        Sheath                           = 0x40000000,
        Unk31                            = 0x80000000,

        IsInCombat                       = PetIsAttackingTarget | AffectingCombat
    }
}
