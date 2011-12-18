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
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, StringWriter writer, SniffFileInfo fileInfo)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            Writer = writer;
            SniffFileInfo = fileInfo;
            Status = ParsedStatus.None;
        }

        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, SniffFileInfo fileInfo)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            Writer = new StringWriter();
            SniffFileInfo = fileInfo;
            Status = ParsedStatus.None;
        }

        public int Opcode { get; private set; }
        public DateTime Time { get; private set; }
        public Direction Direction { get; private set; }
        public int Number { get; private set; }
        public StringWriter Writer { get; private set; }
        public SniffFileInfo SniffFileInfo { get; private set; }
        public ParsedStatus Status { get; set; }

        public void AddSniffData(StoreNameType type, int id, string data)
        {
            if (type == StoreNameType.None)
                return;

            if (id == 0 && type != StoreNameType.Map)
                return; // Only maps can have id 0

            if (type == StoreNameType.Opcode)
                if (!Settings.GetBoolean("OpcodeStatusDB"))
                    return; // Don't add opcodes if its config is not enabled

            var sniffData = new SniffData()
            {
                FileInfo = SniffFileInfo,
                TimeStamp = Utilities.GetUnixTimeFromDateTime(Time),
                ObjectType = type,
                Id = id,
                Data = data,
                Number = Number,
            };
            Stuffing.SniffData.Add(sniffData);
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
            var pkt = new Packet(newarr, Opcode, Time, Direction, Number, Writer, SniffFileInfo);
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