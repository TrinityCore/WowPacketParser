using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMShowLabel
    {
        public int Op;
        public string TargetName;
    }
}
