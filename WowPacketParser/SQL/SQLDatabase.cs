using System;
using System.Collections.Generic;
using System.Data;
using WowPacketParser.Enums;

namespace WowPacketParser.SQL
{
    public static class SQLDatabase
    {
        public static readonly Dictionary<StoreNameType, Dictionary<int, string>> NameStores =
            new Dictionary<StoreNameType, Dictionary<int, string>>();

        private static readonly StoreNameType[] ObjectTypes = new[]
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

        public static void GrabNameData()
        {
            if (!SQLConnector.Connected())
                throw new DataException("Cannot get DB data without an active DB connection.");

            foreach (var objectType in ObjectTypes)
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

        // Returns a dictionary from a DB query with any number of parameters
        public static Dictionary<T, object> GetDict<T>(string query)
        {
            using (var reader = SQLConnector.ExecuteQuery(query))
            {
                if (reader == null)
                    return null;

                var dict = new Dictionary<T, object>();

                while (reader.Read())
                {
                    var pk = (T)reader.GetValue(0);

                    var objs = new object[30];
                    var fieldCount = reader.GetValues(objs);
                    var obj = objs.ToTuple(fieldCount);

                    dict.Add(pk, obj);
                }

                return dict;
            }
        }

        private static object ToTuple(this IList<object> col, int count)
        {
            // I feel stupid...
            switch (count)
            {
                case 2:
                    return Tuple.Create(col[1]);
                case 3:
                    return Tuple.Create(col[1], col[2]);
                case 4:
                    return Tuple.Create(col[1], col[2], col[3]);
                case 5:
                    return Tuple.Create(col[1], col[2], col[3], col[4]);
                case 6:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6]);
                case 8:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7]);
                case 9:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], col[7]);
                case 10:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8]));
                case 11:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8], col[9]));
                case 12:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8], col[9], col[10]));
                case 13:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8], col[9], col[10], col[11]));
                case 14:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8], col[9], col[10], col[11], col[12]));
                default:
                    throw new ArgumentOutOfRangeException("count", count, "Can't convert array to tuple.");
            }
        }
    }
}
