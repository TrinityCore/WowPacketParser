using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct MovementMonsterSpline
    {
        public uint ID;
        public Vector3 Destination;
        public bool CrzTeleport;
        public MovementSpline Move;
    }
}
