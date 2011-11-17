using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;
using WowPacketParser.DBC.DBCStructures;

namespace WowPacketParser.Misc
{
    public static class Extensions
    {
        public static byte ToByte(this bool boolean)
        {
            return (byte)(boolean ? 1 : 0);
        }

        public static bool HasAnyFlag(this Enum value, Enum flag)
        {
            var uFlag = ((IConvertible)flag).ToUInt64(null);
            var uThis = ((IConvertible)value).ToUInt64(null);

            return (uThis & uFlag) != 0;
        }

        public static bool MatchesFilters(this string value, string[] filters)
        {
            foreach (var filter in filters)
                if (value.Contains(filter))
                    return true;

            return false;
        }

        public static Dictionary<T, TK> GetDictionary<T, TK>(this MySqlDataReader reader)
        {
            var dict = new Dictionary<T, TK>();

            while (reader.Read())
                dict.Add((T)reader.GetValue(0), (TK)reader.GetValue(1));

            return dict;
        }

        public static string AsHex(this Packet packet)
        {
            var n = Environment.NewLine;
            var hexDump = new StringBuilder();
            var length = packet.GetLength();
            var stream = packet.GetStream(0);

            var header = "|-------------------------------------------------|------------------" +
                         "---------------|" + n +
                         "| 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F | 0 1 2 3 4 5 6 7 8 9 A B C D E F |" +
                         n + "|-------------------------------------------------|------------------" +
                         "---------------|" + n;

            hexDump.Append(header);

            var end = length;
            for (var i = 0; i < end; i += 16)
            {
                var text = new StringBuilder();
                var hex = new StringBuilder();
                hex.Append("| ");

                for (var j = 0; j < 16; j++)
                {
                    if (j + i < end)
                    {
                        var val = stream[j + i];
                        hex.Append(stream[j + i].ToString("X2"));
                        hex.Append(" ");

                        if (val >= 32 && val <= 127)
                            text.Append((char)val);
                        else
                            text.Append(".");

                        text.Append(" ");
                    }
                    else
                    {
                        hex.Append("   ");
                        text.Append("  ");
                    }
                }

                hex.Append("| ");
                hex.Append(text + "|");
                hex.Append(n);
                hexDump.Append(hex.ToString());
            }

            hexDump.Append("|-------------------------------------------------|------------------" +
                           "---------------|");

            return hexDump.ToString();
        }

        public static ParallelQuery<TSource> SetCulture<TSource>(this ParallelQuery<TSource> source)
        {
            SetCulture(CultureInfo.InvariantCulture);
            return source
                .Select(
                    item =>
                        {
                            SetCulture(CultureInfo.InvariantCulture);
                            return item;
                        });
        }

        private static void SetCulture(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }
    }
}
