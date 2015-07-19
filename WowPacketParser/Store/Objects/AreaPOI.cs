using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("area_poi")]
    public sealed class AreaPOI
    {
        [DBFieldName("Flags")]
        public uint Flags;

        [DBFieldName("Importance")]
        public uint Importance;

        [DBFieldName("FactionID")]
        public uint FactionID;

        [DBFieldName("MapID")]
        public uint MapID;

        [DBFieldName("AreaID")]
        public uint AreaID;

        [DBFieldName("MapID")]
        public uint Icon;

        [DBFieldName("PositionX")]
        public float PositionX;

        [DBFieldName("PositionY")]
        public float PositionY;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("WorldStateID")]
        public uint WorldStateID;

        [DBFieldName("PlayerConditionID")]
        public uint PlayerConditionID;

        [DBFieldName("WorldMapLink")]
        public uint WorldMapLink;

        [DBFieldName("PortLocID")]
        public uint PortLocID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
