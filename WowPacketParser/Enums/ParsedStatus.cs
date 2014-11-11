using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ParsedStatus
    {
        None       = 0x0,
        Success    = 0x1,
        WithErrors = 0x2,
        NotParsed  = 0x4,
        All = Success | WithErrors | NotParsed
    }
}
