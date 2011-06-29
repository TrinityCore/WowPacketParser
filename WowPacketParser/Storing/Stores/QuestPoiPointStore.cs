using WowPacketParser.Enums;

namespace WowPacketParser.Storing.Stores
{
    public sealed class QuestPoiPointStore
    {
        public string GetCommand(int questId, int idx, int objIndex, int pointX, int pointY)
        {
            var builder = new CommandBuilder("quest_poi_points");

            builder.AddColumnValue("questId", questId);

            if (Store.Format == SqlFormat.Trinity)
                builder.AddColumnValue("id", idx);
            else
                builder.AddColumnValue("objIndex", objIndex);

            builder.AddColumnValue("x", pointX);
            builder.AddColumnValue("y", pointY);

            return builder.BuildInsert(true);
        }
    }
}
