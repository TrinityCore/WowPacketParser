using System;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    public abstract class StoreEnum
    {
        public long rawVal;
        public abstract Type GetEnumType();
    }
    public class StoreEnum<T> : StoreEnum where T : struct, IConvertible
    {
        public static Type type = typeof(T);
        public T val;

        public StoreEnum(long _rawVal)
        {
            rawVal = _rawVal;
            var value = Enum.ToObject(type, rawVal);
            val = (T)value;
        }
        public override Type GetEnumType()
        {
            return type;
        }
        public override string ToString()
        {
            return Enum<T>.ToString(rawVal);
        }
        public static implicit operator T(StoreEnum<T> e)
        {
            return e.val;
        }
    }
}
