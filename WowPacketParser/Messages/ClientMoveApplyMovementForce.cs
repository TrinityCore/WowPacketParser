using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveApplyMovementForce
    {
        public CliMovementForce Force;
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}
