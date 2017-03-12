using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
