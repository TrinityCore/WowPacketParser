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
                // pass empty list, because we want to select the whole db table (faster than select only needed columns)
                var poiDb = SQLDatabase.Get(new RowList<QuestPOI>());

                sql = SQLUtil.Compare(Storage.QuestPOIs, poiDb, StoreNameType.Quest);
            }

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_poi_points))
            {
                if (!Storage.QuestPOIPoints.IsEmpty())
                {
                    // pass empty list, because we want to select the whole db table (faster than select only needed columns)
                    var poiDb = SQLDatabase.Get(new RowList<QuestPOIPoint>());

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

            foreach (var requestItemEmote in Parsing.Parsers.QuestHandler.RequestItemEmoteStore)
            {
                QuestRequestItems requestItems = new QuestRequestItems
                {
                    ID = requestItemEmote.Value.ID,
                    CompletionText = requestItemEmote.Value.CompletionText
                };

                requestItems.VerifiedBuild = 0;

                if (requestItemEmote.Value.EmoteOnCompleteDelay >= 0)
                    requestItems.EmoteOnCompleteDelay = (uint)requestItemEmote.Value.EmoteOnCompleteDelay;

                if (requestItemEmote.Value.EmoteOnComplete >= 0)
                    requestItems.EmoteOnComplete = (uint)requestItemEmote.Value.EmoteOnComplete;

                if (requestItemEmote.Value.EmoteOnIncompleteDelay >= 0)
                    requestItems.EmoteOnIncompleteDelay = (uint)requestItemEmote.Value.EmoteOnIncompleteDelay;

                if (requestItemEmote.Value.EmoteOnIncomplete >= 0)
                    requestItems.EmoteOnIncomplete = (uint)requestItemEmote.Value.EmoteOnIncomplete;

                if (requestItemEmote.Value.EmoteOnCompleteDelay >= 0 && requestItemEmote.Value.EmoteOnComplete >= 0 && requestItemEmote.Value.EmoteOnIncompleteDelay >= 0 && requestItemEmote.Value.EmoteOnIncomplete >= 0)
                    requestItems.VerifiedBuild = ClientVersion.BuildInt;

                Storage.QuestRequestItems.Add(requestItems);
            }

            if (Storage.QuestRequestItems.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestRequestItems);

            return SQLUtil.Compare(Storage.QuestRequestItems, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestDescriptionConditional()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Storage.QuestDescriptionConditional.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestDescriptionConditional);

            return SQLUtil.Compare(Storage.QuestDescriptionConditional, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestCompletionLogConditional()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Storage.QuestCompletionLogConditional.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestCompletionLogConditional);

            return SQLUtil.Compare(Storage.QuestCompletionLogConditional, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestOfferRewardConditional()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Storage.QuestOfferRewardConditional.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestOfferRewardConditional);

            return SQLUtil.Compare(Storage.QuestOfferRewardConditional, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestRequestItemsConditional()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Storage.QuestRequestItemsConditional.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestRequestItemsConditional);

            return SQLUtil.Compare(Storage.QuestRequestItemsConditional, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string UIMapQuestLines()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Storage.UIMapQuestLines.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.UIMapQuestLines);

            return SQLUtil.Compare(Storage.UIMapQuestLines, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string UIMapQuests()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Storage.UIMapQuests.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.UIMapQuests);

            return SQLUtil.Compare(Storage.UIMapQuests, templatesDb, x =>
            {
                return $"{StoreGetters.GetName(StoreNameType.Quest, (int)x.QuestId, false)}";
            });
        }

        [BuilderMethod]
        public static string CreatureQuestStarters()
        {
            if (Storage.CreatureQuestStarters.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.CreatureQuestStarters);

            return SQLUtil.Compare(Storage.CreatureQuestStarters, templatesDb, x =>
            {
                string creatureName = StoreGetters.GetName(StoreNameType.Unit, (int)x.CreatureID, false);
                string questName = StoreGetters.GetName(StoreNameType.Quest, (int)x.QuestID, false);
                return $"{questName} offered by {creatureName}";
            });
        }

        [BuilderMethod]
        public static string CreatureQuestEnders()
        {
            if (Storage.CreatureQuestEnders.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.CreatureQuestEnders);

            return SQLUtil.Compare(Storage.CreatureQuestEnders, templatesDb, x =>
            {
                string creatureName = StoreGetters.GetName(StoreNameType.Unit, (int)x.CreatureID, false);
                string questName = StoreGetters.GetName(StoreNameType.Quest, (int)x.QuestID, false);
                return $"{questName} ended by {creatureName}";
            });
        }

        [BuilderMethod]
        public static string GameObjectQuestStarters()
        {
            if (Storage.GameObjectQuestStarters.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.GameObjectQuestStarters);

            return SQLUtil.Compare(Storage.GameObjectQuestStarters, templatesDb, x =>
            {
                string gobName = StoreGetters.GetName(StoreNameType.GameObject, (int)x.GameObjectID, false);
                string questName = StoreGetters.GetName(StoreNameType.Quest, (int)x.QuestID, false);
                return $"{questName} offered by {gobName}";
            });
        }

        [BuilderMethod]
        public static string GameObjectQuestEnders()
        {
            if (Storage.GameObjectQuestEnders.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.GameObjectQuestEnders);

            return SQLUtil.Compare(Storage.GameObjectQuestEnders, templatesDb, x =>
            {
                string gobName = StoreGetters.GetName(StoreNameType.GameObject, (int)x.GameObjectID, false);
                string questName = StoreGetters.GetName(StoreNameType.Quest, (int)x.QuestID, false);
                return $"{questName} ended by {gobName}";
            });
        }
    }
}
