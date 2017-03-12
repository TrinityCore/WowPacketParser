using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliStructSendMail
    {
        public ulong Mailbox;
        public string Target;
        public string Subject;
        public string Body;
        public int StationeryID;
        public int PackageID;
        public List<CliStructMailAttachment> Attachments;
        public ulong SendMoney;
        public ulong Cod;
    }
}
