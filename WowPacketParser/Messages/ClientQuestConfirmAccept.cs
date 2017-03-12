using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestConfirmAccept
    {
        public string QuestTitle;
        public ulong InitiatedBy;
        public int QuestID;
    }
}
