using System;
using System.Diagnostics.CodeAnalysis;
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
        private static readonly bool SniffData = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffData);
        private static readonly bool SniffDataOpcodes = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffDataOpcodes);

        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "MemoryStream is disposed in ClosePacket().")]
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, StringWriter writer, string fileName)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            Writer = writer;
            FileName = fileName;
            Status = ParsedStatus.None;
            WriteToFile = true;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "MemoryStream is disposed in ClosePacket().")]
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, string fileName)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            Writer = null;
            FileName = fileName;
            Status = ParsedStatus.None;
            WriteToFile = true;
        }

        public int Opcode { get; private set; }
        public DateTime Time { get; private set; }
        public Direction Direction { get; private set; }
        public int Number { get; private set; }
        public StringWriter Writer { get; private set; }
        public string FileName { get; private set; }
        public ParsedStatus Status { get; set; }
        public bool WriteToFile { get; private set; }

        public void AddSniffData(StoreNameType type, int id, string data)
        {
            if (type == StoreNameType.None)
                return;

            if (id == 0 && type != StoreNameType.Map)
                return; // Only maps can have id 0

            if (type == StoreNameType.Opcode && !SniffDataOpcodes)
                return; // Don't add opcodes if its config is not enabled

            if (type != StoreNameType.Opcode && !SniffData)
                return;

            var item = new SniffData
            {
                FileName = FileName,
                TimeStamp = Utilities.GetUnixTimeFromDateTime(Time),
                ObjectType = type,
                Id = id,
                Data = data,
                Number = Number,
            };

           Storage.SniffData.Add(item);
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

            // Cannot use "using" here
            var pkt = new Packet(newarr, Opcode, Time, Direction, Number, Writer, FileName);
            return pkt;
        }

        public byte[] GetStream(long offset)
        {
            var pos = Position;
            SetPosition(offset);
            var buffer = ReadToEnd();
            SetPosition(pos);
            return buffer;
        }

        public long Position
        {
            get { return BaseStream.Position; }
        }

        public void SetPosition(long val)
        {
            BaseStream.Position = val;
        }

        public long Length
        {
            get { return BaseStream.Length; }
        }

        public bool CanRead()
        {
            return Position != Length;
        }

        public void Write(object format, params object[] args)
        {
            if (Writer == null)
                Writer = new StringWriter();

            var str = string.Format(format.ToString(), args);
            Writer.Write(str);
        }

        public void WriteLine()
        {
            if (Writer == null)
                Writer = new StringWriter();

            Writer.WriteLine();
        }

        public void WriteLine(string value)
        {
            if (Writer == null)
                Writer = new StringWriter();

            Writer.WriteLine(value);
        }

        public void WriteLine(object value)
        {
            if (Writer == null)
                Writer = new StringWriter();

            Writer.WriteLine(value);
        }

        public void WriteLine(string format, params object[] args)
        {
            if (Writer == null)
                Writer = new StringWriter();

            Writer.WriteLine(string.Format(format, args));
        }

        public void ClosePacket()
        {
            if (Writer != null)
                Writer.Close();

// ReSharper disable ConditionIsAlwaysTrueOrFalse (/slap R#)
            if (BaseStream != null)
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                BaseStream.Close();
            Dispose(true);
        }
    }
}