using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCharFactionChangeResult
    {
        public byte Result;
        public ulong Guid;
        public CharFactionChangeDisplayInfo? Display; // Optional
    }
}
