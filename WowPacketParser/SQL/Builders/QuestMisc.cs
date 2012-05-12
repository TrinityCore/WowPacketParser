using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.SQL.Builders
{
    public static class QuestMisc
    {
        public static string QuestPOI()
        {
            if (Storage.QuestPOIs.IsEmpty())
                return String.Empty;

            const string tableName1 = "quest_poi";
            const string tableName2 = "quest_poi_points";

            // `quest_poi`
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var quest in Storage.QuestPOIs)
            {
                var questPOI = quest.Value.Item1;

                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("questId", quest.Key.Item1);
                row.AddValue("id", quest.Key.Item2);
                row.AddValue("objIndex", questPOI.ObjectiveIndex);
                row.AddValue("mapid", questPOI.Map);
                row.AddValue("WorldMapAreaId", questPOI.WorldMapAreaId);
                row.AddValue("FloorId", questPOI.FloorId);
                row.AddValue("unk3", questPOI.UnkInt1);
                row.AddValue("unk4", questPOI.UnkInt2);
                row.Comment = StoreGetters.GetName(StoreNameType.Quest, (int)quest.Key.Item1, false);

                rows.Add(row);
            }

            var result = new QueryBuilder.SQLInsert(tableName1, rows, 2).Build();

            // `quest_poi_points`
            rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var quest in Storage.QuestPOIs)
            {
                var questPOI = quest.Value.Item1;

                if (questPOI.Points != null) // Needed?
                    foreach (var point in questPOI.Points)
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("questId", quest.Key.Item1);
                        row.AddValue("id", quest.Key.Item2);
                        row.AddValue("idx", point.Index); // Not on sniffs
                        row.AddValue("x", point.X);
                        row.AddValue("y", point.Y);
                        row.Comment = StoreGetters.GetName(StoreNameType.Quest, (int)quest.Key.Item1, false);

                        rows.Add(row);
                    }
            }

            result += new QueryBuilder.SQLInsert(tableName2, rows, 2).Build();

            return result;
        }
    }
}
