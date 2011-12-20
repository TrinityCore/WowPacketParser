using System;
using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Filters
    {
        public static readonly Dictionary<StoreNameType, List<int>> NameStores =
            new Dictionary<StoreNameType, List<int>>();

        public static void Initialize(string config)
        {
            string[] filters = config.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var filter in filters)
            {
                string[] elements = filter.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length < 2)
                    continue;

                StoreNameType type;
                if (!Enum.TryParse<StoreNameType>(elements[0], out type))
                    continue;

                List<int> list = new List<int>();
                int element = 0;
                for (var i = 1; i < elements.Length; ++i)
                    if (Int32.TryParse(elements[i], out element))
                        list.Add(element);
                if (list.Count > 0)
                    NameStores.Add(type, list);
            }
        }

        public static bool CheckFilter(StoreNameType type, int entry)
        {
            bool result = true;
            List<int> filters;

            if (NameStores.TryGetValue(type, out filters))
                result = !filters.Contains(entry);

            return result;
        }

        public static bool CheckFilter(Guid guid)
        {
            bool result = true;

            if (guid.HasEntry())
                result = Filters.CheckFilter(Utilities.ObjectTypeToStore(guid.GetObjectType()), (int)guid.GetEntry());

            return result;
        }
    }
}
