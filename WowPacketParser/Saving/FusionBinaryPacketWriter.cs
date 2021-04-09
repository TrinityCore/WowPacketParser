using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Saving
{
    public static class FusionBinaryPacketWriter
    {
        private static Encoding _encoding;

        public static void Write(IEnumerable<Packet> packets, Encoding encoding)
        {
            _encoding = encoding;

            // split packets by opcode (group is a set of packets all with the same opcode)
            var groups = packets.GroupBy(p => p.Opcode);

            // since we have one file per opcode it's possible to parallelize groups writing
            Parallel.ForEach(groups, new ParallelOptions { MaxDegreeOfParallelism = 1 }, WriteGroup);
        }

        private static void WriteGroup(IGrouping<int, Packet> groups)
        {
            var groupDir = groups.GroupBy(p => p.Direction);
            foreach (var group in groupDir)
            {
                var fileName = "Fusion.pkt";

                using (var fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                    using (var writer = new BinaryWriter(fileStream, _encoding))
                        foreach (var packet in group)
                        {
                            writer.Write((ushort)packet.Opcode);
                            writer.Write((int)packet.Length);
                            writer.Write((byte)packet.Direction);
                            writer.Write(Utilities.GetUnixTimeFromDateTime(packet.Time));
                            writer.Write(packet.GetStream(0));
                            // TODO: Add ConnIdx in a backwards compatible way
                        }
            }
        }
    }
}
