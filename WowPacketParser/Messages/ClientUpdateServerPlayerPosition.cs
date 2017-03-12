using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUpdateServerPlayerPosition
    {
        public Vector3 Position;
    }
}
