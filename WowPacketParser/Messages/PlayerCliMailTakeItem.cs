using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliMailTakeItem
    {
        public ulong Mailbox;
        public int AttachID;
        public int MailID;
    }
}
