using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ImmigrantHostSearchLog
    {
        public ulong PartyMember;
        public bool IsLeader;
        public uint Realm;
        public uint ImmigrantPop;
        public ImmigrationState State;
    }
}
