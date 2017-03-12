using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGuildApplications
    {
        public List<LFGuildApplicationData> Application;
        public int NumRemaining;
    }
}
