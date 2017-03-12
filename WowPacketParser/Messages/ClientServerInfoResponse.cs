using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientServerInfoResponse
    {
        public List<CliServerInfoLine> UserServerInfo;
        public List<CliServerInfoLine> WorldServerInfo;
    }
}
