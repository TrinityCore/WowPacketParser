using System;

namespace WowPacketParser.Misc
{
    public struct Quaternion
    {
        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public readonly float X;

        public readonly float Y;

        public readonly float Z;

        public readonly float W;

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y + " Z: " + Z + " W: " + W;
        }

        public override bool Equals(object obj)
        {
            if (obj is Quaternion)
                return Equals((Quaternion)obj);

            return false;
        }

        public bool Equals(Quaternion other)
        {
            return Math.Abs(other.X - X) < float.Epsilon &&
                Math.Abs(other.Y - Y) < float.Epsilon &&
                Math.Abs(other.Z - Z) < float.Epsilon &&
                Math.Abs(other.W - W) < float.Epsilon;
        }

        public static bool operator ==(Quaternion first, Quaternion other)
        {
            return first.Equals(other);
        }

        public static bool operator !=(Quaternion first, Quaternion other)
        {
            return !(first == other);
        }

        public override int GetHashCode()
        {
            var result = X.GetHashCode();
            unchecked
            {
                result = (result * 397) ^ Y.GetHashCode();
                result = (result * 397) ^ Z.GetHashCode();
                result = (result * 397) ^ W.GetHashCode();
            }
            return result;
        }
    }
}
