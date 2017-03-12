using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveKnockBack
    {
        public ulong MoverGUID;
        public Vector2 Direction;
        public float HorzSpeed;
        public uint SequenceIndex;
        public float VertSpeed;
    }
}
