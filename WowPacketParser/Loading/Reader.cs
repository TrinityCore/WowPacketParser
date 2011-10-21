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
        public static List<Packet> Read(string fileName, string[] filters, string[] ignoreFilters, int packetNumberLow, int packetNumberHigh, int packetsToRead)
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

                if (++packetNum < packetNumberLow)
                    continue;

                //check for filters
                bool add =
                    filters == null || filters.Length == 0 ||
                    Opcodes.GetOpcodeName(packet.Opcode).MatchesFilters(filters);

                //check for ignore filters
                if (add && ignoreFilters != null && ignoreFilters.Length > 0)
                    add = !Opcodes.GetOpcodeName(packet.Opcode).MatchesFilters(ignoreFilters);

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
