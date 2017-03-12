using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestPOIQueryResponse
    {
        public List<QuestPOIData> QuestPOIData;
        public int NumPOIs;
    }
}
