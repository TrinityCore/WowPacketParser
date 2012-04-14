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
        public static IEnumerable<Packet> Read(string fileName)
        {
            IPacketReader reader;
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

            var packets = new LinkedList<Packet>();
            try
            {
                var packetNum = 0;
                while (reader.CanRead())
                {
                    var packet = reader.Read(packetNum, fileName);
                    if (packet == null)
                        continue;

                    if (packetNum == 0)
                    {
                        // determine build version based on date of first packet if not specified otherwise
                        if (ClientVersion.IsUndefined())
                            ClientVersion.SetVersion(packet.Time);
                    }

                    if (packetNum++ < Settings.FilterPacketNumLow)
                        continue;

                    // check for filters
                    var opcodeName = Opcodes.GetOpcodeName(packet.Opcode);

                    var add = true;
                    if (Settings.Filters.Length > 0)
                        add = opcodeName.MatchesFilters(Settings.Filters);
                    // check for ignore filters
                    if (add && Settings.IgnoreFilters.Length > 0)
                        add = !opcodeName.MatchesFilters(Settings.IgnoreFilters);

                    if (add)
                    {
                        packets.AddLast(packet);
                        if (Settings.FilterPacketsNum > 0 && packets.Count == Settings.FilterPacketsNum)
                            break;
                    }

                    if (Settings.FilterPacketNumHigh > 0 && packetNum > Settings.FilterPacketNumHigh)
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Data);
                Trace.WriteLine(ex.GetType());
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
            }
            finally
            {
                reader.Dispose();
            }

            return packets;
        }
    }
}
