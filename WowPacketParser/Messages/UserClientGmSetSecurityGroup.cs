using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGmSetSecurityGroup
    {
        public string Name;
        public uint Group;
    }
}
