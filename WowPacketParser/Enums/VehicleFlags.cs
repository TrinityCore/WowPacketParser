using System;

namespace WowPacketParser.Enums
{
    [Flags]
    enum VehicleFlags // 4.x
    {
        NoStrafe = 0x00000001,
        NoJumping = 0x00000002,
        FullSpeedTurning = 0x00000004,

        AllowPitching = 0x00000010,
        FullSpeedPitching = 0x00000020,
        CustomPitch = 0x00000040,
        AdjustAimAngle = 0x00000400,
        AdjustAimPower = 0x00000800,
    }

    [Flags]
    enum VehicleSeatFlags // 4.x
    {
        HasLowerAnimForEnter = 0x1,
        HasLowerAnimForRide = 0x2,

        ShouldUseVehicleSeatExitAnimationOnVoluntaryExit = 0x8,

        HidePassanger = 0x200,
        AllowsTurning = 0x400,
        CanControl = 0x800,
        Uncontrolled = 0x2000,
        CanAttack = 0x4000,
        ShouldUseVehicleSeatExitAnimationOnForcedExit = 0x8000,
        Unk1 = 0x20000,
        HasVehicleExitAnimForVoluntaryExit = 0x40000,
        HasVehicleExitAnimForForcedExit = 0x80000,
        Unk2 = 0x200000,
        RecHasVehicleEnterAnim = 0x400000,
        EnableVehicleZoom = 0x1000000,
        CanEnterOrExit = 0x2000000,
        CanSwitchFromSeat = 0x4000000,
        HasStartWaitingForVehicleTransitionAnimEnter = 0x8000000,
        HasStartWaitingForVehicleTransitionAnimExit = 0x10000000,
        HasVehicleUI = 0x20000000,
        // AllowsInteraction = 0x80000000
    }

    [Flags]
    enum VehicleSeatFlagsB // 4.x
    {
        None = 0x00000000,
        UsableForced = 0x00000002,
        TargetsInRaidUI = 0x00000008,
        Ejectable = 0x00000020,
        UsableForced2 = 0x00000040,
        UsableForced3 = 0x00000100,

        CanSwitch = 0x04000000,

        // VehiclePlayerFrameUI = 0x80000000,
    }
}
