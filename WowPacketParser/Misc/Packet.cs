using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip.Compression;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    public sealed partial class Packet : BinaryReader, ITreeNode
    {
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
            StoreData = new NamedTreeNode();
            StoreDataCache = StoreData;
            StoreIndexedLists = new LinkedList<Tuple<NamedTreeNode, IndexedTreeNode>>();
            StoreObjects = new Stack<Tuple<NamedTreeNode, LinkedList<Tuple<NamedTreeNode, IndexedTreeNode>>>>();
            FileName = fileName;
            Status = ParsedStatus.None;
            SubPacketNumber = 0;
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
            StoreData = new NamedTreeNode();
            StoreDataCache = StoreData;
            StoreIndexedLists = new LinkedList<Tuple<NamedTreeNode, IndexedTreeNode>>();
            StoreObjects = new Stack<Tuple<NamedTreeNode, LinkedList<Tuple<NamedTreeNode, IndexedTreeNode>>>>();
            FileName = parent.FileName;
            Status = ParsedStatus.None;
            Parent = (parent.Parent != null) ? parent.Parent : parent;
            SubPacketNumber = ++Parent.SubPacketCount;
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
        public int SubPacketNumber;
        private int SubPacketCount = 0;
        public bool SubPacket
        {
            get
            {
                return Parent != null;
            }
        }
        public int ParentOpcode
        {
            get
            {
                if (SubPacket)
                    return Parent.Opcode;
                return 0;
            }
        }

        public int ParseID = -1;

        public NamedTreeNode GetData()
        {
            return StoreData;
        }

        public string GetHeader()
        {
            StringBuilder output = new StringBuilder(100);
            output.Append(Enum<Direction>.ToString(Direction));
            output.Append(": ");
            output.Append(Opcodes.GetOpcodeName(Opcode));
            output.Append(" (0x");
            output.Append(Opcode.ToString("X4"));
            output.Append(") Length: ");
            output.Append(Length);
            output.Append(" Time: ");
            output.Append(Time.ToString("MM/dd/yyyy HH:mm:ss.fff"));
            output.Append(" Number: ");
            output.Append(Number);
            if (SubPacket)
            {
                output.Append(" (subpacket of packet: opcode ");
                output.Append(Opcodes.GetOpcodeName(ParentOpcode));
                output.Append(" (0x");
                output.Append(ParentOpcode.ToString("X4"));
                output.Append(") )");
            }
            output.AppendLine();
            return output.ToString();
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

            Parent = null;

            Dispose(true);
        }
        public NodeType GetNode<NodeType>(params string[] address)
        {
            NodeType ret;
            if (TryGetNode<NodeType>(out ret, address))
                return ret;
            throw new Exception(String.Format("Could not receive object of type {0} from address{1}", typeof(NodeType), address));
        }
        public bool TryGetNode<NodeType>(out NodeType ret, params string[] address)
        {
            return TryGetNode<NodeType>(out ret, address, 0);
        }
        public bool TryGetNode<NodeType>(out NodeType ret, string[] address, int addrIndex)
        {
            if (address.Length == addrIndex)
            {
                try
                {
                    ret = (NodeType)((Object)this);
                    return true;
                }
                catch
                {
                    ret = default(NodeType);
                    return false;
                }
            }
            return StoreData.TryGetNode<NodeType>(out ret, address, addrIndex);
        }
        public TreeNodeEnumerator GetTreeEnumerator()
        {
            return new TreeNodeEnumerator(this);
        }
    }
}
