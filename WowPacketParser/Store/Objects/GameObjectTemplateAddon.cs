using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject_template_addon")]
    public sealed record GameObjectTemplateAddon : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("faction")]
        public uint? Faction;

        [DBFieldName("flags")]
        public GameObjectFlag? Flags;

        [DBFieldName("WorldEffectID", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic | TargetedDatabaseFlag.CataClassic)]
        public uint? WorldEffectID;

        [DBFieldName("AIAnimKitID", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic | TargetedDatabaseFlag.CataClassic)]
        public uint? AIAnimKitID;
    }
}
