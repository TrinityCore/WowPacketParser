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
        public string FileName { get; }
        public IPacketReader PacketReader { get; }

        public Reader(string fileName, SniffType type)
        {
            FileName = fileName;
            PacketReader = GetPacketReader(fileName, type);
        }

        private static IPacketReader GetPacketReader(string fileName, SniffType type)
        {
            switch (type)
            {
                case SniffType.Sqlite:
                    return new SqLitePacketReader(type, fileName, Encoding.ASCII);
                default: // .pkt
                    return new BinaryPacketReader(type, fileName, Encoding.ASCII);
            }
        }

        private int _packetNum;
        private int _count;

        public bool TryRead(out Packet packet)
        {
            try
            {
                packet = PacketReader.Read(_packetNum++, FileName);
                if (packet == null)
                    return false; // continue
                
                // check for filters
                var opcodeName = Opcodes.GetOpcodeName(packet.Opcode, packet.Direction);

                var add = true;
                if (Settings.Instance.Filters.Length > 0)
                    add = opcodeName.MatchesFilters(Settings.Instance.Filters);
                // check for ignore filters
                if (add && Settings.Instance.IgnoreFilters.Length > 0)
                    add = !opcodeName.MatchesFilters(Settings.Instance.IgnoreFilters);

                if (add)
                {
                    if (Settings.Instance.FilterPacketsNum > 0 && _count++ == Settings.Instance.FilterPacketsNum)
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
                    var packet = reader.Read(packetNum++, fileName);
                    if (packet == null)
                        continue;

                    // check for filters
                    var opcodeName = Opcodes.GetOpcodeName(packet.Opcode, packet.Direction);

                    var add = true;
                    if (Settings.Instance.Filters.Length > 0)
                        add = opcodeName.MatchesFilters(Settings.Instance.Filters);
                    // check for ignore filters
                    if (add && Settings.Instance.IgnoreFilters.Length > 0)
                        add = !opcodeName.MatchesFilters(Settings.Instance.IgnoreFilters);

                    if (add)
                    {
                        action(Tuple.Create(packet, reader.GetCurrentSize(), reader.GetTotalSize()));
                        if (Settings.Instance.FilterPacketsNum > 0 && count++ == Settings.Instance.FilterPacketsNum)
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
