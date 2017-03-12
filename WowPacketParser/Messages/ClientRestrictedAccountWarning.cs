using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRestrictedAccountWarning
    {
        public uint Arg;
        public byte Type;
    }
}
