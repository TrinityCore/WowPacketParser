using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public sealed partial class Packet
    {
        public WowGuid ReadGuid()
        {
            var guid = new WowGuid(ReadUInt64());
            if (WriteToFile)
                WriteToFile = Filters.CheckFilter(guid);
            return guid;
        }

        public WowGuid ReadPackedGuid()
        {
            var guid = new WowGuid(ReadPackedUInt64());

            if (guid.Full != 0 && WriteToFile)
                WriteToFile = Filters.CheckFilter(guid);

            return guid;
        }

        public ulong ReadPackedUInt64()
        {
            byte mask = ReadByte();

            if (mask == 0)
                return 0;

            ulong res = 0;

            int i = 0;
            while (i < 8)
            {
                if ((mask & 1 << i) != 0)
                    res += (ulong)ReadByte() << (i * 8);

                i++;
            }

            return res;
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

        public T ReadEntry<T>(StoreNameType type, string name, params object[] indexes) where T : struct
        {
            var val = ReadStruct<T>();
            var val32 = Convert.ToInt32(val);
            AddValue(name, StoreGetters.GetName(type, val32) + (Settings.DebugReads ? " (0x" + val32.ToString("X4") + ")" : String.Empty), indexes);
            return val;
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

        public static string GetIndexString(params object[] values)
        {
            return values.Where(value => value != null)
                .Aggregate(string.Empty, (current, value) => current + ("[" + value + "] "));
        }

        public byte ReadByte(string name, params object[] indexes)
        {
            var val = ReadByte();
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X2") + ")" : String.Empty), indexes);
            return val;
        }

        public sbyte ReadSByte(string name, params object[] indexes)
        {
            var val = ReadSByte();
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X2") + ")" : String.Empty), indexes);
            return val;
        }

        public bool ReadBoolean(string name, params object[] indexes)
        {
            var val = ReadBoolean();
            AddValue(name, val ? "true" : "false", indexes);
            return val;
        }

        public bool ReadBoolean(string name, TypeCode code, params object[] indexes)
        {
            var val = ReadValue(code) == 1;
            AddValue(name, val ? "true" : "false", indexes);
            return val;
        }

        public short ReadInt16(string name, params object[] indexes)
        {
            var val = ReadInt16();
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty), indexes);
            return val;
        }

        public ushort ReadUInt16(string name, params object[] indexes)
        {
            var val = ReadUInt16();
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty), indexes);
            return val;
        }

        public float ReadSingle(string name, params object[] indexes)
        {
            var val = ReadSingle();

            if (Settings.DebugReads)
            {
                var bytes = BitConverter.GetBytes(val);
                AddValue(name, val + " (0x" + BitConverter.ToString(bytes) + ")");
            }
            else
                return AddValue(name, val, indexes);

            return val;
        }

        public double ReadDouble(string name, params object[] indexes)
        {
            var val = ReadDouble();

            if (Settings.DebugReads)
            {
                var bytes = BitConverter.GetBytes(val);
                AddValue(name, val + " (0x" + BitConverter.ToString(bytes) + ")");
            }
            else
                return AddValue(name, val, indexes);

            return val;
        }

        public int ReadInt32(string name, params object[] indexes)
        {
            var val = ReadInt32();
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty), indexes);
            return val;
        }

        public uint ReadUInt32(string name, params object[] indexes)
        {
            var val = ReadUInt32();
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty), indexes);
            return val;
        }

        public long ReadInt64(string name, params object[] indexes)
        {
            var val = ReadInt64();
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty), indexes);
            return val;
        }

        public ulong ReadUInt64(string name, params object[] indexes)
        {
            var val = ReadUInt64();
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty), indexes);
            return val;
        }

        public WowGuid ReadGuid(string name, params object[] indexes)
        {
            return AddValue(name, ReadGuid(), indexes);
        }

        public string ReadWoWString(string name, int len, params object[] indexes)
        {
            return AddValue(name, ReadWoWString(len), indexes);
        }

        public string ReadWoWString(string name, uint len, params object[] indexes)
        {
            return AddValue(name, ReadWoWString((int)len), indexes);
        }

        public string ReadCString(string name, params object[] indexes)
        {
            return AddValue(name, ReadCString(), indexes);
        }

        public ulong ReadPackedUInt64(string name, params object[] indexes)
        {
            var val = ReadPackedUInt64();
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty), indexes);
            return val;
        }

        public WowGuid ReadPackedGuid(string name, params object[] indexes)
        {
            return AddValue(name, ReadPackedGuid(), indexes);
        }

        public byte[] ReadBytesString(string name, int length, params object[] indexes)
        {
            var val = ReadBytes(length);
            AddValue(name, Encoding.UTF8.GetString(val), indexes);
            return val;
        }

        public byte[] ReadBytes(string name, int length, params object[] indexes)
        {
            var val = ReadBytes(length);
            AddValue(name, Utilities.ByteArrayToHexString(val), indexes);
            return val;
        }

        public IPAddress ReadIPAddress(string name, params object[] indexes)
        {
            return AddValue(name, ReadIPAddress(), indexes);
        }

        public KeyValuePair<int, bool> ReadEntry(string name, params object[] indexes)
        {
            KeyValuePair<int, bool> entry = ReadEntry();
            AddValue(name, entry.Key, indexes);
            return entry;
        }

        public Vector2 ReadVector2(string name, params object[] indexes)
        {
            return AddValue(name, ReadVector2(), indexes);
        }

        public Vector3 ReadVector3(string name, params object[] indexes)
        {
            return AddValue(name, ReadVector3(), indexes);
        }

        public Vector4 ReadVector4(string name, params object[] indexes)
        {
            return AddValue(name, ReadVector4(), indexes);
        }

        public Quaternion ReadPackedQuaternion(string name, params object[] indexes)
        {
            return AddValue(name, ReadPackedQuaternion(), indexes);
        }

        public DateTime ReadTime(string name, params object[] indexes)
        {
            return AddValue(name, ReadTime(), indexes);
        }

        public DateTime ReadPackedTime(string name, params object[] indexes)
        {
            return AddValue(name, ReadPackedTime(), indexes);
        }

        public LfgEntry ReadLfgEntry(string name, params object[] indexes)
        {
            return AddValue(name, new LfgEntry(ReadInt32()), indexes);
        }

        public long ReadValue(string name, TypeCode typeCode, params object[] indexes)
        {
            var val = ReadValue(typeCode);
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty), indexes);
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

        private T ReadEnum<T>(TypeCode code) where T : struct, IConvertible
        {
            long rawValue = ReadValue(code);
            object value = Enum.ToObject(typeof (T), rawValue);

            if (rawValue > 0)
                Logger.CheckForMissingValues<T>(rawValue);

            return (T) value;
        }

        public T ReadEnum<T>(string name, TypeCode code, params object[] indexes) where T : struct, IConvertible
        {
            var val = ReadEnum<T>(code);
            var val64 = Convert.ToInt64(val);
            AddValue(name, val + " (" + val64 + ")" + (Settings.DebugReads ? " (0x" + val64.ToString("X4") + ")" : String.Empty), indexes);
            return val;
        }

