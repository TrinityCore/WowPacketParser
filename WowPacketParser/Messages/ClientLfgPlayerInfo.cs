using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLfgPlayerInfo
    {
        public LFGBlackList BlackList;
        public List<LfgPlayerDungeonInfo> Dungeon;
    }
}
