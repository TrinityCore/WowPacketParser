using System;
using System.Collections.Concurrent;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Misc
{
    public static class StoreGetters
    {
        public static readonly ConcurrentDictionary<Guid, string> NameDict = new ConcurrentDictionary<Guid, string>();

        public static string GetName(StoreNameType type, int entry, bool withEntry = true)
        {
            if (!SQLConnector.Enabled)
                return entry.ToString(CultureInfo.InvariantCulture);

            if (type != StoreNameType.Map && entry == 0)
                return "0"; // map can be 0

            if (!SQLDatabase.NameStores.ContainsKey(type))
                return entry.ToString(CultureInfo.InvariantCulture);

            string name;
            if (!SQLDatabase.NameStores[type].TryGetValue(entry, out name))
                if (!withEntry)
                    return "-Unknown-";

            if (!String.IsNullOrEmpty(name))
            {
                if (withEntry)
                    return entry + " (" + name + ")";
                return name;
            }

            return entry.ToString(CultureInfo.InvariantCulture);
        }

        public static void AddName(Guid guid, string name)
        {
            NameDict.TryAdd(guid, name);
        }

        public static string GetName(Guid guid)
        {
            string name;

            if (NameDict.TryGetValue(guid, out name))
                return name;

            return null;
        }
    }
}
