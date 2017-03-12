namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientRandomRoll
    {
        public ulong Roller;
        public int Result;
        public int Max;
        public int Min;
    }
}
