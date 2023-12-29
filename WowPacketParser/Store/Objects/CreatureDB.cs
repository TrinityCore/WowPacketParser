using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature")]
    public sealed record CreatureDB : IDataModel
    {
        [DBFieldName("guid", true)]
        public ulong DbGuid;
        [DBFieldName("id")]
        public uint ID;
        [DBFieldName("map")]
        public ushort Map;
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