using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientClearExplore
    {
        public string Target;
        public uint AreaID;
    }
}
