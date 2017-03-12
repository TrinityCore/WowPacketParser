using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryNPCTextResponse
    {
        public uint TextID;
        public bool Allow;
        public Data NpcText;
    }
}
