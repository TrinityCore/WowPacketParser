using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing.Parsers;
using Ionic.Zlib;

namespace WowPacketParser.Misc
{
    public sealed partial class Packet : BinaryReader
    {
        private static readonly bool SniffData = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffData);
        private static readonly bool SniffDataOpcodes = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffDataOpcodes);

        private static DateTime _firstPacketTime;

        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "MemoryStream is disposed in ClosePacket().")]
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, StringBuilder writer, string fileName)
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

            if (number == 0)
                _firstPacketTime = Time;

            TimeSpan = Time - _firstPacketTime;
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

            if (number == 0)
                _firstPacketTime = Time;

            TimeSpan = Time - _firstPacketTime;
        }

        public int Opcode { get; set; } // setter can't be private because it's used in multiple_packets
        public DateTime Time { get; private set; }
        public TimeSpan TimeSpan { get; private set; }
        public Direction Direction { get; private set; }
        public int Number { get; private set; }
        public StringBuilder Writer { get; private set; }
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
                ObjectType = type,
                Id = id,
                Data = data,
            };

            Storage.SniffData.Add(item, TimeSpan);
        }

        public Packet Inflate(int inflatedSize, bool keepStream = true)
        {
            var arr = ReadToEnd();
            var newarr = new byte[inflatedSize];
            if (keepStream)
            {
                SessionHandler.z_stream.InputBuffer = arr;
                SessionHandler.z_stream.NextIn = 0;
                SessionHandler.z_stream.AvailableBytesIn = arr.Length;
                SessionHandler.z_stream.OutputBuffer = newarr;
                SessionHandler.z_stream.NextOut = 0;
                SessionHandler.z_stream.AvailableBytesOut = inflatedSize;
                SessionHandler.z_stream.Inflate(FlushType.Sync);
            }
            else
            {
                /*try
                {
                    var inflater = new Inflater(true);
                    inflater.SetInput(arr, 0, arr.Length);
                    inflater.Inflate(newarr, 0, inflatedSize);
                }
                catch (ICSharpCode.SharpZipLib.SharpZipBaseException)
                {
                    var inflater = new Inflater(true);
                    inflater.SetInput(arr, 0, arr.Length);
                    inflater.Inflate(newarr, 0, inflatedSize);
                }*/
                ZlibCodec stream = new ZlibCodec(CompressionMode.Decompress);
                stream.InputBuffer = arr;
                stream.NextIn = 0;
                stream.AvailableBytesIn = arr.Length;
                stream.OutputBuffer = newarr;
                stream.NextOut = 0;
                stream.AvailableBytesOut = inflatedSize;
                stream.Inflate(FlushType.None);
                stream.Inflate(FlushType.Finish);
                stream.EndInflate();
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

        public string GetHeader(bool isMultiple = false)
        {
            return string.Format("{0}: {1} (0x{2}) Length: {3} Time: {4} Number: {5}{6}",
                Direction, Enums.Version.Opcodes.GetOpcodeName(Opcode), Opcode.ToString("X4"),
                Length, Time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                Number, isMultiple ? " (part of another packet)" : "");
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

        public void Write(string value)
        {
            if (Settings.DumpFormat == DumpFormatType.SqlOnly)
                return;

            if (Writer == null)
                Writer = new StringBuilder();

            Writer.Append(value);
        }

        public void Write(string format, params object[] args)
        {
            if (Settings.DumpFormat == DumpFormatType.SqlOnly)
                return;

            if (Writer == null)
                Writer = new StringBuilder();

            Writer.AppendFormat(format, args);
        }

        public void WriteLine()
        {
            if (Settings.DumpFormat == DumpFormatType.SqlOnly)
                return;

            if (Writer == null)
                Writer = new StringBuilder();

            Writer.AppendLine();
        }

        public void WriteLine(string value)
        {
            if (Settings.DumpFormat == DumpFormatType.SqlOnly)
                return;

            if (Writer == null)
                Writer = new StringBuilder();

            Writer.AppendLine(value);
        }

        public void WriteLine(string format, params object[] args)
        {
            if (Settings.DumpFormat == DumpFormatType.SqlOnly)
                return;

            if (Writer == null)
                Writer = new StringBuilder();

            Writer.AppendLine(string.Format(format, args));
        }

        public void ClosePacket()
        {
            if (Writer != null)
            {
                if (Settings.DumpFormat != DumpFormatType.SqlOnly)
                    Writer.Clear();

                Writer = null;
            }

            if (BaseStream != null)
                BaseStream.Close();

            Dispose(true);
        }
    }
}
