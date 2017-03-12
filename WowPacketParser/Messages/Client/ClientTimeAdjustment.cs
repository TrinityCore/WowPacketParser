namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTimeAdjustment
    {
        public uint SequenceIndex;
        public float TimeScale;
    }
}
