using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveTeleport
    {
        public Vector3 Position;
        public VehicleTeleport? Vehicle; // Optional
        public uint SequenceIndex;
        public ulong MoverGUID;
        public ulong? TransportGUID; // Optional
        public float Facing;
    }
}
