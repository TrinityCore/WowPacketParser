using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliMailTakeMoney
    {
        public ulong Mailbox;
        public ulong Money;
        public int MailID;
    }
}
