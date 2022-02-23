namespace WowPacketParser.Misc
{
    public sealed class MovementInfo
    {
        public WowGuid MoverGuid;

        // NOTE: Do not use flag fields in a generic way to handle anything for producing spawns - different versions have different flags
        public uint Flags;

        public uint Flags2;

        public uint Flags3;

        public bool HasSplineData;

        public Vector3 Position;

        public float Orientation;

        public sealed class TransportInfo
        {
            public WowGuid Guid;

            public Vector4 Offset;
        }

        public TransportInfo Transport;

        public Quaternion Rotation;

        public float WalkSpeed;

        public float RunSpeed;

        public uint VehicleId; // Not exactly related to movement but it is read in ReadMovementUpdateBlock

        public bool HasWpsOrRandMov; // waypoints or random movement

        public Vector4 PositionAsVector4 => new Vector4(Position, Orientation);
    }
}
