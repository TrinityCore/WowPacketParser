using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientReorderEntry
    {
        public ulong PlayerGUID;
        public byte NewPosition;
    }
}
