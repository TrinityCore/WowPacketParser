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
    }
}
