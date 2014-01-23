using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum SplineFlag542 : uint
    {
        None = 0x00000000,
        unk0 = 0x00000008,
        FallingSlow = 0x00000010,
        Done = 0x00000020,
        Falling = 0x00000040,
        NoSpline = 0x00000080,
        unk1 = 0x00000100,
        Flying = 0x00000200,
        FixedOrientation = 0x00000400,
        CatmullRom = 0x00000800,
        Cyclic = 0x00001000,
        EnterCicle = 0x00002000,
        Frozen = 0x00004000,
        TansportEnter = 0x00008000,
        TransportExit = 0x00010000,
        unk2 = 0x00020000,
        unk3 = 0x00040000,
        MovingBackwards = 0x00080000,
        UsePathSmoothing = 0x00100000,
        WalkMode = 0x00200000,
        UncompressedPath = 0x00400000,
        unk4 = 0x00800000,
        AnimationTier = 0x01000000,
        Parabolic = 0x02000000,
        FinalPoint = 0x04000000,
        FinalTarget = 0x08000000,
        FinalAngle = 0x10000000,
        unk5 = 0x20000000,
        unk6 = 0x40000000,
        unk7 = 0x80000000
    }
}
