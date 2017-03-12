using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGrantTitleCheat
    {
        public int TitleID;
        public string TitleName;
    }
}
