using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGmSilence
    {
        public bool Silenced;
        public bool Success;
    }
}
