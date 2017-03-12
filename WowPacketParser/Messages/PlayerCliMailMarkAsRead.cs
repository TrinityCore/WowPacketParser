using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliMailMarkAsRead
    {
        public bool BiReceipt;
        public int MailID;
        public ulong Mailbox;
    }
}
