using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliRaidMarkerData
    {
        public ulong TransportGUID;
        public int MapID;
        public Vector3 Position;
    }
}
