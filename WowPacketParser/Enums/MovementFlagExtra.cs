using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum MovementFlagExtra
    {
        None = 0x0000,
        Unknown1 = 0x0001,
        Unknown2 = 0x0002,
        Unknown3 = 0x0004,
        FullSpeedTurning = 0x0008,
        FullSpeedPitching = 0x0010,
        AlwaysAllowPitching = 0x0020,
        Unknown4 = 0x0040,
        Unknown5 = 0x0080,
        Unknown6 = 0x0100,
        Unknown7 = 0x0200,
        InterpolatedPlayerMovement = 0x0400,
        InterpolatedPlayerTurning = 0x0800,
        InterpolatedPlayerPitching = 0x1000,
        Unknown8 = 0x2000,
        Unknown9 = 0x4000,
        Unknown10 = 0x8000
    }
}
