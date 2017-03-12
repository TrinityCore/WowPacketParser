using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliPosition
    {
        public Vector3 Position;
        public float Facing;
    }
}
