using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.SQL.Builders
{
    public static class DB2
    {
        public static string BroadcastText()
        {
            if (Storage.BroadcastTexts.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.broadcast_text))
               return string.Empty;

            foreach (var broadcastText in Storage.BroadcastTexts)
                broadcastText.Value.Item1.ConvertToDBStruct();

            var entries = Storage.BroadcastTexts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, BroadcastText>(entries, "ID");

            return SQLUtil.CompareDicts(Storage.BroadcastTexts, templatesDb, StoreNameType.BroadcastText, "ID");
        }

        public static string CreatureDifficulty()
        {
            if (Storage.CreatureDifficultys.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            var entries = Storage.CreatureDifficultys.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, CreatureDifficulty>(entries);

            return SQLUtil.CompareDicts(Storage.CreatureDifficultys, templatesDb, StoreNameType.CreatureDifficulty);
        }

        public static string GameObjectDB2()
        {
            if (Storage.GameObjectTemplateDB2s.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_template))
                return string.Empty;

            var entries = Storage.GameObjectTemplateDB2s.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, GameObjectTemplateDB2>(entries);

            return SQLUtil.CompareDicts(Storage.GameObjectTemplateDB2s, templatesDb, StoreNameType.GameObject);
        }

        public static string GameObjectDB2Position()
        {
            if (Storage.GameObjectTemplatePositionDB2s.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_db2_position))
                return string.Empty;

            var entries = Storage.GameObjectTemplatePositionDB2s.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, GameObjectTemplatePositionDB2>(entries);

            return SQLUtil.CompareDicts(Storage.GameObjectTemplatePositionDB2s, templatesDb, StoreNameType.GameObject);
        }
    }
}
