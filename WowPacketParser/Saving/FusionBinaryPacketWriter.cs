using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WowPacketParser.Misc;

namespace WowPacketParser.Saving
{
    public static class FusionBinaryPacketWriter
    {
        public static void Write(IEnumerable<Packet> packets)
        {
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

                BinaryPacketWriter.Write(fileName, FileMode.Append, group);
            }
        }
    }
}
