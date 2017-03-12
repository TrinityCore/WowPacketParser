using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientMailReturnToSender
    {
        public ulong SenderGUID;
        public int MailID;
    }
}
