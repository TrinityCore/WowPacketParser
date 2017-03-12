using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveEnableTransitionBetweenSwimAndFly
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}
