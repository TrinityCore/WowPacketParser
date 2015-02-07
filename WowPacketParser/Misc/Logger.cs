using System;
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

        public static void CheckForMissingValues<TEnum>(long rawValue)
        {
            if (!Settings.LogErrors || !typeof(TEnum).IsEnum || !Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute)))
                return;

            var key = typeof(TEnum).ToString().Replace("WowPacketParser.Enums.", "");

            // Remove all know values
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
                rawValue = rawValue & ~Convert.ToInt64(value, CultureInfo.InvariantCulture);

            if (rawValue == 0)
                return;

            long temp = 1;
            while (temp < rawValue)
            {
                if ((rawValue & temp) == temp)
                    AddEnumErrorLog(key, temp);
                temp <<= 2;
            }
        }

        private static void AddEnumErrorLog(string key, long rawValue)
        {
            var exists = EnumLogs.ContainsKey(key);
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

                var errors = "";
                foreach (var error in pair.Value)
                {
                    if (errors.Length > 0)
                        errors += ", ";

                    var str = "";
                    UnknownFlags enumFlag;
                    if (Enum.TryParse(error.ToString(CultureInfo.InvariantCulture), out enumFlag))
                        str = enumFlag.ToString();

                    errors += str;
                }

                Trace.WriteLine(string.Format("{0} has undefined flags: {1}", pair.Key, errors));
            }
        }
    }
}
