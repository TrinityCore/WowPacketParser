using System;
using System.Runtime.InteropServices;

namespace WowPacketParser.Misc
{
    [StructLayout(LayoutKind.Explicit)]
    public struct UpdateField
    {
        public UpdateField(uint val) : this()
        {
            UInt32Value = val;
        }

        public UpdateField(int val) : this()
        {
            Int32Value = val;
        }

        public UpdateField(float val) : this()
        {
            FloatValue = val;
        }

        [FieldOffset(0)] public readonly uint UInt32Value;
        [FieldOffset(0)] public readonly int Int32Value;
        [FieldOffset(0)] public readonly float FloatValue;

        public override bool Equals(object obj)
        {
            if (obj is UpdateField)
                return Equals((UpdateField) obj);
            return false;
        }

        public bool Equals(UpdateField other)
        {
            if (UInt32Value == other.UInt32Value)
                return true;

            if (Math.Abs(FloatValue - other.FloatValue) < float.Epsilon)
                return true;

            return false;
        }

        public static bool operator ==(UpdateField first, UpdateField other)
        {
            return first.Equals(other);
        }

        public static bool operator !=(UpdateField first, UpdateField other)
        {
            return !(first == other);
        }

        public override int GetHashCode()
        {
            return UInt32Value.GetHashCode();
        }
    }
}
