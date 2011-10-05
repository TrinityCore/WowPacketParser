using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using WowPacketParser.DBC.DBCStructures;
using System.Configuration;

namespace WowPacketParser.DBC.DBCStore
{
    public static class DBC
    {
        public const int ClientVersion = 12340;
        public const int MaxDBCLocale = 16;

        public static bool Enabled()
        {
            var b = ConfigurationManager.AppSettings["DBCEnabled"];
            return b.Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string DBCPath
        {
            get
            {
                return ConfigurationManager.AppSettings["DBCPath"];
            }
        }

        public static Dictionary<uint, SpellEntry> Spell;
        public static Dictionary<uint, MapEntry> Map;
        public static Dictionary<uint, LFGDungeonsEntry> LFGDungeons;
        public static Dictionary<uint, BattlemasterListEntry> BattlemasterList;

        public static Dictionary<uint, string> SpellStrings = new Dictionary<uint, string>();
        public static Dictionary<uint, string> MapStrings = new Dictionary<uint, string>();
        public static Dictionary<uint, string> LFGDungeonsStrings = new Dictionary<uint, string>();
        public static Dictionary<uint, string> BattlemasterListStrings = new Dictionary<uint, string>();

        public static unsafe T ReadStruct<T>(this BinaryReader reader) where T : struct
        {
            byte[] rawData = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            T returnObject = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));

            handle.Free();

            return returnObject;
        }

        public static string ReadCString(this BinaryReader reader)
        {
            byte num;
            var temp = new List<byte>();

            while ((num = reader.ReadByte()) != 0)
                temp.Add(num);

            return Encoding.UTF8.GetString(temp.ToArray());
        }
    }
}
