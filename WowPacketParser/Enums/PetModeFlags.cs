using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum PetModeFlags
    {
        Unknown1       = 0x0010000,
        Unknown2       = 0x0020000,
        Unknown3       = 0x0040000,
        Unknown4       = 0x0080000,
        Unknown5       = 0x0100000,
        Unknown6       = 0x0200000,
        Unknown7       = 0x0400000,
        Unknown8       = 0x0800000,
        Unknown9       = 0x1000000,
        Unknown10      = 0x2000000,
        Unknown11      = 0x4000000,
        DisableActions = 0x8000000
    }

    public enum ReactState
    {
        Passive    = 0,
        Defensive  = 1,
        Aggressive = 2
    }

    public enum CommandState
    {
        Stay    = 0,
        Follow  = 1,
        Attack  = 2,
        Abandon = 3
    }
}
