using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCommentatorStartWargame
    {
        public ulong QueueID;
        public string[/*2*/] Names;
    }
}
