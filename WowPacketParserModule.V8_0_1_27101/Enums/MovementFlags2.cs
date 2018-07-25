using System;

namespace WowPacketParserModule.V8_0_1_27101.Enums
{
    [Flags]
    public enum MovementFlags2 : uint
    {
        None                                = 0x00000000,
        NoStrafe                            = 0x00000001,
        NoJumping                           = 0x00000002,
        FullSpeedTurning                    = 0x00000004,
        FullSpeedPitching                   = 0x00000008,
        AlwaysAllowPitching                 = 0x00000010,
        IsVehicleExitVoluntary              = 0x00000020,
        JumpSplineInAir                     = 0x00000040,
        AnimTierInTrans                     = 0x00000080,
        WaterwalkingFullPitch               = 0x00000100,
        VehiclePassengerIsTransitionAllowed = 0x00000200,
        CanSwimToFlyTrans                   = 0x00000400,
        Unk11                               = 0x00000800,
        CanTurnWhileFalling                 = 0x00001000,
        Unk13                               = 0x00002000,
        IgnoreMovementForces                = 0x00004000,
        Unk15                               = 0x00008000,
        CanDoubleJump                       = 0x00010000,
        DoubleJump                          = 0x00020000,
        Unk18                               = 0x00040000,
        Unk19                               = 0x00080000,
        InterpolatedMovement                = 0x00100000,
        InterpolatedTurning                 = 0x00200000,
        InterpolatedPitching                = 0x00400000
    }
}
