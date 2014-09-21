using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum MovementFlagExtra : ushort
    {
        None                                = 0x0000,
        PreventStrafe                       = 0x0001, // 4.x
        PreventJumping                      = 0x0002, // 4.x
        DisableCollision                    = 0x0004, // 4.x
        FullSpeedTurning                    = 0x0008,
        FullSpeedPitching                   = 0x0010,
        AlwaysAllowPitching                 = 0x0020,
        IsVehicleExitVoluntary              = 0x0040, // 4.x
        IsJumpSplineInAir                   = 0x0080, // 4.x
        IsAnimTierInTrans                   = 0x0100, // 4.x
        PreventChangePitch                  = 0x0200, // 4.x
        InterpolateMove                     = 0x0400, // 4.x (Interpolation is player only)
        InterpolateTurning                  = 0x0800,
        InterpolatePitching                 = 0x1000,
        VehiclePassengerIsTransitionAllowed = 0x2000, // 4.x
        CanTransitionBetweenSwimAndFly      = 0x4000, // 4.x
        Unknown10                           = 0x8000
    }
}
