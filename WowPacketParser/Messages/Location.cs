using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct Location
    {
        public ulong Transport;
        public Vector3 Loc;
    }
}
