using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("playercreateinfo")]
    public sealed record PlayerCreateInfo : IDataModel
    {
        [DBFieldName("race", true)]
        public Race? Race;

        [DBFieldName("class", true)]
        public Class? Class;

        [DBFieldName("map")]
        public uint? Map;

        [DBFieldName("zone", TargetedDatabase.Zero, TargetedDatabase.Shadowlands)]
        public uint? Zone;

        [DBFieldName("position_x")]
        public float? X;

        [DBFieldName("position_y")]
        public float? Y;

        [DBFieldName("position_z")]
        public float? Z;

        [DBFieldName("orientation")]
        public float? Orientation;

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
