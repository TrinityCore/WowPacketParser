using System;

namespace WowPacketParser.Enums
{
    [Flags]
    enum VehicleFlags // 4.x
    {
        NoStrafe          = 0x00000001,
        NoJumping         = 0x00000002,
        FullSpeedTurning  = 0x00000004,

        AllowPitching     = 0x00000010,
        FullSpeedPitching = 0x00000020,
        CustomPitch       = 0x00000040,
        AdjustAimAngle    = 0x00000400,
        AdjustAimPower    = 0x00000800
    }

    [Flags]
    enum VehicleSeatFlags : uint
    {
        HasLowerAnimForEnter                             = 0x00000001,
        HasLowerAnimForRide                              = 0x00000002,

        Unknown1                                         = 0x00000004,

        ShouldUseVehicleSeatExitAnimationOnVoluntaryExit = 0x00000008,

        Unknown2                                         = 0x00000020,
        Unknown3                                         = 0x00000040,
        Unknown4                                         = 0x00000080,
        Unknown5                                         = 0x00000100,

        HidePassanger                                    = 0x00000200,
        AllowsTurning                                    = 0x00000400,
        CanControl                                       = 0x00000800,

        Unknown6                                         = 0x00001000,

        Uncontrolled                                     = 0x00002000,
        CanAttack                                        = 0x00004000,
        ShouldUseVehicleSeatExitAnimationOnForcedExit    = 0x00008000,

        Unknown7                                         = 0x00010000,
        Unknown8                                         = 0x00020000,

        HasVehicleExitAnimForVoluntaryExit               = 0x00040000,
        HasVehicleExitAnimForForcedExit                  = 0x00080000,

        Unknown9                                         = 0x00100000,
        Unknown10                                        = 0x00200000,

        RecHasVehicleEnterAnim                           = 0x00400000,

        Unknown11                                        = 0x00800000,

        EnableVehicleZoom                                = 0x01000000,
        CanEnterOrExit                                   = 0x02000000,
        CanSwitchFromSeat                                = 0x04000000,
        HasStartWaitingForVehicleTransitionAnimEnter     = 0x08000000,
        HasStartWaitingForVehicleTransitionAnimExit      = 0x10000000,
        HasVehicleUI                                     = 0x20000000,
        AllowsInteraction                                = 0x80000000
    }

    [Flags]
    enum VehicleSeatFlagsB : uint
    {
        None                                                = 0x00000000,
        Unknown1                                            = 0x00000001,
        UsableForced                                        = 0x00000002,
        Unknown2                                            = 0x00000004,
        TargetsInRaidUI                                     = 0x00000008,
        Unknown3                                            = 0x00000010,
        Ejectable                                           = 0x00000020,
        UsableForced2                                       = 0x00000040,
        Unknown4                                            = 0x00000080,
        UsableForced3                                       = 0x00000100,
        CanSwitch                                           = 0x04000000,

        VehiclePlayerFrameUI                                = 0x80000000
    }
}
