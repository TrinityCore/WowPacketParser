namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LFGBlackListSlot
    {
        public uint Slot;
        public uint Reason;
        public int SubReason1;
        public int SubReason2;
    }
}
