namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ElaspedTimer
    {
        public uint TimerID;
        public UnixTime CurrentDuration;
    }
}
