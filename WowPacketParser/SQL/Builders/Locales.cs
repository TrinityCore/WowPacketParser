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
        public static string BroadcastTextLocale()
        {
            if (Storage.BroadcastTextLocales.IsEmpty())
                return string.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.broadcast_text_locale))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.BroadcastTextLocales, Settings.HotfixesDatabase);

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.BroadcastTextLocales, templatesDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }

        [BuilderMethod]
        public static string LocalesQuest()
        {
            if (Storage.LocalesQuests.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.locales_quest))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.LocalesQuests);

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.LocalesQuests, templatesDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";           
        }

        [BuilderMethod]
        public static string LocalesQuestObjectives()
        {
            if (Storage.LocalesQuestObjectives.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.locales_quest_objectives))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.LocalesQuestObjectives);

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.LocalesQuestObjectives, templatesDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }
    }
}
