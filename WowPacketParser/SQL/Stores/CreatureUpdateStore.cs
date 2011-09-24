using WowPacketParser.SQL.Builder;

namespace WowPacketParser.SQL.Stores
{
    public sealed class CreatureUpdateStore
    {
        public string GetCommand(string field, uint where, object value)
        {
            var builder = new SQLUpdate();
            builder.Table = "creature_template";
            builder.AddValue(field, value);
            builder.AddWhere("entry", where);
            return builder.Build();
        }
    }
}
