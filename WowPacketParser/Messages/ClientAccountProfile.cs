using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAccountProfile
    {
        public string Filename;
        public Data Profile;
    }
}
