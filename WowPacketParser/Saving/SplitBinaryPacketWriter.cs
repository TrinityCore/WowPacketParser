using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Saving
{
    public static class SplitBinaryPacketWriter
    {
        private const string Folder = "split"; // might want to move to config later

        public static void Write(IEnumerable<Packet> packets)
        {
            Directory.CreateDirectory(Folder); // not doing anything if it exists already

            // split packets by opcode (group is a set of packets all with the same opcode)
            var groups = packets.GroupBy(p => p.Opcode);

            // since we have one file per opcode it's possible to parallelize groups writing
            Parallel.ForEach(groups, WriteGroup);
        }

        private static void WriteGroup(IGrouping<int, Packet> groups)
        {
            var groupDir = groups.GroupBy(p => p.Direction);
            foreach (var group in groupDir)
            {
                var fileName = Folder + "/" + Opcodes.GetOpcodeName(groups.Key, group.Key) + ".pkt";

                BinaryPacketWriter.Write(fileName, FileMode.Append, group);
            }
        }
    }
}
