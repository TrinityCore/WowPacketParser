using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct MovementSpline
    {
        public uint Flags;
        public byte Face;
        public byte AnimTier;
        public uint TierTransStartTime;
        public int Elapsed;
        public uint MoveTime;
        public float JumpGravity;
        public uint SpecialTime;
        public List<Vector3> Points;
        public byte Mode;
        public byte VehicleExitVoluntary;
        public ulong TransportGUID;
        public sbyte VehicleSeat;
        public List<uint> PackedDeltas;
        public MonsterSplineFilter? SplineFilter; // Optional
        public float FaceDirection;
        public ulong FaceGUID;
        public Vector3 FaceSpot;
    }
}
