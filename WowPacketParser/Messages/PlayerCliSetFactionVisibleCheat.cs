using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetFactionVisibleCheat
    {
        public bool Visible;
        public int FactionID;
    }
}
