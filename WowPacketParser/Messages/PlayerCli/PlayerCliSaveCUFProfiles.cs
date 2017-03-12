using System.Collections.Generic;
using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSaveCUFProfiles
    {
        public List<CliCUFProfile> Profiles;
    }
}
