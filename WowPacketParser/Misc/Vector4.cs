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
    }
}
