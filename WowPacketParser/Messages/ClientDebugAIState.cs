using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugAIState
    {
        public List<DebugAIState> DebugStates;
        public ulong Guid;
    }
}
