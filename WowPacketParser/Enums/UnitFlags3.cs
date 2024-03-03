using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UnitFlags3 : uint // 7.x
    {
        None                                   = 0x00000000,
        Unk0                                   = 0x00000001,
        UnconsciousOnDeath                     = 0x00000002,
        AllowMountedCombat                     = 0x00000004,
        GarrisonPet                            = 0x00000008,
        UiCanGetPosition                       = 0x00000010,
        AiObstacle                             = 0x00000020,
        AlternativeDefaultLanguage             = 0x00000040,
        SuppressAllNpcFeedback                 = 0x00000080,
        IgnoreCombat                           = 0x00000100,
        SuppressNpcFeedback                    = 0x00000200,
        Unk10                                  = 0x00000400,
        Unk11                                  = 0x00000800,
        Unk12                                  = 0x00001000,
        FakeDead                               = 0x00002000,
        NoFacingOnInteractAndFastFacingChase   = 0x00004000,
        UntargeteableFromUi                    = 0x00008000,
        NoFacingOnInteracthileFakeDead         = 0x00010000,
        AlreadySkinned                         = 0x00020000,
        SuppressAllNpcSounds                   = 0x00040000,
        SuppressNpcSounds                      = 0x00080000,
        AllowInteractionWhileInCombat          = 0x00100000,
        Unk21                                  = 0x00200000,
        DontFadeOut                            = 0x00400000,
        Unk23                                  = 0x00800000,
        ForceHideNameplate                     = 0x01000000,
        Unk25                                  = 0x02000000,
        Unk26                                  = 0x04000000,
        Unk27                                  = 0x08000000,
        Unk28                                  = 0x10000000,
        Unk29                                  = 0x20000000,
        Unk30                                  = 0x40000000,
        Unk31                                  = 0x80000000,

        Disallowed                             = (IgnoreCombat | FakeDead | AlreadySkinned | AllowInteractionWhileInCombat)
    }
}
