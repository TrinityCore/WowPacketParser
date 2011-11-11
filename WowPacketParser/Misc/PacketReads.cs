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
            return new Guid(ReadUInt64());
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

            return new Guid(res);
        }

        public DateTime ReadTime()
        {
            return Utilities.GetDateTimeFromUnixTime(ReadInt32());
        }

        public DateTime ReadMillisecondTime()
        {
            return Utilities.GetDateTimeFromUnixTime(ReadInt32()/1000);
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

        public string ReadCString(Encoding encoding)
        {
            var bytes = new List<byte>();

            byte b;
            while ((b = ReadByte()) != 0)
                bytes.Add(b);

            return encoding.GetString(bytes.ToArray());
        }

        public string ReadCString()
        {
            return ReadCString(Encoding.UTF8);
        }

        public KeyValuePair<int, bool> ReadEntry()
        {
            uint entry = ReadUInt32();
            uint masked = entry & 0x80000000;

            bool result = masked != 0;
            if (result)
                entry = entry ^ 0x80000000;

            return new KeyValuePair<int, bool>((int) entry, result);
        }

        public LfgEntry ReadLfgEntry()
        {
            return new LfgEntry(ReadInt32());
        }

        public UpdateField ReadUpdateField()
        {
            long pos = GetPosition();
            int svalue = ReadInt32();
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
            var length = (int) (GetLength() - GetPosition());
            return ReadBytes(length);
        }

        private string GetIndexString(params int[] values)
        {
            string indexes = string.Empty;

            foreach (int value in values)
            {
                if (value == -1) continue;
                indexes += "[" + value + "] ";
            }

            return indexes;
        }

        public byte ReadByte(string name, params int[] values)
        {
            byte val = ReadByte();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public sbyte ReadSByte(string name, params int[] values)
        {
            sbyte val = ReadSByte();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public bool ReadBoolean(string name, params int[] values)
        {
            bool val = ReadBoolean();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public bool ReadBoolean(string name, TypeCode code, params int[] values)
        {
            bool val = ReadValue(code) == 1;
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public short ReadInt16(string name, params int[] values)
        {
            short val = ReadInt16();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public ushort ReadUInt16(string name, params int[] values)
        {
            ushort val = ReadUInt16();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public float ReadSingle(string name, params int[] values)
        {
            float val = ReadSingle();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public double ReadDouble(string name, params int[] values)
        {
            double val = ReadDouble();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public int ReadInt32(string name, params int[] values)
        {
            int val = ReadInt32();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public uint ReadUInt32(string name, params int[] values)
        {
            uint val = ReadUInt32();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public long ReadInt64(string name, params int[] values)
        {
            long val = ReadInt64();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public ulong ReadUInt64(string name, params int[] values)
        {
            ulong val = ReadUInt64();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public Guid ReadGuid(string name, params int[] values)
        {
            Guid val = ReadGuid();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public string ReadCString(string name, params int[] values)
        {
            string val = ReadCString();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public Guid ReadPackedGuid(string name, params int[] values)
        {
            Guid val = ReadPackedGuid();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public KeyValuePair<int, bool> ReadEntry(string name, params int[] values)
        {
            KeyValuePair<int, bool> entry = ReadEntry();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, entry.Key);
            return entry;
        }

        public Vector3 ReadVector3(string name, params int[] values)
        {
            Vector3 val = ReadVector3();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public Vector4 ReadVector4(string name, params int[] values)
        {
            Vector4 val = ReadVector4();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public Quaternion ReadPackedQuaternion(string name, params int[] values)
        {
            Quaternion val = ReadPackedQuaternion();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public DateTime ReadTime(string name, params int[] values)
        {
            DateTime val = ReadTime();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public DateTime ReadPackedTime(string name, params int[] values)
        {
            DateTime val = ReadPackedTime();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }

        public LfgEntry ReadLfgEntry(string name, params int[] values)
        {
            var val = new LfgEntry(ReadInt32());
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
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
            return new KeyValuePair<long, T>(rawValue, (T) value);
        }

        public T ReadEnum<T>(string name, TypeCode code, params int[] values)
        {
            KeyValuePair<long, T> val = ReadEnum<T>(code);
            Writer.WriteLine("{0}{1}: {2} ({3})", GetIndexString(values), name, val.Value, val.Key);
            return val.Value;
        }

        public int ReadEntryWithName<T>(StoreNameType type, string name, params int[] values)
        {
            var val = (int) ReadValue(Type.GetTypeCode(typeof (T)));
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, StoreGetters.GetName(type, val));
            return val;
        }

        // Bitstream reader - start
        private byte _bitpos = 8;
        private byte _curbitval = 0;

        public bool ReadBit(string name, params int[] values)
        {
            var bit = ReadBit();
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, bit ? "1" : "0");
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
            Writer.WriteLine("{0}{1}: {2}", GetIndexString(values), name, val);
            return val;
        }
        public uint ReadBits(int bits)
        {
            uint value = 0;
            for (int i = bits - 1; i >= 0; --i)
                if (ReadBit())
                    value |= (uint)((1 << (_bitpos)));
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
            Writer.WriteLine("{0}{1}: {2} ({3})", GetIndexString(values), name, val.Value, val.Key);
            return val.Value;
        }
        // Bitstream reader - end

    }
}
