using WowPacketParser.Enums;
using WowPacketParser.Misc;
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

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
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
        public float rotationX;

        [DBFieldName("rotation1")]
        public float rotationY;

        [DBFieldName("rotation2")]
        public float rotationZ;

        [DBFieldName("rotation3")]
        public float rotationW;

        [DBFieldName("PhaseID", ClientVersionBuild.V6_0_2_19033)]
        public int PhaseId;

        [DBFieldName("PhaseGroupID", ClientVersionBuild.V6_0_2_19033)]
        public int PhaseGroupId;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
