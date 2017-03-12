using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBFMgrEjected
    {
        public ulong QueueID;
        public bool Relocated;
        public sbyte Reason;
        public sbyte BattleState;
    }
}
