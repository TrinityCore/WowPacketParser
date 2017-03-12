using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveTimeSkipped
    {
        public ulong MoverGUID;
        public uint TimeSkipped;
    }
}
