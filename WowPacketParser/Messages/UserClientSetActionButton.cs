using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetActionButton
    {
        public ulong Action;
        public byte Index;
    }
}
