using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCorpseTransportQuery
    {
        public Vector3 Position;
        public float Facing;
    }
}
