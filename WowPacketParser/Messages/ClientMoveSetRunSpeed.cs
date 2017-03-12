using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSetRunSpeed
    {
        public ulong MoverGUID;
        public float Speed;
        public uint SequenceIndex;
    }
}
