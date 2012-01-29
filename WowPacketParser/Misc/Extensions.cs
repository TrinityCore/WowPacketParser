using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace WowPacketParser.Misc
{
    public static class Extensions
    {
        public static byte ToByte(this bool boolean)
        {
            return (byte)(boolean ? 1 : 0);
        }

        public static bool HasAnyFlag(this IConvertible value, IConvertible flag)
        {
            var uFlag = flag.ToUInt64(null);
            var uThis = value.ToUInt64(null);

            return (uThis & uFlag) != 0;
        }

        public static bool MatchesFilters(this string value, IEnumerable<string> filters)
        {
            // Return true if our string is a substring of any filter (case insensitive)
            // Note: IndexOf returns -1 if string was not found
            return filters.Any(filter => value.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) != -1);
        }

        public static void AsHex(this Packet packet)
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

            packet.Writer.WriteLine(hexDump.ToString());
        }

        public static void AsBinary(this Packet packet)
        {
            var n = Environment.NewLine;
            var hexDump = new StringBuilder();
            var length = packet.GetLength();
            var stream = packet.GetStream(0);

            var header = "|-------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------|" + n +
                         "| 01234567 01234567 01234567 01234567 01234567 01234567 01234567 01234567 01234567 01234567 01234567 01234567 01234567 01234567 01234567 01234567 | 0 1 2 3 4 5 6 7 8 9 A B C D E F |" + n +
                         "|-------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------|" + n;

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

                        hex.Append(Convert.ToString(stream[j + i], 2).PadLeft(8, '0'));
                        hex.Append(" ");

                        if (val >= 32 && val <= 127)
                            text.Append((char)val);
                        else
                            text.Append(".");

                        text.Append(" ");
                    }
                    else
                    {
                        hex.Append("         ");
                        text.Append("  ");
                    }
                }

                hex.Append("| ");
                hex.Append(text + "|");
                hex.Append(n);
                hexDump.Append(hex.ToString());
            }

            hexDump.Append("|-------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------|");

            packet.Writer.WriteLine(hexDump.ToString());
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

        public static string ToFormattedString(this TimeSpan span)
        {
            return string.Format("{0:00}:{1:00}:{2:00}.{3:000}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
        }
    }
}
