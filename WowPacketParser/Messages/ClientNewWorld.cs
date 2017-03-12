using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientNewWorld
    {
        public int MapID;
        public uint Reason;
        public float Facing;
        public Vector3 Position;
    }
}
