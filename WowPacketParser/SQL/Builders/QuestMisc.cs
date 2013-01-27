using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    public static class QuestMisc
    {
        public static string QuestOffer()
        {
            if (Storage.QuestOffers.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            var entries = Storage.QuestOffers.Keys();
            var offerDb = SQLDatabase.GetDict<uint, QuestOffer>(entries, "Id");

            return SQLUtil.CompareDicts(Storage.QuestOffers, offerDb, StoreNameType.Quest, "Id");
        }

        public static string QuestReward()
        {
            if (Storage.QuestRewards.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            var entries = Storage.QuestRewards.Keys();
            var rewardDb = SQLDatabase.GetDict<uint, QuestReward>(entries, "Id");

            return SQLUtil.CompareDicts(Storage.QuestRewards, rewardDb, StoreNameType.Quest, "Id");
        }

        public static string QuestPOI()
        {
            if (Storage.QuestPOIs.IsEmpty())
                return String.Empty;

            var sql = string.Empty;

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_poi))
            {
                var entries = Storage.QuestPOIs.Keys();
                var poiDb = SQLDatabase.GetDict<uint, uint, QuestPOI>(entries, "questid", "id");

                sql = SQLUtil.CompareDicts(Storage.QuestPOIs, poiDb, StoreNameType.Quest, "questid", "id");
            }

            //var points = new StoreMulti<Tuple<uint, uint>, QuestPOIPoint>();
            //
            //foreach (KeyValuePair<Tuple<uint, uint>, Tuple<QuestPOI, TimeSpan?>> pair in Storage.QuestPOIs)
            //    foreach (var point in pair.Value.Item1.Points)
            //        points.Add(pair.Key, point, pair.Value.Item2);
            //
            //var entries2 = points.Keys();
            //var poiPointsDb = SQLDatabase.GetDictMulti<uint, uint, QuestPOIPoint>(entries2, "questid", "id");

            const string tableName2 = "quest_poi_points";

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_poi_points))
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
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
                            row.Comment = StoreGetters.GetName(StoreNameType.Quest, (int) quest.Key.Item1, false);

                            rows.Add(row);
                        }
                }

                sql += new QueryBuilder.SQLInsert(tableName2, rows, 2).Build();

            }

            return sql;
        }
    }
}
