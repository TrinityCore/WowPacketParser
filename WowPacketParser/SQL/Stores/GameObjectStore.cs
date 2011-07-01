using WowPacketParser.Enums;


namespace WowPacketParser.SQL.Stores
{
    public sealed class GameObjectStore
    {
        public string GetCommand(int entry, GameObjectType type, int dispId, string name,
            string iconName, string castCaption, string unkStr, int[] data, float size,
            int[] qItem)
        {
            var builder = new SQLCommandBuilder("gameobject_template");

            builder.AddColumnValue("entry", entry);
            builder.AddColumnValue("type", (int)type);
            builder.AddColumnValue("displayId", dispId);
            builder.AddColumnValue("name", name);
            builder.AddColumnValue("castBarCaption", castCaption);
            builder.AddColumnValue("unk1", unkStr);
            builder.AddColumnValue("faction", 35);
            builder.AddColumnValue("flags", 0);
            builder.AddColumnValue("size", size);

            for (var i = 0; i < 24; i++)
                builder.AddColumnValue("data" + i, data[i]);

            builder.AddColumnValue("ScriptName", string.Empty);

            return builder.BuildInsert();
        }
    }
}
