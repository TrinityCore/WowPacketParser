using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum MovementFlag3 : uint
    {
        None            = 0x00000000,
        DisableInertia  = 0x00000001,
        CanAdvFly       = 0x00000002,
        AdvFlying       = 0x00000004,
        CannotSwim      = 0x00002000,
        CanDrive        = 0x00004000,
        DrivingForward  = 0x00008000,
        DrivingBackward = 0x00010000,
    }
}
