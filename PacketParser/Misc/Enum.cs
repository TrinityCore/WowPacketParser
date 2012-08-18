/*
 * This file was downloaded from the following location 
 * http://www.codeproject.com/KB/dotnet/enum.aspx
 * 
 * Author: Boris Dongarov (ideafixxxer)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketParser.Misc
{
    #region Enum

    /// <summary>
    /// Helper class for enum types
    /// </summary>
    /// <typeparam name="T">Must be enum type (declared using <c>enum</c> keyword)</typeparam>
    public static class Enum<T> where T : struct, IConvertible
    {
        private static readonly EnumConverter Converter;

        private static readonly FlagChecker Checker;

        abstract class FlagChecker
        {
            public abstract bool HasFlag(dynamic val, dynamic flag);
        }

        class FlagCheckerUInt64 : FlagChecker
        {
            public override bool HasFlag(dynamic val, dynamic flag)
            {
                return ((UInt64)val & (UInt64)flag) != 0;
            }
        }

        class FlagCheckerInt64 : FlagChecker
        {
            public override bool HasFlag(dynamic val, dynamic flag)
            {
                return ((Int64)val & (Int64)flag) != 0;
            }
        }

        class FlagCheckerUInt32 : FlagChecker
        {
            public override bool HasFlag(dynamic val, dynamic flag)
            {
                return ((UInt32)val & (UInt32)flag) != 0;
            }
        }

        class FlagCheckerInt32 : FlagChecker
        {
            public override bool HasFlag(dynamic val, dynamic flag)
            {
                return ((Int32)val & (Int32)flag) != 0;
            }
        }

        class FlagCheckerUInt16 : FlagChecker
        {
            public override bool HasFlag(dynamic val, dynamic flag)
            {
                return ((UInt16)val & (UInt16)flag) != 0;
            }
        }

        class FlagCheckerInt16 : FlagChecker
        {
            public override bool HasFlag(dynamic val, dynamic flag)
            {
                return ((Int16)val & (Int16)flag) != 0;
            }
        }

        class FlagCheckerByte : FlagChecker
        {
            public override bool HasFlag(dynamic val, dynamic flag)
            {
                return ((Byte)val & (Byte)flag) != 0;
            }
        }

        class FlagCheckerSByte : FlagChecker
        {
            public override bool HasFlag(dynamic val, dynamic flag)
            {
                return ((SByte)val & (SByte)flag) != 0;
            }
        }

        #region Nested types
        abstract class EnumConverter
        {
            public abstract string ToStringInternal(long value);
            public abstract long ParseInternal(string value, bool ignoreCase, bool parseNumber);
            public abstract bool TryParseInternal(string value, bool ignoreCase, bool parseNumber, out long result);
        }

        class ArrayEnumConverter : EnumConverter
        {
            private readonly string[] _names = Enum.GetNames(typeof(T));

            public ArrayEnumConverter(string[] names)
            {
                _names = names;
            }

            public override string ToStringInternal(long value)
            {
                return value >= 0 && value < _names.Length ? _names[value] : value.ToString();
            }

            public override long ParseInternal(string value, bool ignoreCase, bool parseNumber)
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value.Length == 0) throw new ArgumentException("Value is empty", "value");
                char f = value[0];
                if (parseNumber && (Char.IsDigit(f) || f == '+' || f == '-'))
                    return Int64.Parse(value);
                StringComparison stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                for (long i = 0; i < _names.Length; i++)
                    if (_names[i].Equals(value, stringComparison))
                        return i;
                throw new ArgumentException("Enum value wasn't found", "value");
            }

            public override bool TryParseInternal(string value, bool ignoreCase, bool parseNumber, out long result)
            {
                result = 0;
                if (String.IsNullOrEmpty(value)) return false;
                char f = value[0];
                if (parseNumber && (Char.IsDigit(f) || f == '+' || f == '-'))
                {
                    long i;
                    if (Int64.TryParse(value, out i))
                    {
                        result = i;
                        return true;
                    }
                    return false;
                }
                StringComparison stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                for (long i = 0; i < _names.Length; i++)
                    if (_names[i].Equals(value, stringComparison))
                    {
                        result = i;
                        return true;
                    }
                return false;
            }
        }

        class DictionaryEnumConverter : EnumConverter
        {
            protected readonly Dictionary<long, string> _dic;

            public DictionaryEnumConverter(string[] names, T[] values)
            {
                _dic = new Dictionary<long, string>(names.Length);
                for (long j = 0; j < names.Length; j++)
                    _dic.Add(Convert.ToInt64(values[j], null), names[j]);
            }

            public override string ToStringInternal(long value)
            {
                string n;
                return _dic.TryGetValue(value, out n) ? n : value.ToString();
            }

            public override long ParseInternal(string value, bool ignoreCase, bool parseNumber)
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value.Length == 0) throw new ArgumentException("Value is empty", "value");
                char f = value[0];
                if (parseNumber && (Char.IsDigit(f) || f == '+' || f == '-'))
                    return Int64.Parse(value);
                StringComparison stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                foreach (KeyValuePair<long, string> pair in _dic)
                {
                    if (pair.Value.Equals(value, stringComparison))
                        return pair.Key;
                }
                throw new ArgumentException("Enum value wasn't found", "value");
            }

            public override bool TryParseInternal(string value, bool ignoreCase, bool parseNumber, out long result)
            {
                result = 0;
                if (String.IsNullOrEmpty(value)) return false;
                char f = value[0];
                if (parseNumber && (Char.IsDigit(f) || f == '+' || f == '-'))
                {
                    long i;
                    if (Int64.TryParse(value, out i))
                    {
                        result = i;
                        return true;
                    }
                    return false;
                }
                StringComparison stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                foreach (KeyValuePair<long, string> pair in _dic)
                {
                    if (pair.Value.Equals(value, stringComparison))
                    {
                        result = pair.Key;
                        return true;
                    }
                }
                return false;
            }
        }

        class FlagsEnumConverter : DictionaryEnumConverter
        {
            private readonly ulong[] _values;
            private static readonly string[] Seps = new[] { "," };

            public FlagsEnumConverter(string[] names, T[] values)
                : base(names, values)
            {
                _values = new ulong[values.Length];
                for (long i = 0; i < values.Length; i++)
                    _values[i] = values[i].ToUInt64(null);
            }

            public override string ToStringInternal(long value)
            {
                string n;
                if (_dic.TryGetValue(value, out n)) return n;
                var sb = new StringBuilder();
                const string sep = ", ";
                ulong uval;
                unchecked
                {
                    uval = (ulong)value;

                    for (long i = _values.Length - 1; i >= 0; i--)
                    {
                        ulong v = _values[i];
                        if (v == 0) continue;
                        if ((v & uval) == v)
                        {
                            uval &= ~v;
                            sb.Insert(0, sep).Insert(0, _dic[(long)v]);
                        }
                    }
                }
                return uval == 0 && sb.Length > sep.Length ? sb.ToString(0, sb.Length - sep.Length) : value.ToString();
            }

            public override long ParseInternal(string value, bool ignoreCase, bool parseNumber)
            {
                string[] parts = value.Split(Seps, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 1) return base.ParseInternal(value, ignoreCase, parseNumber);
                long val = 0;
                for (long i = 0; i < parts.Length; i++)
                {
                    string part = parts[i];
                    long t = base.ParseInternal(part.Trim(), ignoreCase, parseNumber);
                    val |= t;
                }
                return val;
            }

            public override bool TryParseInternal(string value, bool ignoreCase, bool parseNumber, out long result)
            {
                string[] parts = value.Split(Seps, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 1) return base.TryParseInternal(value, ignoreCase, parseNumber, out result);
                long val = 0;
                for (long i = 0; i < parts.Length; i++)
                {
                    string part = parts[i];
                    long t;
                    if (!base.TryParseInternal(part.Trim(), ignoreCase, parseNumber, out t))
                    {
                        result = 0;
                        return false;
                    }
                    val |= t;
                }
                result = val;
                return true;
            }
        }

        #endregion

        static Enum()
        {
            Type type = typeof(T);
            if (!type.IsEnum) 
                throw new ArgumentException("Generic Enum type works only with enums");
            string[] names = Enum.GetNames(type);
            var values = (T[])Enum.GetValues(type);
            if (type.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0)
            {
                Converter = new FlagsEnumConverter(names, values);
            }
            else
            {
                if (values.Where((t, i) => Convert.ToInt64(t) != i).Any())
                {
                    Converter = new DictionaryEnumConverter(names, values);
                }
                if (Converter == null)
                    Converter = new ArrayEnumConverter(names);
            }

            switch (Type.GetTypeCode(Enum.GetUnderlyingType(type)))
            {
                case TypeCode.UInt64:
                    Checker = new FlagCheckerUInt64();
                    break;
                case TypeCode.Int64:
                    Checker = new FlagCheckerInt64();
                    break;
                case TypeCode.UInt32:
                    Checker = new FlagCheckerUInt32();
                    break;
                case TypeCode.Int32:
                    Checker = new FlagCheckerInt32();
                    break;
                case TypeCode.UInt16:
                    Checker = new FlagCheckerUInt16();
                    break;
                case TypeCode.Int16:
                    Checker = new FlagCheckerInt16();
                    break;
                case TypeCode.Byte:
                    Checker = new FlagCheckerByte();
                    break;
                case TypeCode.SByte:
                    Checker = new FlagCheckerSByte();
                    break;
                default:
                    throw new Exception("Unknown underlying type!");
            }
        }

        public static bool HasFlag(T val, T flag)
        {
            return Checker.HasFlag(val, flag);
        }

        /// <summary>
        /// Converts enum value to string
        /// </summary>
        /// <param name="value">Enum value converted to long</param>
        /// <returns>If <paramref name="value"/> is defined, the enum member name; otherwise the string representation of the <paramref name="value"/>.
        /// If <see cref="FlagsAttribute"/> is applied, can return comma-separated list of values</returns>
        public static string ToString(long value)
        {
            return Converter.ToStringInternal(value);
        }

        /// <summary>
        /// Converts enum value to string
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>If <paramref name="value"/> is defined, the enum member name; otherwise the string representation of the <paramref name="value"/>.
        /// If <see cref="FlagsAttribute"/> is applied, can return comma-separated list of values</returns>
        public static string ToString(T value)
        {
            return Converter.ToStringInternal(value.ToInt64(null));
        }

        public static T Parse(string value, bool ignoreCase = false, bool parseNumeric = true)
        {
            return (T) Enum.ToObject(typeof(T), Converter.ParseInternal(value, ignoreCase, parseNumeric));
        }

        public static bool TryParse(string value, bool ignoreCase, bool parseNumeric, out T result)
        {
            long ir;
            bool b = Converter.TryParseInternal(value, ignoreCase, parseNumeric, out ir);
            result = (T) Enum.ToObject(typeof(T), ir);
            return b;
        }

        public static bool TryParse(string value, bool ignoreCase, out T result)
        {
            long ir;
            bool b = Converter.TryParseInternal(value, ignoreCase, true, out ir);
            result = (T)Enum.ToObject(typeof(T), ir);
            return b;
        }

        public static bool TryParse(string value, out T result)
        {
            long ir;
            bool b = Converter.TryParseInternal(value, false, true, out ir);
            result = (T)Enum.ToObject(typeof(T), ir);
            return b;
        }
    }

    #endregion
}
