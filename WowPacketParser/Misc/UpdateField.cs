using System;
using System.Runtime.InteropServices;

namespace WowPacketParser.Misc
{
    [StructLayout(LayoutKind.Explicit)]
    public struct UpdateField
    {
        public UpdateField(uint val)
        {
            SingleValue = 0; // CS0171
            UInt32Value = val;
        }

        public UpdateField(float val)
        {
            UInt32Value = 0; // CS0171
            SingleValue = val;
        }

        [FieldOffset(0)]
        public readonly uint UInt32Value;

        [FieldOffset(0)]
        public readonly float SingleValue;

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

            if (Math.Abs(SingleValue - other.SingleValue) < float.Epsilon)
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
