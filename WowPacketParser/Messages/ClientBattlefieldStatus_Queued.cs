using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlefieldStatus_Queued
    {
        public uint AverageWaitTime;
        public ClientBattlefieldStatus_Header Hdr;
        public bool AsGroup;
        public bool SuspendedQueue;
        public bool EligibleForMatchmaking;
        public uint WaitTime;
    }
}
