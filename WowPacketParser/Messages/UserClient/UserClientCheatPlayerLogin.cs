using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCheatPlayerLogin
    {
        public ulong PlayerGUID;
        public CliClientSettings ClientSettings;
    }
}
