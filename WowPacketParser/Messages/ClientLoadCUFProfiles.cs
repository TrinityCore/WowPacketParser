using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLoadCUFProfiles
    {
        public List<CliCUFProfile> Profiles;
    }
}
