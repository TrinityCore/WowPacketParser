using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobjects")]
    public sealed class GameObjects
    {
        [DBFieldName("MapID")]
        public uint MapID;

        [DBFieldName("DisplayID")]
        public uint DisplayId;

        [DBFieldName("PositionX")]
        public float PositionX;

        [DBFieldName("PositionY")]
        public float PositionY;

        [DBFieldName("PositionZ")]
        public float PositionZ;

        [DBFieldName("RotationX")]
        public float RotationX;

        [DBFieldName("RotationY")]
        public float RotationY;

        [DBFieldName("RotationZ")]
        public float RotationZ;

        [DBFieldName("RotationW")]
        public float RotationW;

        [DBFieldName("Size")]
        public float Size;

        [DBFieldName("PhaseID", ClientVersionBuild.V6_0_2_19033)]
        public uint PhaseId;

        [DBFieldName("PhaseGroupID", ClientVersionBuild.V6_0_2_19033)]
        public uint PhaseGroupId;

        [DBFieldName("Type")]
        public GameObjectType Type;

        [DBFieldName("Data", ClientVersionBuild.Zero, ClientVersionBuild.V6_0_3_19103, 4, true)]
        [DBFieldName("Data", ClientVersionBuild.V6_0_3_19103, 8, true)]
        public int[] Data;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
