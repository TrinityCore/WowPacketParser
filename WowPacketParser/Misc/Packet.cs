using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class InflaterHeader
    {
        public static bool useHeader { get; set;  }
    }

    public sealed partial class Packet : BinaryReader
    {
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, StringWriter writer)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            Writer = writer;
        }

        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            Writer = new StringWriter();
        }

        public int Opcode { get; set; }
        public DateTime Time { get; private set; }
        public Direction Direction { get; private set; }
        public int Number { get; private set; }
        public StringWriter Writer { get; private set; }

        public Packet Inflate(int inflatedSize)
        {
            var arr = ReadToEnd();
            var newarr = new byte[inflatedSize];
            var inflater = new Inflater(InflaterHeader.useHeader);
            inflater.SetInput(arr, 0, arr.Length);
            inflater.Inflate(newarr, 0, inflatedSize);
            InflaterHeader.useHeader = true;
            var pkt = new Packet(newarr, Opcode, Time, Direction, Number, Writer);
            return pkt;
        }

        public byte[] GetStream(long offset)
        {
            var pos = GetPosition();
            SetPosition(offset);
            var buffer = ReadToEnd();
            SetPosition(pos);
            return buffer;
        }

        public long GetPosition()
        {
            return BaseStream.Position;
        }

        public void SetPosition(long val)
        {
            BaseStream.Position = val;
        }

        public long GetLength()
        {
            return BaseStream.Length;
        }

        public bool CanRead()
        {
            return GetPosition() != GetLength();
        }
    }
}
