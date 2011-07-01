

namespace WowPacketParser.SQL.Stores
{
    public sealed class QuestPoiPointStore
    {
        public string GetCommand(int questId, int idx, int objIndex, int pointX, int pointY)
        {
            var builder = new SQLCommandBuilder("quest_poi_points");

            builder.AddColumnValue("questId", questId);
            builder.AddColumnValue("id", idx);
            builder.AddColumnValue("x", pointX);
            builder.AddColumnValue("y", pointY);

            return builder.BuildInsert();
        }
    }
}
