using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Logger
    {
        private static readonly Dictionary<string, List<long>> EnumLogs =
            new Dictionary<string, List<long>>();

        private static readonly ConcurrentDictionary<Type, long> KnownEnumFlags = new ConcurrentDictionary<Type, long>();

        public static void CheckForMissingValues<TEnum>(long rawValue)
        {
            if (!Settings.LogErrors || !typeof(TEnum).IsEnum || !Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute)))
                return;

            string key = typeof(TEnum).ToString().Replace("WowPacketParser.Enums.", "");

            // Remove all known values
            rawValue &= ~KnownEnumFlags.GetOrAdd(typeof(TEnum), type =>
            {
                long knownValues = 0;
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
                    knownValues |= Convert.ToInt64(value, CultureInfo.InvariantCulture);

                return knownValues;
            });

            if (rawValue == 0)
                return;

            long temp = 1;
            while (temp <= rawValue)
            {
                if ((rawValue & temp) == temp)
                    AddEnumErrorLog(key, temp);
                temp <<= 1;
            }
        }

        private static void AddEnumErrorLog(string key, long rawValue)
        {
            bool exists = EnumLogs.ContainsKey(key);
            var list = exists ? EnumLogs[key] : new List<long>();

            if (list.Contains(rawValue))
                return;

            list.Add(rawValue);
            if (!exists)
                EnumLogs.Add(key, list);
        }

        public static void WriteErrors()
        {
            Trace.WriteLine(Environment.NewLine);
            foreach (var pair in EnumLogs)
            {
                pair.Value.Sort();

                string errors = "";
                foreach (long error in pair.Value)
                {
                    if (errors.Length > 0)
                        errors += ", ";

                    string str = "";
                    UnknownFlags enumFlag;
                    if (Enum.TryParse(error.ToString(CultureInfo.InvariantCulture), out enumFlag))
                        str = enumFlag.ToString();

                    errors += str;
                }

                Trace.WriteLine($"{pair.Key} has undefined flags: {errors}");
            }
        }
    }
}
