using System.IO;
using PacketParser.DataStructures;

namespace PacketParser.Loading
{
    public static class Reader
    {
        public static IPacketReader GetReader(string fileName, string fileType)
        {
            IPacketReader reader = null;
            switch (fileType)
            {
                case "pkt":
                    reader = new BinaryPacketReader(fileName);
                    break;
                case "izi":
                    reader = new IzidorPacketReader(fileName);
                    break;
                case "kszor":
                    reader = new KSnifferZorReader(fileName);
                    break;
                case "tiawps":
                    reader = new SQLitePacketReader(fileName);
                    break;
                case "sniffitzt":
                    reader = new SniffitztReader(fileName);
                    break;
                case "kszack":
                    reader = new KSnifferZackReader(fileName);
                    break;
                case "newzor":
                    reader = new NewZorReader(fileName);
                    break;
                case "zor":
                    reader = new ZorReader(fileName);
                    break;
                case "wlp":
                    reader = new WlpReader(fileName);
                    break;
                default:
                {
                    var extension = Path.GetExtension(fileName);
                    switch (extension.ToLower())
                    {
                        case ".pkt":
                            reader = new BinaryPacketReader(fileName);
                            break;
                        case ".sqlite":
                            reader = new SQLitePacketReader(fileName);
                            break;
                        case ".izi":
                            reader = new IzidorPacketReader(fileName);
                            break;
                        default:
                            reader = new KSnifferZorReader(fileName);
                            break;
                    }
                    break;
                }
            }

            return reader;
        }
    }
}
