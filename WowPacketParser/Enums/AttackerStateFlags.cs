using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum AttackerStateFlags
    {
        None                     = 0,
        Unk1                     = 0x1,
        PlayWoundAnimation       = 0x2,
        OffHand                  = 0x4,
        Miss                     = 0x10,
        AbsorbType1              = 0x20,
        AbsorbType2              = 0x40,
        ResistType1              = 0x80,
        ResistType2              = 0x100,
        Critical                 = 0x200,

        Block                    = 0x2000,
        HideWorldTextForNoDamage = 0x4000,
        BloodSpurtInBack         = 0x8000,
        Glancing                 = 0x10000,
        Crushing                 = 0x20000,
        Ignore                   = 0x40000,
        ModifyPredictedPower     = 0x800000,
        ForceShowBloodSpurt      = 0x1000000
    }
}
