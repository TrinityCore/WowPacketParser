using System;
using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Filters
    {
        private static readonly Dictionary<StoreNameType, List<int>> NameStoreFilter = new();
        private static readonly Dictionary<StoreNameType, List<int>> NameStoreIgnoreFilter = new();

        public static void Initialize()
        {
            foreach (var filter in Settings.EntryFilters)
            {
                var elements = filter.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length < 2)
                    continue;

                StoreNameType type;
                if (!Enum.TryParse(elements[0], out type))
                    continue;

                var list = new List<int>();
                int element;
                for (var i = 1; i < elements.Length; ++i)
                    if (Int32.TryParse(elements[i], out element))
                        list.Add(element);
                if (list.Count > 0)
                    NameStoreFilter.Add(type, list);
            }

            foreach (var filter in Settings.IgnoreByEntryFilters)
            {
                var elements = filter.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length < 2)
                    continue;

                StoreNameType type;
                if (!Enum.TryParse(elements[0], out type))
                    continue;

                var list = new List<int>();
                int element;
                for (var i = 1; i < elements.Length; ++i)
                    if (Int32.TryParse(elements[i], out element))
                        list.Add(element);
                if (list.Count > 0)
                    NameStoreIgnoreFilter.Add(type, list);
            }

            foreach (var filter in Settings.Filters)
            {
                if (filter.StartsWith("-")) // ignore error checking for negative values
                    continue;

                if (Enum.GetNames(typeof (Opcode)).All(opcode => opcode.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) == -1))
                    Console.WriteLine("Warning: No opcode name matches filter \"" + filter + "\"");
            }

            foreach (var filter in Settings.IgnoreFilters)
            {
                if (filter.StartsWith("-")) // ignore error checking for negative values
                    continue;

                if (Enum.GetNames(typeof(Opcode)).All(opcode => opcode.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) == -1))
                    Console.WriteLine("Warning: No opcode name matches ignore filter \"" + filter + "\"");
            }
        }

        public static bool CheckFilter(StoreNameType type, int entry)
        {
            var result = true;

            if (NameStoreFilter.TryGetValue(type, out var filters))
                result = filters.Contains(entry);

            if (NameStoreIgnoreFilter.TryGetValue(type, out var ignoreFilters))
                result = !ignoreFilters.Contains(entry);

            return result;
        }

        public static bool CheckFilter(WowGuid guid)
        {
            var result = true;

            if (guid.HasEntry())
                result = CheckFilter(Utilities.ObjectTypeToStore(guid.GetObjectType()), (int)guid.GetEntry());

            return result;
        }

        public static bool CheckFilter(WowGuid128 guid)
        {
            var result = true;

            if (guid.GetObjectType() == ObjectType.Player || guid.HasEntry())
                result = CheckFilter(Utilities.ObjectTypeToStore(guid.GetObjectType()), (int)guid.GetEntry());

            return result;
        }
    }
}
