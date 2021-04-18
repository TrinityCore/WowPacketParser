using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum PhaseShiftFlags : uint
    {
        None            = 0x00,
        AlwaysVisible   = 0x01, // Ignores all phasing, can see everything and be seen by everything
        Inverse         = 0x02, // By default having at least one shared phase for two objects means they can see each other
                                // this flag makes objects see each other if they have at least one non-shared phase
        InverseUnphased = 0x04,
        Unphased        = 0x08,
        NoCosmetic      = 0x10  // This flag ignores shared cosmetic phases (two players that both have shared cosmetic phase but no other phase cannot see each other)
    }
}
