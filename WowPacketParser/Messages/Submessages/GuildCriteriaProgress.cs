namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct GuildCriteriaProgress
    {
        public int CriteriaID;
        public UnixTime DateCreated;
        public UnixTime DateStarted;
        public UnixTime DateUpdated;
        public ulong Quantity;
        public ulong PlayerGUID;
        public int Flags;
    }
}
