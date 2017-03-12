using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliMovementTransport
    {
        public ulong Guid;
        public Vector3 Position;
        public float Facing;
        public byte VehicleSeatIndex;
        public uint MoveTime;
        public uint? PrevMoveTime; // Optional
        public int? VehicleRecID; // Optional
    }
}
