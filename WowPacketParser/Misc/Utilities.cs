using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Utilities
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime GetDateTimeFromUnixTime(double unixTime)
        {
            return Epoch.AddSeconds(unixTime);
        }

        public static double GetUnixTimeFromDateTime(DateTime time)
        {
            return (time - Epoch).TotalSeconds;
        }

        public static byte[] HexStringToBinary(string data)
        {
            var bytes = new List<byte>();
            byte result;
            for (var i = 0; i < data.Length; i += 2)
                if (Byte.TryParse(data.Substring(i, 2), NumberStyles.HexNumber, null, out result))
                    bytes.Add(result);

            return bytes.ToArray();
        }

        public static string ByteArrayToHexString(byte[] data)
        {
            var str = String.Empty;
            for (var i = 0; i < data.Length; ++i)
                str += data[i].ToString("X2", CultureInfo.InvariantCulture);

            return str;
        }

        public static DateTime GetDateTimeFromGameTime(int packedDate)
        {
            var minute = packedDate & 0x3F;
            var hour = (packedDate >> 6) & 0x1F;
            // something = (packedDate >> 11) & 7;
            var day = (packedDate >> 14) & 0x3F;
            var month = (packedDate >> 20) & 0xF;
            var year = (packedDate >> 24) & 0x1F;
            // something = (packedDate >> 29) & 3;

            return new DateTime(2000, 1, 1).AddYears(year).AddMonths(month).AddDays(day).AddHours(hour).AddMinutes(minute);
        }

        public static StoreNameType ObjectTypeToStore(ObjectType type)
        {
            var result = StoreNameType.None;

            switch (type)
            {
                case ObjectType.Item:
                    result = StoreNameType.Item;
                    break;
                case ObjectType.Corpse:
                case ObjectType.Unit:
                    result = StoreNameType.Unit;
                    break;
                case ObjectType.Container: // ?
                case ObjectType.GameObject:
                    result = StoreNameType.GameObject;
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
        internal static void GetMemUse(string prefix, bool inKBs = false)
        {
            var process = Process.GetCurrentProcess();
            const int megabyte = 1048576;
            const int kilobyte = 8192;
            var bytesstr = inKBs ? "KB" : "MB";
            var bytes = inKBs ? kilobyte : megabyte;

            Trace.WriteLine(String.Format("Memory GC: {0,5}{3}, Process: {1,5}{3} - {2}",
                GC.GetTotalMemory(true) / bytes, process.PrivateMemorySize64 / bytes, prefix, bytesstr));
        }

        public static void SetUpListeners()
        {
            Trace.Listeners.Clear();

            using (var consoleListener = new ConsoleTraceListener(true))
                Trace.Listeners.Add(consoleListener);

            if (Settings.ParsingLog)
            {
                using (var fileListener = new TextWriterTraceListener(String.Format("parsing_log_{0}.txt", Path.GetRandomFileName())))
                {
                    fileListener.Name = "ConsoleMirror";
                    Trace.Listeners.Add(fileListener);
                }
            }

            Trace.AutoFlush = true;
        }

        public static bool GetFiles(ref List<string> files)
        {
            if (files.Count == 1 && files[0].Contains('*'))
            {
                try
                {
                    files = Directory.GetFiles(@".\", files[0]).ToList();
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
            {
                return Math.Abs((float)o1 - (float)o2) < 0.01;
            }

            if (o1 is double && o2 is double)
            {
                return Math.Abs((double)o1 - (double)o2) < 0.001;
            }

            // TODO: Simplify this
            // Notice that if one of the values is DBNull, DBNull == "" must return true
            if (o1 is string && !(o2 is string))
                return (string)o1 == Convert.ToString(o2);
            if (!(o1 is string) && o2 is string)
                return (string)o2 == Convert.ToString(o1);
            if (o1 is string)
                return o1.Equals(o2);

            // this still works if objects are booleans or enums
            return Convert.ToInt64(o1) == Convert.ToInt64(o2);
        }

        /// <summary>
        /// Get a list of fields and attributes from a type. Only fields with the
        /// specified attribute are returned.
        /// </summary>
        /// <typeparam name="T">Type (class/struct)</typeparam>
        /// <typeparam name="TK">Attribute</typeparam>
        /// <returns>A list of tuples where Item1 is FieldInfo and Item2 the corresponding attribute</returns>
        public static List<Tuple<FieldInfo, TK>> GetFieldsAndAttribute<T, TK>() where TK : Attribute
        {
            var fi = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
            if (fi.Length <= 0)
                return null;

            var list = new List<Tuple<FieldInfo, TK>>(fi.Length);

            foreach (var field in fi)
            {
                var attrs = /*(TK[])*/field.GetCustomAttributes(typeof(TK), false);
                if (attrs.Length <= 0)
                    continue;

                list.Add(Tuple.Create(field, (TK)attrs[0]));
            }

            return list;
        }
    }
}
