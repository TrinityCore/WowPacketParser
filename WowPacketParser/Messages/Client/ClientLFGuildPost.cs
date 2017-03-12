using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGuildPost
    {
        public GuildPostData? Post; // Optional
    }
}
