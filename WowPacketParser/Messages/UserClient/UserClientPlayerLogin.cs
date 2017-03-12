using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientPlayerLogin
    {
        public CliClientSettings ClientSettings;
        public ulong PlayerGUID;
    }
}
