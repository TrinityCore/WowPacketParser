using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatJoinChannel
    {
        public string Password;
        public string ChannelName;
        public bool CreateVoiceSession;
        public int ChatChannelId;
        public bool Internal;
    }
}
