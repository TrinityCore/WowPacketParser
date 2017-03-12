using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryQuestInfoResponse
    {
        public bool Allow;
        public CliQuestInfo Info;
        public uint QuestID;
    }
}
