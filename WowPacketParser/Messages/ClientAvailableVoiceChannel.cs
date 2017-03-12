using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAvailableVoiceChannel
    {
        public byte SessionType;
        public ulong LocalGUID;
        public ulong SessionGUID;
        public string ChannelName;
    }
}
