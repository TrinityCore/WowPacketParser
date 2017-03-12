using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Cli
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
