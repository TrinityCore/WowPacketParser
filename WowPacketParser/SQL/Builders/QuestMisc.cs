using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class QuestMisc
    {
        [BuilderMethod]
        public static string QuestOfferReward()
        {
            if (Storage.QuestOfferRewards.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            var offerDb = SQLDatabase.Get(Storage.QuestOfferRewards);

            return SQLUtil.Compare(Storage.QuestOfferRewards, offerDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestPOI()
        {
            if (Storage.QuestPOIs.IsEmpty())
                return string.Empty;

            string sql = string.Empty;

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_poi))
            {
                var poiDb = SQLDatabase.Get(Storage.QuestPOIs);

                sql = SQLUtil.Compare(Storage.QuestPOIs, poiDb, StoreNameType.Quest);
            }

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_poi_points))
            {
                if (!Storage.QuestPOIPoints.IsEmpty())
                {
                    var poiDb = SQLDatabase.Get(Storage.QuestPOIPoints);

                    sql += SQLUtil.Compare(Storage.QuestPOIPoints, poiDb, StoreNameType.Quest);
                }
            }

            return sql;
        }

        [BuilderMethod]
        public static string QuestGreeting()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Settings.TargetedDatabase == TargetedDatabase.WrathOfTheLichKing ||
                Settings.TargetedDatabase == TargetedDatabase.Cataclysm)
                return string.Empty;

            if (Storage.QuestGreetings.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestGreetings);

            return SQLUtil.Compare(Storage.QuestGreetings, templatesDb, StoreNameType.QuestGreeting);
        }

        [BuilderMethod]
        public static string QuestDetails()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Storage.QuestDetails.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestDetails);

            return SQLUtil.Compare(Storage.QuestDetails, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestRequestItems()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Storage.QuestRequestItems.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestRequestItems);

            return SQLUtil.Compare(Storage.QuestRequestItems, templatesDb, StoreNameType.Quest);
         }
    }
}
