using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression;
using WowPacketParser.Enums;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Misc
{
    public sealed partial class Packet : BinaryReader
    {
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, StringWriter writer, SniffData sniffData)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            Writer = writer;
            SniffData = sniffData;
        }

        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, SniffData sniffData)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            Writer = new StringWriter();
            SniffData = sniffData;
        }

        public int Opcode { get; set; }
        public DateTime Time { get; private set; }
        public Direction Direction { get; private set; }
        public int Number { get; private set; }
        public StringWriter Writer { get; private set; }
        public SniffData SniffData { get; set; }

        public void AddSniffData()
        {
            SniffData.TimeStamp = Utilities.GetUnixTimeFromDateTime(Time);

            if (SniffData.ObjectType != StoreNameType.None)
                Stuffing.SniffData.Add(SniffData);
        }

        public Packet Inflate(int inflatedSize)
        {
            var arr = ReadToEnd();
            var newarr = new byte[inflatedSize];
            try
            {
                var inflater = new Inflater();
                inflater.SetInput(arr, 0, arr.Length);
                inflater.Inflate(newarr, 0, inflatedSize);
            }
            catch (ICSharpCode.SharpZipLib.SharpZipBaseException)
            {
                var inflater = new Inflater(true);
                inflater.SetInput(arr, 0, arr.Length);
                inflater.Inflate(newarr, 0, inflatedSize);
            }
            var pkt = new Packet(newarr, Opcode, Time, Direction, Number, Writer, SniffData);
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
