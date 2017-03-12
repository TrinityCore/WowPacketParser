using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLiveRegionCharacterCopyResult
    {
        public bool Success;
        public uint Token;
    }
}
