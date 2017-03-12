using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientWorldTeleport
    {
        public ulong Transport;
        public float Facing;
        public uint MapID;
        public Vector3 Position;
    }
}
