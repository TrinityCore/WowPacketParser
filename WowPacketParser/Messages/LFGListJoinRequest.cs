using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LFGListJoinRequest
    {
        public uint ActivityID;
        public uint RequiredItemLevel;
        public string Name;
        public string Comment;
        public string VoiceChat;
    }
}
