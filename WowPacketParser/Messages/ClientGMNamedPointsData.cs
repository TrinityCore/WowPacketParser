using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMNamedPointsData
    {
        public ulong Transport;
        public Vector3 Position;
        public float Facing;
        public string InternalName;
        public string Name;
    }
}
