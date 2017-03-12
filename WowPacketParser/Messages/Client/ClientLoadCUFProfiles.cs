using System.Collections.Generic;
using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLoadCUFProfiles
    {
        public List<CliCUFProfile> Profiles;
    }
}
