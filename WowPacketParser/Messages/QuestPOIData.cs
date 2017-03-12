using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct QuestPOIData
    {
        public int QuestID;
        public int NumBlobs;
        public List<QuestPOIBlobData> Blobs;
    }
}
