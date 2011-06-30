namespace WowPacketParser.SQL.Store.Stores
{
    public sealed class QuestPoiStore
    {
        public string GetCommand(int questId, int idx, int objIndex, int mapId, int wmaId, int floorId,
            int unk3, int unk4)
        {
            var builder = new CommandBuilder("quest_poi");

            builder.AddColumnValue("questid", questId);
            builder.AddColumnValue("id", idx);
            builder.AddColumnValue("objIndex", objIndex);
            builder.AddColumnValue("mapId", mapId);
            builder.AddColumnValue("WorldMapAreaId", wmaId);
            builder.AddColumnValue("FloorId", floorId);
            builder.AddColumnValue("unk3", unk3);
            builder.AddColumnValue("unk4", unk4);

            return builder.BuildInsert();
        }
    }
}
