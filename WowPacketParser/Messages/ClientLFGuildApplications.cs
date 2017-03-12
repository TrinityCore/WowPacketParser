using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGuildApplications
    {
        public List<LFGuildApplicationData> Application;
        public int NumRemaining;
    }
}
