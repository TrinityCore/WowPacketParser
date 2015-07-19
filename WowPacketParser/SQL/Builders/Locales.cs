using System;
using System.Collections.Generic;
using System.Linq;
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
                return String.Empty;

            if (!Settings.HotfixSQLOutputFlag.HasAnyFlagBit(HotfixSQLOutput.broadcast_text_locale))
                return String.Empty;

            const string tableName = "broadcast_text_locale";

            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

            foreach (var broadcastTextLocale in Settings.SQLOrderByKey ? Storage.BroadcastTextLocales.OrderBy(blub => blub.Key).ToList() : Storage.BroadcastTextLocales.ToList())
            {
                if (SQLDatabase.BroadcastTextLocaleStores != null && SQLDatabase.BroadcastTextLocaleStores.ContainsKey(Tuple.Create(broadcastTextLocale.Key.Item1, broadcastTextLocale.Key.Item2)))
                {
                    var row = new QueryBuilder.SQLUpdateRow();
                    var broadcastTextLocaleDB = SQLDatabase.BroadcastTextLocaleStores[Tuple.Create(broadcastTextLocale.Key.Item1, broadcastTextLocale.Key.Item2)];

                    if (!Utilities.EqualValues(broadcastTextLocaleDB.MaleText_lang, broadcastTextLocale.Value.Item1.MaleText_lang))
                        row.AddValue("MaleText_lang", broadcastTextLocale.Value.Item1.MaleText_lang);

                    if (!Utilities.EqualValues(broadcastTextLocaleDB.FemaleText_lang, broadcastTextLocale.Value.Item1.FemaleText_lang))
                        row.AddValue("FemaleText_lang", broadcastTextLocale.Value.Item1.FemaleText_lang);

                    if (!Utilities.EqualValues(broadcastTextLocaleDB.VerifiedBuild, broadcastTextLocale.Value.Item1.VerifiedBuild))
                        row.AddValue("VerifiedBuild", broadcastTextLocale.Value.Item1.VerifiedBuild);

                    row.AddWhere("ID", broadcastTextLocale.Key.Item1);
                    row.AddWhere("locale", broadcastTextLocale.Key.Item2);

                    row.Table = tableName;

                    rowsUpd.Add(row);
                }
                else // insert new
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("ID", broadcastTextLocale.Key.Item1);
                    row.AddValue("locale", broadcastTextLocale.Key.Item2);

                    row.AddValue("MaleText_lang", broadcastTextLocale.Value.Item1.MaleText_lang);
                    row.AddValue("FemaleText_lang", broadcastTextLocale.Value.Item1.FemaleText_lang);

                    row.AddValue("VerifiedBuild", broadcastTextLocale.Value.Item1.VerifiedBuild);

                    rowsIns.Add(row);
                }
            }

            var result = new QueryBuilder.SQLInsert(tableName, rowsIns, deleteDuplicates: false, primaryKeyNumber: 2).Build() +
                         new QueryBuilder.SQLUpdate(rowsUpd).Build();

            return "SET NAMES 'utf8';" + Environment.NewLine + result + Environment.NewLine + "SET NAMES 'latin1';";
        }

        [BuilderMethod]
        public static string LocalesQuest()
        {
            if (Storage.LocalesQuests.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.locales_quest))
                return String.Empty;

            const string tableName = "quest_template_locale";

            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

            foreach (var localesQuest in Settings.SQLOrderByKey ? Storage.LocalesQuests.OrderBy(blub => blub.Key).ToList() : Storage.LocalesQuests.ToList())
            {
                if (SQLDatabase.LocalesQuestStores != null && SQLDatabase.LocalesQuestStores.ContainsKey(Tuple.Create(localesQuest.Key.Item1, localesQuest.Key.Item2)))
                {
                    var row = new QueryBuilder.SQLUpdateRow();
                    var localesQuestDB = SQLDatabase.LocalesQuestStores[Tuple.Create(localesQuest.Key.Item1, localesQuest.Key.Item2)];

                    if (!Utilities.EqualValues(localesQuestDB.LogTitle, localesQuest.Value.Item1.LogTitle))
                        row.AddValue("LogTitle", localesQuest.Value.Item1.LogTitle);

                    if (!Utilities.EqualValues(localesQuestDB.LogDescription, localesQuest.Value.Item1.LogDescription))
                        row.AddValue("LogDescription", localesQuest.Value.Item1.LogDescription);

                    if (!Utilities.EqualValues(localesQuestDB.QuestDescription, localesQuest.Value.Item1.QuestDescription))
                        row.AddValue("QuestDescription", localesQuest.Value.Item1.QuestDescription);

                    if (!Utilities.EqualValues(localesQuestDB.AreaDescription, localesQuest.Value.Item1.AreaDescription))
                        row.AddValue("AreaDescription", localesQuest.Value.Item1.AreaDescription);

                    if (!Utilities.EqualValues(localesQuestDB.QuestCompletionLog, localesQuest.Value.Item1.QuestCompletionLog))
                        row.AddValue("QuestCompletionLog", localesQuest.Value.Item1.QuestCompletionLog);

                    if (!Utilities.EqualValues(localesQuestDB.PortraitGiverText, localesQuest.Value.Item1.PortraitGiverText))
                        row.AddValue("PortraitGiverText", localesQuest.Value.Item1.PortraitGiverText);

                    if (!Utilities.EqualValues(localesQuestDB.PortraitGiverName, localesQuest.Value.Item1.PortraitGiverName))
                        row.AddValue("PortraitGiverName", localesQuest.Value.Item1.PortraitGiverName);

                    if (!Utilities.EqualValues(localesQuestDB.PortraitTurnInText, localesQuest.Value.Item1.PortraitTurnInText))
                        row.AddValue("PortraitTurnInText", localesQuest.Value.Item1.PortraitTurnInText);

                    if (!Utilities.EqualValues(localesQuestDB.PortraitTurnInName, localesQuest.Value.Item1.PortraitTurnInName))
                        row.AddValue("PortraitTurnInName", localesQuest.Value.Item1.PortraitTurnInName);

                    if (!Utilities.EqualValues(localesQuestDB.VerifiedBuild, localesQuest.Value.Item1.VerifiedBuild))
                        row.AddValue("VerifiedBuild", localesQuest.Value.Item1.VerifiedBuild);

                    row.AddWhere("ID", localesQuest.Key.Item1);
                    row.AddWhere("locale", localesQuest.Key.Item2);

                    row.Table = tableName;

                    rowsUpd.Add(row);
                }
                else // insert new
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("ID", localesQuest.Key.Item1);
                    row.AddValue("locale", localesQuest.Key.Item2);

                    row.AddValue("LogTitle", localesQuest.Value.Item1.LogTitle);
                    row.AddValue("LogDescription", localesQuest.Value.Item1.LogDescription);
                    row.AddValue("QuestDescription", localesQuest.Value.Item1.QuestDescription);
                    row.AddValue("AreaDescription", localesQuest.Value.Item1.AreaDescription);
                    row.AddValue("PortraitGiverText", localesQuest.Value.Item1.AreaDescription);
                    row.AddValue("PortraitGiverName", localesQuest.Value.Item1.PortraitGiverName);
                    row.AddValue("PortraitTurnInText", localesQuest.Value.Item1.PortraitTurnInText);
                    row.AddValue("PortraitTurnInName", localesQuest.Value.Item1.PortraitTurnInName);
                    row.AddValue("QuestCompletionLog", localesQuest.Value.Item1.QuestCompletionLog);

                    row.AddValue("VerifiedBuild", localesQuest.Value.Item1.VerifiedBuild);

                    rowsIns.Add(row);
                }
            }

            var result = new QueryBuilder.SQLInsert(tableName, rowsIns, deleteDuplicates: false, primaryKeyNumber: 2).Build() +
                         new QueryBuilder.SQLUpdate(rowsUpd).Build();

            return "SET NAMES 'utf8';" + Environment.NewLine + result + Environment.NewLine + "SET NAMES 'latin1';";
        }

        [BuilderMethod]
        public static string LocalesQuestObjectives()
        {
            if (Storage.LocalesQuestObjectives.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.locales_quest_objectives))
                return String.Empty;

            const string tableName = "quest_objectives_locale";

            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

            foreach (var localesQuestObjective in Settings.SQLOrderByKey ? Storage.LocalesQuestObjectives.OrderBy(blub => blub.Key).ToList() : Storage.LocalesQuestObjectives.ToList())
            {
                if (SQLDatabase.LocalesQuestObjectiveStores != null && SQLDatabase.LocalesQuestObjectiveStores.ContainsKey(Tuple.Create(localesQuestObjective.Key.Item1, localesQuestObjective.Key.Item2)))
                {
                    var row = new QueryBuilder.SQLUpdateRow();
                    var localesQuestObjectiveDB = SQLDatabase.LocalesQuestObjectiveStores[Tuple.Create(localesQuestObjective.Key.Item1, localesQuestObjective.Key.Item2)];

                    if (!Utilities.EqualValues(localesQuestObjectiveDB.QuestId, localesQuestObjective.Value.Item1.QuestId))
                        row.AddValue("QuestId", localesQuestObjective.Value.Item1.QuestId);

                    if (!Utilities.EqualValues(localesQuestObjectiveDB.StorageIndex, localesQuestObjective.Value.Item1.StorageIndex))
                        row.AddValue("StorageIndex", localesQuestObjective.Value.Item1.StorageIndex);

                    if (!Utilities.EqualValues(localesQuestObjectiveDB.Description, localesQuestObjective.Value.Item1.Description))
                        row.AddValue("Description", localesQuestObjective.Value.Item1.Description);

                    if (!Utilities.EqualValues(localesQuestObjectiveDB.VerifiedBuild, localesQuestObjective.Value.Item1.VerifiedBuild))
                        row.AddValue("VerifiedBuild", localesQuestObjective.Value.Item1.VerifiedBuild);

                    row.AddWhere("ID", localesQuestObjective.Key.Item1);
                    row.AddWhere("locale", localesQuestObjective.Key.Item2);

                    row.Table = tableName;

                    rowsUpd.Add(row);
                }
                else // insert new
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("ID", localesQuestObjective.Key.Item1);
                    row.AddValue("locale", localesQuestObjective.Key.Item2);

                    row.AddValue("QuestId", localesQuestObjective.Value.Item1.QuestId);
                    row.AddValue("StorageIndex", localesQuestObjective.Value.Item1.StorageIndex);
                    row.AddValue("Description", localesQuestObjective.Value.Item1.Description);

                    row.AddValue("VerifiedBuild", localesQuestObjective.Value.Item1.VerifiedBuild);

                    rowsIns.Add(row);
                }
            }

            var result = new QueryBuilder.SQLInsert(tableName, rowsIns, deleteDuplicates: false, primaryKeyNumber: 2).Build() +
                         new QueryBuilder.SQLUpdate(rowsUpd).Build();

            return "SET NAMES 'utf8';" + Environment.NewLine + result + Environment.NewLine + "SET NAMES 'latin1';";
        }
    }
}
