using System.Collections.Generic;
using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSaveCUFProfiles
    {
        public List<CliCUFProfile> Profiles;
    }
}
