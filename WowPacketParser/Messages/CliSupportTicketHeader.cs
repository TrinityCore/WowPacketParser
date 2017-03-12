using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliSupportTicketHeader
    {
        public int MapID;
        public Vector3 Position;
        public float Facing;
    }
}
