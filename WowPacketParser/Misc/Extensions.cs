using System;
using System.Collections.Concurrent;
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

            var uFlag = 1UL << uBit;
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
                hexDump.Append(hex);
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

        public static void Clear<T>(this ConcurrentBag<T> bag)
        {
            T t;
            while (bag.Count > 0)
                bag.TryTake(out t);
        }

        /// <summary>
        /// Compare two dictionaries
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionaries</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionaries</typeparam>
        /// <param name="first">First dictionary</param>
        /// <param name="second">Second dictionary</param>
        /// <returns>true if dictionaries are equal, false otherwise</returns>
        public static bool DictionaryEqual<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            if (first == second) return true;
            if ((first == null) || (second == null)) return false;
            if (first.Count != second.Count) return false;

            var comparer = EqualityComparer<TValue>.Default;

            foreach (var kvp in first)
            {
                TValue secondValue;
                if (!second.TryGetValue(kvp.Key, out secondValue)) return false;
                if (!comparer.Equals(kvp.Value, secondValue)) return false;
            }
            return true;
        }

        /// <summary>
        /// Flattens an IEnumerable
        /// Example:
        /// [1, 2, [3, [4]], 5] -> [1, 2, 3, 4, 5]
        /// </summary>
        /// <typeparam name="T">Type of each object</typeparam>
        /// <param name="values">Input IEnumerable</param>
        /// <returns>Flatten result</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> values)
        {
            var list = values.ToList();

            while (list.Any(o => o is IEnumerable<T>))
            {
                for (var i = 0; i < list.Count; i++)
                {
                    var arr = list[i] as IEnumerable<T>;
                    if (arr == null) continue;
                    var arrList = arr.ToList();
                    list.RemoveAt(i);

                    list.InsertRange(i, arrList);
                    i += arrList.Count - 1;
                }
            }

            return list;
        }
    }
}
