using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSetNormalFall
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}
