using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public sealed class Packet : BinaryReader
    {
        public Packet(byte[] input, int opcode, DateTime time, Direction direction, int number)
            : base(new MemoryStream(input, 0, input.Length), Encoding.UTF8)
        {
            Opcode = opcode;
            Time = time;
            Direction = direction;
            Number = number;
        }

        public int Opcode {get; private set; }
        public DateTime Time { get; private set; }
        public Direction Direction { get; private set; }
        public int Number { get; private set; }

        public Packet Inflate(int inflatedSize)
        {
            var arr = ReadToEnd();
            var newarr = new byte[inflatedSize];
            var inflater = new Inflater();
            inflater.SetInput(arr, 0, arr.Length);
            inflater.Inflate(newarr, 0, inflatedSize);

            var pkt = new Packet(newarr, Opcode, Time, Direction, Number);
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

        public Guid ReadGuid()
        {
            return new Guid(ReadUInt64());
        }

        public Guid ReadPackedGuid()
        {
            var mask = ReadByte();

            if (mask == 0)
                return new Guid(0);

            ulong res = 0;

            var i = 0;
            while (i < 8)
            {
                if ((mask & 1 << i) != 0)
                    res += (ulong)ReadByte() << (i * 8);

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
            return Utilities.GetDateTimeFromUnixTime(ReadInt32() / 1000);
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
            var packed = ReadInt32();
            var x = ((packed & 0x7FF) << 21 >> 21) * 0.25f;
            var y = ((((packed >> 11) & 0x7FF) << 21) >> 21) * 0.25f;
            var z = ((packed >> 22 << 22) >> 22) * 0.25f;
            return new Vector3(x, y, z);
        }

        public Vector4 ReadVector4()
        {
            return new Vector4(ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle());
        }

        public Quaternion ReadPackedQuaternion()
        {
            var packed = ReadInt64();
            var x = (packed >> 42) * (1.0f / 2097152.0f);
            var y = (((packed << 22) >> 32) >> 11) * (1.0f / 1048576.0f);
            var z = (packed << 43 >> 43) * (1.0f / 1048576.0f);

            var w = x * x + y * y + z * z;
            if (Math.Abs(w - 1.0f) >= (1 / 1048576.0f))
                w = (float)Math.Sqrt(1.0f - w);
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
            var entry = ReadInt32();
            var masked = (int)(entry & 0x80000000);

            var result = masked != 0;
            if (result)
                entry = masked;

            return new KeyValuePair<int, bool>(entry, result);
        }

        public LfgEntry ReadLfgEntry()
        {
            return new LfgEntry(ReadInt32());
        }

        public UpdateField ReadUpdateField()
        {
            var pos = GetPosition();
            var svalue = ReadInt32();
            SetPosition(pos);
            var fvalue = ReadSingle();

            var field = new UpdateField(svalue, fvalue);
            return field;
        }

        public IPAddress ReadIPAddress()
        {
            var val = ReadBytes(4);
            return new IPAddress(val);
        }

        public T ReadStruct<T>()
            where T : struct
        {
            var rawData = ReadBytes(Marshal.SizeOf(typeof(T)));
            var handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            var returnObject = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return returnObject;
        }

        public byte[] ReadToEnd()
        {
            var length = (int)(GetLength() - GetPosition());
            return ReadBytes(length);
        }

        public byte ReadByte(string name)
        {
            var val = ReadByte();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public byte ReadByte(string name, string prepend, string format)
        {
            var val = ReadByte();
            Console.WriteLine("{0}: {1}{2}", name, prepend, val.ToString(format));
            return val;
        }

        public short ReadInt16(string name)
        {
            var val = ReadInt16();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public short ReadInt16(string name, string prepend, string format)
        {
            var val = ReadInt16();
            Console.WriteLine("{0}: {1}{2}", name, prepend, val.ToString(format));
            return val;
        }

        public int ReadInt32(string name)
        {
            var val = ReadInt32();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public int ReadInt32(string name, string prepend, string format)
        {
            var val = ReadInt32();
            Console.WriteLine("{0}: {1}{2}", name, prepend, val.ToString(format));
            return val;
        }

        public long ReadInt64(string name)
        {
            var val = ReadInt64();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public long ReadInt64(string name, string prepend, string format)
        {
            var val = ReadInt64();
            Console.WriteLine("{0}: {1}{2}", name, prepend, val.ToString(format));
            return val;
        }

        public ushort ReadUInt16(string name)
        {
            var val = ReadUInt16();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public ushort ReadUInt16(string name, string prepend, string format)
        {
            var val = ReadUInt16();
            Console.WriteLine("{0}: {1}{2}", name, prepend, val.ToString(format));
            return val;
        }

        public uint ReadUInt32(string name)
        {
            var val = ReadUInt32();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public uint ReadUInt32(string name, string prepend, string format)
        {
            var val = ReadUInt32();
            Console.WriteLine("{0}: {1}{2}", name, prepend, val.ToString(format));
            return val;
        }

        public ulong ReadUInt64(string name)
        {
            var val = ReadUInt64();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public ulong ReadUInt64(string name, string prepend, string format)
        {
            var val = ReadUInt64();
            Console.WriteLine("{0}: {1}{2}", name, prepend, val.ToString(format));
            return val;
        }

        public Guid ReadGuid(string name)
        {
            var val = ReadGuid();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public string ReadCString(string name)
        {
            var val = ReadCString();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public Guid ReadPackedGuid(string name)
        {
            var val = ReadPackedGuid();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public float ReadSingle(string name)
        {
            var val = ReadSingle();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public bool ReadBoolean(string name)
        {
            var val = ReadBoolean();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public KeyValuePair<int, bool> ReadEntry(string name)
        {
            var entry = ReadEntry();
            Console.WriteLine("{0}: {1}", name, entry.Key);
            return entry;
        }

        public Vector4 ReadVector4(string name)
        {
            var val = ReadVector4();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public Vector3 ReadVector3(string name)
        {
            var val = ReadVector3();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public Quaternion ReadPackedQuaternion(string name)
        {
            var val = ReadPackedQuaternion();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public DateTime ReadTime(string name)
        {
            var val = ReadTime();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public DateTime ReadPackedTime(string name)
        {
            var val = ReadPackedTime();
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        public LfgEntry ReadLfgEntry(string name)
        {
            var val = new LfgEntry(ReadInt32());
            Console.WriteLine("{0}: {1}", name, val);
            return val;
        }

        private KeyValuePair<long, T> ReadEnum<T>(TypeCode code)
        {
            var type = typeof(T);
            long rawVal = 0;

            if (code == TypeCode.Empty)
                code = Type.GetTypeCode(type.GetEnumUnderlyingType());

            switch (code)
            {
                case TypeCode.SByte:
                    rawVal = ReadSByte();
                    break;
                case TypeCode.Byte:
                    rawVal = ReadByte();
                    break;
                case TypeCode.Int16:
                    rawVal = ReadInt16();
                    break;
                case TypeCode.UInt16:
                    rawVal = ReadUInt16();
                    break;
                case TypeCode.Int32:
                    rawVal = ReadInt32();
                    break;
                case TypeCode.UInt32:
                    rawVal = ReadUInt32();
                    break;
                case TypeCode.Int64:
                    rawVal = ReadInt64();
                    break;
                case TypeCode.UInt64:
                    rawVal = (long)ReadUInt64();
                    break;
            }
            var value = Enum.ToObject(type, rawVal);

            return new KeyValuePair<long, T>(rawVal, (T)value);
        }

        public T ReadEnum<T>(string name, TypeCode code = TypeCode.Empty)
        {
            KeyValuePair<long, T> val = ReadEnum<T>(code);   
            Console.WriteLine("{0}: {1} ({2})", name, val.Value, val.Key);
            return val.Value;
        }
    }
}
