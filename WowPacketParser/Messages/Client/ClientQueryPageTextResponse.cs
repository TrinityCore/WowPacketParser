using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryPageTextResponse
    {
        public bool Allow;
        public CliPageTextInfo Info;
        public uint PageTextID;
    }
}
