using System.Collections.Generic;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public static class Reader
    {
        public static List<Packet> Read(string fileName, string[] filters, string[] ignoreFilters, int packetNumberLow, int packetNumberHigh, int packetsToRead)
        {
            var packets = new List<Packet>();
            var packetNum = -1;

            IPacketReader reader = null;
            var extension = Path.GetExtension(fileName);
            if (extension != null)
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

            while (reader != null && reader.CanRead())
            {
                if (++packetNum < packetNumberLow)
                    continue;

                var packet = reader.Read(packetNum);
                if (packet == null)
                    continue;

                //check for filters
                bool add =
                    filters == null || filters.Length == 0 ||
                    packet.GetOpcode().ToString().MatchesFilters(filters);

                //check for ignore filters
                if (add && ignoreFilters != null && ignoreFilters.Length > 0)
                    add = !packet.GetOpcode().ToString().MatchesFilters(ignoreFilters);

                if (add)
                {
                    packets.Add(packet);
                    if (packetsToRead > 0 && packets.Count == packetsToRead)
                        break;
                }

                if (packetNumberHigh > 0 && packetNum > packetNumberHigh)
                    break;
            }

            if (reader != null)
                reader.Close();

            return packets;
        }
    }
}
