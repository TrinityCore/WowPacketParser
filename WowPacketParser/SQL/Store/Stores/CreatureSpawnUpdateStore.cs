namespace WowPacketParser.SQL.Store.Stores
{
    public sealed class CreatureSpawnUpdateStore
    {
        public string GetCommand(string field, uint where, object value)
        {
            var builder = new CommandBuilder("creature");
            builder.AddUpdateValue(field, value);
            return builder.BuildUpdate("guid = " + where);
        }
    }
}
