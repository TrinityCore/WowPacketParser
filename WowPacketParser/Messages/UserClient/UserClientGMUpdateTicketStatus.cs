namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientGMUpdateTicketStatus
    {
        public ulong TargetGUID;
        public int TicketID;
        public int StatusInt;
    }
}
