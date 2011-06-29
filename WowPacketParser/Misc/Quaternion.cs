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
    }
}
