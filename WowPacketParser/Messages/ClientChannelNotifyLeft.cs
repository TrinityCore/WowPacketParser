using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChannelNotifyLeft
    {
        public string Channel;
        public int ChatChannelID;
        public bool Suspended;
    }
}
