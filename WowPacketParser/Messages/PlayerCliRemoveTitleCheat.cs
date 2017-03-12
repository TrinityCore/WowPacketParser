using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliRemoveTitleCheat
    {
        public string TitleName;
        public int TitleID;
    }
}
