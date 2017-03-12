using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGossipMessage
    {
        public List<ClientGossipOptions> GossipOptions;
        public int FriendshipFactionID;
        public ulong GossipGUID;
        public List<ClientGossipText> GossipQuestText;
        public int TextID;
        public int GossipID;
    }
}
