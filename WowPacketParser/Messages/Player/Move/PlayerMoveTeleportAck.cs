namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveTeleportAck
    {
        public ulong MoverGUID;
        public uint AckIndex;
        public uint MoveTime;
    }
}
