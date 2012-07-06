using System.Collections.Generic;
using PacketParser.Misc;
using PacketParser.SQL;

namespace PacketParser.DataStructures
{
    [DBTableName("quest_poi")]
    public class QuestPOI : ITextOutputDisabled
    {
        [DBFieldName("objIndex")]
        public int ObjectiveIndex;

        [DBFieldName("mapid")]
        public uint Map;

        [DBFieldName("WorldMapAreaId")]
        public uint WorldMapAreaId;

        [DBFieldName("FloorId")]
        public uint FloorId;

        [DBFieldName("unk3")]
        public uint UnkInt1;

        [DBFieldName("unk4")]
        public uint UnkInt2;

        public ICollection<QuestPOIPoint> Points;
    }
}
