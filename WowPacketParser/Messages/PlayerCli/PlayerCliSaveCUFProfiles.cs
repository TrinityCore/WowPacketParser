using System.Collections.Generic;
using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSaveCUFProfiles
    {
        public List<CliCUFProfile> Profiles;
    }
}
