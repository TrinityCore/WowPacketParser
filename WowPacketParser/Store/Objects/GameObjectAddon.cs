using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject_addon")]
    public sealed record GameObjectAddon : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("parent_rotation0")]
        public float? parentRot0;

        [DBFieldName("parent_rotation1")]
        public float? parentRot1;

        [DBFieldName("parent_rotation2")]
        public float? parentRot2;

        [DBFieldName("parent_rotation3")]
        public float? parentRot3;

        [DBFieldName("WorldEffectID", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public uint? WorldEffectID;

        [DBFieldName("AIAnimKitID", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public uint? AIAnimKitID;
    }
}
