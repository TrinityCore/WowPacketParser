using System.Collections.Generic;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliLearnTalents
    {
        public List<ushort> Talents;
    }
}
