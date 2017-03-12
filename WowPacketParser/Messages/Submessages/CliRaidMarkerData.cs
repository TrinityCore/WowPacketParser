using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliRaidMarkerData
    {
        public ulong TransportGUID;
        public int MapID;
        public Vector3 Position;
    }
}
