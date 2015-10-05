using System;
using System.Diagnostics;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public class Reader
    {
        public string FileName { get; private set; }
        public IPacketReader PacketReader { get; private set; }

        public Reader(string fileName, SniffType type)
        {
            FileName = fileName;
            PacketReader = GetPacketReader(fileName, type);
        }

        private static IPacketReader GetPacketReader(string fileName, SniffType type)
        {
            return new BinaryPacketReader(type, fileName, Encoding.ASCII);
        }

        private int _packetNum;
        private int _count;

        public bool TryRead(out Packet packet)
        {
            try
            {
                packet = PacketReader.Read(_packetNum, FileName);
                if (packet == null)
                    return false; // continue

                if (_packetNum++ == 0)
                {
                    // determine build version based on date of first packet if not specified otherwise
                    if (ClientVersion.IsUndefined())
                        ClientVersion.SetVersion(packet.Time);
                }

                // check for filters
                var opcodeName = Opcodes.GetOpcodeName(packet.Opcode, packet.Direction);

                var add = true;
                if (Settings.Filters.Length > 0)
                    add = opcodeName.MatchesFilters(Settings.Filters);
                // check for ignore filters
                if (add && Settings.IgnoreFilters.Length > 0)
                    add = !opcodeName.MatchesFilters(Settings.IgnoreFilters);

                if (add)
                {
                    if (Settings.FilterPacketsNum > 0 && _count++ == Settings.FilterPacketsNum)
                        return true; // break
                    return false; // continue
                }

                packet.ClosePacket();
                packet = null;
                return false;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Data);
                Trace.WriteLine(ex.GetType());
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
            }

            packet = null;
            return false;
        }

        public static void Read(string fileName, SniffType type, Action<Tuple<Packet, long, long>> action)
        {
            var reader = GetPacketReader(fileName, type);

            try
            {
                int packetNum = 0, count = 0;
                while (reader.CanRead())
                {
                    var packet = reader.Read(packetNum, fileName);
                    if (packet == null)
                        continue;

                    if (packetNum++ == 0)
                    {
                        // determine build version based on date of first packet if not specified otherwise
                        if (ClientVersion.IsUndefined())
                            ClientVersion.SetVersion(packet.Time);
                    }

                    // check for filters
                    var opcodeName = Opcodes.GetOpcodeName(packet.Opcode, packet.Direction);

                    var add = true;
                    if (Settings.Filters.Length > 0)
                        add = opcodeName.MatchesFilters(Settings.Filters);
                    // check for ignore filters
                    if (add && Settings.IgnoreFilters.Length > 0)
                        add = !opcodeName.MatchesFilters(Settings.IgnoreFilters);

                    if (add)
                    {
                        action(Tuple.Create(packet, reader.GetCurrentSize(), reader.GetTotalSize()));
                        if (Settings.FilterPacketsNum > 0 && count++ == Settings.FilterPacketsNum)
                            break;
                    }
                    else
                        packet.ClosePacket();
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
        }
    }
}
