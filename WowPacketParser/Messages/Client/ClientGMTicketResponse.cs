namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMTicketResponse
    {
        public uint ResponseID;
        public string Description;
        public uint TicketID;
        public string ResponseText;
    }
}
