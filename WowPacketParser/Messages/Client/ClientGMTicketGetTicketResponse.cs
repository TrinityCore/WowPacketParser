namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMTicketGetTicketResponse
    {
        public ClientGMTicketInfo? Info; // Optional
        public int Result;
    }
}
