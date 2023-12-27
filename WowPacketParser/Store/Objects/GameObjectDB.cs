using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject")]
    public sealed record GameObjectDB : IDataModel
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
        [DBFieldName("rotation0")]
        public decimal Rot0;
        [DBFieldName("rotation1")]
        public decimal Rot1;
        [DBFieldName("rotation2")]
        public decimal Rot2;
        [DBFieldName("rotation3")]
        public decimal Rot3;
    }
}
