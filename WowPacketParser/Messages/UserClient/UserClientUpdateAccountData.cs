using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientUpdateAccountData
    {
        public UnixTime Time;
        public uint Size;
        public byte DataType;
        public Data CompressedData;
    }
}
