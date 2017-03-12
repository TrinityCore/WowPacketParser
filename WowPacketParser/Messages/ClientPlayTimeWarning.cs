using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPlayTimeWarning
    {
        public uint Flags;
        public int Remaining;
    }
}
