using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum MovementFlag3 : uint
    {
        None           = 0x00000000,
        DisableInertia = 0x00000001,
    }
}
