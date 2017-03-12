using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientUpdateClientSettings
    {
        public CliClientSettings Settings;
    }
}
