using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace WowPacketParser.Misc
{
    public static class Extensions
    {
        /// <summary>
        /// Convert bool to byte
        /// </summary>
        /// <param name="value">A boolean</param>
        /// <returns>A byte</returns>
        public static byte ToByte(this bool value)
        {
            return (byte)(value ? 1 : 0);
        }

        /// <summary>
        /// Returns true if flag exists in value (&)
        /// </summary>
        /// <param name="value">An enum, int, ...</param>
        /// <param name="flag">An enum, int, ...</param>
        /// <returns>A boolean</returns>
        public static bool HasAnyFlag(this IConvertible value, IConvertible flag)
        {
            var uFlag = flag.ToUInt64(null);
            var uThis = value.ToUInt64(null);

            return (uThis & uFlag) != 0;
        }

        /// <summary>
        /// Returns true if bit is set in value (&)
        /// </summary>
        /// <param name="value">An enum, int, ...</param>
        /// <param name="bit">An int</param>
        /// <returns>A boolean</returns>
        public static bool HasAnyFlagBit(this IConvertible value, IConvertible bit)
        {
            var uBit = bit.ToInt32(null);

            Contract.Assert(uBit >= 0 && uBit <= 63);

            var uFlag = (UInt64)(1 << uBit);
            var uThis = value.ToUInt64(null);

            return (uThis & uFlag) != 0;
        }

        /// <summary>
        /// Return true if our string is a substring of any filter (case insensitive)
        /// </summary>
        /// <param name="value">String</param>
        /// <param name="filters">List of strings</param>
        /// <returns>A boolean</returns>
        public static bool MatchesFilters(this string value, IEnumerable<string> filters)
        {
            // Note: IndexOf returns -1 if string was not found
            return filters.Any(filter => value.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) != -1);
        }

        /// <summary>
        /// Shows our hex representation of a packet
        /// </summary>
        /// <param name="packet">A packet</param>
        public static void AsHex(this Packet packet)
        {
            var n = Environment.NewLine;
            var hexDump = new StringBuilder();
            var length = packet.Length;
            var stream = packet.GetStream(0);

            var header = "|-------------------------------------------------|---------------------------------|" + n +
                         "| 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F | 0 1 2 3 4 5 6 7 8 9 A B C D E F |" + n +
                         "|-------------------------------------------------|---------------------------------|" + n;

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

            hexDump.Append("|-------------------------------------------------|---------------------------------|");

            packet.WriteLine(hexDump.ToString());
        }

        /// <summary>
        /// <para>Define the culture of the thread as CultureInfo.InvariantCulture</para>
        /// <remarks>This is required since new threads will have the culture of the machine (to be changed in .NET 4.5)</remarks>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts a timespan in a string (hh:mm:ss.ms)
        /// </summary>
        /// <param name="span">A timespan</param>
        /// <returns>A string</returns>
        public static string ToFormattedString(this TimeSpan span)
        {
            return string.Format("{0:00}:{1:00}:{2:00}.{3:000}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
        }

        /// <summary>
        /// <para>Converts an IList (array, list) to a tuple. Up to 14 elements</para>
        /// <remarks>1st value (0) of the IList is NOT used</remarks>
        /// </summary>
        /// <param name="col">The IList to be converted</param>
        /// <param name="count">Number of elements</param>
        /// <returns></returns>
        public static object ToTuple(this IList<object> col, int count)
        {
            if (count > col.Count)
                throw new ArgumentOutOfRangeException("col", col.Count,
                    "Number of elements of the IList needs to be higher or equal than count");

            // I still feel stupid...
            switch (count)
            {
                case 2:
                    return Tuple.Create(col[1]);
                case 3:
                    return Tuple.Create(col[1], col[2]);
                case 4:
                    return Tuple.Create(col[1], col[2], col[3]);
                case 5:
                    return Tuple.Create(col[1], col[2], col[3], col[4]);
                case 6:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5]);
                case 7:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6]);
                case 8:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7]);
                case 9:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], col[8]);
                case 10:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8]));
                case 11:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8], col[9]));
                case 12:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8], col[9], col[10]));
                case 13:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8], col[9], col[10], col[11]));
                case 14:
                    return Tuple.Create(col[1], col[2], col[3], col[4], col[5], col[6], col[7], Tuple.Create(col[8], col[9], col[10], col[11], col[12]));
                default:
                    throw new ArgumentOutOfRangeException("count", count, "Numbers of element in IList to Tuple not supported.");
            }
        }
    }
}
