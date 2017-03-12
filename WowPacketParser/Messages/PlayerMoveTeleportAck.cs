using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveTeleportAck
    {
        public ulong MoverGUID;
        public uint AckIndex;
        public uint MoveTime;
    }
}
