using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSetFeatherFall
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}
