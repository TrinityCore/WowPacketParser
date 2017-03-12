using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGmSilence
    {
        public int Action;
        public string Name;
    }
}
