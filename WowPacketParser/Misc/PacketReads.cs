using System;
using System.Collections.Generic;
using System.Globalization;
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
            var guid = new WowGuid64(ReadUInt64());
            if (WriteToFile)
                WriteToFile = Filters.CheckFilter(guid);
            return guid;
        }

        public WowGuid ReadPackedGuid()
        {
            var guid = new WowGuid64(ReadPackedUInt64());

            if (!guid.IsEmpty() && WriteToFile)
                WriteToFile = Filters.CheckFilter(guid);

            return guid;
        }

        public WowGuid ReadPackedGuid128()
        {
            var guidLowMask = ReadByte();
            var guidHighMask = ReadByte();

            var guid = new WowGuid128(ReadPackedUInt64(guidLowMask), ReadPackedUInt64(guidHighMask));
            if (WriteToFile)
                WriteToFile = Filters.CheckFilter(guid);

            return guid;
        }

        public WowGuid WriteGuid(string name, byte[] stream, params object[] indexes)
        {
            return AddValue(name, new WowGuid64(BitConverter.ToUInt64(stream, 0)), indexes);
        }

        public ulong ReadPackedUInt64()
        {
            return ReadPackedUInt64(ReadByte());
        }

        private ulong ReadPackedUInt64(byte mask)
        {
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

        public DateTime ReadTime64()
        {
            return Utilities.GetDateTimeFromUnixTime(ReadInt64());
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
            return new Quaternion(packed);
        }

        public Quaternion ReadQuaternion()
        {
            return new Quaternion(ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle());
        }

        public string ReadWoWString(int len)
        {
            Encoding encoding = Encoding.UTF8;
            var bytes = ReadBytes(len).Where(b => b != 0).ToArray();
            string s = encoding.GetString(bytes);
            return s;
        }

        public string ReadDynamicString(int len)
        {
            if (len == 1)
                return string.Empty;

            return ReadWoWString(len);
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
            // Entries masked with 0x80000000 are invalid entries OR used to tell apart NPCs and GOs

            var entry = ReadUInt32();
            var realEntry = entry & 0x7FFFFFFF;

            return new KeyValuePair<int, bool>((int)realEntry, realEntry != entry);
        }

        private T ReadEntry<T>(StoreNameType type, string name, params object[] indexes) where T : struct
        {
            var val = ReadValue<T>();
            int val32;
            if (val > Int32.MaxValue)
                val32 = unchecked((int)Convert.ToUInt32(val));
            else
                val32 = Convert.ToInt32(val);

            if (type == StoreNameType.Spell && Settings.ParseSpellInfos)
                ReadSpellEntry((uint)val32, name, indexes);
            else
                AddValue(name, FormatInteger(val32, StoreGetters.GetName(type, val32, false)), indexes);

            return (T) Convert.ChangeType(val, typeof (T));
        }

        public string ReadSpellEntry(uint spellID, string name, params object[] indexes)
        {
            AddValue(name, FormatInteger(spellID, StoreGetters.GetName(StoreNameType.Spell, (int)spellID, false)), indexes);

            string spellInfo = string.Empty;
            for (uint idx = 0; idx < 32; idx++)
            {
                var tuple = Tuple.Create(spellID, idx);
                if (DBC.DBC.SpellEffectStores.ContainsKey(tuple))
                {
                    spellInfo += Environment.NewLine;
                    var effect = DBC.DBC.SpellEffectStores[tuple];
                    var difficulty = DBC.DBC.Difficulty.ContainsKey((int)effect.DifficultyID) ? DBC.DBC.Difficulty[(int)effect.DifficultyID].Name : "DIFFICULTY_NONE";
                    var aura = (AuraTypeLegion)effect.EffectAura;
                    var misc = effect.EffectMiscValue[0];

                    AddValue("SpellInfo", $"Effect { effect.EffectIndex }: Id { effect.Effect  } ({ (SpellEffects)effect.Effect  }) { difficulty }", indexes);
                    AddValue("SpellInfo", $"Targets ({  effect.ImplicitTarget[0] }, { effect.ImplicitTarget[1] }) ({ (Targets)effect.ImplicitTarget[0] }, { (Targets)effect.ImplicitTarget[1] })", indexes);

                    if (effect.EffectAura == 0)
                    {
                        AddValue("SpellInfo", $"EffectMiscValueA = { effect.EffectMiscValue[0] }", indexes);
                        AddValue("SpellInfo", $"EffectMiscValueB = { effect.EffectMiscValue[1] }", indexes);
                        AddValue("SpellInfo", $"EffectAmplitude = { effect.EffectAmplitude }", indexes);
                        continue;
                    }

                    string auraInfo = string.Empty;
                    auraInfo += $"Aura Id {aura:D} ({ aura })";
                    auraInfo += $", value = { effect.EffectBasePointsF }";
                    auraInfo += $", misc = { misc } (";

                    switch (aura)
                    {
                        case AuraTypeLegion.SPELL_AURA_MOD_STAT:
                            spellInfo += $"{ (UnitMods)misc }";
                            break;
                        case AuraTypeLegion.SPELL_AURA_MOD_RATING:
                        case AuraTypeLegion.SPELL_AURA_MOD_RATING_PCT:
                            spellInfo += $"{ (CombatRating)misc }";
                            break;
                        case AuraTypeLegion.SPELL_AURA_ADD_FLAT_MODIFIER:
                        case AuraTypeLegion.SPELL_AURA_ADD_PCT_MODIFIER:
                            spellInfo += $"{ (SpellModOp)misc }";
                            break;
                        default:
                            spellInfo += $"{ misc }";
                            break;
                    }

                    auraInfo += $"), miscB = { effect.EffectMiscValue[1] } , amplitude = { effect.EffectAmplitude }, periodic = { effect.EffectAuraPeriod }";

                    AddValue("SpellInfo", auraInfo, indexes);
                }
            }

            return spellInfo;
        }

        public LfgEntry ReadLfgEntry()
        {
            return new LfgEntry(ReadInt32());
        }

        public UpdateField ReadUpdateField()
        {
            uint val = ReadUInt32();

            var field = new UpdateField(val);
            return field;
        }

        public IPAddress ReadIPAddress()
        {
            byte[] val = ReadBytes(4);
            return new IPAddress(val);
        }

        public IPAddress ReadIPv6Address()
        {
            byte[] val = ReadBytes(16);
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

        private TEnum ReadEnum<TEnum, T>(string name, params object[] indexes) where TEnum : struct, IConvertible
            where T : struct, IEquatable<T>
        {
            var e = ReadEnum<TEnum, T>();
            AddValue(name, FormatInteger(Convert.ToInt64(e), e.ToString(CultureInfo.InvariantCulture)), indexes);
            return e;
        }

        public TEnum ReadByteE<TEnum>(string name, params object[] idx) where TEnum : struct, IConvertible { return ReadEnum<TEnum, Byte>(name, idx); }
        public TEnum ReadSByteE<TEnum>(string name, params object[] idx) where TEnum : struct, IConvertible { return ReadEnum<TEnum, SByte>(name, idx); }
        public TEnum ReadInt16E<TEnum>(string name, params object[] idx) where TEnum : struct, IConvertible { return ReadEnum<TEnum, Int16>(name, idx); }
        public TEnum ReadUInt16E<TEnum>(string name, params object[] idx) where TEnum : struct, IConvertible { return ReadEnum<TEnum, UInt16>(name, idx); }
        public TEnum ReadInt32E<TEnum>(string name, params object[] idx) where TEnum : struct, IConvertible { return ReadEnum<TEnum, Int32>(name, idx); }
        public TEnum ReadUInt32E<TEnum>(string name, params object[] idx) where TEnum : struct, IConvertible { return ReadEnum<TEnum, UInt32>(name, idx); }
        public TEnum ReadInt64E<TEnum>(string name, params object[] idx) where TEnum : struct, IConvertible { return ReadEnum<TEnum, Int64>(name, idx); }
        public TEnum ReadUInt64E<TEnum>(string name, params object[] idx) where TEnum : struct, IConvertible { return ReadEnum<TEnum, UInt64>(name, idx); }

        public TEnum ReadByteE<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, Byte>(); }
        public TEnum ReadSByteE<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, SByte>(); }
        public TEnum ReadInt16E<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, Int16>(); }
        public TEnum ReadUInt16E<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, UInt16>(); }
        public TEnum ReadInt32E<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, Int32>(); }
        public TEnum ReadUInt32E<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, UInt32>(); }
        public TEnum ReadInt64E<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, Int64>(); }
        public TEnum ReadUInt64E<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, UInt64>(); }

        public SByte ReadSByte<T>(string name, params object[] idx) where T : IId { return ReadEntry<SByte>(StoreName.ToEnum<T>(), name, idx); }
        public Byte ReadByte<T>(string name, params object[] idx) where T : IId { return ReadEntry<Byte>(StoreName.ToEnum<T>(), name, idx); }
        public Int16 ReadInt16<T>(string name, params object[] idx) where T : IId { return ReadEntry<Int16>(StoreName.ToEnum<T>(), name, idx); }
        public UInt16 ReadUInt16<T>(string name, params object[] idx) where T : IId { return ReadEntry<UInt16>(StoreName.ToEnum<T>(), name, idx); }
        public Int32 ReadInt32<T>(string name, params object[] idx) where T : IId { return ReadEntry<Int32>(StoreName.ToEnum<T>(), name, idx); }
        public UInt32 ReadUInt32<T>(string name, params object[] idx) where T : IId { return ReadEntry<UInt32>(StoreName.ToEnum<T>(), name, idx); }
        public Int64 ReadInt64<T>(string name, params object[] idx) where T : IId { return ReadEntry<Int64>(StoreName.ToEnum<T>(), name, idx); }
        public UInt64 ReadUInt64<T>(string name, params object[] idx) where T : IId { return ReadEntry<UInt64>(StoreName.ToEnum<T>(), name, idx); }

        public byte ReadByte(string name, params object[] indexes)
        {
            var val = ReadByte();
            AddValue(name, FormatInteger(val), indexes);
            return val;
        }

        public sbyte ReadSByte(string name, params object[] indexes)
        {
            var val = ReadSByte();
            AddValue(name, FormatInteger(val), indexes);
            return val;
        }

        public bool ReadBool(string name, params object[] indexes)
        {
            var val = ReadBoolean();
            AddValue(name, val ? "true" : "false", indexes);
            return val;
        }

        public bool ReadBool<T>(string name, params object[] indexes) where T : struct, IEquatable<T>
        {
            var val = ReadValue<T>().Equals(default(T));
            AddValue(name, !val ? "true" : "false", indexes);
            return val;
        }

        public short ReadInt16(string name, params object[] indexes)
        {
            var val = ReadInt16();
            AddValue(name, FormatInteger(val), indexes);
            return val;
        }

        public ushort ReadUInt16(string name, params object[] indexes)
        {
            var val = ReadUInt16();
            AddValue(name, FormatInteger(val), indexes);
            return val;
        }

        public float ReadSingle(string name, params object[] indexes)
        {
            var val = ReadSingle();
            AddValue(name, FormatFloat(val), indexes);
            return val;
        }

        public double ReadDouble(string name, params object[] indexes)
        {
            var val = ReadDouble();
            AddValue(name, FormatFloat(val), indexes);
            return val;
        }

        public int ReadInt32(string name, params object[] indexes)
        {
            var val = ReadInt32();
            AddValue(name, FormatInteger(val), indexes);
            return val;
        }

        public uint ReadUInt32(string name, params object[] indexes)
        {
            var val = ReadUInt32();
            AddValue(name, FormatInteger(val), indexes);
            return val;
        }

        public long ReadInt64(string name, params object[] indexes)
        {
            var val = ReadInt64();
            AddValue(name, FormatInteger(val), indexes);
            return val;
        }

        public ulong ReadUInt64(string name, params object[] indexes)
        {
            var val = ReadUInt64();
            AddValue(name, FormatInteger(val), indexes);
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

        public string ReadDynamicString(string name, int len, params object[] indexes)
        {
            return AddValue(name, ReadDynamicString(len), indexes);
        }

        public string ReadDynamicString(string name, uint len, params object[] indexes)
        {
            return AddValue(name, ReadDynamicString((int)len), indexes);
        }

        public string ReadCString(string name, params object[] indexes)
        {
            return AddValue(name, ReadCString(), indexes);
        }

        public ulong ReadPackedUInt64(string name, params object[] indexes)
        {
            var val = ReadPackedUInt64();
            AddValue(name, FormatInteger(val), indexes);
            return val;
        }

        public WowGuid ReadPackedGuid(string name, params object[] indexes)
        {
            return AddValue(name, ReadPackedGuid(), indexes);
        }

        public WowGuid ReadPackedGuid128(string name, params object[] indexes)
        {
            return AddValue(name, ReadPackedGuid128(), indexes);
        }

        public byte[] ReadBytesString(string name, int length, params object[] indexes)
        {
            var val = ReadBytes(length);
            AddValue(name, Encoding.UTF8.GetString(val), indexes);
            return val;
        }

        public byte[] ReadBytesTable(string name, int length, params object[] indexes)
        {
            var val = ReadBytes(length);
            AddValue(name, Utilities.ByteArrayToHexTable(val, true,
                GetIndexString(indexes).Length + name.Length + ": ".Length),
                indexes);
            return val;
        }

        public byte[] ReadBytes(string name, int length, params object[] indexes)
        {
            var val = ReadBytes(length);
            AddValue(name, Convert.ToHexString(val), indexes);
            return val;
        }

        public IPAddress ReadIPAddress(string name, params object[] indexes)
        {
            return AddValue(name, ReadIPAddress(), indexes);
        }
        public IPAddress ReadIPv6Address(string name, params object[] indexes)
        {
            return AddValue(name, ReadIPv6Address(), indexes);
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

        public Quaternion ReadQuaternion(string name, params object[] indexes)
        {
            return AddValue(name, ReadQuaternion(), indexes);
        }

        public DateTime ReadTime(string name, params object[] indexes)
        {
            return AddValue(name, ReadTime(), indexes);
        }

        public DateTime ReadTime64(string name, params object[] indexes)
        {
            return AddValue(name, ReadTime64(), indexes);
        }

        public DateTime ReadPackedTime(string name, params object[] indexes)
        {
            return AddValue(name, ReadPackedTime(), indexes);
        }

        public LfgEntry ReadLfgEntry(string name, params object[] indexes)
        {
            return AddValue(name, ReadLfgEntry(), indexes);
        }

        public long ReadValue(string name, TypeCode typeCode, params object[] indexes)
        {
            var val = ReadValue(typeCode);
            AddValue(name, FormatInteger(val), indexes);
            return val;
        }

        private long ReadValue<T>() where T : struct
        {
            var code = Type.GetTypeCode(typeof (T));
            return ReadValue(code);
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

        private TEnum ReadEnum<TEnum, T>() where TEnum : struct, IConvertible where T : struct
        {
            var rawValue = Convert.ToInt64(ReadValue<T>());
            var value = Enum.ToObject(typeof (TEnum), rawValue);

            if (rawValue > 0)
                Logger.CheckForMissingValues<TEnum>(rawValue);

            return (TEnum) value;
        }

#region BitStream

        private byte _bitpos = 8;
        private byte _curbitval;

        public Bit ReadBit(string name, params object[] indexes)
        {
            return AddValue(name, ReadBit(), indexes);
        }

        public Bit ReadBit()
        {
            if (_bitpos == 8)
            {
                _bitpos = 0;
                _curbitval = ReadByte();
            }

            var bit = ((_curbitval >> (7 - _bitpos)) & 1) != 0;
            ++_bitpos;
            return bit;
        }

        public bool HasUnreadBitsInBuffer()
        {
            return _bitpos != 8;
        }

        public void ResetBitReader()
        {
            _bitpos = 8;
        }

        public uint ReadBits(string name, int bits, params object[] indexes)
        {
            var val = ReadBits(bits);
            AddValue(name, FormatInteger(val), indexes);
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

        private object ReadBitsE(Type type, int bits)
        {
            long rawVal = ReadBits(bits);
            return Enum.ToObject(type, rawVal);
        }

        private TEnum ReadBitsE<TEnum>(int bits) where TEnum : struct, IConvertible
        {
            return (TEnum)ReadBitsE(typeof(TEnum), bits);
        }

        public object ReadBitsE(Type type, string name, int bits, params object[] indexes)
        {
            var val = ReadBitsE(type, bits);
            var val64 = Convert.ToInt64(val);
            AddValue(name, FormatInteger(val64, val.ToString()), indexes);
            return val;
        }

        public TEnum ReadBitsE<TEnum>(string name, int bits, params object[] indexes) where TEnum : struct, IConvertible
        {
            var val = ReadBitsE<TEnum>(bits);
            var val64 = Convert.ToInt64(val);
            AddValue(name, FormatInteger(val64, val.ToString(CultureInfo.InvariantCulture)), indexes);
            return val;
        }

        private uint ReadEntry(StoreNameType type, string name, int bits, params object[] indexes)
        {
            var val = ReadBits(bits);
            AddValue(name, FormatInteger(val, StoreGetters.GetName(type, (int) val, false)), indexes);
            return val;
        }

        public uint ReadBits<T>(string name, int bits, params object[] idx) where T : IId { return ReadEntry(StoreName.ToEnum<T>(), name, bits, idx); }

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

        private static string FormatInteger(IFormattable value)
        {
            if (Settings.DebugReads)
                return value + " (0x" + value.ToString("X4", NumberFormatInfo.CurrentInfo) + ")";
            return value.ToString();
        }

        private static string FormatFloat(float value)
        {
            if (!Settings.DebugReads)
                return string.Format("{0:F20}", value).Substring(0, 20).TrimEnd('0').TrimEnd('.');

            var bytes = BitConverter.GetBytes(value);
            return value + " (0x" + BitConverter.ToString(bytes) + ")";
        }

        private string FormatFloat(double value)
        {
            if (!Settings.DebugReads)
                return value.ToString(CultureInfo.InvariantCulture);

            var bytes = BitConverter.GetBytes(value);
            return value + " (0x" + BitConverter.ToString(bytes) + ")";
        }

        private static string FormatInteger(IFormattable value, string name)
        {
            if (Settings.DebugReads)
                return $"{value} (0x{value:X4}) ({name})";
            return value + " (" + name + ")";
        }

        private static string GetIndexString(params object[] values)
        {
            var list = values.Flatten();

            return list.Where(value => value != null)
                .Aggregate(string.Empty, (current, value) =>
                {
                    var s = value is string ? "()" : "[]";
                    return current + (s[0] + value.ToString() + s[1] + ' ');
                });
        }
    }
}
