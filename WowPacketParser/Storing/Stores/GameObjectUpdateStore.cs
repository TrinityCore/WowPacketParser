namespace WowPacketParser.Storing.Stores
{
    public sealed class GameObjectUpdateStore
    {
        public string GetCommand(string field, uint where, object value)
        {
            var builder = new CommandBuilder("gameobject_template");
            builder.AddUpdateValue(field, value);
            return builder.BuildUpdate("entry = " + where);
        }
    }
}
