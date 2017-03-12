using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveUnsetCanTurnWhileFalling
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}
