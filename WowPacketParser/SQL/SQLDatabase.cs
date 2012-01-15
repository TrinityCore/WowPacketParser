using System;
using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.SQL
{
    public static class SQLDatabase
    {
        public static readonly Dictionary<StoreNameType, Dictionary<int, string>> NameStores =
            new Dictionary<StoreNameType, Dictionary<int, string>>();

        private static readonly StoreNameType[] _objectTypes = new[]
        {
            StoreNameType.Spell,
            StoreNameType.Map,
            StoreNameType.LFGDungeon,
            StoreNameType.Battleground,
            StoreNameType.Unit,
            StoreNameType.GameObject,
            StoreNameType.Item,
            StoreNameType.Quest,
            StoreNameType.Zone,
            StoreNameType.Area,
            StoreNameType.Player
        };

        public static void GrabData()
        {
            if (!SQLConnector.Connected())
                throw new Exception("Cannot get DB data without an active DB connection.");

            foreach (var objectType in _objectTypes)
                NameStores.Add(objectType, GetDict<int, string>(string.Format("SELECT `Id`, `Name` FROM `ObjectNames` WHERE `ObjectType`='{0}';", objectType)));
        }

        // Returns a dictionary from a DB query with two parameters (e.g <creature_entry, creature_name>)
        private static Dictionary<T, TK> GetDict<T, TK>(string query)
        {
            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null)
                    return null;

                var dict = new Dictionary<T, TK>();

                while (reader.Read())
                    dict.Add((T)reader.GetValue(0), (TK)reader.GetValue(1));

                return dict;
            }
                
        }
    }
}
