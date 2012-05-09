using System;

namespace WowPacketParser.Misc
{
    public struct UpdateField
    {
        public UpdateField(uint val1, float val2)
        {
            UInt32Value = val1;
            SingleValue = val2;
        }

        public readonly uint UInt32Value;

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
            unchecked
            {
                return (UInt32Value.GetHashCode()*397) ^ SingleValue.GetHashCode();
            }
        }
    }
}
