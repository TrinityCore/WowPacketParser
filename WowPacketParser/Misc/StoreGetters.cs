using System;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Misc
{
    public static class StoreGetters
    {
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
    }
}
