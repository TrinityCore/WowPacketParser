using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum MovementFlag : uint
    {
        None               = 0x00000000,
        Forward            = 0x00000001,
        Backward           = 0x00000002,
        StrafeLeft         = 0x00000004,
        StrafeRight        = 0x00000008,
        TurnLeft           = 0x00000010,
        TurnRight          = 0x00000020,
        PitchUp            = 0x00000040,
        PitchDown          = 0x00000080,
        WalkMode           = 0x00000100,
        OnTransport        = 0x00000200,
        DisableGravity     = 0x00000400,
        Root               = 0x00000800,
        Falling            = 0x00001000,
        FallingFar         = 0x00002000,
        PendingStop        = 0x00004000,
        PendingStrafeStop  = 0x00008000,
        PendingForward     = 0x00010000,
        PendingBackward    = 0x00020000,
        PendingStrafeLeft  = 0x00040000,
        PendingStrafeRight = 0x00080000,
        PendingRoot        = 0x00100000,
        Swimming           = 0x00200000,
        Ascending          = 0x00400000,
        Descending         = 0x00800000,
        CanFly             = 0x01000000,
        Flying             = 0x02000000,
        SplineElevation    = 0x04000000,
        SplineEnabled      = 0x08000000,
        Waterwalking       = 0x10000000,
        CanSafeFall        = 0x20000000,
        Hover              = 0x40000000,
        LocalDirty         = 0x80000000
    }
}
