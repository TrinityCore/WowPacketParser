using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientLiveRegionCharacterCopy
    {
        public ulong Guid;
        public uint VirtualRealmAddress;
        public uint Token;
    }
}
