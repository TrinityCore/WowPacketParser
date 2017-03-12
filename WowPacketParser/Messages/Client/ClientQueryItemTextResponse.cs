using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryItemTextResponse
    {
        public ulong Id;
        public bool Valid;
        public CliItemTextCache Item;
    }
}
