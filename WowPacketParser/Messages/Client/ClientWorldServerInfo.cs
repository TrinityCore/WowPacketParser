using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
