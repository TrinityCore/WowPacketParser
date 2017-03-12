using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct QuestPOIBlobData
    {
        public int BlobIndex;
        public int ObjectiveIndex;
        public int QuestObjectiveID;
        public int QuestObjectID;
        public int MapID;
        public int WorldMapAreaID;
        public int Floor;
        public int Priority;
        public int Flags;
        public int WorldEffectID;
        public int PlayerConditionID;
        public int NumPoints;
        public List<QuestPOIBlobPoint> Points;
    }
}
