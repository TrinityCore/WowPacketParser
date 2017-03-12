using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestGiverStatusMultiple
    {
        public List<QuestGiverStatus> QuestGiver;
    }
}
