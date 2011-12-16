using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    // Note: When adding more queries here, do not forget to update StoreGetters.cs
    public static class SQLDatabase
    {
        public static readonly Dictionary<StoreNameType, Dictionary<int, string>> NameStores =
            new Dictionary<StoreNameType, Dictionary<int, string>>();

        private static readonly StoreNameType[] ObjectTypes = new StoreNameType[]
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
                                                       StoreNameType.Area
                                                   };

        public static void GrabData()
        {
            if (!SQLConnector.Connected())
                throw new Exception("Cannot get DB data without an active DB connection.");

            foreach (var objectType in ObjectTypes)
                NameStores.Add(objectType, GetDict<int, string>(string.Format("SELECT `Id`, `Name` FROM `ObjectNames` WHERE `ObjectType`='{0}';", objectType)));
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
