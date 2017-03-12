using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMovementSpline
    {
        public uint ID;
        public Vector3 Destination;
        public CliMovementSplineMove Move; // Optional
    }
}
