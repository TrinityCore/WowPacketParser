using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Saving
{
    public static class SanitizedBinaryPacketWriter
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "fileStream is disposed when writer is disposed.")]
        public static void Write(SniffType type, string fileName, Encoding encoding, byte[] fileHeader, IEnumerable<Packet> packets)
        {
            var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            using (var writer = new BinaryWriter(fileStream, encoding))
            {
                writer.Write(fileHeader);
                foreach (var packet in packets)
                {
                    writer.Write(packet.Header);
                    writer.Write(packet.GetStream(0));
                }
            }
        }
    }
}
