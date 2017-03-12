using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliMovementSpline
    {
        public uint ID;
        public Vector3 Destination;
        public CliMovementSplineMove? Move; // Optional
    }
}
