using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("playercreateinfo")]
    public class StartPosition
    {
        [DBFieldName("map")]
        public uint Map;

        //[DBFieldName("zone")] always 0
        public uint Zone;

        [DBFieldName("position_x")]
        public float X;

        [DBFieldName("position_y")]
        public float Y;

        [DBFieldName("position_z")]
        public float Z;

        //[DBFieldName("orientation")]
        //public double O;

        private Vector3 _position;
        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                X = _position.X;
                Y = _position.Y;
                Z = _position.Z;
            }
        }
    }
}
