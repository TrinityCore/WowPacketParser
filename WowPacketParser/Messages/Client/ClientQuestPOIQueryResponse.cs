using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQuestPOIQueryResponse
    {
        public List<QuestPOIData> QuestPOIData;
        public int NumPOIs;
    }
}
