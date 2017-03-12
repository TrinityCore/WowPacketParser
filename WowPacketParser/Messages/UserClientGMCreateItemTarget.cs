using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMCreateItemTarget
    {
        public ItemContext CreationContext;
        public int ItemID;
        public string Target;
        public ulong Guid;
    }
}
