using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryGuildInfoResponse
    {
        public ulong GuildGuid;
        public bool Allow;
        public CliGuildInfo Info;
    }
}
