using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMGuildSaveResponse
    {
        public bool Success;
        public Data ProfileData;
    }
}
