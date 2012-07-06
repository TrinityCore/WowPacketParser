using System;
using System.Text;

namespace PacketParser.DataStructures
{
    public struct Vector4
    {
        public Vector4(float x, float y, float z, float o)
        {
            X = x;
            Y = y;
            Z = z;
            O = o;
        }

        public float X;

        public float Y;

        public float Z;

        public float O;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(50);
            builder.Append("X: ");
            builder.Append(X);
            builder.Append(" Y: ");
            builder.Append(Y);
            builder.Append(" Z: ");
            builder.Append(Z);
            builder.Append(" O: ");
            builder.Append(O);
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector4)
                return Equals((Vector4)obj);

            return false;
        }

        public bool Equals(Vector4 other)
        {
            return Math.Abs(other.X - X) < float.Epsilon &&
                Math.Abs(other.Y - Y) < float.Epsilon &&
                Math.Abs(other.Z - Z) < float.Epsilon &&
                Math.Abs(other.O - O) < float.Epsilon;
        }

        public static bool operator ==(Vector4 first, Vector4 other)
        {
            return first.Equals(other);
        }

        public static bool operator !=(Vector4 first, Vector4 other)
        {
            return !(first == other);
        }

        public override int GetHashCode()
        {
            var result = X.GetHashCode();
            result = (result * 397) ^ Y.GetHashCode();
            result = (result * 397) ^ Z.GetHashCode();
            result = (result * 397) ^ O.GetHashCode();
            return result;
        }
    }
}
