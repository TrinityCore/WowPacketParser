using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientPlayerLogin
    {
        public CliClientSettings ClientSettings;
        public ulong PlayerGUID;
    }
}
