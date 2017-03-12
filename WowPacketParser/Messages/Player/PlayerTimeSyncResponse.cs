namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerTimeSyncResponse
    {
        public uint ClientTime;
        public uint SequenceIndex;
    }
}
