using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserRouterClientAuthContinuedSession
    {
        public ulong Key;
        public ulong DosResponse;
        public fixed byte Digest[20];
    }
}
