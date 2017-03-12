using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveDisableGravity
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}
