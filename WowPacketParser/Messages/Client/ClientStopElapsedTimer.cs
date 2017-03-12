namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientStopElapsedTimer
    {
        public uint TimerID;
        public bool KeepTimer;
    }
}
