using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature")]
    public sealed record CreatureDB : IDataModel
    {
        [DBFieldName("guid", true)]
        public uint DbGuid;
        [DBFieldName("id")]
        public uint ID;
        [DBFieldName("map")]
        public uint Map;
        [DBFieldName("position_x")]
        public decimal PosX;
        [DBFieldName("position_y")]
        public decimal PosY;
        [DBFieldName("position_z")]
        public decimal PosZ;
        [DBFieldName("orientation")]
        public decimal Orientation;
    }
}