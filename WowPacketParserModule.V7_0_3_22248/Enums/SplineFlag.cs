using System;

namespace WowPacketParserModule.V7_0_3_22248.Enums
{
    [Flags]
    public enum SplineFlag : uint
    {
        None                = 0x00000000,
        AnimTierSwim        = 0x00000001,
        AnimTierHover       = 0x00000002,
        AnimTierFly         = 0x00000003,
        AnimTierSubmerged   = 0x00000004,
        JumpOrientationFixed= 0x00000008,
        FallingSlow         = 0x00000010,
        Done                = 0x00000020,
        Falling             = 0x00000040,
        NoSpline            = 0x00000080,
        Unknown1            = 0x00000100,
        Flying              = 0x00000200,
        OrientationFixed    = 0x00000400,
        Catmullrom          = 0x00000800,
        Cyclic              = 0x00001000,
        EnterCycle          = 0x00002000,
        Turning             = 0x00004000, // Frozen before 11.1.7 (never used so just using new name unconditionally)
        TransportEnter      = 0x00008000,
        TransportExit       = 0x00010000,
        Unknown2            = 0x00020000,
        Unknown3            = 0x00040000,
        Backward            = 0x00080000,
        SmoothGroundPath    = 0x00100000,
        CanSwim             = 0x00200000,
        UncompressedPath    = 0x00400000,
        Unknown4            = 0x00800000,
        FastSteering        = 0x01000000,
        Animation           = 0x02000000,
        Parabolic           = 0x04000000,
        FadeObject          = 0x08000000,
        Steering            = 0x10000000,
        UnlimitedSpeed      = 0x20000000,
        Unknown9            = 0x40000000,
        Unknown10           = 0x80000000,
    }
}
