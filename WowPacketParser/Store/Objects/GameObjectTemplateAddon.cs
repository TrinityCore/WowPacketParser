using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject_template_addon")]
    public sealed class GameObjectTemplateAddon : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("faction")]
        public uint? Faction;

        [DBFieldName("flags")]
        public GameObjectFlag? Flags;
    }
}
