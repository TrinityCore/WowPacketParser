using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientWorldServerInfo
    {
        public uint? IneligibleForLootMask; // Optional
        public UnixTime WeeklyReset;
        public uint? InstanceGroupSize; // Optional
        public byte IsTournamentRealm;
        public uint? RestrictedAccountMaxLevel; // Optional
        public uint? RestrictedAccountMaxMoney; // Optional
        public uint DifficultyID;
    }
}
