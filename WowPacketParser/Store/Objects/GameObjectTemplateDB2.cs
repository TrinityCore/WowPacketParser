using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject_template")]
    public sealed class GameObjectTemplateDB2
    {
        [DBFieldName("type")]
        public GameObjectType Type;

        [DBFieldName("displayId")]
        public uint DisplayId;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("data", 4, true)]
        public int[] Data;

        [DBFieldName("size")]
        public float Size;

        [DBFieldName("WDBVerified")]
        public int WDBVerified;
    }

    [DBTableName("gameobject_db2_position")]
    public sealed class GameObjectTemplatePositionDB2
    {
        [DBFieldName("map")]
        public uint map;

        [DBFieldName("position_x")]
        public float positionX;

        [DBFieldName("position_y")]
        public float positionY;

        [DBFieldName("position_x")]
        public float positionZ;

        [DBFieldName("rotation0")]
        public float rotation0;

        [DBFieldName("rotation1")]
        public float rotation1;

        [DBFieldName("rotation2")]
        public float rotation2;

        [DBFieldName("rotation3")]
        public float rotation3;

        [DBFieldName("WDBVerified")]
        public int WDBVerified;
    }
}
