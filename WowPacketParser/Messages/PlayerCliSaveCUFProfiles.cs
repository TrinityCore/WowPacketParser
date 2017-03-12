using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSaveCUFProfiles
    {
        public List<CliCUFProfile> Profiles;
    }
}
