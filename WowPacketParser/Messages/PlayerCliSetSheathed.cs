using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetSheathed
    {
        public int CurrentSheathState;
        public bool Animate;
    }
}
