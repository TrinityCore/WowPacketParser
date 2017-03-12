using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
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
