using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "reader is disposed in the finally block.")]
        public static ICollection<Packet> Read(SniffFileInfo fileInfo)
        {
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
                    throw new IOException(String.Format("Invalid file type {0}", extension.ToLower()));
            }

            var firstPacketBuild = 0;
            Packet parsingPacket = null; // For debugging
            var packets = new List<Packet>();
            try
            {
                var packetNum = 0;
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

                        firstPacketBuild = ClientVersion.BuildInt;
                    }

                    if (++packetNum < Settings.FilterPacketNumLow)
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
                        packets.Add(packet);
                        if (Settings.FilterPacketsNum > 0 && packets.Count == Settings.FilterPacketsNum)
                            break;
                    }

                    if (Settings.FilterPacketNumHigh > 0 && packetNum > Settings.FilterPacketNumHigh)
                        break;
                }
            }
            catch (Exception ex)
            {
                if (parsingPacket != null)
                    Trace.WriteLine(string.Format("Failed at parsing packet: (Opcode: {0}, Number: {1})",
                                                  parsingPacket.Opcode, parsingPacket.Number));

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
