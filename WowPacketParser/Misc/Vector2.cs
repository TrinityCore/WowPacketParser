namespace WowPacketParser.Misc
{
    public struct Vector2
    {
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public readonly float X;

        public readonly float Y;

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2)
                return Equals((Vector2)obj);

            return false;
        }

        public bool Equals(Vector2 other)
        {
            return other.X == X && other.Y == Y;
        }

        public static bool operator ==(Vector2 first, Vector2 other)
        {
            return first.Equals(other);
        }

        public static bool operator !=(Vector2 first, Vector2 other)
        {
            return !(first == other);
        }

        public override int GetHashCode()
        {
            var result = X.GetHashCode();
            result = (result * 397) ^ Y.GetHashCode();
            return result;
        }
    }
}
