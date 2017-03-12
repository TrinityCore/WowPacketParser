using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugAIState
    {
        public List<DebugAIState> DebugStates;
        public ulong Guid;
    }
}
