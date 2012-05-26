using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    public static class WDBTemplates
    {
        public static string Quest()
        {
            if (Storage.QuestTemplates.IsEmpty())
                return String.Empty;

            if (Storage.QuestTemplates.IsEmpty())
                return string.Empty;

            var entries = Storage.QuestTemplates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, QuestTemplate>(entries, "Id");

            return SQLUtil.CompareDicts(Storage.QuestTemplates, templatesDb, StoreNameType.Quest);
        }

        public static string Npc()
        {
            if (Storage.UnitTemplates.IsEmpty())
                return String.Empty;

            var entries = Storage.UnitTemplates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, UnitTemplate>(entries);

            return SQLUtil.CompareDicts(Storage.UnitTemplates, templatesDb, StoreNameType.Unit);
        }

        public static string GameObject()
        {
            if (Storage.GameObjectTemplates.IsEmpty())
                return String.Empty;

            var entries = Storage.GameObjectTemplates.Keys();
            var tempatesDb = SQLDatabase.GetDict<uint, GameObjectTemplate>(entries);

            return SQLUtil.CompareDicts(Storage.GameObjectTemplates, tempatesDb, StoreNameType.GameObject);
        }

        public static string PageText()
        {
            if (Storage.PageTexts.IsEmpty())
                return String.Empty;

            var entries = Storage.PageTexts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, PageText>(entries);

            return SQLUtil.CompareDicts(Storage.PageTexts, templatesDb, StoreNameType.PageText);
        }

        public static string NpcText()
        {
            if (Storage.NpcTexts.IsEmpty())
                return String.Empty;

            // Not TDB structure
            const string tableName = "npc_text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcTextPair in Storage.NpcTexts)
            {
                var row = new QueryBuilder.SQLInsertRow();
                var npcText = npcTextPair.Value.Item1;

                row.AddValue("Id", npcTextPair.Key);

                for (var i = 0; i < npcText.Probabilities.Length; i++)
                    row.AddValue("Probability" + (i + 1), npcText.Probabilities[i]);

                for (var i = 0; i < npcText.Texts1.Length; i++)
                    row.AddValue("Text1_" + (i + 1), npcText.Texts1[i]);

                for (var i = 0; i < npcText.Texts2.Length; i++)
                    row.AddValue("Text2_" + (i + 1), npcText.Texts2[i]);

                for (var i = 0; i < npcText.Languages.Length; i++)
                    row.AddValue("Language" + (i + 1), npcText.Languages[i]);

                for (var i = 0; i < npcText.EmoteDelays[0].Length; i++)
                    for (var j = 0; j < npcText.EmoteDelays[1].Length; j++)
                        row.AddValue("EmoteDelay" + (i + 1) + "_" + (j + 1), npcText.EmoteDelays[i][j]);

                for (var i = 0; i < npcText.EmoteIds[0].Length; i++)
                    for (var j = 0; j < npcText.EmoteIds[1].Length; j++)
                        row.AddValue("EmoteId" + (i + 1) + "_" + (j + 1), npcText.EmoteDelays[i][j]);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }
    }
}
