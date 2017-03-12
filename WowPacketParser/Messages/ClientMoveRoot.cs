using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveRoot
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}
