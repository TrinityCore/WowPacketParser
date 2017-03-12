using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
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
