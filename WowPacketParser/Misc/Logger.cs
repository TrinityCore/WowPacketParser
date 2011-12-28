using System;
using System.Collections.Generic;

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

            if (Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
            {
               // TODO: Add missing values only, not raw values
               key = "[F] " + key;
            }

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
                var errors = "";
                foreach (var error in pair.Value)
                {
                    if (errors.Length > 0)
                        errors += ", ";
                    errors += error.ToString();
                }

                var flags = pair.Key.Contains("[F]");
                var key = flags ? pair.Key.Replace("[F] ", "") : pair.Key;
                var text = flags ? "flags contained in " : String.Empty;

                Console.WriteLine("{0} has undefined {1}values: {2}", key, text, errors);
            }
        }
    }
}
