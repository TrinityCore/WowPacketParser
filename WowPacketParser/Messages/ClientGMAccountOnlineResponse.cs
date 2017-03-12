using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMAccountOnlineResponse
    {
        public ulong PlayerGuid;
        public uint AccountID;
    }
}
