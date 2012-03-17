namespace WowPacketParser.Misc
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

        public readonly float X;

        public readonly float Y;

        public readonly float Z;

        public readonly float O;

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y + " Z: " + Z + " O: " + O;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector4)
                return Equals((Vector4)obj);

            return false;
        }

        public bool Equals(Vector4 other)
        {
            return other.X == X && other.Y == Y && other.Z == Z && other.O == O;
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
