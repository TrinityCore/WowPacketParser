using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Utilities
    {
        private static ConcurrentDictionary<Type, Dictionary<FieldInfo, List<IAttribute>>> _typeCache = new();

        public static DateTime GetDateTimeFromUnixTime(long unixTime)
        {
            try
            {
                return DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime;
            }
            catch (ArgumentOutOfRangeException)
            {
                // weird values are sent sometimes that make no sense, clamp to valid range
                return unixTime < DateTimeOffset.MinValue.ToUnixTimeSeconds() ? DateTimeOffset.MinValue.UtcDateTime : DateTimeOffset.MaxValue.UtcDateTime;
            }
        }

        public static long GetUnixTimeFromDateTime(DateTime time)
        {
            return ((DateTimeOffset)DateTime.SpecifyKind(time, DateTimeKind.Utc)).ToUnixTimeSeconds();
        }

        public static string ByteArrayToHexTable(byte[] data, bool sh0rt = false, int offset = 0, bool noOffsetFirstLine = true)
        {
            var n = Environment.NewLine;

            var prefix = new string(' ', offset);

            var hexDump = new StringBuilder(noOffsetFirstLine ? "" : prefix);

            if (!sh0rt)
            {
                var header = "|-------------------------------------------------|---------------------------------|" + n +
                             "| 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F | 0 1 2 3 4 5 6 7 8 9 A B C D E F |" + n +
                             "|-------------------------------------------------|---------------------------------|" + n;

                hexDump.Append(header);
            }

            for (var i = 0; i < data.Length; i += 16)
            {
                var text = new StringBuilder();
                var hex = new StringBuilder(i == 0 ? "" : prefix);

                if (!sh0rt)
                    hex.Append("| ");

                for (var j = 0; j < 16; j++)
                {
                    if (j + i < data.Length)
                    {
                        var val = data[j + i];
                        hex.Append(data[j + i].ToString("X2"));

                        if (!sh0rt)
                            hex.Append(" ");

                        if (val >= 32 && val <= 127)
                            text.Append((char)val);
                        else
                            text.Append(".");

                        if (!sh0rt)
                            text.Append(" ");
                    }
                    else
                    {
                        hex.Append(sh0rt ? "  " : "   ");
                        text.Append(sh0rt ? " " : "  ");
                    }
                }

                hex.Append(sh0rt ? "|" : "| ");
                hex.Append(text);
                if (!sh0rt)
                    hex.Append("|");
                hex.Append(n);
                hexDump.Append(hex);
            }

            if (!sh0rt)
                hexDump.Append("|-------------------------------------------------|---------------------------------|");

            return hexDump.ToString();
        }

        public static DateTime GetDateTimeFromGameTime(int packedDate)
        {
            var minute = packedDate & 0x3F;
            var hour = (packedDate >> 6) & 0x1F;
            // var weekDay = (packedDate >> 11) & 7;
            var day = (packedDate >> 14) & 0x3F;
            var month = (packedDate >> 20) & 0xF;
            var year = (packedDate >> 24) & 0x1F;
            // var something2 = (packedDate >> 29) & 3; always 0

            return new DateTime(2000, 1, 1).AddYears(year).AddMonths(month).AddDays(day).AddHours(hour).AddMinutes(minute);
        }

        public static StoreNameType ObjectTypeToStore(ObjectType type)
        {
            StoreNameType result;

            switch (type)
            {
                case ObjectType.Item:
                    result = StoreNameType.Item;
                    break;
                case ObjectType.Player:
                    result = StoreNameType.Player;
                    break;
                case ObjectType.Corpse:
                case ObjectType.Unit:
                    result = StoreNameType.Unit;
                    break;
                case ObjectType.Container: // ?
                case ObjectType.GameObject:
                    result = StoreNameType.GameObject;
                    break;
                default:
                    result = StoreNameType.None;
                    break;
            }

            return result;
        }

        public static bool FileIsInUse(string fileName)
        {
            // If the file does not exists or does not return any exception
            // when trying to open it, we assume that it is safe to be written
            try
            {
                if (!File.Exists(fileName))
                    return false;

                File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None).Dispose();
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        [SecurityCritical]
        internal static void GetMemUse(string prefix)
        {
            Process process = Process.GetCurrentProcess();
            Trace.WriteLine($"Memory GC: {BytesToString(GC.GetTotalMemory(true)),5}, Process: {BytesToString(process.PrivateMemorySize64),5} - {prefix}");
        }

        public static void RemoveConfigOptions(ref List<string> files)
        {
            for (var i = 0; i < files.Count - 1; ++i)
            {
                if (files[i].StartsWith("--", StringComparison.CurrentCultureIgnoreCase))
                {
                    // remove value
                    files.RemoveAt(i + 1);
                    // remove option name
                    files.RemoveAt(i);
                    --i;
                    continue;
                }
                break;
            }
        }

        public static bool GetFiles(ref List<string> files)
        {
            if (files.Count == 1 && files[0].Contains('*'))
            {
                try
                {
                    files = Directory.GetFiles(@".", files[0]).ToList();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.GetType());
                    Trace.WriteLine(ex.Message);
                    Trace.WriteLine(ex.StackTrace);
                    return false;
                }
            }

            for (var i = 0; i < files.Count; ++i)
            {
                if (!File.Exists(files[i]))
                {
                    Trace.WriteLine("File " + files[i] + " was not found, removed.");
                    files.RemoveAt(i);
                    --i;
                }
            }

            if (files.Count == 0)
            {
                Trace.WriteLine("No files specified.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Compares two objects (values) with special cases for floats and strings
        /// </summary>
        /// <param name="o1">First object</param>
        /// <param name="o2">Second object</param>
        /// <returns>True if equal</returns>
        public static bool EqualValues(object o1, object o2)
        {
            if (o1 is float && o2 is float)
                return Math.Abs((float)o1 - (float)o2) < 0.01;

            if (o1 is double && o2 is double)
                return Math.Abs((double)o1 - (double)o2) < 0.001;

            if (o1 is float || o2 is double)
                return false;

            if (o1 is Blob blob1 && o2 is Blob blob2)
                return blob1.Data.SequenceEqual(blob2.Data);

            // Notice that if one of the values is DBNull, DBNull == "" must return true
            string str1 = o1 as string;
            string str2 = o2 as string;

            str1 = str1?.TrimEnd('\r', '\n', ' ');

            str2 = str2?.TrimEnd('\r', '\n', ' ');

            if (str1 != null && str2 == null)
                return str1 == Convert.ToString(o2);
            if (str1 == null && str2 != null)
                return str2 == Convert.ToString(o1);
            if (str1 != null)
                return str1 == str2;

            if (o1 is ulong && o2 is ulong)
                return Convert.ToUInt64(o1) == Convert.ToUInt64(o2);

            // this still works if objects are booleans or enums
            return Convert.ToInt64(o1) == Convert.ToInt64(o2);
        }

        /// <summary>
        /// Get a list of fields and attributes from a type. Only fields with the
        /// specified attribute are returned.
        /// </summary>
        /// <param name="remove">Remove fields without the specified attribute.</param>
        /// <typeparam name="T">Type (class/struct)</typeparam>
        /// <typeparam name="TK">Attribute</typeparam>
        /// <returns>A list of tuples where Item1 is FieldInfo and Item2 the corresponding attribute</returns>
        public static Dictionary<FieldInfo, List<IAttribute>> GetFieldsAndAttributes<T, TK>(bool remove = true) where TK : IAttribute
        {
            if (_typeCache.TryGetValue(typeof(T), out var dict))
                return dict;

            var fi = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
            if (fi.Length <= 0)
                return null;

            dict = new Dictionary<FieldInfo, List<IAttribute>>(fi.Length);

            foreach (FieldInfo field in fi)
            {
                var attrs = field.GetCustomAttributes(typeof(TK), false);
                if (remove && attrs.Length <= 0)
                    continue;

                dict.Add(field, ((IAttribute[]) attrs).ToList());
            }

            _typeCache[typeof(T)] = dict;

            return dict;
        }

        public static List<T> GetAttributes<T>(MemberInfo field) where T : Attribute
        {
            var list = new List<T>();

            var attrs = field.GetCustomAttributes(typeof(T), false);
            if (attrs.Length <= 0)
                return new List<T>();

            list.AddRange(attrs.Select(attr => (T)attr));
            return list;
        }

        /// <summary>
        /// Returns current date time to be used in our file names (sqls, parsing logs, ...)
        /// </summary>
        /// <returns></returns>
        public static string FormattedDateTimeForFiles()
        {
            return DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }

        /// <summary>
        /// Human-readable byte count
        /// </summary>
        /// <param name="byteCount">Number of bytes</param>
        /// <returns>String with byte suffix</returns>
        public static string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num) + suf[place];
        }


        /// <summary>
        /// Retrieves the values of an enumeration
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>Enumerable with the enum values</returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static ulong MAKE_PAIR64(uint l, uint h)
        {
            return (l | (ulong)h << 32);
        }

        public static uint PAIR64_HIPART(ulong x)
        {
            return (uint)((x >> 32) & 0x00000000FFFFFFFF);
        }

        public static uint PAIR64_LOPART(ulong x)
        {
            return (uint)(x & 0x00000000FFFFFFFF);
        }
    }
}
