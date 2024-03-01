using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UnitFlags : uint
    {
        None                             = 0x00000000,
        ServerControlled                 = 0x00000001,
        Spawning                         = 0x00000002,
        RemoveClientControl              = 0x00000004,
        PlayerControlled                 = 0x00000008,
        Rename                           = 0x00000010,
        Preparation                      = 0x00000020,
        Unk6                             = 0x00000040,
        NotAttackable                    = 0x00000080,
        ImmunePC                         = 0x00000100,
        ImmuneNPC                        = 0x00000200,
        Looting                          = 0x00000400,
        PetIsAttackingTarget             = 0x00000800,
        PVP                              = 0x00001000,
        Silenced                         = 0x00002000, // ForceNameplate (9.0)
        CantSwim                         = 0x00004000,
        CanSwim                          = 0x00008000,
        NotAttackable2                   = 0x00010000,
        Pacified                         = 0x00020000,
        Stunned                          = 0x00040000,
        AffectingCombat                  = 0x00080000,
        OnTaxi                           = 0x00100000,
        Disarmed                         = 0x00200000,
        Confused                         = 0x00400000,
        Fleeing                          = 0x00800000,
        Possessed                        = 0x01000000,
        Uninteractible                   = 0x02000000,
        Skinnable                        = 0x04000000,
        Mount                            = 0x08000000,
        PreventKneelingWhenLooting       = 0x10000000,
        PreventEmotes                    = 0x20000000,
        Sheath                           = 0x40000000,
        Immune                           = 0x80000000,

        IsInCombat                       = PetIsAttackingTarget | AffectingCombat,

        Disallowed                       = (ServerControlled | NotAttackable | RemoveClientControl |
                                            PlayerControlled | Rename | Preparation | /* Unk6 | */
                                            NotAttackable | Looting | PetIsAttackingTarget | PVP |
                                            Silenced | CantSwim | CanSwim | NotAttackable2 | Pacified | Stunned |
                                            AffectingCombat | OnTaxi | Disarmed | Confused | Fleeing |
                                            Possessed | Skinnable | Mount | PreventKneelingWhenLooting |
                                            PreventEmotes | Sheath | Immune),

        DisallowedShadowlands            = Disallowed & ~Silenced /* ForceNameplate */
    }
}
