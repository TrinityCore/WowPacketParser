using System;

namespace WowPacketParser.Misc
{
    public struct Vector3
    {
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X;

        public float Y;

        public float Z;

        public static bool operator ==(Vector3 first, Vector3 other)
        {
            return first.Equals(other);
        }

        public static bool operator !=(Vector3 first, Vector3 other)
        {
            return !(first == other);
        }

        public static Vector3 operator +(Vector3 c1, Vector3 c2)
        {
            return new Vector3(c1.X + c2.X, c1.Y + c2.Y, c1.Z + c2.Z);
        }

        public static Vector3 operator -(Vector3 c1, Vector3 c2)
        {
            return new Vector3(c1.X - c2.X, c1.Y - c2.Y, c1.Z - c2.Z);
        }

        public static Vector3 operator *(Vector3 c1, float c2)
        {
            return new Vector3(c1.X * c2, c1.Y * c2, c1.Z * c2);
        }

        public static Vector3 operator /(Vector3 c1, float c2)
        {
            return new Vector3(c1.X / c2, c1.Y / c2, c1.Z / c2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
                return Equals((Vector3)obj);

            return false;
        }

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y + " Z: " + Z;
        }

        public bool Equals(Vector3 other)
        {
            return Math.Abs(other.X - X) < float.Epsilon &&
                Math.Abs(other.Y - Y) < float.Epsilon &&
                Math.Abs(other.Z - Z) < float.Epsilon;
        }

        public override int GetHashCode()
        {
            var result = X.GetHashCode();
            unchecked
            {
                result = (result*397) ^ Y.GetHashCode();
                result = (result*397) ^ Z.GetHashCode();
            }
            return result;
        }
    }
}
