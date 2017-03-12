using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLiveRegionAccountRestoreResult
    {
        public uint Token;
        public bool Success;
    }
}
