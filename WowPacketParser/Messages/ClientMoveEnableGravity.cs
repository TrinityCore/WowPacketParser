using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveEnableGravity
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}
