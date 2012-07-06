using System;

namespace PacketParser.DataStructures
{
    public abstract class StoreEnum
    {
        public long rawVal;
    }
    public class StoreEnum<T> : StoreEnum
    {
        public T val;
        public StoreEnum(long _rawVal)
        {
            var type = typeof(T);
            rawVal = _rawVal;
            var value = Enum.ToObject(type, rawVal);
            val = (T)value;
        }
        public override string ToString()
        {
            return val.ToString();
        }
        public static implicit operator T(StoreEnum<T> e)
        {
            return e.val;
        }
    }
}
