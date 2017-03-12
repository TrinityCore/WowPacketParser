using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMSpawnTrackerDump
    {
        public ulong Target;
        public SpawnTrackerData SpawnTrackerData;
    }
}
