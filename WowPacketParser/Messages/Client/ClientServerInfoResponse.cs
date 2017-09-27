using System.Collections.Generic;
using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientServerInfoResponse
    {
        public List<CliServerInfoLine> UserServerInfo;
        public List<CliServerInfoLine> WorldServerInfo;
    }
}
