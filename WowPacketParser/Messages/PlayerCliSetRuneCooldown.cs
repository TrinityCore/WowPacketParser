using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetRuneCooldown
    {
        public float Blood;
        public float Unholy;
        public float Chromatic;
        public float Frost;
    }
}
