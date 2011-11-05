using System;
using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public static class SQLDatabase
    {
        private const string UnitNameQuery = "SELECT entry, name FROM creature_template;";
        private const string GameObjectNameQuery = "SELECT entry, name FROM gameobject_template;";

        public static Dictionary<uint, string> UnitNames;
        public static Dictionary<uint, string> GameObjectNames;

        public static void GrabData()
        {
            if (!SQLConnector.Connected())
                throw new Exception("Cannot get DB data without an active DB connection.");

            // `creature_template`.`entry`, `creature_template`.`name`
            UnitNames = GetDict<uint, string>(UnitNameQuery);

            // `gameobject_template`.`id`, `gameobject_template`.`name`
            GameObjectNames = GetDict<uint, string>(GameObjectNameQuery);
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
