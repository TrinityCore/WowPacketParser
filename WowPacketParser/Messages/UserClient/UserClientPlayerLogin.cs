using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientPlayerLogin
    {
        public CliClientSettings ClientSettings;
        public ulong PlayerGUID;
    }
}
