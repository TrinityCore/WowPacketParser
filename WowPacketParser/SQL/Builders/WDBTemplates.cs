using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
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

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            var entries = Storage.QuestTemplates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, QuestTemplate>(entries, "Id");

            return SQLUtil.CompareDicts(Storage.QuestTemplates, templatesDb, StoreNameType.Quest, "Id");
        }

        public static string Npc()
        {
            if (Storage.UnitTemplates.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            var entries = Storage.UnitTemplates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, UnitTemplate>(entries);

            return SQLUtil.CompareDicts(Storage.UnitTemplates, templatesDb, StoreNameType.Unit);
        }

        public static string NpcName()
        {
            var result = "";

            if (Storage.UnitTemplates.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            const string tableName = "creature_template";

            if (SQLConnector.Enabled)
            {
                var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();

                foreach (var npcName in Storage.UnitTemplates)
                {
                    var query = string.Format("SELECT name FROM {0}.creature_template WHERE entry={1};",
                        Settings.TDBDatabase, npcName.Key);

                    using (var reader = SQLConnector.ExecuteQuery(query))
                    {
                        if (reader.HasRows) // possible update
                        {
                            while (reader.Read())
                            {
                                var row = new QueryBuilder.SQLUpdateRow();

                                if (!Utilities.EqualValues(reader.GetValue(0), npcName.Value.Item1.Name))
                                    row.AddValue("name", npcName.Value.Item1.Name);

                                if (Utilities.EqualValues(reader.GetValue(0), npcName.Value.Item1.Name) && npcName.Value.Item1.femaleName != null)
                                    row.AddValue("femaleName", npcName.Value.Item1.femaleName);

                                row.AddWhere("entry", npcName.Key);

                                row.Table = tableName;

                                if (row.ValueCount != 0)
                                    rowsUpd.Add(row);
                            }
                        }
                    }
                }

                result += new QueryBuilder.SQLUpdate(rowsUpd).Build();
            }

            return result;
        }

        public static string GameObject()
        {
            if (Storage.GameObjectTemplates.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_template))
                return string.Empty;

            var entries = Storage.GameObjectTemplates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, GameObjectTemplate>(entries);

            return SQLUtil.CompareDicts(Storage.GameObjectTemplates, templatesDb, StoreNameType.GameObject);
        }

        public static string Item()
        {
            if (Storage.ItemTemplates.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.item_template))
                return string.Empty;

            var entries = Storage.ItemTemplates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, ItemTemplate>(entries);

            return SQLUtil.CompareDicts(Storage.ItemTemplates, templatesDb, StoreNameType.Item);
        }

        public static string PageText()
        {
            if (Storage.PageTexts.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.page_text))
                return string.Empty;

            var entries = Storage.PageTexts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, PageText>(entries);

            return SQLUtil.CompareDicts(Storage.PageTexts, templatesDb, StoreNameType.PageText);
        }

        public static string NpcText()
        {
            if (!Storage.NpcTexts.IsEmpty())
            {
                if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.npc_text))
                    return string.Empty;

                foreach (var npcText in Storage.NpcTexts)
                    npcText.Value.Item1.ConvertToDBStruct();

                var entries = Storage.NpcTexts.Keys();
                var templatesDb = SQLDatabase.GetDict<uint, NpcText>(entries, "ID");

                return SQLUtil.CompareDicts(Storage.NpcTexts, templatesDb, StoreNameType.NpcText, "ID");
            }
            else if (!Storage.NpcTextsMop.IsEmpty())
            {

                if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.npc_text))
                    return string.Empty;

                foreach (var npcText in Storage.NpcTextsMop)
                    npcText.Value.Item1.ConvertToDBStruct();

                var entries = Storage.NpcTextsMop.Keys();
                var templatesDb = SQLDatabase.GetDict<uint, NpcTextMop>(entries, "ID");

                return SQLUtil.CompareDicts(Storage.NpcTextsMop, templatesDb, StoreNameType.NpcText, "ID");
            }
            else
                return String.Empty;
        }
    }
}
