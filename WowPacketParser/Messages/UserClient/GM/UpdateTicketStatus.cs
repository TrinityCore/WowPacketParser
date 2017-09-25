namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct UpdateTicketStatus
    {
        public ulong TargetGUID;
        public int TicketID;
        public int StatusInt;
    }
}
