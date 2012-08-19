using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Saving
{
    public static class BinaryPacketWriter
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "fileStream is disposed when writer is disposed.")]
        public static void Write(SniffType type, string fileName, Encoding encoding, IEnumerable<Packet> packets)
        {
            var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            using (var writer = new BinaryWriter(fileStream, encoding))
            {
                foreach (var packet in packets)
                {
                    if (type == SniffType.Pkt)
                    {
                        writer.Write((ushort) packet.Opcode);
                        writer.Write((int) packet.Length);
                        writer.Write((byte) packet.Direction);
                        writer.Write((ulong) Utilities.GetUnixTimeFromDateTime(packet.Time));
                        writer.Write(packet.GetStream(0));
                    }
                    else
                    {
                        writer.Write(packet.Opcode);
                        writer.Write((int) packet.Length);
                        writer.Write((int) Utilities.GetUnixTimeFromDateTime(packet.Time));
                        writer.Write((byte) packet.Direction);
                        writer.Write(packet.GetStream(0));
                    }
                }
            }
        }
    }
}
