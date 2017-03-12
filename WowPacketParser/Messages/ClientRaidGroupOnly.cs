using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRaidGroupOnly
    {
        public uint Reason;
        public int Delay;
    }
}
