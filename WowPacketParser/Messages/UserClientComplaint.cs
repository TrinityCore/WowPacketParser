using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientComplaint
    {
        public UserClientComplaintOffender Offender;
        public byte ComplaintType;
        public ulong EventGuid;
        public ulong InviteGuid;
        public uint MailID;
        public UserClientComplaintChat Chat;
    }
}
