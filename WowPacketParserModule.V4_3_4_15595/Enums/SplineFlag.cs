using System;

namespace WowPacketParserModule.V4_3_4_15595.Enums
{
    [Flags]
    public enum SplineFlag : uint
    {
        None                = 0x00000000,
        Unknown0            = 0x00000008,           // NOT VERIFIED
        FallingSlow         = 0x00000010,
        Done                = 0x00000020,
        Falling             = 0x00000040,           // Affects elevation computation, can't be combined with Parabolic flag
        No_Spline           = 0x00000080,
        Unknown2            = 0x00000100,           // NOT VERIFIED
        Flying              = 0x00000200,           // Smooth movement(Catmullrom interpolation mode), flying animation
        OrientationFixed    = 0x00000400,           // Model orientation fixed
        Catmullrom          = 0x00000800,           // Used Catmullrom interpolation mode
        Cyclic              = 0x00001000,           // Movement by cycled spline
        Enter_Cycle         = 0x00002000,           // Everytimes appears with cyclic flag in monster move packet, erases first spline vertex after first cycle done
        Frozen              = 0x00004000,           // Will never arrive
        TransportEnter      = 0x00008000,
        TransportExit       = 0x00010000,
        Unknown3            = 0x00020000,           // NOT VERIFIED
        Unknown4            = 0x00040000,           // NOT VERIFIED
        OrientationInversed = 0x00080000,
        SmoothGroundPath    = 0x00100000,
        Walkmode            = 0x00200000,
        UncompressedPath    = 0x00400000,
        Unknown6            = 0x00800000,           // NOT VERIFIED
        Animation           = 0x01000000,           // Plays animation after some time passed
        Parabolic           = 0x02000000,           // Affects elevation computation, can't be combined with Falling flag
        Final_Point         = 0x04000000,
        Final_Target        = 0x08000000,
        Final_Angle         = 0x10000000,
        Unknown7            = 0x20000000,           // NOT VERIFIED
        Unknown8            = 0x40000000,           // NOT VERIFIED
        Unknown9            = 0x80000000           // NOT VERIFIED
    }
}
