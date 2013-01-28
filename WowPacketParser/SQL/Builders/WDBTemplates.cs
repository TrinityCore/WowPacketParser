using System;
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
            if (Storage.NpcTexts.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.npc_text))
                return string.Empty;

            foreach (var npcText in Storage.NpcTexts)
                npcText.Value.Item1.ConvertToDBStruct();

            var entries = Storage.NpcTexts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, NpcText>(entries, "ID");

            return SQLUtil.CompareDicts(Storage.NpcTexts, templatesDb, StoreNameType.NpcText, "ID");
        }
    }
}
