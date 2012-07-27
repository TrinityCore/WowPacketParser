using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public sealed partial class Packet
    {
        public Guid ReadGuid()
        {
            var guid = new Guid(ReadUInt64());

            if (WriteToFile)
                WriteToFile = Filters.CheckFilter(guid);

            return guid;
        }

        public Guid ReadPackedGuid()
        {
            byte mask = ReadByte();

            if (mask == 0)
                return new Guid(0);

            ulong res = 0;

            int i = 0;
            while (i < 8)
            {
                if ((mask & 1 << i) != 0)
                    res += (ulong) ReadByte() << (i*8);

                i++;
            }

            var guid = new Guid(res);

            if (WriteToFile)
                WriteToFile = Filters.CheckFilter(guid);

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

        private static string GetIndexString(params int[] values)
        {
            var indexes = string.Empty;

            foreach (var value in values)
            {
                if (value == -1) continue;
                indexes += "[" + value + "] ";
            }

            return indexes;
        }

        public byte ReadByte(string name, params int[] values)
        {
            byte val = ReadByte();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X2") + ")" : String.Empty));
            return val;
        }

        public sbyte ReadSByte(string name, params int[] values)
        {
            sbyte val = ReadSByte();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X2") + ")" : String.Empty));
            return val;
        }

        public bool ReadBoolean(string name, params int[] values)
        {
            bool val = ReadBoolean();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public bool ReadBoolean(string name, TypeCode code, params int[] values)
        {
            bool val = ReadValue(code) == 1;
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public short ReadInt16(string name, params int[] values)
        {
            short val = ReadInt16();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty));
            return val;
        }

        public ushort ReadUInt16(string name, params int[] values)
        {
            ushort val = ReadUInt16();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty));
            return val;
        }

        public float ReadSingle(string name, params int[] values)
        {
            float val = ReadSingle();
            if (Settings.DebugReads)
            {
                byte[] bytes = BitConverter.GetBytes(val);
                WriteLine("{0}{1}: {2} (0x{3})", GetIndexString(values), name, val, BitConverter.ToString(bytes));
            }
            else
                WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public double ReadDouble(string name, params int[] values)
        {
            double val = ReadDouble();
            if (Settings.DebugReads)
            {
                byte[] bytes = BitConverter.GetBytes(val);
                WriteLine("{0}{1}: {2} (0x{3})", GetIndexString(values), name, val, BitConverter.ToString(bytes));
            }
            else
                WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public int ReadInt32(string name, params int[] values)
        {
            int val = ReadInt32();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X8") + ")" : String.Empty));
            return val;
        }

        public uint ReadUInt32(string name, params int[] values)
        {
            uint val = ReadUInt32();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X8") + ")" : String.Empty));
            return val;
        }

        public long ReadInt64(string name, params int[] values)
        {
            long val = ReadInt64();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X16") + ")" : String.Empty));
            return val;
        }

        public ulong ReadUInt64(string name, params int[] values)
        {
            ulong val = ReadUInt64();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X16") + ")" : String.Empty));
            return val;
        }

        public Guid ReadGuid(string name, params int[] values)
        {
            Guid val = ReadGuid();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public string ReadWoWString(string name, int len, params int[] values)
        {
            string val = ReadWoWString(len);
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public string ReadWoWString(string name, uint len, params int[] values)
        {
            string val = ReadWoWString((int)len);
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public string ReadCString(string name, params int[] values)
        {
            string val = ReadCString();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public Guid ReadPackedGuid(string name, params int[] values)
        {
            Guid val = ReadPackedGuid();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public KeyValuePair<int, bool> ReadEntry(string name, params int[] values)
        {
            KeyValuePair<int, bool> entry = ReadEntry();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, entry.Key);
            return entry;
        }

        public Vector2 ReadVector2(string name, params int[] values)
        {
            Vector2 val = ReadVector2();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public Vector3 ReadVector3(string name, params int[] values)
        {
            Vector3 val = ReadVector3();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public Vector4 ReadVector4(string name, params int[] values)
        {
            Vector4 val = ReadVector4();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public Quaternion ReadPackedQuaternion(string name, params int[] values)
        {
            Quaternion val = ReadPackedQuaternion();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public DateTime ReadTime(string name, params int[] values)
        {
            DateTime val = ReadTime();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty));
            return val;
        }

        public DateTime ReadPackedTime(string name, params int[] values)
        {
            DateTime val = ReadPackedTime();
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, val, (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty));
            return val;
        }

        public LfgEntry ReadLfgEntry(string name, params int[] values)
        {
            var val = new LfgEntry(ReadInt32());
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public long ReadValue(string name, TypeCode typeCode, params int[] values)
        {
            var val = ReadValue(typeCode);
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
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

        private KeyValuePair<long, T> ReadEnum<T>(TypeCode code)
        {
            long rawValue = ReadValue(code);
            object value = Enum.ToObject(typeof (T), rawValue);

            if (rawValue > 0)
                Logger.CheckForMissingValues<T>(rawValue);

            return new KeyValuePair<long, T>(rawValue, (T) value);
        }

        public T ReadEnum<T>(string name, TypeCode code, params int[] values)
        {
            KeyValuePair<long, T> val = ReadEnum<T>(code);
            WriteLine("{0}{1}: {2} ({3}){4}", GetIndexString(values), name, val.Value, val.Key, (Settings.DebugReads ? " (0x" + val.Key.ToString("X4") + ")" : String.Empty));
            return val.Value;
        }

        public int ReadEntryWithName<T>(StoreNameType type, string name, params int[] values)
        {
            var val = (int) ReadValue(Type.GetTypeCode(typeof (T)));
            WriteLine("{0}{1}: {2}{3}", GetIndexString(values), name, StoreGetters.GetName(type, val), (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty));
            if (WriteToFile)
                WriteToFile = Filters.CheckFilter(type, val);
            return val;
        }

        /// <summary>
        /// Bitstream
        /// </summary>

        private byte _bitpos = 8;
        private byte _curbitval;

        public bool ReadBit(string name, params int[] values)
        {
            var bit = ReadBit();
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, bit ? "1" : "0");
            return bit;
        }

        public bool ReadBit()
        {
            ++_bitpos;

            if (_bitpos > 7)
            {
                _bitpos = 0;
                _curbitval = ReadByte();
            }

            var bit = ((_curbitval >> (7 - _bitpos)) & 1) != 0;
            return bit;
        }

        public void ResetBitReader()
        {
            _bitpos = 8;
        }

        public uint ReadBits(string name, int bits, params int[] values)
        {
            var val = ReadBits(bits);
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
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

        private KeyValuePair<long, T> ReadEnum<T>(int bits)
        {
            var type = typeof(T);
            long rawVal = ReadBits(bits);
            var value = Enum.ToObject(type, rawVal);
            return new KeyValuePair<long, T>(rawVal, (T)value);
        }

        public T ReadEnum<T>(string name, int bits, params int[] values)
        {
            var val = ReadEnum<T>(bits);
            WriteLine("{0}{1}: {2} ({3}){4}", GetIndexString(values), name, val.Value, val.Key, (Settings.DebugReads ? " (0x" + val.Key.ToString("X4") + ")" : String.Empty));
            return val.Value;
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

        public byte ParseBitStream(byte[] stream, string name, byte value)
        {
            if (stream[value] != 0)
                return stream[value] ^= ReadByte(name);

            return 0;
        }

        public string WriteGuid(byte[] stream)
        {
            var val = new Guid(BitConverter.ToUInt64(stream, 0));
            WriteLine("Guid: {0}", val);
            return val.ToString();
        }

        public string WriteGuid(string name, byte[] stream, params int[] values)
        {
            var val = new Guid(BitConverter.ToUInt64(stream, 0));
            WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val.ToString();
        }
    }
}
