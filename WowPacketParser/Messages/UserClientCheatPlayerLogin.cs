using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCheatPlayerLogin
    {
        public ulong PlayerGUID;
        public CliClientSettings ClientSettings;
    }
}
