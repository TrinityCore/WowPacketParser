using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliMailDelete
    {
        public int DeleteReason;
        public int MailID;
    }
}
