using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSuspendToken
    {
        public ClientSuspendReason Reason;
        public uint Sequence;
    }
}
