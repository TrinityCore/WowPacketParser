using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobjects")]
    public sealed class GameObjects : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public uint? MapID;

        [DBFieldName("DisplayID")]
        public uint? DisplayID;

        [DBFieldName("PositionX")]
        public float? PositionX;

        [DBFieldName("PositionY")]
        public float? PositionY;

        [DBFieldName("PositionZ")]
        public float? PositionZ;

        [DBFieldName("RotationX")]
        public float? RotationX;

        [DBFieldName("RotationY")]
        public float? RotationY;

        [DBFieldName("RotationZ")]
        public float? RotationZ;

        [DBFieldName("RotationW")]
        public float? RotationW;

        [DBFieldName("Size")]
        public float? Size;

        [DBFieldName("PhaseID")]
        public uint? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public uint? PhaseGroupID;

        [DBFieldName("Type")]
        public GameObjectType? Type;

        [DBFieldName("Data")]
        public int?[] Data;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
