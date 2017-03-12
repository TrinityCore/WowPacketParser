using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBFMgrQueueInvite
    {
        public ulong QueueID;
        public uint InstanceID;
        public uint Timeout;
        public int MapID;
        public int MaxLevel;
        public int MinLevel;
        public sbyte BattleState;
        public sbyte Index;
    }
}
