using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum PetModeFlags
    {
        Unknown0 = 0x0000001,
        Unknown1 = 0x0000100,
        DisableActions = 0x8000000
    }
}
