using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUpdateAccountData
    {
        public ulong Player;
        public UnixTime Time;
        public uint Size;
        public byte DataType;
        public Data CompressedData;
    }
}
