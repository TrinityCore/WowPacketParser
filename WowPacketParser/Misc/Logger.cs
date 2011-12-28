using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Logger
    {
        public static bool Enabled = Settings.GetBoolean("LogErrors", false);

        private static readonly Dictionary<string, List<long>> enumLogs =
            new Dictionary<string, List<long>>();

        public static void CheckForMissingValues<T>(long rawValue)
        {
            if (!Enabled || !typeof(T).IsEnum)
                return;

            var key = typeof(T).ToString().Replace("WowPacketParser.Enums.", "");

            // Remove all know values
            foreach (T value in Enum.GetValues(typeof(T)))
                rawValue = rawValue & ~Convert.ToInt64(value, CultureInfo.InvariantCulture);

            if (rawValue == 0)
                return;

            if (Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
            {
                key = "[F] " + key;
                long temp = 1;
                while (temp < rawValue)
                {
                    if ((rawValue & temp) == temp)
                        AddEnumErrorLog(key, temp);
                    temp <<= 2;
                }
            }
            else
                AddEnumErrorLog(key, rawValue);
        }

        private static void AddEnumErrorLog(string key, long rawValue)
        {
            List<long> list;
            if (enumLogs.ContainsKey(key))
                list = enumLogs[key];
            else
                list = new List<long>();

            if (list.Contains(rawValue))
                return;

            list.Add(rawValue);
            enumLogs.Add(key, list);
        }

        public static void WriteErrors()
        {
            if (!Enabled)
                return;

            Console.WriteLine();
            foreach (var pair in enumLogs)
            {
                pair.Value.Sort();
                var flags = pair.Key.Contains("[F]");
                var key = flags ? pair.Key.Replace("[F] ", "") : pair.Key;

                var errors = "";
                foreach (var error in pair.Value)
                {
                    if (errors.Length > 0)
                        errors += ", ";

                    var str = "";
                    if (flags)
                    {
                        UnknownFlags enumFlag;
                        if (Enum.TryParse<UnknownFlags>(error.ToString(), out enumFlag))
                            str = enumFlag.ToString();
                    }
                    else
                        str = error.ToString();
                    errors += str;
                }

                var text = flags ? "flags" : "values";

                Console.WriteLine("{0} has undefined {1}: {2}", key, text, errors);
            }
        }
    }
}
