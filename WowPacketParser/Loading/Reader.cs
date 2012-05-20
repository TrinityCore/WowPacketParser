using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public static class Reader
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "reader is disposed in the finally block.")]
        public static IPacketReader GetReader(string fileName)
        {
            IPacketReader reader = null;
            switch (Settings.PacketFileType)
            {
                case "pkt":
                    reader = new BinaryPacketReader(fileName);
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
