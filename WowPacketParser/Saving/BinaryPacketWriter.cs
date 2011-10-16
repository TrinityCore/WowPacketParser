using System.Collections.Generic;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public class BinaryPacketWriter
    {
        private readonly BinaryWriter _writer;
        private readonly SniffType _sniffType;

        public BinaryPacketWriter(SniffType type, string fileName, Encoding encoding)
        {
            _sniffType = type;
            _writer = new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None), encoding);
        }

        public void Write(IEnumerable<Packet> packets)
        {
            foreach (var packet in packets)
            {
                if (_sniffType == SniffType.Pkt)
                {
                    _writer.Write((ushort)packet.Opcode);
                    _writer.Write((int)packet.GetLength());
                    _writer.Write((byte)packet.Direction);
                    _writer.Write((ulong)Utilities.GetUnixTimeFromDateTime(packet.Time));
                    _writer.Write(packet.GetStream(0));
                }
                else
                {
                    _writer.Write(packet.Opcode);
                    _writer.Write((int)packet.GetLength());
                    _writer.Write((int)Utilities.GetUnixTimeFromDateTime(packet.Time));
                    _writer.Write((byte)packet.Direction);
                    _writer.Write(packet.GetStream(0));
                }
            }
            _writer.Close();
        }
    }
}
