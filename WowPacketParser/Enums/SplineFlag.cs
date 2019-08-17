using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum SplineFlag : uint
    {
        None                = 0x00000000,
        AnimTierSwim        = 0x00000001,
        AnimTierHover       = 0x00000002,
        AnimTierFly         = 0x00000003,
        AnimTierSubmerged   = 0x00000004,
        Done                = 0x00000100,
        Falling             = 0x00000200,
        NoSpline            = 0x00000400,
        Trajectory          = 0x00000800,
        WalkMode            = 0x00001000,
        Flying              = 0x00002000,
        Knockback           = 0x00004000,
        FinalPoint          = 0x00008000,
        FinalTarget         = 0x00010000,
        FinalOrientation    = 0x00020000,
        CatmullRom          = 0x00040000,
        Cyclic              = 0x00080000,
        EnterCicle          = 0x00100000,
        AnimationTier       = 0x00200000,
        Frozen              = 0x00400000,
        Transport           = 0x00800000,
        TransportExit       = 0x01000000,
        Unknown7            = 0x02000000,
        Unknown8            = 0x04000000,
        OrientationInverted = 0x08000000,
        UsePathSmoothing    = 0x10000000,
        Animation           = 0x20000000,
        UncompressedPath    = 0x40000000,
        Unknown10           = 0x80000000
    }
}
