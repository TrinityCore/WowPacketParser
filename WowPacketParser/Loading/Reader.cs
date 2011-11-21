using System.Collections.Generic;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public static class Reader
    {
        public static List<Packet> Read(string fileName, string[] filters, string[] ignoreFilters, int packetNumberLow, int packetNumberHigh, int packetsToRead, bool summary)
        {
            var packets = new List<Packet>();
            var packetNum = 0;

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

            while (reader.CanRead())
            {
                var packet = reader.Read(packetNum);
                if (packet == null)
                    continue;

                // determine build version based on date of first packet if not specified otherwise
                if (packetNum == 0 && ClientVersion.IsUndefined())
                    ClientVersion.SetVersion(packet.Time);

                if (++packetNum < packetNumberLow)
                    continue;

                // check for filters
                bool add = true;
                var opcodeName = Opcodes.GetOpcodeName(packet.Opcode);
                if (filters != null && filters.Length > 0)
                    add = opcodeName.MatchesFilters(filters);
                // check for ignore filters
                if (add && ignoreFilters != null && ignoreFilters.Length > 0)
                    add = !opcodeName.MatchesFilters(ignoreFilters);

                if (add && summary)
                {
                    add = packets.Find(delegate(Packet found)
                    {
                        return (found.Opcode == packet.Opcode && found.Direction == packet.Direction);
                    }) == null;
                }

                if (add)
                {
                    packets.Add(packet);
                    if (packetsToRead > 0 && packets.Count == packetsToRead)
                        break;
                }

                if (packetNumberHigh > 0 && packetNum > packetNumberHigh)
                    break;
            }

            reader.Close();

            return packets;
        }
    }
}
