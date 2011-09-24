using WowPacketParser.SQL.Builder;

namespace WowPacketParser.SQL.Stores
{
    public sealed class QuestPoiPointStore
    {
        public string GetCommand(int questId, int idx, int objIndex, int pointX, int pointY)
        {
            var builder = new SQLInsert();
            builder.Table = "quest_poi_points";
            builder.AddValue("questId", questId);
            builder.AddValue("id", idx);
            builder.AddValue("x", pointX);
            builder.AddValue("y", pointY);
            builder.AddWhere("questId", questId);
            return builder.Build();
        }
    }
}
