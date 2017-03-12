using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGuildApplications
    {
        public List<LFGuildApplicationData> Application;
        public int NumRemaining;
    }
}
