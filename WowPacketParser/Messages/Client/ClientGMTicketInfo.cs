using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMTicketInfo
    {
        public int TicketID;
        public string TicketDescription;
        public byte Category;
        public UnixTime TicketOpenTime;
        public UnixTime OldestTicketTime;
        public UnixTime UpdateTime;
        public byte AssignedToGM;
        public byte OpenedByGM;
        public string WaitTimeOverrideMessage;
        public int WaitTimeOverrideMinutes;
    }
}
