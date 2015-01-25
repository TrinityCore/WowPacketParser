using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject_template", WPPDatabase.World)]
    public class GameObjectTemplateNonWDB
    {
        [DBFieldName("size")] public float Size;
        [DBFieldName("faction")] public uint Faction;
        [DBFieldName("flags")] public GameObjectFlag Flags;
    }
}
