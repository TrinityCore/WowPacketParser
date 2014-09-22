﻿using System;
using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Filters
    {
        private static readonly Dictionary<StoreNameType, List<int>> NameStores =
            new Dictionary<StoreNameType, List<int>>();

        public static void Initialize()
        {
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
                    NameStores.Add(type, list);
            }
        }

        public static bool CheckFilter(StoreNameType type, int entry)
        {
            var result = true;
            List<int> filters;

            if (NameStores.TryGetValue(type, out filters))
                result = !filters.Contains(entry);

            return result;
        }

        public static bool CheckFilter(WowGuid guid)
        {
            var result = true;

            if (guid.HasEntry())
                result = CheckFilter(Utilities.ObjectTypeToStore(guid.GetObjectType()), (int)guid.GetEntry());

            return result;
        }
    }
}
