using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLoginVerifyWorld
    {
        public float Facing;
        public int MapID;
        public uint Reason;
        public Vector3 Position;
    }
}
