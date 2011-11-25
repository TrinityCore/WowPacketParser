using System;
using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    // Note: When adding more queries here, do not forget to update StoreGetters.cs
    public static class SQLDatabase
    {
        public static Dictionary<uint, string> UnitNames;
        public static Dictionary<uint, string> GameObjectNames;
        public static Dictionary<uint, string> ItemNames;
        public static Dictionary<uint, string> QuestNames;

        public static void GrabData()
        {
            if (!SQLConnector.Connected())
                throw new Exception("Cannot get DB data without an active DB connection.");

            const string unitNameQuery = "SELECT entry, name FROM creature_template;";
            const string gameObjectNameQuery = "SELECT entry, name FROM gameobject_template;";
            const string itemNameQuery = "SELECT entry, name FROM item_template;";
            const string questNameQuery = "SELECT Id, Title FROM quest_template;";

            // creature_template.entry, creature_template.name
            UnitNames = GetDict<uint, string>(unitNameQuery);

            // gameobject_template.entry, gameobject_template.name
            GameObjectNames = GetDict<uint, string>(gameObjectNameQuery);

            // item_template.entry, item_template.name
            ItemNames = GetDict<uint, string>(itemNameQuery);

            // item_template.entry, item_template.name
            QuestNames = GetDict<uint, string>(questNameQuery);
        }

        // Returns a dictionary from a DB query with two parameters (e.g <creature_entry, creature_name>)
        private static Dictionary<T, TK> GetDict<T, TK>(string query)
        {
            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null) // Not sure why I am doing this.
                    return null;    // Probably because I don't know
                                    // a better alternative

                return reader.GetDictionary<T, TK>();
            }
                
        }
    }
}
