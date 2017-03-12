using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSetFlightSpeed
    {
        public ulong MoverGUID;
        public float Speed;
        public uint SequenceIndex;
    }
}
