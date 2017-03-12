using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetPlayerDeclinedNamesResult
    {
        public ulong Player;
        public int ResultCode;
    }
}
