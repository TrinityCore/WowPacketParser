using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace WowPacketParser.DBC.DBCStore
{
    public static class DBC
    {
        public const int ClientVersion = 12340;
        public const int MaxDBCLocale = 16;

        public static string DBCPath
        {
            get { return "./"; }
        }

        public static unsafe T ReadStruct<T>(this BinaryReader reader) where T : struct
        {
            var rawData = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

            var handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
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
