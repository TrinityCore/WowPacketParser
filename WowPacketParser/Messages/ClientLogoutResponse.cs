using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLogoutResponse
    {
        public int LogoutResult;
        public bool Instant;
    }
}
