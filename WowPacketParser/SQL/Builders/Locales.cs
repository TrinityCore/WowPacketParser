using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class Locales
    {
        [BuilderMethod]
        public static string LocalesCreature()
        {
            if (Storage.LocalesCreatures.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template_locale))
                return string.Empty;

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var templatesDb = SQLDatabase.Get(new RowList<Store.Objects.CreatureTemplateLocale>());

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.LocalesCreatures, templatesDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }

        [BuilderMethod]
        public static string LocalesQuest()
        {
            if (Storage.LocalesQuests.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.locales_quest))
                return string.Empty;

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var templatesDb = SQLDatabase.Get(new RowList<Store.Objects.LocalesQuest>());

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.LocalesQuests, templatesDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }

        [BuilderMethod]
        public static string LocalesQuestOfferReward()
        {
            if (Storage.LocalesQuestOfferRewards.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.locales_quest))
                return string.Empty;

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var offersDb = SQLDatabase.Get(new RowList<Store.Objects.QuestOfferRewardLocale>());


            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.LocalesQuestOfferRewards, offersDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }

        [BuilderMethod]
        public static string LocalesQuestObjectives()
        {
            if (Storage.LocalesQuestObjectives.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.locales_quest_objectives))
                return string.Empty;

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var templatesDb = SQLDatabase.Get(new RowList<Store.Objects.QuestObjectivesLocale>());

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.LocalesQuestObjectives, templatesDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }

        [BuilderMethod]
        public static string LocalesQuestGreeting()
        {
            if (Storage.LocalesQuestGreeting.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.locales_quest))
                return string.Empty;

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var greetingDb = SQLDatabase.Get(new RowList<Store.Objects.QuestGreetingLocale>());

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.LocalesQuestGreeting, greetingDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }

        [BuilderMethod]
        public static string LocalesQuestRequestItems()
        {
            if (Storage.LocalesQuestRequestItems.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.locales_quest))
                return string.Empty;

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var requestitemsDb = SQLDatabase.Get(new RowList<Store.Objects.QuestRequestItemsLocale>());

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.LocalesQuestRequestItems, requestitemsDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }

        [BuilderMethod]
        public static string LocalesPageText()
        {
            if (Storage.LocalesPageText.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.page_text_locale))
                return string.Empty;

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var pagetextDb = SQLDatabase.Get(new RowList<Store.Objects.PageTextLocale>());

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.LocalesPageText, pagetextDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }
    }
}
