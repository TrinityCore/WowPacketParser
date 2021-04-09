using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Misc;

namespace WowPacketParser.Saving
{
    public static class SplitSessionBinaryPacketWriter
    {
        private const string Folder = "split_session"; // might want to move to config later
        private static Encoding _encoding;

        public static void Write(IEnumerable<Packet> packets, Encoding encoding)
        {
            _encoding = encoding;
            Directory.CreateDirectory(Folder); // not doing anything if it exists already

            // split packets by session (group is a set of packets all with the same session)
            var groups = packets.GroupBy(p => p.EndPoint);

            // since we have one file per session it's possible to parallelize groups writing
            Parallel.ForEach(groups, WriteGroup);
        }

        private static void WriteGroup(IGrouping<EndPoint, Packet> group)
        {
            string fileName = Folder + "/" + (group.Key?.GetHashCode().ToString(CultureInfo.InvariantCulture) ?? "0") + ".pkt";

            using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream, _encoding))
                {
                    foreach (Packet packet in group)
                    {
                        writer.Write((ushort) packet.Opcode);
                        writer.Write((int) packet.Length);
                        writer.Write((byte) packet.Direction);
                        writer.Write(Utilities.GetUnixTimeFromDateTime(packet.Time));
                        writer.Write(packet.GetStream(0));
                    }
                }
            }
        }
    }
}

