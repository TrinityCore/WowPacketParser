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
    }
}
