using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using Ionic.Zlib;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Parsing.Parsers;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Misc
{
    /// <summary>
    /// This class is the basic data transfer object (DTO) between the 
    /// different components at the processing stage.</summary>
    /// <remarks>
    /// In future developments it should be much more decoupled from
    /// methods dealing with the translation and storing processes.
    /// The "AddValue" method will soon disappear.</remarks>
    /// 
    /// <seealso cref="PacketTranslator">
    /// It uses a Translator class which is responsible for transforming 
    /// the binary representation into the one selected by the output
    /// in <seealso cref="Settings.DumpTextFormat"/>.</seealso>
    /// 
    /// <seealso cref="IPacketFormatter">
    /// In the translation process uses a packet formatter to properly
    /// build the concrete representation depending on the different uses.</seealso>
    public sealed partial class Packet : BinaryReader
    {
        private static readonly bool SniffData = Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.SniffData) || Settings.DumpFormat == DumpFormatType.SniffDataOnly;
        private static readonly bool SniffDataOpcodes = Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.SniffDataOpcodes) || Settings.DumpFormat == DumpFormatType.SniffDataOnly;

        private static DateTime _firstPacketTime;

        public PacketTranslator Translator;

        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "MemoryStream is disposed in ClosePacket().")]
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, IPacketFormatter formatter, string fileName)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            FileName = fileName;
            WriteToFile = true;
            Status = ParsedStatus.None;

            if(opcode == 0x04F6)
            {
                ;
            }

            Formatter = formatter;
            Translator = new PacketTranslator(input, Length, WriteToFile, formatter, this);

            if (number == 0)
                _firstPacketTime = Time;

            TimeSpan = Time - _firstPacketTime;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "MemoryStream is disposed in ClosePacket().")]
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, string fileName)
            : this(input, opcode, time, direction, number, formatterSelector(), fileName)
        {
        }

        private static IPacketFormatter formatterSelector()
        {
            IPacketFormatter formatter;
            if (Settings.DumpTextFormat == TextFormatType.Xml)
                formatter = new XMLPacketFormatter();
            else
                formatter = new TextPacketFormatter();
            return formatter;
        }

        public int Opcode { get; set; } // setter can't be private because it's used in multiple_packets
        public DateTime Time { get; }
        public TimeSpan TimeSpan { get; }
        public Direction Direction { get; }
        public int Number { get; }
        public string FileName { get; }
        public ParsedStatus Status { get; set; }
        public int ConnectionIndex { get; set; }
        public IPEndPoint EndPoint { get; set; }

        public IPacketFormatter Formatter { get; private set; }

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

            SniffData item = new SniffData
            {
                SniffName = FileName,
                ObjectType = type,
                Id = id,
                Data = data
            };

            Storage.SniffData.Add(item, TimeSpan);
        }

        public bool TryInflate(int inflatedSize, int index, byte[] arr, ref byte[] newarr)
        {
            try
            {
                if (!SessionHandler.ZStreams.ContainsKey(index))
                    SessionHandler.ZStreams[index] = new ZlibCodec(CompressionMode.Decompress);
                SessionHandler.ZStreams[index].InputBuffer = arr;
                SessionHandler.ZStreams[index].NextIn = 0;
                SessionHandler.ZStreams[index].AvailableBytesIn = arr.Length;
                SessionHandler.ZStreams[index].OutputBuffer = newarr;
                SessionHandler.ZStreams[index].NextOut = 0;
                SessionHandler.ZStreams[index].AvailableBytesOut = inflatedSize;
                SessionHandler.ZStreams[index].Inflate(FlushType.Sync);
                return true;
            }
            catch (ZlibException)
            {
                return false;
            }
        }

        public Packet Inflate(int inflatedSize, bool keepStream = true)
        {
            var arr = ReadToEnd();
            var newarr = new byte[inflatedSize];

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_0_15005))
                keepStream = false;

            if (keepStream)
            {
                int idx = ConnectionIndex;
                while (!TryInflate(inflatedSize, idx, arr, ref newarr) && idx <= 4)
                    idx += 1;
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
                var stream = new ZlibCodec(CompressionMode.Decompress)
                {
                    InputBuffer = arr,
                    NextIn = 0,
                    AvailableBytesIn = arr.Length,
                    OutputBuffer = newarr,
                    NextOut = 0,
                    AvailableBytesOut = inflatedSize
                };

                stream.Inflate(FlushType.None);
                stream.Inflate(FlushType.Finish);
                stream.EndInflate();
            }

            // Cannot use "using" here
            var pkt = new Packet(newarr, Opcode, Time, Direction, Number, Formatter, FileName)
            {
                ConnectionIndex = ConnectionIndex
            };
            return pkt;
        }

        public Packet Inflate(int arrSize, int inflatedSize, bool keepStream = true)
        {
            var arr = ReadBytes(arrSize);
            var newarr = new byte[inflatedSize];

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_0_15005))
                keepStream = false;

            if (keepStream)
            {
                int idx = ConnectionIndex;
                while (!TryInflate(inflatedSize, idx, arr, ref newarr) && idx <= 4)
                    idx += 1;
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
                var stream = new ZlibCodec(CompressionMode.Decompress)
                {
                    InputBuffer = arr,
                    NextIn = 0,
                    AvailableBytesIn = arr.Length,
                    OutputBuffer = newarr,
                    NextOut = 0,
                    AvailableBytesOut = inflatedSize
                };
                stream.Inflate(FlushType.None);
                stream.Inflate(FlushType.Finish);
                stream.EndInflate();
            }

            // Cannot use "using" here
            var pkt = new Packet(newarr, Opcode, Time, Direction, Number, Formatter, FileName)
            {
                ConnectionIndex = ConnectionIndex
            };
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

        public byte[] ReadToEnd()
        {
            var length = (int)(Length - Position);
            return Translator.ReadBytes(length);
        }

        public string GetHeader(bool isMultiple = false)
        {
            return Formatter.AppendHeaders(Direction, Opcode, Length, 
                ConnectionIndex, EndPoint, Time, Number, isMultiple);
        }

        public long Position => BaseStream.Position;

        public void SetPosition(long val)
        {
            BaseStream.Position = val;
        }

        public long Length => BaseStream.Length;

        public bool WriteToFile { get; private set; }

        public bool CanRead()
        {
            return Position != Length;
        }

        public void ClosePacket(bool clearWriter = true)
        {
            if (clearWriter && Formatter != null)
            {
                if (Settings.DumpFormatWithText())
                    Formatter.CloseItem();
                Formatter = null;
            }

            BaseStream.Close();

            Dispose(true);
        }

        public T AddValue<T>(string name, T obj, params object[] indexes)
        {
            if (Settings.DumpTextFormat == TextFormatType.Xml)
            {
                Formatter.AppendItemWithContent(name, obj.ToString(), "id", Translator.GetIndexString(indexes));
            }
            else
                Formatter.AppendItem("{0}{1}: {2}", Translator.GetIndexString(indexes), name, obj);
            return obj;
        }
    }
}
