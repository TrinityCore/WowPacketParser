

namespace WowPacketParser.SQL.Stores
{
    public sealed class CreatureUpdateStore
    {
        public string GetCommand(string field, uint where, object value)
        {
            var builder = new SQLCommandBuilder("creature_template");
            builder.AddUpdateValue(field, value);
            return builder.BuildUpdate("entry = " + where);
        }
    }
}
