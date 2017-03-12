using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestGiverInvalidQuest
    {
        public int Reason;
        public string ReasonText;
    }
}
