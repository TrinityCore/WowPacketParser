using System.Collections.Generic;

namespace WowPacketParser.Store.Objects
{
    public class QuestPOI
    {
        public int ObjectiveIndex;

        public int Map;

        public int WorldMapAreaId;

        public int FloorId;

        public int UnkInt1;

        public int UnkInt2;

        public ICollection<QuestPOIPoint> Points;
    }
}
