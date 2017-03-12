using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct QuestPOIData
    {
        public int QuestID;
        public int NumBlobs;
        public List<QuestPOIBlobData> Blobs;
    }
}
