using System;
using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class QuestMisc
    {
        [BuilderMethod]
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

        [BuilderMethod]
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

        [BuilderMethod]
        public static string QuestPOI()
        {
            if (Storage.QuestPOIs.IsEmpty())
                return String.Empty;

            var sql = string.Empty;

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_poi))
            {
                var entries = Storage.QuestPOIs.Keys();
                var poiDb = SQLDatabase.GetDict<uint, uint, QuestPOI>(entries, "questid", "id");

                sql = SQLUtil.CompareDicts(Storage.QuestPOIs, poiDb, StoreNameType.Quest, StoreNameType.None, "questid", "id");
            }

            // TODO: fix this piece of code so it compares with db
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
                foreach (var quest in Storage.QuestPOIs.OrderBy(blub => blub.Key.Item1))
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
                            row.AddValue("VerifiedBuild", point.VerifiedBuild);
                            row.Comment = StoreGetters.GetName(StoreNameType.Quest, (int) quest.Key.Item1, false);

                            rows.Add(row);
                        }
                }

                sql += new QueryBuilder.SQLInsert(tableName2, rows, 2).Build();

            }

            return sql;
        }

        [BuilderMethod]
        public static string QuestPOIWoD()
        {
            var sql = string.Empty;

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_poi))
            {
                if (!Storage.QuestPOIWoDs.IsEmpty())
                {
                    var entries = Storage.QuestPOIWoDs.Keys();
                    var poiDb = SQLDatabase.GetDict<int, int, QuestPOIWoD>(entries, "QuestID", "Idx1");
                    sql += SQLUtil.CompareDicts(Storage.QuestPOIWoDs, poiDb, StoreNameType.Quest, StoreNameType.None, "QuestID", "Idx1");
                }
            }

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_poi_points))
            {
                if (!Storage.QuestPOIPointWoDs.IsEmpty())
                {
                    var entries = Storage.QuestPOIPointWoDs.Keys();
                    var poiDb = SQLDatabase.GetDict<int, int, int, QuestPOIPointWoD>(entries, "QuestID", "Idx1", "Idx2");
                    sql += SQLUtil.CompareDicts(Storage.QuestPOIPointWoDs, poiDb, StoreNameType.Quest, StoreNameType.None, StoreNameType.None, "QuestID", "Idx1", "Idx2");
                }
            }

            return sql;
        }

        [BuilderMethod]
        public static string QuestGreeting()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return String.Empty;

            if (Storage.QuestGreetings.IsEmpty())
                return String.Empty;

            var entries = Storage.QuestGreetings.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, QuestGreeting>(entries, "ID");

            return SQLUtil.CompareDicts(Storage.QuestGreetings, templatesDb, StoreNameType.QuestGreeting, "ID");
        }

        [BuilderMethod]
        public static string QuestOfferReward()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return String.Empty;

            if (Storage.QuestOfferRewards.IsEmpty())
                return String.Empty;

            var entries = Storage.QuestOfferRewards.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, QuestOfferReward>(entries, "ID");

            return SQLUtil.CompareDicts(Storage.QuestOfferRewards, templatesDb, StoreNameType.QuestGreeting, "ID");
        }

        [BuilderMethod]
        public static string QuestDetails()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return String.Empty;

            if (Storage.QuestDetails.IsEmpty())
                return String.Empty;

            var entries = Storage.QuestDetails.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, QuestDetails>(entries, "ID");

            return SQLUtil.CompareDicts(Storage.QuestDetails, templatesDb, StoreNameType.Quest, "ID");
        }

        [BuilderMethod]
        public static string QuestRequestItems()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return String.Empty;

            if (Storage.QuestRequestItems.IsEmpty())
                return String.Empty;

            var entries = Storage.QuestRequestItems.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, QuestRequestItems>(entries, "ID");

            return SQLUtil.CompareDicts(Storage.QuestRequestItems, templatesDb, StoreNameType.Quest, "ID");
         }
    }
}
