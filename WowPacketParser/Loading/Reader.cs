using System.IO;
using PacketParser.DataStructures;
using System.Collections.Generic;
using System;

namespace PacketParser.Loading
{
    public static class Reader
    {
        private static Dictionary<string, Type> _readers = GetReaders();

        public static Dictionary<string, Type> GetReaders()
        {
            var readers = new Dictionary<string, Type>();
            readers.Add("pkt", typeof(BinaryPacketReader));
            readers.Add("izi", typeof(IzidorPacketReader));
            readers.Add("kszor", typeof(KSnifferZorReader));
            readers.Add("tiawps", typeof(SQLitePacketReader));
            readers.Add("sniffitzt", typeof(SniffitztReader));
            readers.Add("kszack", typeof(KSnifferZackReader));
            readers.Add("newzor", typeof(NewZorReader));
            readers.Add("zor", typeof(ZorReader));
            readers.Add("wlp", typeof(WlpReader));
            return readers;
        }

        public static List<string> GetReaderTypes()
        {
            var list = new List<string>();
            foreach (var r in _readers)
            {
                list.Add(r.Key);
            }
            return list;
        }

        public static IPacketReader GetReader(string type, string fileName)
        {
            if (_readers.ContainsKey(type))
                return (IPacketReader)Activator.CreateInstance(_readers[type], fileName);
            return null;
        }

        public static string GetReaderTypeByFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            switch (extension.ToLower())
            {
                case ".pkt":
                    return "pkt";
                case ".sqlite":
                    return "tiawps";
                case ".izi":
                    return "izi";
                case ".bin":
                    return "kszor";
                default:
                    return null;
            }
        }
    }
}
