using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliItemBindCheat
    {
        public bool Bind;
        public int ItemID;
    }
}
