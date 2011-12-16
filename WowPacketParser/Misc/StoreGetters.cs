using System;
using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Misc
{
    public static class StoreGetters
    {
        public static string GetDatabaseObjectName(StoreNameType type, int entry)
        {
            if (entry <= 0 || !SQLConnector.Enabled)
                return string.Empty;

            return string.Empty;
        }

        public static string GetName(StoreNameType type, int entry, bool withEntry = true)
        {
            if (type != StoreNameType.Map && entry == 0)
                return "0"; // map can be 0

            if (!SQLDatabase.NameStores.ContainsKey(type))
                return entry.ToString();

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

            return entry.ToString();
        }
    }
}
