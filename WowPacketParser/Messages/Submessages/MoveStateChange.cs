using WowPacketParser.Messages.CliChat;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct MoveStateChange
    {
        public ushort MessageID;
        public uint SequenceIndex;
        public float? Speed; // Optional
        public KnockBackInfo? KnockBack; // Optional
        public int? VehicleRecID; // Optional
        public CollisionHeightInfo? CollisionHeight; // Optional
        public CliMovementForce? MovementForce; // Optional
    }
}
