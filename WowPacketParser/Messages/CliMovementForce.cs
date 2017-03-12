using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMovementForce
    {
        public uint ID;
        public Vector3 Direction;
        public uint TransportID;
        public float Magnitude;
        public byte Type;
    }
}
