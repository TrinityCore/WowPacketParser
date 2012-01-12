using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Loading
{
    public static class Reader
    {
        public static ICollection<Packet> Read(SniffFileInfo fileInfo)
        {
            bool summary = Settings.DumpFormat == DumpFormatType.SummaryHeader;
            var packets = new List<Packet>();
            var packetNum = 0;
            var fileName = fileInfo.FileName;

            var extension = Path.GetExtension(fileName);
            if (extension == null)
                throw new IOException("Invalid file type");

            IPacketReader reader;
            switch (extension.ToLower())
            {
                case ".bin":
                    reader = new BinaryPacketReader(SniffType.Bin, fileName, Encoding.ASCII);
                    break;
                case ".pkt":
                    reader = new BinaryPacketReader(SniffType.Pkt, fileName, Encoding.ASCII);
                    break;
                case ".sqlite":
                    reader = new SQLitePacketReader(fileName);
                    break;
                default:
                    throw new IOException("Invalid file type");
            }

            int firstPacketBuild = 0;
            Packet parsingPacket = null; // For debugging
            try
            {
                while (reader.CanRead())
                {
                    if (packetNum != 0)
                        fileInfo.Build = firstPacketBuild;

                    var packet = reader.Read(packetNum, fileInfo);
                    if (packet == null)
                        continue;

                    parsingPacket = packet;

                    if (packetNum == 0)
                    {
                        // determine build version based on date of first packet if not specified otherwise
                        if (ClientVersion.IsUndefined())
                            ClientVersion.SetVersion(packet.Time);

                        firstPacketBuild = ClientVersion.GetBuildInt();
                    }

                    if (++packetNum < Settings.FilterPacketNumLow)
                        continue;

                    // check for filters
                    var add = true;

                    var opcodeName = Opcodes.GetOpcodeName(packet.Opcode);

                    if (Settings.Filters.Length > 0)
                        add = opcodeName.MatchesFilters(Settings.Filters);
                    // check for ignore filters
                    if (add && Settings.IgnoreFilters.Length > 0)
                        add = !opcodeName.MatchesFilters(Settings.IgnoreFilters);

                    if (add && summary)
                    {
                        add = packets.Find(found => (found.Opcode == packet.Opcode &&
                                                     found.Direction == packet.Direction)) == null;
                    }

                    if (add)
                    {
                        packets.Add(packet);
                        if (Settings.FilterPacketsNum > 0 && packets.Count == Settings.FilterPacketsNum)
                            break;
                    }

                    if (Settings.FilterPacketNumHigh > 0 && packetNum > Settings.FilterPacketNumHigh)
                        break;
                }
            }
            catch(Exception ex)
            {
                if (parsingPacket != null)
                    Console.WriteLine("Failed at parsing packet: (Opcode: {0}, Number: {1})", parsingPacket.Opcode, parsingPacket.Number);

                Console.WriteLine(ex.Data);
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            reader.Close();

            return packets;
        }
    }
}
