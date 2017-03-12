using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
