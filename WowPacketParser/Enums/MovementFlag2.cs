using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum MovementFlag2 : uint
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

    namespace v4
    {
        [Flags]
        public enum MovementFlag2 : uint
        {
            None                                = 0x0000,
            PreventStrafe                       = 0x0001, // 4.x
            PreventJumping                      = 0x0002, // 4.x
            FullSpeedTurning                    = 0x0004,
            FullSpeedPitching                   = 0x0008,
            AlwaysAllowPitching                 = 0x0010,
            IsVehicleExitVoluntary              = 0x0020, // 4.x
            IsJumpSplineInAir                   = 0x0040, // 4.x
            IsAnimTierInTrans                   = 0x0080, // 4.x
            PreventChangePitch                  = 0x0100, // 4.x
            VehiclePassengerIsTransitionAllowed = 0x0200, // 4.x might be wrong
            CanTransitionBetweenSwimAndFly      = 0x0400, // 4.x
            Unknown10                           = 0x0800, // might be wrong
            InterpolateMove                     = 0x2000, // 4.x (Interpolation is player only)
            InterpolateTurning                  = 0x4000,
            InterpolatePitching                 = 0x8000
        }
    }

    namespace v7
    {
        [Flags]
        public enum MovementFlag2 : uint
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

    namespace v9
    {
        [Flags]
        public enum MovementFlag2 : uint
        {
            None                                = 0x00000000,
            NoStrafe                            = 0x00000001,
            NoJumping                           = 0x00000002,
            FullSpeedTurning                    = 0x00000004,
            FullSpeedPitching                   = 0x00000008,
            AlwaysAllowPitching                 = 0x00000010,
            IsVehicleExitVoluntary              = 0x00000020,
            WaterwalkingFullPitch               = 0x00000040,
            VehiclePassengerIsTransitionAllowed = 0x00000080,
            CanSwimToFlyTrans                   = 0x00000100,
            CanTurnWhileFalling                 = 0x00000400,
            IgnoreMovementForces                = 0x00000800,
            CanDoubleJump                       = 0x00001000,
            DoubleJump                          = 0x00002000,
            AwaitingLoad                        = 0x00010000,
            InterpolatedMovement                = 0x00020000,
            InterpolatedTurning                 = 0x00040000,
            InterpolatedPitching                = 0x00080000
        }
    }
}
