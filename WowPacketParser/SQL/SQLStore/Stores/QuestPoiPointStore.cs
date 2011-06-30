using WowPacketParser.Enums;

namespace WowPacketParser.SQLStore.Stores
{
    public sealed class QuestPoiPointStore
    {
        public string GetCommand(int questId, int idx, int objIndex, int pointX, int pointY)
        {
            var builder = new CommandBuilder("quest_poi_points");

            builder.AddColumnValue("questId", questId);
            builder.AddColumnValue("id", idx);
            builder.AddColumnValue("x", pointX);
            builder.AddColumnValue("y", pointY);

            return builder.BuildInsert();
        }
    }
}
