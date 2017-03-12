using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ScenarioBlobData
    {
        public int BlobID;
        public int MapID;
        public int WorldMapAreaID;
        public int Floor;
        public int Priority;
        public int Flags;
        public int WorldEffectID;
        public int PlayerConditionID;
        public List<ScenarioPOIPointData> Points;
    }
}