#region BitStream

        private byte _bitpos = 8;
        private byte _curbitval;

        public Bit ReadBit(string name, params object[] indexes)
        {
            return AddValue(name, ReadBit(), indexes);
        }

        public Bit ReadBitBoolean(string name, params object[] indexes)
        {
            var val = ReadBit();
            AddValue(name, val ? "true" : "false", indexes);
            return val;
        }

        public Bit ReadBit()
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

        public uint ReadBits(string name, int bits, params object[] indexes)
        {
            var val = ReadBits(bits);
            AddValue(name, val + (Settings.DebugReads ? " (0x" + val.ToString("X4") + ")" : String.Empty), indexes);
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

        private T ReadEnum<T>(int bits) where T : struct, IConvertible
        {
            var type = typeof(T);
            long rawVal = ReadBits(bits);
            var value = Enum.ToObject(type, rawVal);
            return (T) value;
        }

        public T ReadEnum<T>(string name, int bits, params object[] indexes) where T : struct, IConvertible
        {
            var val = ReadEnum<T>(bits);
            var val64 = Convert.ToInt64(val);
            AddValue(name, val + " (" + val64 + ")" + (Settings.DebugReads ? " (0x" + val64.ToString("X4") + ")" : String.Empty), indexes);
            return val;
        }

        public byte[] StartBitStream(params int[] values)
        {
            var bytes = new byte[values.Length];

            foreach (var value in values)
                bytes[value] = (byte)(ReadBit() ? 1 : 0);

            return bytes;
        }

        public void StartBitStream(byte[] stream, params int[] values)
        {
            foreach (var value in values)
                stream[value] = (byte)(ReadBit() ? 1 : 0);
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

        public void ReadXORBytes(byte[] stream, params byte[] values)
        {
            foreach (var value in values)
                if (stream[value] != 0)
                    stream[value] ^= ReadByte();
        }

#endregion

        public WowGuid WriteGuid(byte[] stream, params object[] indexes)
        {
            return WriteGuid("Guid", stream, indexes);
        }

        public WowGuid WriteGuid(string name, byte[] stream, params object[] indexes)
        {
            return AddValue(name, new WowGuid(BitConverter.ToUInt64(stream, 0)), indexes);
        }
    }
}
