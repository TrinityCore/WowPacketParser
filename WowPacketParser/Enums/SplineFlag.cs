using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum SplineFlag : uint
    {
        None                = 0x00000000,
        Done                = 0x00000100,
        Falling             = 0x00000200,
        NoSpline            = 0x00000400,
        Parabolic           = 0x00000800,
        CanSwim             = 0x00001000,
        Flying              = 0x00002000,
        OrientationFixed    = 0x00004000,
        FinalPoint          = 0x00008000,
        FinalTarget         = 0x00010000,
        FinalAngle          = 0x00020000,
        CatmullRom          = 0x00040000,
        Cyclic              = 0x00080000,
        EnterCycle          = 0x00100000,
        Animation           = 0x00200000,
        Frozen              = 0x00400000,
        Transport           = 0x00800000,
        TransportExit       = 0x01000000,
        Unknown7            = 0x02000000,
        Unknown8            = 0x04000000,
        Backward            = 0x08000000,
        Unknown10           = 0x10000000,
        Unknown11           = 0x20000000,
        Unknown12           = 0x40000000,
        Unknown13           = 0x80000000
    }
}
