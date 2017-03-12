using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientComplaintChat
    {
        public uint Command;
        public uint ChannelID;
        public string MessageLog;
    }
}
