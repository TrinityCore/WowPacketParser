using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetCollisionHeight
    {
        public float Scale;
        public ulong MoverGUID;
        public uint MountDisplayID;
        public UpdateCollisionHeightReason Reason;
        public uint SequenceIndex;
        public float Height;
    }
}
