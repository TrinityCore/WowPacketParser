using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMovementSplineMove
    {
        public uint Flags;
        public byte Face;
        public int Elapsed;
        public uint Duration;
        public float DurationModifier;
        public float NextDurationModifier;
        public float? JumpGravity; // Optional
        public uint? SpecialTime; // Optional
        public List<Vector3> Points;
        public byte Mode;
        public CliSplineFilter? SplineFilter; // Optional
        public float FaceDirection;
        public ulong FaceGUID;
        public Vector3 FaceSpot;
    }
}
