using System;
using System.Text;

namespace PacketParser.DataStructures
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

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
                return Equals((Vector3)obj);

            return false;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(40);
            builder.Append("X: ");
            builder.Append(X);
            builder.Append(" Y: ");
            builder.Append(Y);
            builder.Append(" Z: ");
            builder.Append(Z);
            return builder.ToString();
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
            result = (result * 397) ^ Y.GetHashCode();
            result = (result * 397) ^ Z.GetHashCode();
            return result;
        }
    }
}
