namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CriteriaProgress
    {
        public int Id;
        public ulong Quantity;
        public ulong Player;
        public int Flags;
        public Data Date;
        public UnixTime TimeFromStart;
        public UnixTime TimeFromCreate;
    }
}
