using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCorpseLocation
    {
        public ulong Transport;
        public Vector3 Position;
        public int ActualMapID;
        public bool Valid;
        public int MapID;
    }
}
