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

        public Quaternion(long packed)
        {
            X = (packed >> 42) * (1.0f / 2097152.0f);
            Y = (((packed << 22) >> 32) >> 11) * (1.0f / 1048576.0f);
            Z = (packed << 43 >> 43) * (1.0f / 1048576.0f);

            W = X * X + Y * Y + Z * Z;
            if (Math.Abs(W - 1.0f) >= (1 / 1048576.0f))
                W = (float)Math.Sqrt(1.0f - W);
            else
                W = 0.0f;
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

        public static implicit operator WowPacketParser.Proto.Quat(Quaternion q) =>
            new() {X = q.X, Y = q.Y, Z = q.Z, W = q.W};

        public static implicit operator Quaternion(WowPacketParser.Proto.Quat q) =>
            new(q.X, q.Y, q.Z, q.W);
    }
}
