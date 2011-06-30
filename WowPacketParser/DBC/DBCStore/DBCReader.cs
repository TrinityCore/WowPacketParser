using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace WowPacketParser.DBC.DBCStore
{
    public static class DBCReader
    {
        public static unsafe Dictionary<uint, T> ReadDBC<T>(Dictionary<uint, string> strDict) where T : struct
        {
            Contract.Requires(DBC.DBCPath != String.Empty);
            Contract.Requires(DBC.DBCPath != null);

            var dict = new Dictionary<uint, T>();
            string fileName = Path.Combine(DBC.DBCPath, typeof(T).Name + ".dbc").Replace("Entry", String.Empty);

            using (var reader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read), Encoding.UTF8))
            {
                if (!File.Exists(fileName))
                    throw new FileNotFoundException();
                // read dbc header
                var header = reader.ReadStruct<DbcHeader>();
                var size = Marshal.SizeOf(typeof(T));

                if (!header.IsDBC)
                    throw new Exception(fileName + " is not DBC files!");

                if (header.RecordSize != size)
                    throw new Exception(string.Format("Size of row in DBC file ({0}) != size of DBC struct ({1}) in DBC: {2}", header.RecordSize, size, fileName));

                // read dbc data
                for (var r = 0; r < header.RecordsCount; ++r)
                {
                    var key = reader.ReadUInt32();
                    reader.BaseStream.Position -= 4;

                    var T_entry = reader.ReadStruct<T>();

                    dict.Add(key, T_entry);
                }

                // read dbc strings
                if (strDict != null)
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        var offset = (uint)(reader.BaseStream.Position - header.StartStringPosition);
                        var str = reader.ReadCString();
                        strDict.Add(offset, str);
                    }
                }
            }
            return dict;
        }

        // TODO: MOVE, should be outside Packet.cs
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
