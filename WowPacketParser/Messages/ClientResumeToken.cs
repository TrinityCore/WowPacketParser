using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientResumeToken
    {
        public uint Sequence;
        public ClientSuspendReason Reason;
    }
}
