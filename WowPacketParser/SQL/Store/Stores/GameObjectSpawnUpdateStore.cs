namespace WowPacketParser.SQL.Store.Stores
{
    public sealed class GameObjectSpawnUpdateStore
    {
        public string GetCommand(string field, uint where, object value)
        {
            var builder = new CommandBuilder("gameobject");
            builder.AddUpdateValue(field, value);
            return builder.BuildUpdate("guid = " + where);
        }
    }
}
