using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDebugAIState
    {
        public List<DebugAIState> DebugStates;
        public ulong Guid;
    }
}
