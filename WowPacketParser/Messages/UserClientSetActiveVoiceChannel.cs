using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetActiveVoiceChannel
    {
        public byte ChannelType;
        public string ChannelName;
    }
}
