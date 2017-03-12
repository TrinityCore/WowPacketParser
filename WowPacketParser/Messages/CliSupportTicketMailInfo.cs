using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliSupportTicketMailInfo
    {
        public int MailID;
        public string MailBody;
        public string MailSubject;
    }
}
