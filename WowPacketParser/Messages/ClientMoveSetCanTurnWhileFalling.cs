using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSetCanTurnWhileFalling
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}
