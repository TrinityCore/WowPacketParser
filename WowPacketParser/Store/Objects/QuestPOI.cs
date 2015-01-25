using System.Collections.Generic;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_poi", WPPDatabase.World)]
    public class QuestPOI
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

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;

        public uint Idx;
        public ICollection<QuestPOIPoint> Points;
    }
}
