using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAccountProfile
    {
        public string Filename;
        public Data Profile;
    }
}
