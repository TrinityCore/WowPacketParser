using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQuestGiverQuestListMessage
    {
        public int GreetEmoteDelay;
        public int GreetEmoteType;
        public ulong QuestGiverGUID;
        public List<ClientGossipText> QuestDataText;
        public string Greeting;
    }
}
