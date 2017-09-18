using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public sealed class MovementInfo
    {
        // NOTE: Do not use flag fields in a generic way to handle anything for producing spawns - different versions have different flags
        public MovementFlag Flags;

        public MovementFlagExtra FlagsExtra;

        public bool HasSplineData;

        public Vector3 Position;

        public float Orientation;

        public WowGuid TransportGuid;

        public Vector4 TransportOffset;

        public Quaternion Rotation;

        public float WalkSpeed;

        public float RunSpeed;

        public uint VehicleId; // Not exactly related to movement but it is read in ReadMovementUpdateBlock

        public bool HasWpsOrRandMov; // waypoints or random movement
    }
}
