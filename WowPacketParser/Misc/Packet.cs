using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip.Compression;
using WowPacketParser.Enums;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using System.Collections.Specialized;

namespace WowPacketParser.Misc
{
    //using NameDict = Dictionary<string, Object>;
    //1using IndexDict = Dictionary<int, Dictionary<string, Object>>;
    using NameDict = OrderedDictionary;
    using IndexDict = Dictionary<int, OrderedDictionary>;

    public sealed partial class Packet : BinaryReader
    {
        private static readonly bool SniffData = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffData);
        private static readonly bool SniffDataOpcodes = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffDataOpcodes);

        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "MemoryStream is disposed in ClosePacket().")]
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number, string fileName)
            : base(new MemoryStream(input.Length), Encoding.UTF8)
        {
            this.BaseStream.Write(input, 0, input.Length);
            SetPosition(0);
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
            StoreData = new NameDict();
            StoreDataCache = StoreData;
            StoreIndexedLists = new LinkedList<Tuple<NameDict, IndexDict>>();
            StoreObjects = new Stack<Tuple<NameDict, LinkedList<Tuple<NameDict, IndexDict>>>>();
            FileName = fileName;
            Status = ParsedStatus.None;
            Parent = null;

            if (number == 0)
                _firstPacketTime = Time;

            TimeSpan = Time - _firstPacketTime;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000", Justification = "MemoryStream is disposed in ClosePacket().")]
        public Packet(byte[] input, int opcode, Packet parent)
            : base(new MemoryStream(input.Length), Encoding.UTF8)
        {
            this.BaseStream.Write(input, 0, input.Length);
            SetPosition(0);
            Opcode = opcode;
            Time = parent.Time;
            Direction = parent.Direction;
            Number = parent.Number;
            StoreData = new NameDict();
            StoreDataCache = StoreData;
            StoreIndexedLists = new LinkedList<Tuple<NameDict, IndexDict>>();
            StoreObjects = new Stack<Tuple<NameDict, LinkedList<Tuple<NameDict, IndexDict>>>>();
            FileName = parent.FileName;
            Status = ParsedStatus.None;
            Parent = parent;
        }

        public int Opcode { get; set; } // setter can't be private because it's used in multiple_packets
        public DateTime Time { get; private set; }
        private DateTime _firstPacketTime;
        public TimeSpan TimeSpan { get; private set; }
        public Direction Direction { get; private set; }
        public int Number { get; private set; }
        public string FileName { get; private set; }
        public ParsedStatus Status { get; set; }
        public string ErrorMessage = "";
        public Packet Parent;

        public NameDict GetData()
        {
            return StoreData;
        }

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

        public void Inflate(int inflatedSize, int bytesToInflate)
        {
            var oldPos = Position;
            var decompress = ReadBytes(bytesToInflate);
            var tailData = ReadToEnd();
            this.BaseStream.SetLength(oldPos + inflatedSize + tailData.Length);
            
            var newarr = new byte[inflatedSize];
            try
            {
                var inflater = new Inflater();
                inflater.SetInput(decompress, 0, bytesToInflate);
                inflater.Inflate(newarr, 0, inflatedSize);
            }
            catch (ICSharpCode.SharpZipLib.SharpZipBaseException)
            {
                var inflater = new Inflater(true);
                inflater.SetInput(decompress, 0, bytesToInflate);
                inflater.Inflate(newarr, 0, inflatedSize);
            }
            SetPosition(oldPos);
            this.BaseStream.Write(newarr, 0, inflatedSize);
            this.BaseStream.Write(tailData, 0, tailData.Length);
            SetPosition(oldPos);
        }
            
        public void Inflate(int inflatedSize)
        {
            Inflate(inflatedSize, (int)(Length - Position));
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

        public void ClosePacket()
        {
            if (BaseStream != null)
                BaseStream.Close();

            Dispose(true);
        }
    }
}
