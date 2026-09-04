using System;
using System.Diagnostics.CodeAnalysis;

namespace WowPacketParserModule.V2_4_3_8606.Enums
{
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum SpellHitInfo243
    {
        HITINFO_DEBUG          = 0x00000001, // unused - debug flag, probably debugging visuals, no effect in non-ptr client
        HITINFO_AFFECTS_VICTIM = 0x00000002,
        HITINFO_OFFHAND        = 0x00000004,
        HITINFO_UNK3           = 0x00000008, // unused (3.3.5a)
        HITINFO_MISS           = 0x00000010,
        HITINFO_ABSORB         = 0x00000020,
        HITINFO_RESIST         = 0x00000040,
        HITINFO_CRITICALHIT    = 0x00000080,
        HITINFO_UNK8           = 0x00000100,
        HITINFO_UNK9           = 0x00000200,
        HITINFO_UNK10          = 0x00000400,
        HITINFO_BLOCK          = 0x00000800,
        HITINFO_UNK12          = 0x00001000,
        HITINFO_UNK13          = 0x00002000,
        HITINFO_GLANCING       = 0x00004000,
        HITINFO_CRUSHING       = 0x00008000,
        HITINFO_NOACTION       = 0x00010000,
        HITINFO_UNK17          = 0x00020000,
        HITINFO_UNK18          = 0x00040000,
        HITINFO_SWINGNOHITSOUND = 0x00080000,
    };
}
