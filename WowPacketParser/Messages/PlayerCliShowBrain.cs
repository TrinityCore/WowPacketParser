using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliShowBrain
    {
        public int Level;
        public bool All;
        public ulong Target;
    }
}
