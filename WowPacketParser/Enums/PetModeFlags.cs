using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum PetModeFlags
    {
        Unknown1 = 0x0000001,
        Unknown2 = 0x0000002,
        Unknown100 = 0x0000100,
        DisableActions = 0x8000000
    }
}
