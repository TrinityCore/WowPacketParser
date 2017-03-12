using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCheatPlayerLogin
    {
        public ulong PlayerGUID;
        public CliClientSettings ClientSettings;
    }
}
