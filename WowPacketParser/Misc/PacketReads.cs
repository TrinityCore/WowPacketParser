using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using PacketParser.Enums;
using PacketParser.Parsing;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    public sealed partial class Packet
    {
        public Guid ReadGuid()
        {
            var guid = new Guid(ReadUInt64());

            return guid;
        }

        public Guid ReadPackedGuid()
        {
            byte mask = ReadByte();

            ulong res = 0;

            int i = 0;
            while (mask != 0)
            {
                if ((mask & 1) != 0)
                    res += (ulong) ReadByte() << i;

                i+=8;
                mask >>= 1;
            }

            var guid = new Guid(res);

            return guid;
        }

        public DateTime ReadTime()
        {
            return Utilities.GetDateTimeFromUnixTime(ReadInt32());
        }

        public DateTime ReadMillisecondTime()
        {
            return Utilities.GetDateTimeFromUnixTime((double)ReadInt32()/1000);
        }

        public DateTime ReadPackedTime()
        {
            return Utilities.GetDateTimeFromGameTime(ReadInt32());
        }

        public Vector2 ReadVector2()
        {
            return new Vector2(ReadSingle(), ReadSingle());
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(ReadSingle(), ReadSingle(), ReadSingle());
        }

        public Vector3 ReadPackedVector3()
        {
            int packed = ReadInt32();
            float x = ((packed & 0x7FF) << 21 >> 21)*0.25f;
            float y = ((((packed >> 11) & 0x7FF) << 21) >> 21)*0.25f;
            float z = ((packed >> 22 << 22) >> 22)*0.25f;
            return new Vector3(x, y, z);
        }

        public Vector4 ReadVector4()
        {
            return new Vector4(ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle());
        }

        public Quaternion ReadPackedQuaternion()
        {
            long packed = ReadInt64();
            float x = (packed >> 42)*(1.0f/2097152.0f);
            float y = (((packed << 22) >> 32) >> 11)*(1.0f/1048576.0f);
            float z = (packed << 43 >> 43)*(1.0f/1048576.0f);

            float w = x*x + y*y + z*z;
            if (Math.Abs(w - 1.0f) >= (1/1048576.0f))
                w = (float) Math.Sqrt(1.0f - w);
            else
                w = 0.0f;

            return new Quaternion(x, y, z, w);
        }

        public string ReadWoWString(int len)
        {
            Encoding encoding = Encoding.UTF8;
            var bytes = ReadBytes(len);
            string s = encoding.GetString(bytes);
            return s;
        }

        public string ReadCString(Encoding encoding)
        {
            var bytes = new List<byte>();

            byte b;
            while (CanRead() && (b = ReadByte()) != 0)  // CDataStore::GetCString calls CanRead too
                bytes.Add(b);

            return encoding.GetString(bytes.ToArray());
        }

        public string ReadCString()
        {
            return ReadCString(Encoding.UTF8);
        }

        public KeyValuePair<int, bool> ReadEntry()
        {
            // Entries masked with 0x8000000 are invalid entries

            var entry = ReadUInt32();
            var realEntry = entry & 0x7FFFFFFF;

            return new KeyValuePair<int, bool>((int)realEntry, realEntry != entry);
        }

        public LfgEntry ReadLfgEntry()
        {
            return new LfgEntry(ReadInt32());
        }

        public UpdateField ReadUpdateField()
        {
            long pos = Position;
            uint svalue = ReadUInt32();
            SetPosition(pos);
            float fvalue = ReadSingle();

            var field = new UpdateField(svalue, fvalue);
            return field;
        }

        public IPAddress ReadIPAddress()
        {
            byte[] val = ReadBytes(4);
            return new IPAddress(val);
        }

        public T ReadStruct<T>()
            where T : struct
        {
            byte[] rawData = ReadBytes(Marshal.SizeOf(typeof (T)));
            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            var returnObject = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof (T));
            handle.Free();
            return returnObject;
        }

        public byte[] ReadToEnd()
        {
            var length = (int) (Length - Position);
            return ReadBytes(length);
        }

        public byte ReadByte(string name, params int[] values)
        {
            byte val = ReadByte();
            Store(name, val, values);
            return val;
        }

        public sbyte ReadSByte(string name, params int[] values)
        {
            sbyte val = ReadSByte();
            Store(name, val, values);
            return val;
        }

        public bool ReadBoolean(string name, params int[] values)
        {
            bool val = ReadBoolean();
            Store(name, val, values);
            return val;
        }

        public bool ReadBoolean(string name, TypeCode code, params int[] values)
        {
            bool val = ReadValue(code) == 1;
            Store(name, val, values);
            return val;
        }

        public short ReadInt16(string name, params int[] values)
        {
            short val = ReadInt16();
            Store(name, val, values);
            return val;
        }

        public ushort ReadUInt16(string name, params int[] values)
        {
            ushort val = ReadUInt16();
            Store(name, val, values);
            return val;
        }

        public float ReadSingle(string name, params int[] values)
        {
            float val = ReadSingle();
            Store(name, val, values);
            return val;
        }

        public double ReadDouble(string name, params int[] values)
        {
            double val = ReadDouble();
            Store(name, val, values);
            return val;
        }

        public int ReadInt32(string name, params int[] values)
        {
            int val = ReadInt32();
            Store(name, val, values);
            return val;
        }

        public uint ReadUInt32(string name, params int[] values)
        {
            uint val = ReadUInt32();
            Store(name, val, values);
            return val;
        }

        public long ReadInt64(string name, params int[] values)
        {
            long val = ReadInt64();
            Store(name, val, values);
            return val;
        }

        public ulong ReadUInt64(string name, params int[] values)
        {
            ulong val = ReadUInt64();
            Store(name, val, values);
            return val;
        }

        public Guid ReadGuid(string name, params int[] values)
        {
            Guid val = ReadGuid();
            Store(name, val, values);
            return val;
        }

        public string ReadWoWString(string name, int len, params int[] values)
        {
            string val = ReadWoWString(len);
            Store(name, val, values);
            return val;
        }

        public string ReadWoWString(string name, uint len, params int[] values)
        {
            string val = ReadWoWString((int)len);
            Store(name, val, values);
            return val;
        }

        public string ReadCString(string name, params int[] values)
        {
            string val = ReadCString();
            Store(name, val, values);
            return val;
        }

        public Guid ReadPackedGuid(string name, params int[] values)
        {
            Guid val = ReadPackedGuid();
            Store(name, val, values);
            return val;
        }

        public KeyValuePair<int, bool> ReadEntry(string name, params int[] values)
        {
            KeyValuePair<int, bool> entry = ReadEntry();
            Store(name, entry, values);
            return entry;
        }

        public Vector2 ReadVector2(string name, params int[] values)
        {
            Vector2 val = ReadVector2();
            Store(name, val, values);
            return val;
        }

        public Vector3 ReadVector3(string name, params int[] values)
        {
            Vector3 val = ReadVector3();
            Store(name, val, values);
            return val;
        }

        public Vector4 ReadVector4(string name, params int[] values)
        {
            Vector4 val = ReadVector4();
            Store(name, val, values);
            return val;
        }

        public Quaternion ReadPackedQuaternion(string name, params int[] values)
        {
            Quaternion val = ReadPackedQuaternion();
            Store(name, val, values);
            return val;
        }

        public DateTime ReadTime(string name, params int[] values)
        {
            DateTime val = ReadTime();
            Store(name, val, values);
            return val;
        }

        public DateTime ReadPackedTime(string name, params int[] values)
        {
            DateTime val = ReadPackedTime();
            Store(name, val, values);
            return val;
        }

        public LfgEntry ReadLfgEntry(string name, params int[] values)
        {
            var val = new LfgEntry(ReadInt32());
            Store(name, val, values);
            return val;
        }

        public long ReadValue(string name, TypeCode typeCode, params int[] values)
        {
            var val = ReadValue(typeCode);
            Store(name, val, values);
            return val;
        }

        private long ReadValue(TypeCode code)
        {
            long rawValue = 0;
            switch (code)
            {
                case TypeCode.SByte:
                    rawValue = ReadSByte();
                    break;
                case TypeCode.Byte:
                    rawValue = ReadByte();
                    break;
                case TypeCode.Int16:
                    rawValue = ReadInt16();
                    break;
                case TypeCode.UInt16:
                    rawValue = ReadUInt16();
                    break;
                case TypeCode.Int32:
                    rawValue = ReadInt32();
                    break;
                case TypeCode.UInt32:
                    rawValue = ReadUInt32();
                    break;
                case TypeCode.Int64:
                    rawValue = ReadInt64();
                    break;
                case TypeCode.UInt64:
                    rawValue = (long) ReadUInt64();
                    break;
            }
            return rawValue;
        }

        public int ReadEntryWithName<T>(StoreNameType type, string name, params int[] values)
        {
            var val = (int)ReadValue(Type.GetTypeCode(typeof(T)));
            var stEntry = new StoreEntry(type, val);
            Store(name, stEntry, values);
            return val;
        }

        private StoreEnum<T> ReadEnum<T>(TypeCode code) where T : struct, IConvertible
        {
            long rawValue = ReadValue(code);
            object value = Enum.ToObject(typeof (T), rawValue);

            return new StoreEnum<T>(rawValue);
        }

        public T ReadEnum<T>(string name, TypeCode code, params int[] values) where T : struct, IConvertible
        {
            var val = ReadEnum<T>(code);
            Store(name, val, values);
            return val.val;
        }

        private StoreEnum<T> ReadEnum<T>(int bits) where T : struct, IConvertible
        {
            var type = typeof(T);
            long rawVal = ReadBits(bits);

            return new StoreEnum<T>(rawVal);
        }

        public T ReadEnum<T>(string name, int bits, params int[] values) where T : struct, IConvertible
        {
            var val = ReadEnum<T>(bits);
            Store(name, val, values);
            return val.val;
        }

        /// <summary>
        /// Bitstream
        /// </summary>

        private byte _bitpos = 8;
        private byte _curbitval;

        public Bit ReadBit(string name, params int[] values)
        {
            var bit = ReadBit();
            Store(name, bit, values);
            return new Bit(bit);
        }

        public Bit ReadBit()
        {
            ++_bitpos;

            if (_bitpos > 7)
            {
                _bitpos = 0;
                _curbitval = ReadByte();
            }

            bool bit = ((_curbitval >> (7 - _bitpos)) & 1) != 0;
            return new Bit(bit);
        }

        public void ResetBitReader()
        {
            _bitpos = 8;
        }

        public uint ReadBits(string name, int bits, params int[] values)
        {
            var val = ReadBits(bits);
            Store(name, val, values);
            return val;
        }

        public uint ReadBits(int bits)
        {
            uint value = 0;
            for (var i = bits - 1; i >= 0; --i)
                if (ReadBit())
                    value |= (uint)(1 << i);

            return value;
        }

        public byte[] StartBitStream(params int[] values)
        {
            var bytes = new byte[values.Length];

            foreach (var value in values)
                bytes[value] = (byte)(ReadBit() ? 1 : 0);

            return bytes;
        }

        public byte ParseBitStream(byte[] stream, byte value)
        {
            if (stream[value] != 0)
                return stream[value] ^= ReadByte();

            return 0;
        }

        public byte[] ParseBitStream(byte[] stream, params byte[] values)
        {
            var tempBytes = new byte[values.Length];
            var i = 0;

            foreach (var value in values)
            {
                if (stream[value] != 0)
                    stream[value] ^= ReadByte();

                tempBytes[i++] = stream[value];
            }

            return tempBytes;
        }

        public byte ReadXORByte(byte[] stream, byte value)
        {
            if (stream[value] != 0)
                return stream[value] ^= ReadByte();

            return 0;
        }

        public char[] ReadChars(string name, int count, params int[] values)
        {
            var val = ReadChars(count);
            Store(name, val, values);
            return val;
        }

        public Guid ToGuid(string name, byte[] stream, params int[] values)
        {
            var val = new Guid(BitConverter.ToUInt64(stream, 0));
            Store(name, val, values);
            return val;
        }

        public Guid StoreBitstreamGuid(string name, byte[] stream, params int[] values)
        {
            return ToGuid(name, stream, values);
        }

        private NamedTreeNode StoreData;
        private NamedTreeNode StoreDataCache;
        private int[] LastIndex = new int[0];
        private LinkedList<Tuple<NamedTreeNode, IndexedTreeNode>> StoreIndexedLists;
        private Stack<Tuple<NamedTreeNode, LinkedList<Tuple<NamedTreeNode, IndexedTreeNode>>>> StoreObjects;

        public NamedTreeNode StoreGetDataByIndexes(params int[] values)
        {
            // use cached data entry if available
            if (LastIndex != null && values.Equals(LastIndex))
            {
                return StoreDataCache;
            }
            var listsCount = StoreIndexedLists.Count;

            if (values.Length > listsCount)
            {
                throw new Exception(String.Format("TreeStore: Received to many list indexes, there're {0} lists, but {1} indexes", listsCount, values.Length));
            }
            else if (values.Length < listsCount)
            {
                throw new Exception(String.Format("TreeStore: Received not enough list indexes, there're {0} lists, but {1} indexes", listsCount, values.Length));
            }
            NamedTreeNode data;
            if (listsCount > 0)
            {
                var itr = StoreIndexedLists.First;
                for (var i = 0; i < listsCount - 1; ++i)
                {
                    NamedTreeNode next;
                    if (!itr.Value.Item2.TryGetValue(values[i], out next) || next != itr.Next.Value.Item1)
                    {
                        throw new Exception("TreeStore: Incorrect sub index number!");
                    }
                    itr = itr.Next;
                }
                if (!itr.Value.Item2.TryGetValue(values[values.Length - 1], out data))
                {
                    data = new NamedTreeNode();
                    itr.Value.Item2.Add(values[values.Length - 1], data);
                }
            }
            else
            {
                data = StoreData;
            }

            // caching - save for future use
            StoreDataCache = data;
            LastIndex = values;
            return data;
        }

        public void StoreContinueList(Tuple<NamedTreeNode, IndexedTreeNode> list, params int[] values)
        {
            // caching disable - make sure errors are checked properly
            LastIndex = null;

            var dat = StoreGetDataByIndexes(values);
            if (dat != list.Item1)
                throw new Exception("TreeStore: Cannot continue reading into indexed list, incorrect scope");
            StoreIndexedLists.AddLast(list);
        }

        // begins reading data list arranged by index number
        public Tuple<NamedTreeNode, IndexedTreeNode> StoreBeginList(string listName, params int[] values)
        {
            // caching disable - make sure errors are checked properly
            LastIndex = null;

            var dat = StoreGetDataByIndexes(values);
            var newList = new IndexedTreeNode();
            Store(listName, newList, values);
            var l = new Tuple<NamedTreeNode, IndexedTreeNode>(dat, newList);
            StoreIndexedLists.AddLast(l);
            return l;
        }

        public void StoreEndList()
        {
            // caching disable - make sure errors are checked properly
            LastIndex = null;

            if (StoreIndexedLists.Count == 0)
                throw new Exception("TreeStore: Cannot end indexed list, no list found in this scope");
            StoreIndexedLists.RemoveLast();
        }

        // begins code in which all data stored composes an object
        public void StoreBeginObj(string name, params int[] values)
        {
            var newObj = new NamedTreeNode();
            Store(name, newObj, values);
            StoreObjects.Push(new Tuple<NamedTreeNode, LinkedList<Tuple<NamedTreeNode, IndexedTreeNode>>>(StoreData, StoreIndexedLists));
            StoreData = newObj;
            StoreIndexedLists = new LinkedList<Tuple<NamedTreeNode, IndexedTreeNode>>();

            // caching
            StoreDataCache = newObj;
            LastIndex = new int[0];
        }

        // ends code in which all data stored composes an object
        public void StoreEndObj()
        {
            if (StoreObjects.Count == 0)
                throw new Exception("TreeStore: Cannot end object, no object found in this scope");
            if (StoreIndexedLists.Count != 0)
                throw new Exception("TreeStore: Cannot end object, there are unfinished indexed lists in object");
            var state = StoreObjects.Pop();
            StoreData = state.Item1;
            StoreIndexedLists = state.Item2;

            // caching disable - make sure errors are checked properly
            LastIndex = null;
        }

        public void Store(string name, Object data, params int[] values)
        {
            var dat = StoreGetDataByIndexes(values);

            if (dat.Contains(name))
                throw new Exception(String.Format("TreeStore: Data with name {0} already stored in this scope, names must not repeat!", name));
            dat.Add(name, data);
        }

        // sub packet with given length
        public Packet ReadSubPacket(int opcode, int length, string name, params int[] values)
        {
            byte[] bytes = ReadBytes(length);
            var newpacket = new Packet(bytes, opcode, this);
            Store(name, newpacket, values);
            Handler.Parse(newpacket);
            return newpacket;
        }

        // sub packet with no given length
        public Packet ReadSubPacket(int opcode, string name, params int[] values)
        {
            byte[] bytes = GetStream(Position);
            var newpacket = new Packet(bytes, opcode, this);
            Store(name, newpacket, values);
            Handler.Parse(newpacket, false);
            switch (newpacket.Status)
            {
                case ParsedStatus.NotParsed:
                    // subpacket not parsed, we can continue parsing because we don't know the length
                    throw new Exception(String.Format("Sub packet (opcode {0}) was not parsed, can't continue!", opcode));
                case ParsedStatus.WithErrors:
                    throw new Exception(String.Format("Sub packet (opcode {0}) was parsed with error, can't continue!", opcode));
                case ParsedStatus.Success:
                    break;
            }
            byte[] parsed = newpacket.GetStream(0);
            var mypos = Position;
            // write data from subpacket to current packet, so offsets will match in case of subpacket data change (inflate funct for example)
            BaseStream.SetLength(mypos + parsed.Length);
            BaseStream.Write(parsed, 0, parsed.Length);
            // mark parsed data as used in this packet
            SetPosition(mypos + newpacket.Position);

            // set length to the amount of bytes read
            newpacket.BaseStream.SetLength(newpacket.Position);
            return newpacket;
        }
    }
}
